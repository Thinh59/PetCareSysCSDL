namespace PetCare.KhachHang
{
    partial class UC_KH_XemVaHuyDichVu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_KH_XemVaHuyDichVu));
            this.pnl_KH_XemHuyDV = new System.Windows.Forms.Panel();
            this.btn_KH_QuayLai = new System.Windows.Forms.Button();
            this.dataGridView_KH_XemHuyDV = new System.Windows.Forms.DataGridView();
            this.lb_KH_XemHuyDV = new System.Windows.Forms.Label();
            this.pic_icon_KH_XemHuyDV = new System.Windows.Forms.PictureBox();
            this.Ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaLSDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HuyDV = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnl_KH_XemHuyDV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_XemHuyDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_XemHuyDV)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_KH_XemHuyDV
            // 
            this.pnl_KH_XemHuyDV.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_XemHuyDV.Controls.Add(this.btn_KH_QuayLai);
            this.pnl_KH_XemHuyDV.Controls.Add(this.dataGridView_KH_XemHuyDV);
            this.pnl_KH_XemHuyDV.Controls.Add(this.lb_KH_XemHuyDV);
            this.pnl_KH_XemHuyDV.Controls.Add(this.pic_icon_KH_XemHuyDV);
            this.pnl_KH_XemHuyDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_XemHuyDV.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_XemHuyDV.Name = "pnl_KH_XemHuyDV";
            this.pnl_KH_XemHuyDV.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_XemHuyDV.TabIndex = 2;
            // 
            // btn_KH_QuayLai
            // 
            this.btn_KH_QuayLai.AutoSize = true;
            this.btn_KH_QuayLai.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_KH_QuayLai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KH_QuayLai.Location = new System.Drawing.Point(420, 570);
            this.btn_KH_QuayLai.Name = "btn_KH_QuayLai";
            this.btn_KH_QuayLai.Size = new System.Drawing.Size(85, 48);
            this.btn_KH_QuayLai.TabIndex = 10;
            this.btn_KH_QuayLai.Text = "Quay lại";
            this.btn_KH_QuayLai.UseVisualStyleBackColor = false;
            this.btn_KH_QuayLai.Click += new System.EventHandler(this.btn_KH_QuayLai_Click);
            // 
            // dataGridView_KH_XemHuyDV
            // 
            this.dataGridView_KH_XemHuyDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_KH_XemHuyDV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_KH_XemHuyDV.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_KH_XemHuyDV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_KH_XemHuyDV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_KH_XemHuyDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_KH_XemHuyDV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ngay,
            this.MaLSDV,
            this.MaDV,
            this.TenDV,
            this.TrangThai,
            this.HuyDV});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_KH_XemHuyDV.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_KH_XemHuyDV.Location = new System.Drawing.Point(150, 139);
            this.dataGridView_KH_XemHuyDV.Name = "dataGridView_KH_XemHuyDV";
            this.dataGridView_KH_XemHuyDV.RowHeadersVisible = false;
            this.dataGridView_KH_XemHuyDV.RowHeadersWidth = 62;
            this.dataGridView_KH_XemHuyDV.RowTemplate.Height = 28;
            this.dataGridView_KH_XemHuyDV.Size = new System.Drawing.Size(759, 309);
            this.dataGridView_KH_XemHuyDV.TabIndex = 9;
            this.dataGridView_KH_XemHuyDV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_KH_XemHuyDV_CellContentClick);
            // 
            // lb_KH_XemHuyDV
            // 
            this.lb_KH_XemHuyDV.AutoSize = true;
            this.lb_KH_XemHuyDV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_XemHuyDV.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_XemHuyDV.Location = new System.Drawing.Point(218, 60);
            this.lb_KH_XemHuyDV.Name = "lb_KH_XemHuyDV";
            this.lb_KH_XemHuyDV.Size = new System.Drawing.Size(691, 55);
            this.lb_KH_XemHuyDV.TabIndex = 8;
            this.lb_KH_XemHuyDV.Text = "XEM VÀ HỦY DỊCH VỤ ĐÃ ĐẶT";
            // 
            // pic_icon_KH_XemHuyDV
            // 
            this.pic_icon_KH_XemHuyDV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_icon_KH_XemHuyDV.Image = ((System.Drawing.Image)(resources.GetObject("pic_icon_KH_XemHuyDV.Image")));
            this.pic_icon_KH_XemHuyDV.Location = new System.Drawing.Point(150, 60);
            this.pic_icon_KH_XemHuyDV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_icon_KH_XemHuyDV.Name = "pic_icon_KH_XemHuyDV";
            this.pic_icon_KH_XemHuyDV.Size = new System.Drawing.Size(62, 55);
            this.pic_icon_KH_XemHuyDV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_icon_KH_XemHuyDV.TabIndex = 7;
            this.pic_icon_KH_XemHuyDV.TabStop = false;
            // 
            // Ngay
            // 
            this.Ngay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Ngay.HeaderText = "Ngày";
            this.Ngay.MinimumWidth = 8;
            this.Ngay.Name = "Ngay";
            this.Ngay.Width = 85;
            // 
            // MaLSDV
            // 
            this.MaLSDV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaLSDV.HeaderText = "Mã Lịch sử Dịch vụ";
            this.MaLSDV.MinimumWidth = 8;
            this.MaLSDV.Name = "MaLSDV";
            this.MaLSDV.ReadOnly = true;
            this.MaLSDV.Width = 128;
            // 
            // MaDV
            // 
            this.MaDV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaDV.HeaderText = "Mã Dịch Vụ";
            this.MaDV.MinimumWidth = 8;
            this.MaDV.Name = "MaDV";
            this.MaDV.ReadOnly = true;
            this.MaDV.Width = 129;
            // 
            // TenDV
            // 
            this.TenDV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TenDV.HeaderText = "Tên Dịch Vụ";
            this.TenDV.MinimumWidth = 8;
            this.TenDV.Name = "TenDV";
            this.TenDV.ReadOnly = true;
            this.TenDV.Width = 111;
            // 
            // TrangThai
            // 
            this.TrangThai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TrangThai.HeaderText = "Trạng Thái";
            this.TrangThai.MinimumWidth = 8;
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.ReadOnly = true;
            this.TrangThai.Width = 120;
            // 
            // HuyDV
            // 
            this.HuyDV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HuyDV.HeaderText = "Hủy";
            this.HuyDV.MinimumWidth = 8;
            this.HuyDV.Name = "HuyDV";
            this.HuyDV.Width = 48;
            // 
            // UC_KH_XemVaHuyDichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_XemHuyDV);
            this.Name = "UC_KH_XemVaHuyDichVu";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_XemHuyDV.ResumeLayout(false);
            this.pnl_KH_XemHuyDV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_XemHuyDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_XemHuyDV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_KH_XemHuyDV;
        private System.Windows.Forms.Button btn_KH_QuayLai;
        private System.Windows.Forms.DataGridView dataGridView_KH_XemHuyDV;
        private System.Windows.Forms.Label lb_KH_XemHuyDV;
        private System.Windows.Forms.PictureBox pic_icon_KH_XemHuyDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngay;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaLSDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.DataGridViewButtonColumn HuyDV;
    }
}
