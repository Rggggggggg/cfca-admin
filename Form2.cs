using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CFCA_ADMIN
{
    public partial class Form2 : Form
    {
        private string _displayName;
        private byte[] _imageData;
        private string  role;
        public Form2(string displayName, byte[] imageData, string role)
        {
            InitializeComponent();
            _displayName = displayName;
            _imageData = imageData;
            this.role = role;
        }
        private void LoadControl(System.Windows.Forms.UserControl uc) 
        {
            panelContainer.Controls.Clear();       // Clear previous controls
            uc.Dock = DockStyle.Fill;              // Make it fill the space
            panelContainer.Controls.Add(uc);       // Load new control
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            lblAdmin.Text = btnDashboard.Text; // Set the label text to the button text
            LoadControl(new dashboard()); // Assuming DashboardControl is a UserControl you already created
        }

        private void btnInstructor_Click(object sender, EventArgs e)
        {
            lblAdmin.Text = btnInstructor.Text; // Set the label text to the button text
            LoadControl(new instructors()); 
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            lblAdmin.Text = btnSchedule.Text;
            LoadControl(new schedule());
        }
        private bool isSubMenuVisible = false;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            lblAdmin.Text = btnNewEnrollees.Text; // Set the label text to the button text
            if (isSubMenuVisible)
            {
                // Collapse
                panelSubEnrollees.Height = 0;
                panelSubEnrollees.Visible = false;


            }
            else
            {
                // Expand (adjust height based on # of buttons)
                panelSubEnrollees.Height = panelSubEnrollees.Controls.Count * 46; // adjust 35 per button height
                panelSubEnrollees.Visible = true;

            }

            isSubMenuVisible = !isSubMenuVisible;

            // Adjust position of next controls (e.g., btnAdmin)
            AdjustSidebarButtons();
            LoadControl(new enrollees());

        }
        private void AdjustSidebarButtons()
        {
            if (panelSubEnrollees.Visible)
            {
                btnAdmin.Top = panelSubEnrollees.Bottom + 5;
            }
            else
            {
                btnAdmin.Top = btnNewEnrollees.Bottom + 5; // if submenu is hidden
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            lblName.Text = _displayName;
            lblRole.Text = role;
            if (_imageData != null && _imageData.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(_imageData))
                {
                    pic.Image = Image.FromStream(ms);
                }
            }
            panelSubEnrollees.Visible = false;
            panelSubEnrollees.Height = 0;
            AdjustSidebarButtons();
            btnBasicEd.Click += SubMenuButton_Click;
            btnJHS.Click += SubMenuButton_Click;
            btnSHS.Click += SubMenuButton_Click;

        }
        private void SubMenuButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedBtn = sender as Guna.UI2.WinForms.Guna2Button;
            if (clickedBtn != null)
            {
                lblAdmin.Text = clickedBtn.Text;

                // Optional: Load a user control based on the button
                if (clickedBtn == btnBasicEd)
                {
                    LoadControl(new BasicEducation());
                }
                else if (clickedBtn == btnJHS)
                {
                    LoadControl(new Junior_High_School());
                }
                else if (clickedBtn == btnSHS)
                {
                    LoadControl(new Senior_High_School());
                }
            }
        }


        private void picArrow_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            lblAdmin.Text = btnAdmin.Text; // Set the label text to the button text
            LoadControl(new Admin());
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}