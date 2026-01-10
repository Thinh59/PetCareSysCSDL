namespace PetCare
{
    partial class Usc_TT10_LSTiemPhong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usc_TT10_LSTiemPhong));
            this.dataGridView_KH_LSTiemPhong = new System.Windows.Forms.DataGridView();
            this.MaLSDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaThuCung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenThuCung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayTiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoaiVacxin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LieuLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BacSi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaGoiTiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_KH_LSTiemPhong = new System.Windows.Forms.Panel();
            this.btn_TT10_QuayLaiLSK = new System.Windows.Forms.Button();
            this.lb_KH_LSTiemPhong = new System.Windows.Forms.Label();
            this.pic_icon_KH_LSTiemPhong = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_LSTiemPhong)).BeginInit();
            this.pnl_KH_LSTiemPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_LSTiemPhong)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_KH_LSTiemPhong
            // 
            this.dataGridView_KH_LSTiemPhong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView_KH_LSTiemPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_KH_LSTiemPhong.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_KH_LSTiemPhong.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_KH_LSTiemPhong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_KH_LSTiemPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_KH_LSTiemPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_KH_LSTiemPhong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaLSDV,
            this.MaThuCung,
            this.TenThuCung,
            this.NgayTiem,
            this.LoaiVacxin,
            this.LieuLuong,
            this.BacSi,
            this.MaGoiTiem});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_KH_LSTiemPhong.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_KH_LSTiemPhong.Location = new System.Drawing.Point(58, 99);
            this.dataGridView_KH_LSTiemPhong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView_KH_LSTiemPhong.Name = "dataGridView_KH_LSTiemPhong";
            this.dataGridView_KH_LSTiemPhong.RowHeadersVisible = false;
            this.dataGridView_KH_LSTiemPhong.RowHeadersWidth = 62;
            this.dataGridView_KH_LSTiemPhong.RowTemplate.Height = 28;
            this.dataGridView_KH_LSTiemPhong.Size = new System.Drawing.Size(859, 337);
            this.dataGridView_KH_LSTiemPhong.TabIndex = 11;
            this.dataGridView_KH_LSTiemPhong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_KH_LSTiemPhong_CellContentClick);
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
            // MaThuCung
            // 
            this.MaThuCung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaThuCung.HeaderText = "Mã Thú cưng";
            this.MaThuCung.MinimumWidth = 8;
            this.MaThuCung.Name = "MaThuCung";
            this.MaThuCung.Width = 115;
            // 
            // TenThuCung
            // 
            this.TenThuCung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TenThuCung.HeaderText = "Tên Thú cưng";
            this.TenThuCung.MinimumWidth = 8;
            this.TenThuCung.Name = "TenThuCung";
            this.TenThuCung.ReadOnly = true;
            this.TenThuCung.Width = 119;
            // 
            // NgayTiem
            // 
            this.NgayTiem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NgayTiem.HeaderText = "Ngày tiêm";
            this.NgayTiem.MinimumWidth = 8;
            this.NgayTiem.Name = "NgayTiem";
            this.NgayTiem.ReadOnly = true;
            this.NgayTiem.Width = 95;
            // 
            // LoaiVacxin
            // 
            this.LoaiVacxin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LoaiVacxin.HeaderText = "Loại Vaccine";
            this.LoaiVacxin.MinimumWidth = 8;
            this.LoaiVacxin.Name = "LoaiVacxin";
            this.LoaiVacxin.ReadOnly = true;
            this.LoaiVacxin.Width = 110;
            // 
            // LieuLuong
            // 
            this.LieuLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LieuLuong.HeaderText = "Liều lượng";
            this.LieuLuong.MinimumWidth = 8;
            this.LieuLuong.Name = "LieuLuong";
            this.LieuLuong.ReadOnly = true;
            // 
            // BacSi
            // 
            this.BacSi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BacSi.HeaderText = "Bác sĩ phụ trách";
            this.BacSi.MinimumWidth = 8;
            this.BacSi.Name = "BacSi";
            this.BacSi.ReadOnly = true;
            this.BacSi.Width = 101;
            // 
            // MaGoiTiem
            // 
            this.MaGoiTiem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaGoiTiem.HeaderText = "Mã Gói tiêm";
            this.MaGoiTiem.MinimumWidth = 8;
            this.MaGoiTiem.Name = "MaGoiTiem";
            this.MaGoiTiem.Width = 110;
            // 
            // pnl_KH_LSTiemPhong
            // 
            this.pnl_KH_LSTiemPhong.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_LSTiemPhong.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.pnl_KH_LSTiemPhong.Controls.Add(this.btn_TT10_QuayLaiLSK);
            this.pnl_KH_LSTiemPhong.Controls.Add(this.lb_KH_LSTiemPhong);
            this.pnl_KH_LSTiemPhong.Controls.Add(this.pic_icon_KH_LSTiemPhong);
            this.pnl_KH_LSTiemPhong.Controls.Add(this.panel1);
            this.pnl_KH_LSTiemPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_LSTiemPhong.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_LSTiemPhong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl_KH_LSTiemPhong.Name = "pnl_KH_LSTiemPhong";
            this.pnl_KH_LSTiemPhong.Size = new System.Drawing.Size(1221, 753);
            this.pnl_KH_LSTiemPhong.TabIndex = 1;
            // 
            // btn_TT10_QuayLaiLSK
            // 
            this.btn_TT10_QuayLaiLSK.AutoSize = true;
            this.btn_TT10_QuayLaiLSK.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_TT10_QuayLaiLSK.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TT10_QuayLaiLSK.Location = new System.Drawing.Point(591, 606);
            this.btn_TT10_QuayLaiLSK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_TT10_QuayLaiLSK.Name = "btn_TT10_QuayLaiLSK";
            this.btn_TT10_QuayLaiLSK.Size = new System.Drawing.Size(76, 38);
            this.btn_TT10_QuayLaiLSK.TabIndex = 12;
            this.btn_TT10_QuayLaiLSK.Text = "Quay lại";
            this.btn_TT10_QuayLaiLSK.UseVisualStyleBackColor = false;
            this.btn_TT10_QuayLaiLSK.Click += new System.EventHandler(this.btn_TT10_QuayLaiLSK_Click);
            // 
            // lb_KH_LSTiemPhong
            // 
            this.lb_KH_LSTiemPhong.AutoSize = true;
            this.lb_KH_LSTiemPhong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_LSTiemPhong.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_LSTiemPhong.Location = new System.Drawing.Point(440, 116);
            this.lb_KH_LSTiemPhong.Name = "lb_KH_LSTiemPhong";
            this.lb_KH_LSTiemPhong.Size = new System.Drawing.Size(433, 47);
            this.lb_KH_LSTiemPhong.TabIndex = 8;
            this.lb_KH_LSTiemPhong.Text = "LỊCH SỬ TIÊM PHÒNG";
            // 
            // pic_icon_KH_LSTiemPhong
            // 
            this.pic_icon_KH_LSTiemPhong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_icon_KH_LSTiemPhong.Image = ((System.Drawing.Image)(resources.GetObject("pic_icon_KH_LSTiemPhong.Image")));
            this.pic_icon_KH_LSTiemPhong.Location = new System.Drawing.Point(379, 116);
            this.pic_icon_KH_LSTiemPhong.Name = "pic_icon_KH_LSTiemPhong";
            this.pic_icon_KH_LSTiemPhong.Size = new System.Drawing.Size(55, 44);
            this.pic_icon_KH_LSTiemPhong.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_icon_KH_LSTiemPhong.TabIndex = 7;
            this.pic_icon_KH_LSTiemPhong.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView_KH_LSTiemPhong);
            this.panel1.Location = new System.Drawing.Point(154, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 600);
            this.panel1.TabIndex = 13;
            // 
            // Usc_TT10_LSTiemPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_LSTiemPhong);
            this.Name = "Usc_TT10_LSTiemPhong";
            this.Size = new System.Drawing.Size(1221, 753);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_LSTiemPhong)).EndInit();
            this.pnl_KH_LSTiemPhong.ResumeLayout(false);
            this.pnl_KH_LSTiemPhong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_LSTiemPhong)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_KH_LSTiemPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaLSDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaThuCung;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenThuCung;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayTiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoaiVacxin;
        private System.Windows.Forms.DataGridViewTextBoxColumn LieuLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn BacSi;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaGoiTiem;
        private System.Windows.Forms.Panel pnl_KH_LSTiemPhong;
        private System.Windows.Forms.Label lb_KH_LSTiemPhong;
        private System.Windows.Forms.PictureBox pic_icon_KH_LSTiemPhong;
        private System.Windows.Forms.Button btn_TT10_QuayLaiLSK;
        private System.Windows.Forms.Panel panel1;
    }
}
