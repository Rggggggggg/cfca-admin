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
        private void LoadStudentData()
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



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            LoadStudentData();
        }

        private void enrollees_Load(object sender, EventArgs e)
        {
            dtgEnrollees.AlternatingRowsDefaultCellStyle = dtgEnrollees.RowsDefaultCellStyle;
            dtgEnrollees.AutoGenerateColumns = false;
            LoadStudentData();
        }

        private void dtgEnrollees_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                {
                    // Skip header row
                    if (e.RowIndex < 0)
                        return;

                    // Confirm button
                    if (dtgEnrollees.Columns[e.ColumnIndex].Name == "btnConfirm")
                    {
                        string name = dtgEnrollees.Rows[e.RowIndex].Cells["name"].Value.ToString();
                        MessageBox.Show("Confirmed student: " + name);
                        // Add confirmation logic here
                    }

                    // Delete button
                    if (dtgEnrollees.Columns[e.ColumnIndex].Name == "btnDelete")
                    {
                        string name = dtgEnrollees.Rows[e.RowIndex].Cells["name"].Value.ToString();
                        DialogResult result = MessageBox.Show("Are you sure you want to delete student: " + name + "?", "Confirm Delete", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            // Add delete logic here
                            MessageBox.Show("Deleted student: " + name);
                        }
                    }
                }
            }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string filterText = tbSearch.Text.ToLower();

            foreach (DataGridViewRow row in dtgEnrollees.Rows)
            {
                if (row.IsNewRow) continue;

                // Assuming column 1 = Student Number, column 3 = Full Name
                string studentNumber = row.Cells[0].Value?.ToString().ToLower();
                string levelApplied = row.Cells[2].Value?.ToString().ToLower();
                string fullName = row.Cells[1].Value?.ToString().ToLower();

                bool visible = studentNumber.Contains(filterText) || fullName.Contains(filterText) || levelApplied.Contains(filterText);
                row.Visible = visible;
            }
        }
    }
}
