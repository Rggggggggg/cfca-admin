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
    public partial class Admin : UserControl
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Form mainForm = this.FindForm(); // get owner form

            OverlayForm overlay = new OverlayForm(mainForm); // ✅ pass mainForm to constructor
            overlay.Show(mainForm); // display the overlay with owner set

            add_Admin addForm = new add_Admin(overlay); // pass overlay to modal
            addForm.FormBorderStyle = FormBorderStyle.None;
            addForm.StartPosition = FormStartPosition.CenterParent;
            addForm.ShowDialog(mainForm);// modal dialog with owner
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
    }
}
