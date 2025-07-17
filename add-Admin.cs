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
    public partial class add_Admin : Form
    {
        private OverlayForm overlayRef;
        public add_Admin(OverlayForm overlay)
        {
            InitializeComponent();
            this.overlayRef = overlay;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
                
                if (tbPassword.Text != tbConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            try
            {
                using (MySqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = "INSERT INTO admin_accounts (firstname,middlename,lastname,gender,role,contact_no,email,photo,username, password) VALUES (@fname,@mname,@lname,@gender,@role,@contact,@email,@photo,@username,@password)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    string HashedPassword = BCrypt.Net.BCrypt.HashPassword(tbPassword.Text);

                    cmd.Parameters.AddWithValue("@fname", tbFirstname.Text);
                    cmd.Parameters.AddWithValue("@mname", tbMiddlename.Text);
                    cmd.Parameters.AddWithValue("@lname", tbLastname.Text);
                    cmd.Parameters.AddWithValue("@gender", cbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@role", cbRole.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@contact", tbContact.Text);
                    cmd.Parameters.AddWithValue("@email", tbEmail.Text);
                    cmd.Parameters.AddWithValue("@username", tbUsername.Text);
                    cmd.Parameters.AddWithValue("@password", HashedPassword);

                    Image img = btnChooseImage.Image;
                    byte[] imageBytes = ImageToByteArray(img);
                    cmd.Parameters.AddWithValue("@photo", imageBytes);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Admin successfully added!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting admin: " + ex.Message);
            }
        }

        private void add_Admin_Load(object sender, EventArgs e)
        {

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to go back? Unsaved changes will be lost.",
                    "Confirm Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    this.Close(); // closes this modal
                    overlayRef?.Close(); // closes the dimmed overlay
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnChooseImage_Click_1(object sender, EventArgs e)
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
    }
}
