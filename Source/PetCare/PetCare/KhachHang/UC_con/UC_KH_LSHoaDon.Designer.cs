namespace PetCare.KhachHang
{
    partial class UC_KH_LSHoaDon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_KH_LSHoaDon));
            this.btn_KH_QuayLai = new System.Windows.Forms.Button();
            this.lb_KH_LSHoaDon = new System.Windows.Forms.Label();
            this.pnl_KH_LSHoaDon = new System.Windows.Forms.Panel();
            this.comboBox_KH_LocDV = new System.Windows.Forms.ComboBox();
            this.lb_KH_LocDV = new System.Windows.Forms.Label();
            this.dateTimePicker_KH_LocNgay = new System.Windows.Forms.DateTimePicker();
            this.lb_KH_LocNgay = new System.Windows.Forms.Label();
            this.textBox_KH_LocMa = new System.Windows.Forms.TextBox();
            this.lb_KH_LocMa = new System.Windows.Forms.Label();
            this.textBox_KH_TimKiem = new System.Windows.Forms.TextBox();
            this.btn_KH_TimKiem = new System.Windows.Forms.Button();
            this.dataGridView_KH_DSHD = new System.Windows.Forms.DataGridView();
            this.MaHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayLap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NV_Lap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XemChiTiet = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lb_KH_DanhSachHD = new System.Windows.Forms.Label();
            this.pic_icon_KH_LSHoaDon = new System.Windows.Forms.PictureBox();
            this.btn_ApDung = new System.Windows.Forms.Button();
            this.pnl_KH_LSHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_DSHD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_LSHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_KH_QuayLai
            // 
            this.btn_KH_QuayLai.AutoSize = true;
            this.btn_KH_QuayLai.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_KH_QuayLai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KH_QuayLai.ForeColor = System.Drawing.Color.Teal;
            this.btn_KH_QuayLai.Location = new System.Drawing.Point(435, 597);
            this.btn_KH_QuayLai.Name = "btn_KH_QuayLai";
            this.btn_KH_QuayLai.Size = new System.Drawing.Size(85, 43);
            this.btn_KH_QuayLai.TabIndex = 10;
            this.btn_KH_QuayLai.Text = "Quay lại";
            this.btn_KH_QuayLai.UseVisualStyleBackColor = false;
            this.btn_KH_QuayLai.Click += new System.EventHandler(this.btn_KH_QuayLai_Click);
            // 
            // lb_KH_LSHoaDon
            // 
            this.lb_KH_LSHoaDon.AutoSize = true;
            this.lb_KH_LSHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_LSHoaDon.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_LSHoaDon.Location = new System.Drawing.Point(275, 62);
            this.lb_KH_LSHoaDon.Name = "lb_KH_LSHoaDon";
            this.lb_KH_LSHoaDon.Size = new System.Drawing.Size(400, 50);
            this.lb_KH_LSHoaDon.TabIndex = 8;
            this.lb_KH_LSHoaDon.Text = "LỊCH SỬ HÓA ĐƠN";
            // 
            // pnl_KH_LSHoaDon
            // 
            this.pnl_KH_LSHoaDon.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_LSHoaDon.Controls.Add(this.btn_ApDung);
            this.pnl_KH_LSHoaDon.Controls.Add(this.comboBox_KH_LocDV);
            this.pnl_KH_LSHoaDon.Controls.Add(this.lb_KH_LocDV);
            this.pnl_KH_LSHoaDon.Controls.Add(this.dateTimePicker_KH_LocNgay);
            this.pnl_KH_LSHoaDon.Controls.Add(this.lb_KH_LocNgay);
            this.pnl_KH_LSHoaDon.Controls.Add(this.textBox_KH_LocMa);
            this.pnl_KH_LSHoaDon.Controls.Add(this.lb_KH_LocMa);
            this.pnl_KH_LSHoaDon.Controls.Add(this.textBox_KH_TimKiem);
            this.pnl_KH_LSHoaDon.Controls.Add(this.btn_KH_TimKiem);
            this.pnl_KH_LSHoaDon.Controls.Add(this.dataGridView_KH_DSHD);
            this.pnl_KH_LSHoaDon.Controls.Add(this.lb_KH_DanhSachHD);
            this.pnl_KH_LSHoaDon.Controls.Add(this.btn_KH_QuayLai);
            this.pnl_KH_LSHoaDon.Controls.Add(this.lb_KH_LSHoaDon);
            this.pnl_KH_LSHoaDon.Controls.Add(this.pic_icon_KH_LSHoaDon);
            this.pnl_KH_LSHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_LSHoaDon.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_LSHoaDon.Name = "pnl_KH_LSHoaDon";
            this.pnl_KH_LSHoaDon.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_LSHoaDon.TabIndex = 4;
            // 
            // comboBox_KH_LocDV
            // 
            this.comboBox_KH_LocDV.FormattingEnabled = true;
            this.comboBox_KH_LocDV.Items.AddRange(new object[] {
            "Đã thanh toán",
            "Chưa thanh toán"});
            this.comboBox_KH_LocDV.Location = new System.Drawing.Point(385, 201);
            this.comboBox_KH_LocDV.Name = "comboBox_KH_LocDV";
            this.comboBox_KH_LocDV.Size = new System.Drawing.Size(135, 28);
            this.comboBox_KH_LocDV.TabIndex = 38;
            // 
            // lb_KH_LocDV
            // 
            this.lb_KH_LocDV.AutoSize = true;
            this.lb_KH_LocDV.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_LocDV.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_KH_LocDV.Location = new System.Drawing.Point(381, 180);
            this.lb_KH_LocDV.Name = "lb_KH_LocDV";
            this.lb_KH_LocDV.Size = new System.Drawing.Size(155, 20);
            this.lb_KH_LocDV.TabIndex = 37;
            this.lb_KH_LocDV.Text = "Lọc theo Trạng thái:";
            // 
            // dateTimePicker_KH_LocNgay
            // 
            this.dateTimePicker_KH_LocNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_KH_LocNgay.Location = new System.Drawing.Point(220, 203);
            this.dateTimePicker_KH_LocNgay.Name = "dateTimePicker_KH_LocNgay";
            this.dateTimePicker_KH_LocNgay.Size = new System.Drawing.Size(116, 26);
            this.dateTimePicker_KH_LocNgay.TabIndex = 36;
            // 
            // lb_KH_LocNgay
            // 
            this.lb_KH_LocNgay.AutoSize = true;
            this.lb_KH_LocNgay.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_LocNgay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_KH_LocNgay.Location = new System.Drawing.Point(216, 180);
            this.lb_KH_LocNgay.Name = "lb_KH_LocNgay";
            this.lb_KH_LocNgay.Size = new System.Drawing.Size(120, 20);
            this.lb_KH_LocNgay.TabIndex = 35;
            this.lb_KH_LocNgay.Text = "Lọc theo Ngày:";
            // 
            // textBox_KH_LocMa
            // 
            this.textBox_KH_LocMa.Location = new System.Drawing.Point(75, 203);
            this.textBox_KH_LocMa.Name = "textBox_KH_LocMa";
            this.textBox_KH_LocMa.Size = new System.Drawing.Size(100, 26);
            this.textBox_KH_LocMa.TabIndex = 34;
            // 
            // lb_KH_LocMa
            // 
            this.lb_KH_LocMa.AutoSize = true;
            this.lb_KH_LocMa.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_LocMa.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_KH_LocMa.Location = new System.Drawing.Point(71, 180);
            this.lb_KH_LocMa.Name = "lb_KH_LocMa";
            this.lb_KH_LocMa.Size = new System.Drawing.Size(106, 20);
            this.lb_KH_LocMa.TabIndex = 33;
            this.lb_KH_LocMa.Text = "Lọc theo Mã:";
            // 
            // textBox_KH_TimKiem
            // 
            this.textBox_KH_TimKiem.Location = new System.Drawing.Point(72, 139);
            this.textBox_KH_TimKiem.Name = "textBox_KH_TimKiem";
            this.textBox_KH_TimKiem.Size = new System.Drawing.Size(234, 26);
            this.textBox_KH_TimKiem.TabIndex = 29;
            // 
            // btn_KH_TimKiem
            // 
            this.btn_KH_TimKiem.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_KH_TimKiem.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_KH_TimKiem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_KH_TimKiem.Location = new System.Drawing.Point(312, 136);
            this.btn_KH_TimKiem.Name = "btn_KH_TimKiem";
            this.btn_KH_TimKiem.Size = new System.Drawing.Size(87, 33);
            this.btn_KH_TimKiem.TabIndex = 28;
            this.btn_KH_TimKiem.Text = "Search";
            this.btn_KH_TimKiem.UseVisualStyleBackColor = false;
            this.btn_KH_TimKiem.Click += new System.EventHandler(this.btn_KH_TimKiem_Click);
            // 
            // dataGridView_KH_DSHD
            // 
            this.dataGridView_KH_DSHD.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_KH_DSHD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_KH_DSHD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_KH_DSHD.ColumnHeadersHeight = 34;
            this.dataGridView_KH_DSHD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHD,
            this.NgayLap,
            this.NV_Lap,
            this.TongTien,
            this.TrangThai,
            this.XemChiTiet});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_KH_DSHD.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_KH_DSHD.Location = new System.Drawing.Point(72, 260);
            this.dataGridView_KH_DSHD.Name = "dataGridView_KH_DSHD";
            this.dataGridView_KH_DSHD.RowHeadersVisible = false;
            this.dataGridView_KH_DSHD.RowHeadersWidth = 62;
            this.dataGridView_KH_DSHD.RowTemplate.Height = 28;
            this.dataGridView_KH_DSHD.Size = new System.Drawing.Size(875, 300);
            this.dataGridView_KH_DSHD.TabIndex = 27;
            this.dataGridView_KH_DSHD.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_KH_DSHD_CellContentClick);
            // 
            // MaHD
            // 
            this.MaHD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaHD.HeaderText = "Mã Hóa đơn";
            this.MaHD.MinimumWidth = 8;
            this.MaHD.Name = "MaHD";
            this.MaHD.ReadOnly = true;
            this.MaHD.Width = 143;
            // 
            // NgayLap
            // 
            this.NgayLap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NgayLap.HeaderText = "Ngày lập";
            this.NgayLap.MinimumWidth = 8;
            this.NgayLap.Name = "NgayLap";
            this.NgayLap.ReadOnly = true;
            this.NgayLap.Width = 114;
            // 
            // NV_Lap
            // 
            this.NV_Lap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NV_Lap.HeaderText = "Nhân viên lập";
            this.NV_Lap.MinimumWidth = 8;
            this.NV_Lap.Name = "NV_Lap";
            this.NV_Lap.ReadOnly = true;
            this.NV_Lap.Width = 153;
            // 
            // TongTien
            // 
            this.TongTien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.MinimumWidth = 8;
            this.TongTien.Name = "TongTien";
            this.TongTien.ReadOnly = true;
            this.TongTien.Width = 119;
            // 
            // TrangThai
            // 
            this.TrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrangThai.HeaderText = "Trạng thái";
            this.TrangThai.MinimumWidth = 8;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.TrangThai.Width = 125;
            // 
            // XemChiTiet
            // 
            this.XemChiTiet.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.XemChiTiet.HeaderText = "Xem chi tiết";
            this.XemChiTiet.MinimumWidth = 8;
            this.XemChiTiet.Name = "XemChiTiet";
            this.XemChiTiet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.XemChiTiet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.XemChiTiet.Text = "Xem chi tiết";
            this.XemChiTiet.ToolTipText = "Xem chi tiết";
            this.XemChiTiet.UseColumnTextForButtonValue = true;
            this.XemChiTiet.Width = 138;
            // 
            // lb_KH_DanhSachHD
            // 
            this.lb_KH_DanhSachHD.AutoSize = true;
            this.lb_KH_DanhSachHD.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_DanhSachHD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lb_KH_DanhSachHD.Location = new System.Drawing.Point(68, 237);
            this.lb_KH_DanhSachHD.Name = "lb_KH_DanhSachHD";
            this.lb_KH_DanhSachHD.Size = new System.Drawing.Size(153, 20);
            this.lb_KH_DanhSachHD.TabIndex = 26;
            this.lb_KH_DanhSachHD.Text = "Danh sách hóa đơn:";
            // 
            // pic_icon_KH_LSHoaDon
            // 
            this.pic_icon_KH_LSHoaDon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_icon_KH_LSHoaDon.Image = ((System.Drawing.Image)(resources.GetObject("pic_icon_KH_LSHoaDon.Image")));
            this.pic_icon_KH_LSHoaDon.Location = new System.Drawing.Point(219, 62);
            this.pic_icon_KH_LSHoaDon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_icon_KH_LSHoaDon.Name = "pic_icon_KH_LSHoaDon";
            this.pic_icon_KH_LSHoaDon.Size = new System.Drawing.Size(50, 50);
            this.pic_icon_KH_LSHoaDon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_icon_KH_LSHoaDon.TabIndex = 7;
            this.pic_icon_KH_LSHoaDon.TabStop = false;
            // 
            // btn_ApDung
            // 
            this.btn_ApDung.AutoSize = true;
            this.btn_ApDung.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_ApDung.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_ApDung.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_ApDung.Location = new System.Drawing.Point(565, 198);
            this.btn_ApDung.Name = "btn_ApDung";
            this.btn_ApDung.Size = new System.Drawing.Size(87, 33);
            this.btn_ApDung.TabIndex = 39;
            this.btn_ApDung.Text = "Áp dụng";
            this.btn_ApDung.UseVisualStyleBackColor = false;
            this.btn_ApDung.Click += new System.EventHandler(this.btn_ApDung_Click);
            // 
            // UC_KH_LSHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_LSHoaDon);
            this.Name = "UC_KH_LSHoaDon";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_LSHoaDon.ResumeLayout(false);
            this.pnl_KH_LSHoaDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_DSHD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_LSHoaDon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_KH_QuayLai;
        private System.Windows.Forms.Label lb_KH_LSHoaDon;
        private System.Windows.Forms.PictureBox pic_icon_KH_LSHoaDon;
        private System.Windows.Forms.Panel pnl_KH_LSHoaDon;
        private System.Windows.Forms.DataGridView dataGridView_KH_DSHD;
        private System.Windows.Forms.Label lb_KH_DanhSachHD;
        private System.Windows.Forms.TextBox textBox_KH_TimKiem;
        private System.Windows.Forms.Button btn_KH_TimKiem;
        private System.Windows.Forms.DateTimePicker dateTimePicker_KH_LocNgay;
        private System.Windows.Forms.Label lb_KH_LocNgay;
        private System.Windows.Forms.TextBox textBox_KH_LocMa;
        private System.Windows.Forms.Label lb_KH_LocMa;
        private System.Windows.Forms.Label lb_KH_LocDV;
        private System.Windows.Forms.ComboBox comboBox_KH_LocDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayLap;
        private System.Windows.Forms.DataGridViewTextBoxColumn NV_Lap;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewButtonColumn XemChiTiet;
        private System.Windows.Forms.Button btn_ApDung;
    }
}
