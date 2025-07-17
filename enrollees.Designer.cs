namespace CFCA_ADMIN
{
    partial class enrollees
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(enrollees));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.dtgEnrollees = new Guna.UI2.WinForms.Guna2DataGridView();
            this.tbSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.Student_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.level_applied = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.submitted_at = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirm = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEnrollees)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(130)))), ((int)(((byte)(254)))));
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(731, 71);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(80, 33);
            this.guna2Button1.TabIndex = 1;
            this.guna2Button1.Text = "Refresh";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 10;
            // 
            // dtgEnrollees
            // 
            this.dtgEnrollees.AllowUserToAddRows = false;
            this.dtgEnrollees.AllowUserToDeleteRows = false;
            this.dtgEnrollees.AllowUserToResizeColumns = false;
            this.dtgEnrollees.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dtgEnrollees.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgEnrollees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dtgEnrollees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgEnrollees.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgEnrollees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgEnrollees.ColumnHeadersHeight = 50;
            this.dtgEnrollees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dtgEnrollees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Student_Number,
            this.name,
            this.level_applied,
            this.gender,
            this.age,
            this.contact,
            this.submitted_at,
            this.btnConfirm,
            this.btnDelete});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgEnrollees.DefaultCellStyle = dataGridViewCellStyle5;
            this.dtgEnrollees.EnableHeadersVisualStyles = true;
            this.dtgEnrollees.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtgEnrollees.Location = new System.Drawing.Point(12, 110);
            this.dtgEnrollees.Name = "dtgEnrollees";
            this.dtgEnrollees.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgEnrollees.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgEnrollees.RowHeadersVisible = false;
            this.dtgEnrollees.RowTemplate.Height = 45;
            this.dtgEnrollees.Size = new System.Drawing.Size(837, 433);
            this.dtgEnrollees.TabIndex = 2;
            this.dtgEnrollees.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dtgEnrollees.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dtgEnrollees.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dtgEnrollees.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dtgEnrollees.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dtgEnrollees.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dtgEnrollees.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtgEnrollees.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dtgEnrollees.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtgEnrollees.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgEnrollees.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dtgEnrollees.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dtgEnrollees.ThemeStyle.HeaderStyle.Height = 50;
            this.dtgEnrollees.ThemeStyle.ReadOnly = true;
            this.dtgEnrollees.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dtgEnrollees.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dtgEnrollees.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgEnrollees.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dtgEnrollees.ThemeStyle.RowsStyle.Height = 45;
            this.dtgEnrollees.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dtgEnrollees.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dtgEnrollees.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgEnrollees_CellContentClick);
            // 
            // tbSearch
            // 
            this.tbSearch.BorderRadius = 10;
            this.tbSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbSearch.DefaultText = "";
            this.tbSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tbSearch.IconLeft = ((System.Drawing.Image)(resources.GetObject("tbSearch.IconLeft")));
            this.tbSearch.Location = new System.Drawing.Point(572, 68);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.PlaceholderText = "Search";
            this.tbSearch.SelectedText = "";
            this.tbSearch.Size = new System.Drawing.Size(153, 36);
            this.tbSearch.TabIndex = 3;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // Student_Number
            // 
            this.Student_Number.FillWeight = 80F;
            this.Student_Number.HeaderText = "student_number";
            this.Student_Number.Name = "Student_Number";
            this.Student_Number.ReadOnly = true;
            this.Student_Number.Width = 92;
            // 
            // name
            // 
            this.name.FillWeight = 80F;
            this.name.HeaderText = "name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 185;
            // 
            // level_applied
            // 
            this.level_applied.FillWeight = 160F;
            this.level_applied.HeaderText = "level_applied";
            this.level_applied.Name = "level_applied";
            this.level_applied.ReadOnly = true;
            this.level_applied.Width = 92;
            // 
            // gender
            // 
            this.gender.FillWeight = 60F;
            this.gender.HeaderText = "gender";
            this.gender.Name = "gender";
            this.gender.ReadOnly = true;
            this.gender.Width = 69;
            // 
            // age
            // 
            this.age.FillWeight = 50F;
            this.age.HeaderText = "age";
            this.age.Name = "age";
            this.age.ReadOnly = true;
            this.age.Width = 57;
            // 
            // contact
            // 
            this.contact.FillWeight = 110F;
            this.contact.HeaderText = "contact";
            this.contact.Name = "contact";
            this.contact.ReadOnly = true;
            this.contact.Width = 127;
            // 
            // submitted_at
            // 
            this.submitted_at.FillWeight = 110F;
            this.submitted_at.HeaderText = "submitted_at";
            this.submitted_at.Name = "submitted_at";
            this.submitted_at.ReadOnly = true;
            this.submitted_at.Width = 127;
            // 
            // btnConfirm
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle3.NullValue")));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10);
            this.btnConfirm.DefaultCellStyle = dataGridViewCellStyle3;
            this.btnConfirm.FillWeight = 40F;
            this.btnConfirm.HeaderText = "";
            this.btnConfirm.Image = global::CFCA_ADMIN.Properties.Resources.edit_interface_sign;
            this.btnConfirm.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.ReadOnly = true;
            this.btnConfirm.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnConfirm.Width = 43;
            // 
            // btnDelete
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle4.NullValue")));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10);
            this.btnDelete.DefaultCellStyle = dataGridViewCellStyle4;
            this.btnDelete.FillWeight = 40F;
            this.btnDelete.HeaderText = "";
            this.btnDelete.Image = global::CFCA_ADMIN.Properties.Resources.delete_square;
            this.btnDelete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ReadOnly = true;
            this.btnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnDelete.Width = 43;
            // 
            // enrollees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.dtgEnrollees);
            this.Controls.Add(this.guna2Button1);
            this.Name = "enrollees";
            this.Size = new System.Drawing.Size(871, 601);
            this.Load += new System.EventHandler(this.enrollees_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgEnrollees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DataGridView dtgEnrollees;
        private Guna.UI2.WinForms.Guna2TextBox tbSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Student_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn level_applied;
        private System.Windows.Forms.DataGridViewTextBoxColumn gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn age;
        private System.Windows.Forms.DataGridViewTextBoxColumn contact;
        private System.Windows.Forms.DataGridViewTextBoxColumn submitted_at;
        private System.Windows.Forms.DataGridViewImageColumn btnConfirm;
        private System.Windows.Forms.DataGridViewImageColumn btnDelete;
    }
}
