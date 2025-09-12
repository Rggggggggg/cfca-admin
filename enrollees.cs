using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCA_ADMIN
{
    public partial class enrollees : UserControl
    {
        public enrollees()
        {
            InitializeComponent();
        }
        private void LoadStudentData(string statusFilter = "Pending")
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT student_number, level_applied, surname, 
                             first_name, middle_name, gender, age, contact, 
                             submitted_at
                             FROM basic_ed_enrollment";

                    if (statusFilter != "All")
                    {
                        query += $" WHERE enrollment_status = '{statusFilter}'";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dtgEnrollees.Rows.Clear(); // Clear existing rows

                        while (reader.Read())
                        {
                            string fullName = $"{reader["surname"]}, {reader["first_name"]} {reader["middle_name"]}";

                            dtgEnrollees.Rows.Add(
                                reader["student_number"].ToString(),
                                fullName,
                                reader["level_applied"].ToString(),
                                reader["gender"].ToString(),
                                reader["age"].ToString(),
                                reader["contact"].ToString(),
                                Convert.ToDateTime(reader["submitted_at"]).ToString("MM/dd/yyyy")
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        private void enrollees_Load(object sender, EventArgs e)
        {
            dtgEnrollees.AlternatingRowsDefaultCellStyle = dtgEnrollees.RowsDefaultCellStyle;
            dtgEnrollees.AutoGenerateColumns = false;

            cbStatusFilter.SelectedIndex = 0; // Default = Pending
            LoadStudentData("Pending");
        }

        private void dtgEnrollees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string studentNumber = dtgEnrollees.Rows[e.RowIndex].Cells["student_number"].Value?.ToString();
            string name = dtgEnrollees.Rows[e.RowIndex].Cells["name"].Value?.ToString();
            string columnName = dtgEnrollees.Columns[e.ColumnIndex].Name;

            if (columnName == "btnConfirm")
            {
                ConfirmEnrollment(studentNumber, name);
            }
            else if (columnName == "btnDelete")
            {
               RejectEnrollment(studentNumber, name);
            }
        }

        private void ConfirmEnrollment(string studentNumber, string name)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Use INSERT INTO ... SELECT to copy data in one operation
                    string transferQuery = @"
                INSERT INTO students (
                    student_number, level_for_registrar, lrn, level_applied, surname, first_name, middle_name, age, gender, dob,
                    pob, citizenship, religion, address, contact, email, father_name, father_occupation, father_office, father_contact, 
                    father_email, mother_name, mother_occupation, mother_office, mother_contact, mother_email, status, guardian_name, guardian_relation, 
                    guardian_occupation, guardian_contact, guardian_email, has_siblings, prev_ctfcai, prev_grade, prev_sy, prev_school, prev_school_sy, prev_school_addr, 
                    health_conditions, emergency_name, emergency_address, emergency_contact, student_photo, signature_filename, signature_date
                )
                SELECT 
                    student_number, level_for_registrar, lrn, level_applied, surname, first_name, middle_name, age, gender, dob,
                    pob, citizenship, religion, address, contact, email, father_name, father_occupation, father_office, father_contact, 
                    father_email, mother_name, mother_occupation, mother_office, mother_contact, mother_email, status, guardian_name, guardian_relation, 
                    guardian_occupation, guardian_contact, guardian_email, has_siblings, prev_ctfcai, prev_grade, prev_sy, prev_school, prev_school_sy, prev_school_addr, 
                    health_conditions, emergency_name, emergency_address, emergency_contact, student_photo, signature_filename, signature_date
                FROM basic_ed_enrollment 
                WHERE student_number = @student_number";

                    MySqlCommand transferCmd = new MySqlCommand(transferQuery, conn);
                    transferCmd.Parameters.AddWithValue("@student_number", studentNumber);

                    int rowsAffected = transferCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        UpdateStatus(conn, studentNumber);
                        MessageBox.Show($"Student {name} enrolled successfully");
                        LoadStudentData(cbStatusFilter.Text);
                    }
                    else
                    {
                        MessageBox.Show("Student record not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void RejectEnrollment(string studentNumber, string name)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to reject this student: {name}?",
                                                 "Confirm Reject", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) return;

            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string updateQuery = "UPDATE basic_ed_enrollment SET enrollment_status = 'Rejected' WHERE student_number = @student_number";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@student_number", studentNumber);
                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show($"Student {name} rejected successfully");
                    LoadStudentData(cbStatusFilter.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void UpdateStatus(MySqlConnection conn, string studentNumber)
        {
            string updateQuery = "UPDATE basic_ed_enrollment SET enrollment_status = 'Confirmed' WHERE student_number = @student_number";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
            updateCmd.Parameters.AddWithValue("@student_number", studentNumber);
            updateCmd.ExecuteNonQuery();
        }


        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = tbSearch.Text.ToLower();

            foreach (DataGridViewRow row in dtgEnrollees.Rows)
            { 
                string studentNumber = row.Cells[0].Value?.ToString().ToLower();
                string levelApplied = row.Cells[2].Value?.ToString().ToLower();
                string fullName = row.Cells[1].Value?.ToString().ToLower();

                bool visible = studentNumber.Contains(filterText) || fullName.Contains(filterText) || levelApplied.Contains(filterText);
                row.Visible = visible;
            }
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbSearch.Clear();
            string selectedStatus = cbStatusFilter.SelectedItem.ToString();
            LoadStudentData(selectedStatus);
        }
    }
}
