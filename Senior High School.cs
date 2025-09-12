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
    public partial class Senior_High_School : UserControl
    {
        public Senior_High_School()
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
                    string query = @"SELECT application_no, application_date,grade_level,strand,
                    CONCAT(surname, ' ', first_name, ' ', middle_name) AS name, gender, age, cellphone
                    FROM shs_enrollments";

                    if (statusFilter != "All")
                    {
                        query += $" WHERE enrollment_status = '{statusFilter}'";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dtgSHS.Rows.Clear();

                        while (reader.Read())
                        {
                            dtgSHS.Rows.Add(
                                reader["application_no"].ToString(),
                                reader["name"].ToString(),
                                reader["grade_level"].ToString(),
                                reader["strand"].ToString(),
                                reader["gender"].ToString(),
                                reader["age"].ToString(),
                                reader["cellphone"].ToString(),
                                Convert.ToDateTime(reader["application_date"]).ToString("MM/dd/yyyy"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Senior_High_School_Load(object sender, EventArgs e)
        {
            cbStatusFilter.SelectedIndex = 0; // Default = Pending
            LoadStudentData("Pending");
        }

        private void dtgSHS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string application_no = dtgSHS.Rows[e.RowIndex].Cells["Application_No"].Value.ToString();
            string name = dtgSHS.Rows[e.RowIndex].Cells["name"].Value.ToString();
            string columnName = dtgSHS.Columns[e.ColumnIndex].Name;

            if (columnName == "btnConfirm")
            {
                ConfirmEnrollment(name, application_no);
            }
            else if (columnName == "btnDelete")
            {
                RejectEnrollment(application_no, name);
            }

        }

        private void ConfirmEnrollment(string name, string application_no)
        {
            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO students (`application_no`, 
                        `application_date`, `grade_level`, `strand`, `surname`, `first_name`, `middle_name`,
                        `nickname`, `home_address`, `telephone`, `cellphone`, `birth_date`, `age`, `gender`,
                        `father_name`, `mother_name`, `step_parent_name`, `guardian_name`, `father_occupation`, 
                        `mother_occupation`, `step_parent_occupation`, `guardian_occupation`, `father_company`, 
                        `mother_company`, `step_parent_company`, `guardian_company`, `father_income`, `mother_income`,
                        `step_parent_income`, `guardian_income`, `father_office`, `mother_office`, `step_parent_office`,
                        `guardian_office`, `father_tel`, `mother_tel`, `step_parent_tel`, `guardian_tel`, `father_mobile`,
                        `mother_mobile`, `step_parent_mobile`, `guardian_mobile`, `father_religion`, `mother_religion`, 
                        `step_parent_religion`, `guardian_religion`, `marital_status`, `handed`, `religion`, `church_name`,
                        `pastor_name`, `last_school`, `last_school_address`, `ncae_result`, `test_schedule`, `test_result`,
                        `remarks`, `health`, `health_eye`, `health_ear`, `health_allergies`, `health_others`, 
                        `id_photo_filename`, `signature_filename`, `signature_date`)
                        SELECT
                        `application_no`, 
                        `application_date`, `grade_level`, `strand`, `surname`, `first_name`, `middle_name`,
                        `nickname`, `home_address`, `telephone`, `cellphone`, `birth_date`, `age`, `gender`,
                        `father_name`, `mother_name`, `step_parent_name`, `guardian_name`, `father_occupation`, 
                        `mother_occupation`, `step_parent_occupation`, `guardian_occupation`, `father_company`, 
                        `mother_company`, `step_parent_company`, `guardian_company`, `father_income`, `mother_income`,
                        `step_parent_income`, `guardian_income`, `father_office`, `mother_office`, `step_parent_office`,
                        `guardian_office`, `father_tel`, `mother_tel`, `step_parent_tel`, `guardian_tel`, `father_mobile`,
                        `mother_mobile`, `step_parent_mobile`, `guardian_mobile`, `father_religion`, `mother_religion`, 
                        `step_parent_religion`, `guardian_religion`, `marital_status`, `handed`, `religion`, `church_name`,
                        `pastor_name`, `last_school`, `last_school_address`, `ncae_result`, `test_schedule`, `test_result`,
                        `remarks`, `health`, `health_eye`, `health_ear`, `health_allergies`, `health_others`, 
                        `id_photo_filename`, `signature_filename`, `signature_date` FROM shs_enrollments WHERE application_no = @application_no";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@application_no", application_no);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            UpdateStatus(application_no, conn);
                            MessageBox.Show($"Student {name} enrolled successfully");
                            LoadStudentData(cbStatusFilter.Text);
                        }
                        else
                        {
                            MessageBox.Show("Student record not found.");
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void RejectEnrollment(string application_no, string name)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to reject this student: {name}?",
                "Confirm Rejection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string UpdateQuery = "UPDATE shs_enrollments SET enrollment_status = 'Rejected'  WHERE application_no = @application_no";
                    using (MySqlCommand deletecmd = new MySqlCommand(UpdateQuery, conn))
                    {
                        deletecmd.Parameters.AddWithValue("@application_no", application_no);
                        deletecmd.ExecuteNonQuery();
                    }
                    MessageBox.Show($"Student {name} has been rejected successfully.");
                    LoadStudentData(cbStatusFilter.Text); // Refresh the data grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void UpdateStatus(string application_no, MySqlConnection conn)
        {
            string UpdateQuery = "UPDATE shs_enrollments SET enrollment_status = 'Confirmed' WHERE application_no = @application_no";
            using (MySqlCommand deletecmd = new MySqlCommand(UpdateQuery, conn))
            {
                deletecmd.Parameters.AddWithValue("@application_no", application_no);
                deletecmd.ExecuteNonQuery();
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string filtertext = tbSearch.Text.ToLower();

            foreach (DataGridViewRow row in dtgSHS.Rows)
            {
                bool isVisible = row.Cells["name"].Value.ToString().ToLower().Contains(filtertext) ||
                                 row.Cells["level_applied"].Value.ToString().ToLower().Contains(filtertext) ||
                                 row.Cells["Strand"].Value.ToString().ToLower().Contains(filtertext);
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
