using Guna.UI2.WinForms;
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
    public partial class add_instructor : UserControl
    {
        private instructors _parent;

        public add_instructor(instructors parent, string instructorId = null)
        {
            InitializeComponent();
            _parent = parent;

            if (!string.IsNullOrEmpty(instructorId))
            {
                LoadInstructorData(instructorId);
            }
        }

        public add_instructor()
        {
            InitializeComponent();
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void instructor_Load(object sender, EventArgs e)
        {
            clbGradesLevelHandled.ItemCheck += clbGradesLevelHandled_ItemCheck;
            // Only apply these defaults if not in edit mode
            if (string.IsNullOrEmpty(tbInstructorID.Text)) // Means: we are adding, not editing
            {
                if (cbGender.Items.Count > 0)
                    cbGender.SelectedIndex = 0;
                if (cbCivilStatus.Items.Count > 0)
                    cbCivilStatus.SelectedIndex = 0;
                if (cbEmployment.Items.Count > 0)
                    cbEmployment.SelectedIndex = 0;
            }
        }
        private byte[] ImageToByteArray(Image image)
        {
            int maxWidth = 300;  // Resize width
            int maxHeight = 300; // Resize height

            int newWidth = image.Width;
            int newHeight = image.Height;

            // Maintain aspect ratio
            if (image.Width > maxWidth || image.Height > maxHeight)
            {
                float ratioX = (float)maxWidth / image.Width;
                float ratioY = (float)maxHeight / image.Height;
                float ratio = Math.Min(ratioX, ratioY);
                newWidth = (int)(image.Width * ratio);
                newHeight = (int)(image.Height * ratio);
            }

            using (Bitmap resized = new Bitmap(newWidth, newHeight))
            {
                using (Graphics g = Graphics.FromImage(resized))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }

                using (var ms = new System.IO.MemoryStream())
                {
                    resized.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // JPEG = smaller file
                    return ms.ToArray();
                }
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Only treat as edit if the instructor already exists in the database
                    bool isEdit = false;
                    using (var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM instructor_accounts WHERE instructors_id = @id", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", tbInstructorID.Text);
                        isEdit = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                    }

                    string query;
                    if (isEdit)
                    {
                        // UPDATE if editing
                        query = @"UPDATE instructor_accounts SET
                            firstname = @firstname,
                            middlename = @middlename,
                            lastname = @lastname,
                            gender = @gender,
                            age = @age,
                            date_of_birth = @dob,
                            civil_status = @civil_status,
                            contact_no = @contact_no,
                            email = @email,
                            image = @image,
                            employment = @employment,
                            subject_assigned = @subject_assigned,
                            grades_level_handled = @grades_level_handled
                        WHERE instructors_id = @instructors_id";
                    }
                    else
                    {
                        // INSERT if adding
                        query = @"INSERT INTO instructor_accounts
                            (firstname, middlename, lastname, gender, age, date_of_birth, civil_status, contact_no, email, image, employment, instructors_id, subject_assigned, grades_level_handled)
                            VALUES
                            (@firstname, @middlename, @lastname, @gender, @age, @dob, @civil_status, @contact_no, @email, @image, @employment, @instructors_id, @subject_assigned, @grades_level_handled)";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@firstname", tbFirstname.Text);
                    cmd.Parameters.AddWithValue("@middlename", tbMiddlename.Text);
                    cmd.Parameters.AddWithValue("@lastname", tbLastname.Text);
                    cmd.Parameters.AddWithValue("@gender", cbGender.Text);
                    cmd.Parameters.AddWithValue("@age", tbAge.Text);
                    cmd.Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@civil_status", cbCivilStatus.Text);
                    cmd.Parameters.AddWithValue("@contact_no", tbContact.Text);
                    cmd.Parameters.AddWithValue("@email", tbEmail.Text);

                    Image img = btnChooseImage.Image;
                    byte[] imageBytes = ImageToByteArray(img);
                    cmd.Parameters.AddWithValue("@image", imageBytes);

                    cmd.Parameters.AddWithValue("@employment", cbEmployment.Text);
                    cmd.Parameters.AddWithValue("@instructors_id", tbInstructorID.Text);
                    cmd.Parameters.AddWithValue("@subject_assigned", string.Join(", ", clbSubjectAssigned.CheckedItems.Cast<string>()));
                    cmd.Parameters.AddWithValue("@grades_level_handled", string.Join(", ", clbGradesLevelHandled.CheckedItems.Cast<string>()));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(isEdit ? "Instructor updated successfully!" : "Instructor saved successfully!");
                    _parent.LoadInstructors();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        // Add this method to handle loading instructor data
        private void LoadInstructorData(string instructorId)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM instructor_accounts WHERE instructors_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", instructorId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbInstructorID.Text = reader["instructors_id"].ToString();
                            tbFirstname.Text = reader["firstname"].ToString();
                            tbMiddlename.Text = reader["middlename"].ToString();
                            tbLastname.Text = reader["lastname"].ToString();
                            cbGender.SelectedItem = reader["gender"].ToString();
                            tbAge.Text = reader["age"].ToString();
                            dtpDOB.Value = Convert.ToDateTime(reader["date_of_birth"]);
                            cbCivilStatus.SelectedItem = reader["civil_status"].ToString();
                            tbContact.Text = reader["contact_no"].ToString();
                            tbEmail.Text = reader["email"].ToString();
                            cbEmployment.SelectedItem = reader["employment"].ToString();

                            // Load image
                            if (reader["image"] != DBNull.Value)
                            {
                                byte[] imgBytes = (byte[])reader["image"];
                                using (var ms = new System.IO.MemoryStream(imgBytes))
                                {
                                    btnChooseImage.Image = Image.FromStream(ms);
                                    btnChooseImage.Text = "";
                                }
                            }

                            // Load subject_assigned (assumes comma-separated)
                            if (reader["subject_assigned"] != DBNull.Value)
                            {
                                string[] subjects = reader["subject_assigned"].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < clbSubjectAssigned.Items.Count; i++)
                                {
                                    clbSubjectAssigned.SetItemChecked(i, subjects.Any(s => s.Trim() == clbSubjectAssigned.Items[i].ToString()));
                                }
                            }

                            // Load grades_level_handled (assumes comma-separated)
                            if (reader["grades_level_handled"] != DBNull.Value)
                            {
                                string[] grades = reader["grades_level_handled"].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < clbGradesLevelHandled.Items.Count; i++)
                                {
                                    clbGradesLevelHandled.SetItemChecked(i, grades.Any(g => g.Trim() == clbGradesLevelHandled.Items[i].ToString()));
                                }
                                UpdateSubjectsBasedOnGrades();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading instructor data: " + ex.Message);
                }
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var img = Image.FromFile(ofd.FileName))
                    {
                        btnChooseImage.Image = new Bitmap(img); // clone the image
                        btnChooseImage.Text = "";
                    }

                }
            }
        }

        private void cbCivilStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to go back? Unsaved changes will be lost.",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Form parentForm = this.FindForm();
                    parentForm?.Close();
                }
            }
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {

        }
        private Dictionary<string, List<string>> gradeToSubjects = new Dictionary<string, List<string>>()
{
    { "Nursery", new List<string> {
        "FILIPINO", "LANGUAGE", "READING", "MATH", "SCIENCE", "G.M.R.C.", "WRITING"
    }},
    { "Kinder 1", new List<string> {
        "FILIPINO", "LANGUAGE", "READING", "MATH", "SCIENCE", "G.M.R.C.", "WRITING", "ARALING PANLIPUNAN"
    }},
    { "Kinder 2 (P)", new List<string> {
        "FILIPINO", "LANGUAGE", "READING", "MATH", "SCIENCE", "G.M.R.C.", "WRITING", "ARALING PANLIPUNAN"
    }},
    { "Grade 1", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS", "SCIENCE & HEALTH", "SOCIAL STUDIES",
        "MAPEH", "COMPUTER", "G.M.R.C."
    }},
    { "Grade 2", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS", "SCIENCE & HEALTH", "SOCIAL STUDIES",
        "MAPEH", "COMPUTER", "G.M.R.C.", "MOTHER TONGUE"
    }},
    { "Grade 3", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS", "SCIENCE & HEALTH", "SOCIAL STUDIES",
        "MAPEH", "COMPUTER", "G.M.R.C.", "MOTHER TONGUE"
    }},
    { "Grade 4", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS", "SCIENCE & HEALTH", "SOCIAL STUDIES",
        "MAPEH", "HELE & COMPUTER", "G.M.R.C."
    }},
    { "Grade 5", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS", "SCIENCE & HEALTH", "SOCIAL STUDIES",
        "MAPEH", "HELE & COMPUTER", "G.M.R.C."
    }},
    { "Grade 6", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS", "SCIENCE & HEALTH", "SOCIAL STUDIES",
        "MAPEH", "HELE & COMPUTER", "G.M.R.C."
    }},
    { "Grade 7", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS", "SCIENCE & TECHNOLOGY", "ARALING PANLIPUNAN",
        "TECHNOLOGY & LIVELIHOOD EDUCATION / COMPUTER", "MAPEH", "VALUES EDUCATION"
    }},
    { "Grade 8", new List<string> {
        "FILIPINO", "ENGLISH & JOURNALISM", "MATHEMATICS", "SCIENCE & TECHNOLOGY",
        "ARALING PANLIPUNAN", "TECHNOLOGY & LIVELIHOOD EDUCATION / COMPUTER",
        "MAPEH", "VALUES EDUCATION"
    }},
    { "Grade 9", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS / TRIGONOMETRY", "SCIENCE & TECHNOLOGY",
        "ARALING PANLIPUNAN", "TECHNOLOGY & LIVELIHOOD EDUCATION / COMPUTER",
        "MAPEH", "VALUES EDUCATION"
    }},
    { "Grade 10", new List<string> {
        "FILIPINO", "ENGLISH", "MATHEMATICS / ANA.GEOM.", "SCIENCE & TECHNOLOGY",
        "ARALING PANLIPUNAN", "TECHNOLOGY & LIVELIHOOD EDUCATION / COMPUTER",
        "MAPEH", "VALUES EDUCATION", "COMMUNITY SERVICE"
    }},
    { "Grade 11 STEM", new List<string> {
        "General Mathematics", "Oral Communication", "Earth Science",
        "Komunikasyon at Pananaliksik sa Wikang Filipino",
        "21st Century Literature from the Philippines and the World",
        "Physical Education and Health", "Empowerment Technologies (E-Tech)",
        "Pagsulat sa Filipino sa Piling Larangan (Akademik, Isports, Sining at Tech-Voc)",
        "Pre-Calculus", "General Biology 1", "Statistics and Probability",
        "Reading and Writing", "Disaster Readines and Risk Reduction",
        "Pagbasa at Pagsusuri sa Iba't ibang Teksto Tungo sa Pananaliksik",
        "Personal Development", "Entrepreneurship", "Practical Research 1",
        "Basic Calculus", "General Biology 2"
    }},
    { "Grade 11 ABM", new List<string> {
        "General Mathematics", "21st Century Literature from the Philippines and the World",
        "Oral Communication", "Earth and Life Science",
        "Komunikasyon at Pananaliksik sa Wika at Kulturang Pilipino",
        "Physical Education and Health", "Empowerment Technologies (E-Tech)",
        "Pagsulat sa Filipino sa Piling Larangan (Akademik, Isports, Sining at Tech-Voc)",
        "Business Math", "Organization and Management", "Statistics and Probability",
        "Reading and Writing", "Pagbasa at Pagsusuri sa Iba't ibang Teksto Tungo sa Pananaliksik",
        "Physical Science", "Personal Development", "Entrepreneurship",
        "Research Project", "Fundamentals of Accounting and Business Management 1",
        "Principles of Marketing"
    }},
    { "Grade 11 HUMSS", new List<string> {
        "General Mathematics", "21st Century Literature from the Philippines and the World",
        "Oral Communication", "Earth and Life Science",
        "Komunikasyon at Pananaliksik sa Wika at Kulturang Pilipino",
        "Physical Education and Health", "Pagsulat sa Filipino sa Piling Larangan (Akademik)",
        "Empowerment Technologies (E-Tech): ICT for Professional Tracks",
        "Discipline and Ideas in the Social Sciences", "Reading and Writing Skills",
        "Pagbasa at Pagsusuri ng Ibat't-Ibang Teksto Tungo sa Pananaliksik",
        "Physical Science", "Statistics and Probability",
        "Personal Development / Pansariling Kaunlaran", "Entrepreneurship",
        "Research in Daily Life 1", "Philippine Politics and Governance"
    }},
    { "Grade 12 STEM", new List<string> {
        "Understanding Culture, Society and Politics", "Contemporary Philippine Arts from the Region",
        "Introduction to the Philosophy", "Physical Education and Health",
        "English for Academic and Professional Purposes", "Practical Research 2",
        "General Physics 1", "Chemistry 1", "Media and Information Literacy",
        "Inquiries, Investigations and Immersion", "General Physics 2",
        "General Chemistry 2", "Capstone Project / Research Project"
    }},
    { "Grade 12 ABM", new List<string> {
        "Understanding Culture, Society and Politics", "Introduction to the Philosophy of the Human Person",
        "Contemporary Philippine Arts from the Region", "Physical Education and Health",
        "English for Academic and Professional Purposes", "Practical Research 2",
        "Applied Economics", "Fundamentals of Accounting and Business Management 2",
        "Media and Information Literacy", "Research Project",
        "Business Finance", "Business Enterprise Simulation",
        "Business Ethics and Social Responsibility"
    }},
    { "Grade 12 HUMSS", new List<string> {
        "Understanding Culture, Society and Politics", "Introduction to the Philosophy of the Human Person",
        "Contemporary Philippine Arts from the Region", "Physical Education and Health",
        "English for Academic and Professional Purposes", "Research in Daily Life 2",
        "Discipline and Ideas in Applied Social Sciences", "Creative Writing",
        "Media and Information Literacy", "Research Project",
        "Community Engagement, Solidarity and Citizenship",
        "Introduction to World Religions and Belief System", "Culminating Activity",
        "Trends, Networks and Critical Thinking in the 21st Century",
        "Creative Nonfiction: Literary Essay"
    }}
};

        private void clbGradesLevelHandled_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void clbGradesLevelHandled_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (this.IsHandleCreated)
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    UpdateSubjectsBasedOnGrades();
                });
            }

        }

        private void UpdateSubjectsBasedOnGrades()
        {
            // Clear and repopulate the subject list, grouped by grade with headers
            clbSubjectAssigned.Items.Clear();

            var checkedGrades = clbGradesLevelHandled.CheckedItems.Cast<string>().ToList();
            var addedSubjects = new HashSet<string>();

            foreach (string grade in checkedGrades)
            {
                if (gradeToSubjects.ContainsKey(grade))
                {
                    // Add a header for the grade (disabled, not checkable)
                    int headerIndex = clbSubjectAssigned.Items.Add($"--- {grade} ---");
                    clbSubjectAssigned.SetItemCheckState(headerIndex, CheckState.Indeterminate);

                    foreach (string subject in gradeToSubjects[grade])
                    {
                        // Only add subject if not already added under another grade
                        if (!addedSubjects.Contains(subject))
                        {
                            clbSubjectAssigned.Items.Add(subject, true);
                            addedSubjects.Add(subject);
                        }
                    }
                }
            }
        }
    }
}
