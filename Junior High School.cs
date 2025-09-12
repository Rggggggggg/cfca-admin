using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCA_ADMIN
{
    public partial class Junior_High_School : UserControl
    {
        public Junior_High_School()
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
                    string query = @"SELECT lrn, CONCAT(surname, ' ', first_name, ' ', middle_name) AS name,
                    level_applied, gender, age, contact, date_created FROM jhs_enrollments";

                    if (statusFilter != "All")
                    {
                        query += $" WHERE enrollment_status = '{statusFilter}'";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dtgJHS.Rows.Clear(); // Clear existing rows
                        while(reader.Read())
                        {
                            dtgJHS.Rows.Add(
                            reader["lrn"].ToString(),
                            reader["name"].ToString(),
                            reader["level_applied"].ToString(),
                            reader["gender"].ToString(),
                            reader["age"].ToString(),
                            reader["contact"].ToString(),
                            Convert.ToDateTime(reader["date_created"]).ToString("MM/dd/yyyy")
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
        private void Junior_High_School_Load(object sender, EventArgs e)
        {
            cbStatusFilter.SelectedIndex = 0; // Default = Pending
            LoadStudentData("Pending");
        }

        private void dtgJHS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

          string lrn = dtgJHS.Rows[e.RowIndex].Cells["lrn"].Value?.ToString();
          string name = dtgJHS.Rows[e.RowIndex].Cells["name"].Value?.ToString();
          string columnName = dtgJHS.Columns[e.ColumnIndex].Name;

            if(columnName == "btnConfirm")
            {
                ConfirmEnrollment(lrn, name);
            }
            else if(columnName == "btnDelete")
            {
                RejectEnrollment(lrn, name);
            }


        }

        private void ConfirmEnrollment(string lrn, string name)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO students (`lrn`, 
                    `level_applied`, `surname`, `first_name`, `middle_name`, `age`, `gender`, 
                    `birth_date`, `birth_place`, `citizenship`, `religion`, `address`, `contact`,
                    `email`, `father_name`, `father_occupation`, `father_office`, `father_contact`,
                    `father_email`, `mother_name`, `mother_occupation`, `mother_office`, 
                    `mother_contact`, `mother_email`, `marital_status`, `guardian_name`, `guardian_relation`, 
                    `guardian_occupation`, `guardian_contact`, `guardian_email`, `has_siblings`, `previously_enrolled`, 
                    `prev_grade`, `prev_sy`, `prev_school`, `prev_school_sy`, `prev_school_addr`, `health_conditions`, 
                    `emergency_name`, `emergency_address`, `emergency_contact`, `student_photo`, `signature_filename`, 
                    `signature_date`)
                    SELECT 
                    `lrn`, 
                    `level_applied`, `surname`, `first_name`, `middle_name`, `age`, `gender`, 
                    `birth_date`, `birth_place`, `citizenship`, `religion`, `address`, `contact`,
                    `email`, `father_name`, `father_occupation`, `father_office`, `father_contact`,
                    `father_email`, `mother_name`, `mother_occupation`, `mother_office`, 
                    `mother_contact`, `mother_email`, `marital_status`, `guardian_name`, `guardian_relation`, 
                    `guardian_occupation`, `guardian_contact`, `guardian_email`, `has_siblings`, `previously_enrolled`, 
                    `prev_grade`, `prev_sy`, `prev_school`, `prev_school_sy`, `prev_school_addr`, `health_conditions`, 
                    `emergency_name`, `emergency_address`, `emergency_contact`, `student_photo`, `signature_filename`, 
                    `signature_date` FROM jhs_enrollments 
                    WHERE lrn = @lrn";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@lrn", lrn);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if(rowsAffected > 0)
                        {
                            UpdateStatus(conn, lrn);
                            MessageBox.Show($"Student {name} enrolled successfully");
                            LoadStudentData(cbStatusFilter.Text); // Refresh the data grid
                        }
                        else
                        {
                            MessageBox.Show($"Student record not found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void RejectEnrollment(string lrn, string name)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to reject this student: {name}?", 
                "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string UpdateQuery = "UPDATE jhs_enrollments SET enrollment_status = 'Rejected' WHERE lrn = @lrn";
                    MySqlCommand updatecmd = new MySqlCommand(UpdateQuery, conn);
                    updatecmd.Parameters.AddWithValue("@lrn", lrn);
                    updatecmd.ExecuteNonQuery();
                    MessageBox.Show($"Student {name} rejected successfully");
                    LoadStudentData(cbStatusFilter.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void UpdateStatus(MySqlConnection conn, string lrn)
        {
            string UpdateQuery = "UPDATE jhs_enrollments SET enrollment_status = 'Confirmed' WHERE lrn = @lrn";
            using (MySqlCommand updatecmd = new MySqlCommand(UpdateQuery, conn))
            {
                updatecmd.Parameters.AddWithValue("@lrn", lrn);
                updatecmd.ExecuteNonQuery();
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string filtertext = tbSearch.Text.ToLower();

            foreach (DataGridViewRow row in dtgJHS.Rows)
            {
                bool isVisible = row.Cells["name"].Value.ToString().ToLower().Contains(filtertext) ||
                                 row.Cells["lrn"].Value.ToString().ToLower().Contains(filtertext) ||
                                 row.Cells["level_applied"].Value.ToString().ToLower().Contains(filtertext);
                row.Visible = isVisible;
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
