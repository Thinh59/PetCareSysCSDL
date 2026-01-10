namespace PetCare.KhachHang
{
    partial class UC_KH_LSKhamBenh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_KH_LSKhamBenh));
            this.pnl_KH_LSKhamBenh = new System.Windows.Forms.Panel();
            this.btn_KH_QuayLai = new System.Windows.Forms.Button();
            this.dataGridView_KH_LSKhamBenh = new System.Windows.Forms.DataGridView();
            this.MaLSDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThuCung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayKham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BacSi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrieuChung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChanDoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayHen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToaThuoc = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lb_KH_LSKhamBenh = new System.Windows.Forms.Label();
            this.pic_icon_KH_LSKhamBenh = new System.Windows.Forms.PictureBox();
            this.txb_KH4_Time = new System.Windows.Forms.TextBox();
            this.lab_ALL_TDN = new System.Windows.Forms.Label();
            this.pnl_KH_LSKhamBenh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_LSKhamBenh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_LSKhamBenh)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_KH_LSKhamBenh
            // 
            this.pnl_KH_LSKhamBenh.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_LSKhamBenh.Controls.Add(this.txb_KH4_Time);
            this.pnl_KH_LSKhamBenh.Controls.Add(this.btn_KH_QuayLai);
            this.pnl_KH_LSKhamBenh.Controls.Add(this.lab_ALL_TDN);
            this.pnl_KH_LSKhamBenh.Controls.Add(this.dataGridView_KH_LSKhamBenh);
            this.pnl_KH_LSKhamBenh.Controls.Add(this.lb_KH_LSKhamBenh);
            this.pnl_KH_LSKhamBenh.Controls.Add(this.pic_icon_KH_LSKhamBenh);
            this.pnl_KH_LSKhamBenh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_LSKhamBenh.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_LSKhamBenh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl_KH_LSKhamBenh.Name = "pnl_KH_LSKhamBenh";
            this.pnl_KH_LSKhamBenh.Size = new System.Drawing.Size(889, 560);
            this.pnl_KH_LSKhamBenh.TabIndex = 1;
            // 
            // btn_KH_QuayLai
            // 
            this.btn_KH_QuayLai.AutoSize = true;
            this.btn_KH_QuayLai.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_KH_QuayLai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KH_QuayLai.Location = new System.Drawing.Point(356, 448);
            this.btn_KH_QuayLai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_KH_QuayLai.Name = "btn_KH_QuayLai";
            this.btn_KH_QuayLai.Size = new System.Drawing.Size(76, 38);
            this.btn_KH_QuayLai.TabIndex = 10;
            this.btn_KH_QuayLai.Text = "Quay lại";
            this.btn_KH_QuayLai.UseVisualStyleBackColor = false;
            this.btn_KH_QuayLai.Click += new System.EventHandler(this.btn_KH_QuayLai_Click);
            // 
            // dataGridView_KH_LSKhamBenh
            // 
            this.dataGridView_KH_LSKhamBenh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_KH_LSKhamBenh.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_KH_LSKhamBenh.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_KH_LSKhamBenh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_KH_LSKhamBenh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_KH_LSKhamBenh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_KH_LSKhamBenh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaLSDV,
            this.ThuCung,
            this.NgayKham,
            this.BacSi,
            this.TrieuChung,
            this.ChanDoan,
            this.NgayHen,
            this.ToaThuoc});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_KH_LSKhamBenh.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_KH_LSKhamBenh.Location = new System.Drawing.Point(20, 98);
            this.dataGridView_KH_LSKhamBenh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView_KH_LSKhamBenh.Name = "dataGridView_KH_LSKhamBenh";
            this.dataGridView_KH_LSKhamBenh.RowHeadersVisible = false;
            this.dataGridView_KH_LSKhamBenh.RowHeadersWidth = 62;
            this.dataGridView_KH_LSKhamBenh.RowTemplate.Height = 28;
            this.dataGridView_KH_LSKhamBenh.Size = new System.Drawing.Size(850, 334);
            this.dataGridView_KH_LSKhamBenh.TabIndex = 9;
            this.dataGridView_KH_LSKhamBenh.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_KH_LSKhamBenh_CellContentClick_1);
            // 
            // MaLSDV
            // 
            this.MaLSDV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaLSDV.HeaderText = "Mã Lịch sử dịch vụ";
            this.MaLSDV.MinimumWidth = 8;
            this.MaLSDV.Name = "MaLSDV";
            this.MaLSDV.ReadOnly = true;
            this.MaLSDV.Width = 107;
            // 
            // ThuCung
            // 
            this.ThuCung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ThuCung.HeaderText = "Thú Cưng";
            this.ThuCung.MinimumWidth = 8;
            this.ThuCung.Name = "ThuCung";
            this.ThuCung.ReadOnly = true;
            this.ThuCung.Width = 95;
            // 
            // NgayKham
            // 
            this.NgayKham.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NgayKham.HeaderText = "Ngày Khám";
            this.NgayKham.MinimumWidth = 8;
            this.NgayKham.Name = "NgayKham";
            this.NgayKham.ReadOnly = true;
            this.NgayKham.Width = 104;
            // 
            // BacSi
            // 
            this.BacSi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BacSi.HeaderText = "Bác Sĩ";
            this.BacSi.MinimumWidth = 8;
            this.BacSi.Name = "BacSi";
            this.BacSi.ReadOnly = true;
            this.BacSi.Width = 62;
            // 
            // TrieuChung
            // 
            this.TrieuChung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrieuChung.HeaderText = "Triệu Chứng";
            this.TrieuChung.MinimumWidth = 8;
            this.TrieuChung.Name = "TrieuChung";
            this.TrieuChung.ReadOnly = true;
            this.TrieuChung.Width = 110;
            // 
            // ChanDoan
            // 
            this.ChanDoan.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ChanDoan.HeaderText = "Chẩn Đoán";
            this.ChanDoan.MinimumWidth = 8;
            this.ChanDoan.Name = "ChanDoan";
            this.ChanDoan.ReadOnly = true;
            this.ChanDoan.Width = 101;
            // 
            // NgayHen
            // 
            this.NgayHen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NgayHen.HeaderText = "Ngày Hẹn";
            this.NgayHen.MinimumWidth = 8;
            this.NgayHen.Name = "NgayHen";
            this.NgayHen.ReadOnly = true;
            this.NgayHen.Width = 94;
            // 
            // ToaThuoc
            // 
            this.ToaThuoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ToaThuoc.HeaderText = "Toa Thuốc";
            this.ToaThuoc.MinimumWidth = 8;
            this.ToaThuoc.Name = "ToaThuoc";
            this.ToaThuoc.Width = 75;
            // 
            // lb_KH_LSKhamBenh
            // 
            this.lb_KH_LSKhamBenh.AutoSize = true;
            this.lb_KH_LSKhamBenh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_LSKhamBenh.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_LSKhamBenh.Location = new System.Drawing.Point(194, 48);
            this.lb_KH_LSKhamBenh.Name = "lb_KH_LSKhamBenh";
            this.lb_KH_LSKhamBenh.Size = new System.Drawing.Size(416, 47);
            this.lb_KH_LSKhamBenh.TabIndex = 8;
            this.lb_KH_LSKhamBenh.Text = "LỊCH SỬ KHÁM BỆNH";
            // 
            // pic_icon_KH_LSKhamBenh
            // 
            this.pic_icon_KH_LSKhamBenh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_icon_KH_LSKhamBenh.Image = ((System.Drawing.Image)(resources.GetObject("pic_icon_KH_LSKhamBenh.Image")));
            this.pic_icon_KH_LSKhamBenh.Location = new System.Drawing.Point(133, 48);
            this.pic_icon_KH_LSKhamBenh.Name = "pic_icon_KH_LSKhamBenh";
            this.pic_icon_KH_LSKhamBenh.Size = new System.Drawing.Size(55, 44);
            this.pic_icon_KH_LSKhamBenh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_icon_KH_LSKhamBenh.TabIndex = 7;
            this.pic_icon_KH_LSKhamBenh.TabStop = false;
            // 
            // txb_KH4_Time
            // 
            this.txb_KH4_Time.Location = new System.Drawing.Point(690, 62);
            this.txb_KH4_Time.Multiline = true;
            this.txb_KH4_Time.Name = "txb_KH4_Time";
            this.txb_KH4_Time.Size = new System.Drawing.Size(99, 29);
            this.txb_KH4_Time.TabIndex = 70;
            this.txb_KH4_Time.TextChanged += new System.EventHandler(this.txb_KH4_Time_TextChanged);
            // 
            // lab_ALL_TDN
            // 
            this.lab_ALL_TDN.AutoSize = true;
            this.lab_ALL_TDN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_ALL_TDN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_ALL_TDN.Location = new System.Drawing.Point(690, 34);
            this.lab_ALL_TDN.Name = "lab_ALL_TDN";
            this.lab_ALL_TDN.Size = new System.Drawing.Size(67, 25);
            this.lab_ALL_TDN.TabIndex = 69;
            this.lab_ALL_TDN.Text = "Time: ";
            // 
            // UC_KH_LSKhamBenh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_LSKhamBenh);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UC_KH_LSKhamBenh";
            this.Size = new System.Drawing.Size(889, 560);
            this.pnl_KH_LSKhamBenh.ResumeLayout(false);
            this.pnl_KH_LSKhamBenh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_LSKhamBenh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_LSKhamBenh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_KH_LSKhamBenh;
        private System.Windows.Forms.Button btn_KH_QuayLai;
        private System.Windows.Forms.DataGridView dataGridView_KH_LSKhamBenh;
        private System.Windows.Forms.Label lb_KH_LSKhamBenh;
        private System.Windows.Forms.PictureBox pic_icon_KH_LSKhamBenh;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaLSDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThuCung;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayKham;
        private System.Windows.Forms.DataGridViewTextBoxColumn BacSi;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrieuChung;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChanDoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayHen;
        private System.Windows.Forms.DataGridViewButtonColumn ToaThuoc;
        private System.Windows.Forms.TextBox txb_KH4_Time;
        private System.Windows.Forms.Label lab_ALL_TDN;
    }
}
