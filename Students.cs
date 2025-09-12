using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCA_ADMIN
{
    public partial class Students : UserControl
    {
        public Students()
        {
            InitializeComponent();
        }

        private void Students_Load(object sender, EventArgs e)
        {
            cbGradeLevel.SelectedIndex = 0;
            LoadStudentData();
            
        }
        private void LoadStudentData(string gradeFilter = "All", string searchKeyword = "")
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT 
                                COALESCE(student_number, lrn, application_no) AS id,
                                COALESCE(level_applied, grade_level) AS level_applied,
                                COALESCE(contact, cellphone) AS contact,
                                CONCAT(surname, ' ', first_name, ' ', middle_name) AS name,
                                gender, age, strand
                             FROM students
                             WHERE 1=1";

                    // Grade Level filter
                    if (gradeFilter != "All")
                    {
                        query += " AND COALESCE(level_applied, grade_level) = @gradeFilter";
                    }

                    // Search filter
                    if (!string.IsNullOrWhiteSpace(searchKeyword))
                    {
                        query += " AND (surname LIKE @search OR first_name LIKE @search OR middle_name LIKE @search)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (gradeFilter != "All")
                        {
                            cmd.Parameters.AddWithValue("@gradeFilter", gradeFilter);
                        }
                        if (!string.IsNullOrWhiteSpace(searchKeyword))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dtgStudents.Rows.Clear();
                            while (reader.Read())
                            {
                                dtgStudents.Rows.Add(
                                    reader["id"].ToString(),
                                    reader["name"].ToString(),
                                    reader["level_applied"].ToString(),
                                    reader["gender"].ToString(),
                                    reader["age"].ToString(),
                                    reader["contact"].ToString(),
                                    reader["strand"] != DBNull.Value ? reader["strand"].ToString() : "N/A"
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string selectedGrade = cbGradeLevel.SelectedItem?.ToString() ?? "All";
            string searchKeyword = tbSearch.Text.Trim();

            if (selectedGrade != "All")
            {
                // Use SQL filter for specific grade
                LoadStudentData(selectedGrade, searchKeyword);
            }
            else
            {
                // Just filter existing rows in DataGridView
                string search = searchKeyword.ToLower();

                foreach (DataGridViewRow row in dtgStudents.Rows)
                {
                    bool visible =
                        row.Cells["Grades"].Value.ToString().ToLower().Contains(search) ||
                        row.Cells["strand"].Value.ToString().ToLower().Contains(search) ||
                        row.Cells["name"].Value.ToString().ToLower().Contains(search);

                    row.Visible = visible;
                }
            }
        }


        private void dtgStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string studentID = dtgStudents.Rows[e.RowIndex].Cells["id"].Value?.ToString();
            string name = dtgStudents.Rows[e.RowIndex].Cells["name"].Value?.ToString();
            string columnName = dtgStudents.Columns[e.ColumnIndex].Name;

            if (columnName == "btnConfirm")
            {
                EditStudent(studentID);
            }
            else if (columnName == "btnDelete")
            {
                DeleteStudent(studentID, name);
            }
        }

        private void EditStudent(string studentID)
        {
            Form parentForm = this.FindForm();
            OverlayForm overlay = new OverlayForm(parentForm);
            overlay.Show();
            EditStudent editForm = new EditStudent(studentID);
            editForm.ShowDialog();
            LoadStudentData();
            overlay.Dispose();

        }

        private void DeleteStudent(string studentID, string name)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this student: " + name, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;


            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM students WHERE COALESCE(student_number, lrn, application_no) = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", studentID);
                        cmd.ExecuteNonQuery();
                    }
                    LoadStudentData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void cbGradeLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSearch.Clear();
            string selectedGrade = cbGradeLevel.SelectedItem.ToString();
            string searchKeyword = tbSearch.Text.Trim();
            LoadStudentData(selectedGrade, searchKeyword);
        }
    }
}

