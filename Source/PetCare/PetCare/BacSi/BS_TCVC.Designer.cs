namespace PetCare
{
    partial class BS_TCVC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_BS_TCVC = new System.Windows.Forms.Button();
            this.lab_BS_TCVC = new System.Windows.Forms.Label();
            this.tbox_BS_TCVC_MaVC = new System.Windows.Forms.TextBox();
            this.lab_BS_TCVC_MaVC = new System.Windows.Forms.Label();
            this.pnl_BS_TCVC = new System.Windows.Forms.Panel();
            this.tabControl_BS_TC = new System.Windows.Forms.TabControl();
            this.tagP_BS_TC_T = new System.Windows.Forms.TabPage();
            this.data_BC_TC_T = new System.Windows.Forms.DataGridView();
            this.BS_TC_T_MT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_T_TT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_T_DVT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_T_GB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_T_SLTK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_T_NHH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabP_BS_TC_VC = new System.Windows.Forms.TabPage();
            this.data_BS_TC_VC = new System.Windows.Forms.DataGridView();
            this.BS_TC_VC_MVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_VC_TVC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_VC_GT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_TC_VC_SLTK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_BS_TCVC.SuspendLayout();
            this.tabControl_BS_TC.SuspendLayout();
            this.tagP_BS_TC_T.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BC_TC_T)).BeginInit();
            this.tabP_BS_TC_VC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BS_TC_VC)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_BS_TCVC
            // 
            this.btn_BS_TCVC.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_BS_TCVC.Location = new System.Drawing.Point(209, 92);
            this.btn_BS_TCVC.Name = "btn_BS_TCVC";
            this.btn_BS_TCVC.Size = new System.Drawing.Size(92, 37);
            this.btn_BS_TCVC.TabIndex = 32;
            this.btn_BS_TCVC.Text = "Tìm Kiếm";
            this.btn_BS_TCVC.UseVisualStyleBackColor = false;
            this.btn_BS_TCVC.Click += new System.EventHandler(this.btn_BS_TCVC_Click);
            // 
            // lab_BS_TCVC
            // 
            this.lab_BS_TCVC.AutoSize = true;
            this.lab_BS_TCVC.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BS_TCVC.Location = new System.Drawing.Point(259, 34);
            this.lab_BS_TCVC.Name = "lab_BS_TCVC";
            this.lab_BS_TCVC.Size = new System.Drawing.Size(457, 45);
            this.lab_BS_TCVC.TabIndex = 0;
            this.lab_BS_TCVC.Text = "Tra Cứu Thuốc/Vaccine";
            this.lab_BS_TCVC.Click += new System.EventHandler(this.lab_BS_TCVC_Click);
            // 
            // tbox_BS_TCVC_MaVC
            // 
            this.tbox_BS_TCVC_MaVC.Location = new System.Drawing.Point(224, 55);
            this.tbox_BS_TCVC_MaVC.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbox_BS_TCVC_MaVC.Name = "tbox_BS_TCVC_MaVC";
            this.tbox_BS_TCVC_MaVC.Size = new System.Drawing.Size(214, 22);
            this.tbox_BS_TCVC_MaVC.TabIndex = 6;
            this.tbox_BS_TCVC_MaVC.TextChanged += new System.EventHandler(this.tbox_BS_TCVC_MaVC_TextChanged);
            // 
            // lab_BS_TCVC_MaVC
            // 
            this.lab_BS_TCVC_MaVC.AutoSize = true;
            this.lab_BS_TCVC_MaVC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_BS_TCVC_MaVC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BS_TCVC_MaVC.Location = new System.Drawing.Point(46, 55);
            this.lab_BS_TCVC_MaVC.Name = "lab_BS_TCVC_MaVC";
            this.lab_BS_TCVC_MaVC.Size = new System.Drawing.Size(173, 25);
            this.lab_BS_TCVC_MaVC.TabIndex = 3;
            this.lab_BS_TCVC_MaVC.Text = "Mã/Tên Sản Phẩm:";
            // 
            // pnl_BS_TCVC
            // 
            this.pnl_BS_TCVC.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_BS_TCVC.Controls.Add(this.tabControl_BS_TC);
            this.pnl_BS_TCVC.Controls.Add(this.lab_BS_TCVC);
            this.pnl_BS_TCVC.Controls.Add(this.panel1);
            this.pnl_BS_TCVC.Location = new System.Drawing.Point(192, 30);
            this.pnl_BS_TCVC.Name = "pnl_BS_TCVC";
            this.pnl_BS_TCVC.Size = new System.Drawing.Size(837, 696);
            this.pnl_BS_TCVC.TabIndex = 25;
            // 
            // tabControl_BS_TC
            // 
            this.tabControl_BS_TC.Controls.Add(this.tagP_BS_TC_T);
            this.tabControl_BS_TC.Controls.Add(this.tabP_BS_TC_VC);
            this.tabControl_BS_TC.Location = new System.Drawing.Point(58, 275);
            this.tabControl_BS_TC.Name = "tabControl_BS_TC";
            this.tabControl_BS_TC.SelectedIndex = 0;
            this.tabControl_BS_TC.Size = new System.Drawing.Size(721, 379);
            this.tabControl_BS_TC.TabIndex = 37;
            this.tabControl_BS_TC.SelectedIndexChanged += new System.EventHandler(this.tabControl_BS_TC_SelectedIndexChanged);
            // 
            // tagP_BS_TC_T
            // 
            this.tagP_BS_TC_T.Controls.Add(this.data_BC_TC_T);
            this.tagP_BS_TC_T.Location = new System.Drawing.Point(4, 25);
            this.tagP_BS_TC_T.Name = "tagP_BS_TC_T";
            this.tagP_BS_TC_T.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tagP_BS_TC_T.Size = new System.Drawing.Size(713, 350);
            this.tagP_BS_TC_T.TabIndex = 0;
            this.tagP_BS_TC_T.Text = "Thuốc";
            this.tagP_BS_TC_T.UseVisualStyleBackColor = true;
            // 
            // data_BC_TC_T
            // 
            this.data_BC_TC_T.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.data_BC_TC_T.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.data_BC_TC_T.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.data_BC_TC_T.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkSalmon;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data_BC_TC_T.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.data_BC_TC_T.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_BC_TC_T.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BS_TC_T_MT,
            this.BS_TC_T_TT,
            this.BS_TC_T_DVT,
            this.BS_TC_T_GB,
            this.BS_TC_T_SLTK,
            this.BS_TC_T_NHH});
            this.data_BC_TC_T.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_BC_TC_T.EnableHeadersVisualStyles = false;
            this.data_BC_TC_T.Location = new System.Drawing.Point(3, 3);
            this.data_BC_TC_T.Name = "data_BC_TC_T";
            this.data_BC_TC_T.RowHeadersWidth = 51;
            this.data_BC_TC_T.RowTemplate.Height = 24;
            this.data_BC_TC_T.Size = new System.Drawing.Size(707, 344);
            this.data_BC_TC_T.TabIndex = 32;
            this.data_BC_TC_T.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_BC_TC_T_CellContentClick);
            // 
            // BS_TC_T_MT
            // 
            this.BS_TC_T_MT.DataPropertyName = "Mã Thuốc";
            this.BS_TC_T_MT.HeaderText = "Mã Thuốc";
            this.BS_TC_T_MT.MinimumWidth = 6;
            this.BS_TC_T_MT.Name = "BS_TC_T_MT";
            // 
            // BS_TC_T_TT
            // 
            this.BS_TC_T_TT.DataPropertyName = "Tên Thuốc";
            this.BS_TC_T_TT.HeaderText = "Tên Thuốc";
            this.BS_TC_T_TT.MinimumWidth = 6;
            this.BS_TC_T_TT.Name = "BS_TC_T_TT";
            // 
            // BS_TC_T_DVT
            // 
            this.BS_TC_T_DVT.DataPropertyName = "Đơn Vị Tính";
            this.BS_TC_T_DVT.HeaderText = "Đơn Vị Tính";
            this.BS_TC_T_DVT.MinimumWidth = 6;
            this.BS_TC_T_DVT.Name = "BS_TC_T_DVT";
            // 
            // BS_TC_T_GB
            // 
            this.BS_TC_T_GB.DataPropertyName = "Giá Bán";
            this.BS_TC_T_GB.HeaderText = "Giá Bán";
            this.BS_TC_T_GB.MinimumWidth = 6;
            this.BS_TC_T_GB.Name = "BS_TC_T_GB";
            // 
            // BS_TC_T_SLTK
            // 
            this.BS_TC_T_SLTK.DataPropertyName = "Số Lượng Tồn Kho";
            this.BS_TC_T_SLTK.HeaderText = "Số Lượng Tồn Kho";
            this.BS_TC_T_SLTK.MinimumWidth = 6;
            this.BS_TC_T_SLTK.Name = "BS_TC_T_SLTK";
            // 
            // BS_TC_T_NHH
            // 
            this.BS_TC_T_NHH.DataPropertyName = "Ngày Hết Hạn";
            this.BS_TC_T_NHH.HeaderText = "Ngày Hết Hạn";
            this.BS_TC_T_NHH.MinimumWidth = 6;
            this.BS_TC_T_NHH.Name = "BS_TC_T_NHH";
            // 
            // tabP_BS_TC_VC
            // 
            this.tabP_BS_TC_VC.Controls.Add(this.data_BS_TC_VC);
            this.tabP_BS_TC_VC.Location = new System.Drawing.Point(4, 25);
            this.tabP_BS_TC_VC.Name = "tabP_BS_TC_VC";
            this.tabP_BS_TC_VC.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabP_BS_TC_VC.Size = new System.Drawing.Size(713, 350);
            this.tabP_BS_TC_VC.TabIndex = 1;
            this.tabP_BS_TC_VC.Text = "Vaccine";
            this.tabP_BS_TC_VC.UseVisualStyleBackColor = true;
            // 
            // data_BS_TC_VC
            // 
            this.data_BS_TC_VC.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.data_BS_TC_VC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSalmon;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data_BS_TC_VC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.data_BS_TC_VC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_BS_TC_VC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BS_TC_VC_MVC,
            this.BS_TC_VC_TVC,
            this.BS_TC_VC_GT,
            this.BS_TC_VC_SLTK});
            this.data_BS_TC_VC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_BS_TC_VC.EnableHeadersVisualStyles = false;
            this.data_BS_TC_VC.Location = new System.Drawing.Point(3, 3);
            this.data_BS_TC_VC.Name = "data_BS_TC_VC";
            this.data_BS_TC_VC.RowHeadersWidth = 51;
            this.data_BS_TC_VC.RowTemplate.Height = 24;
            this.data_BS_TC_VC.Size = new System.Drawing.Size(707, 344);
            this.data_BS_TC_VC.TabIndex = 32;
            this.data_BS_TC_VC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_BS_TC_VC_CellContentClick);
            // 
            // BS_TC_VC_MVC
            // 
            this.BS_TC_VC_MVC.DataPropertyName = "Mã Vaccine";
            this.BS_TC_VC_MVC.HeaderText = "Mã Vaccine";
            this.BS_TC_VC_MVC.MinimumWidth = 6;
            this.BS_TC_VC_MVC.Name = "BS_TC_VC_MVC";
            this.BS_TC_VC_MVC.Width = 125;
            // 
            // BS_TC_VC_TVC
            // 
            this.BS_TC_VC_TVC.DataPropertyName = "Tên Vaccine";
            this.BS_TC_VC_TVC.HeaderText = "Tên Vaccine";
            this.BS_TC_VC_TVC.MinimumWidth = 6;
            this.BS_TC_VC_TVC.Name = "BS_TC_VC_TVC";
            this.BS_TC_VC_TVC.Width = 125;
            // 
            // BS_TC_VC_GT
            // 
            this.BS_TC_VC_GT.DataPropertyName = "Giá Tiền";
            this.BS_TC_VC_GT.HeaderText = "Giá Tiền";
            this.BS_TC_VC_GT.MinimumWidth = 6;
            this.BS_TC_VC_GT.Name = "BS_TC_VC_GT";
            this.BS_TC_VC_GT.Width = 125;
            // 
            // BS_TC_VC_SLTK
            // 
            this.BS_TC_VC_SLTK.DataPropertyName = "Số Lượng Tồn Kho";
            this.BS_TC_VC_SLTK.HeaderText = "Số Lượng Tồn Kho";
            this.BS_TC_VC_SLTK.MinimumWidth = 6;
            this.BS_TC_VC_SLTK.Name = "BS_TC_VC_SLTK";
            this.BS_TC_VC_SLTK.Width = 125;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PeachPuff;
            this.panel1.Controls.Add(this.btn_BS_TCVC);
            this.panel1.Controls.Add(this.tbox_BS_TCVC_MaVC);
            this.panel1.Controls.Add(this.lab_BS_TCVC_MaVC);
            this.panel1.Location = new System.Drawing.Point(188, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 149);
            this.panel1.TabIndex = 36;
            // 
            // BS_TCVC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_BS_TCVC);
            this.Name = "BS_TCVC";
            this.Size = new System.Drawing.Size(1215, 753);
            this.Load += new System.EventHandler(this.BS_TCVC_Load);
            this.pnl_BS_TCVC.ResumeLayout(false);
            this.pnl_BS_TCVC.PerformLayout();
            this.tabControl_BS_TC.ResumeLayout(false);
            this.tagP_BS_TC_T.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data_BC_TC_T)).EndInit();
            this.tabP_BS_TC_VC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data_BS_TC_VC)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_BS_TCVC;
        private System.Windows.Forms.Label lab_BS_TCVC;
        private System.Windows.Forms.TextBox tbox_BS_TCVC_MaVC;
        private System.Windows.Forms.Label lab_BS_TCVC_MaVC;
        private System.Windows.Forms.Panel pnl_BS_TCVC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl_BS_TC;
        private System.Windows.Forms.TabPage tagP_BS_TC_T;
        private System.Windows.Forms.DataGridView data_BC_TC_T;
        private System.Windows.Forms.TabPage tabP_BS_TC_VC;
        private System.Windows.Forms.DataGridView data_BS_TC_VC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_T_MT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_T_TT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_T_DVT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_T_GB;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_T_SLTK;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_T_NHH;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_VC_MVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_VC_TVC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_VC_GT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_TC_VC_SLTK;
    }
}
