namespace PetCare.KhachHang
{
    partial class UC_KH_XemToaThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_KH_XemToaThuoc));
            this.pnl_KH_CTThuoc = new System.Windows.Forms.Panel();
            this.btn_KH_QuayLai = new System.Windows.Forms.Button();
            this.dataGridView_KH_CTThuoc = new System.Windows.Forms.DataGridView();
            this.lb_KH_CTThuoc = new System.Windows.Forms.Label();
            this.pic_icon_KH_CTThuoc = new System.Windows.Forms.PictureBox();
            this.MaThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonViTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LieuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_KH_CTThuoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_CTThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_CTThuoc)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_KH_CTThuoc
            // 
            this.pnl_KH_CTThuoc.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_CTThuoc.Controls.Add(this.btn_KH_QuayLai);
            this.pnl_KH_CTThuoc.Controls.Add(this.dataGridView_KH_CTThuoc);
            this.pnl_KH_CTThuoc.Controls.Add(this.lb_KH_CTThuoc);
            this.pnl_KH_CTThuoc.Controls.Add(this.pic_icon_KH_CTThuoc);
            this.pnl_KH_CTThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_CTThuoc.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_CTThuoc.Name = "pnl_KH_CTThuoc";
            this.pnl_KH_CTThuoc.Size = new System.Drawing.Size(1128, 744);
            this.pnl_KH_CTThuoc.TabIndex = 2;
            // 
            // btn_KH_QuayLai
            // 
            this.btn_KH_QuayLai.AutoSize = true;
            this.btn_KH_QuayLai.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_KH_QuayLai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KH_QuayLai.Location = new System.Drawing.Point(500, 490);
            this.btn_KH_QuayLai.Name = "btn_KH_QuayLai";
            this.btn_KH_QuayLai.Size = new System.Drawing.Size(85, 48);
            this.btn_KH_QuayLai.TabIndex = 10;
            this.btn_KH_QuayLai.Text = "Quay lại";
            this.btn_KH_QuayLai.UseVisualStyleBackColor = false;
            // 
            // dataGridView_KH_CTThuoc
            // 
            this.dataGridView_KH_CTThuoc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_KH_CTThuoc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_KH_CTThuoc.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_KH_CTThuoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_KH_CTThuoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_KH_CTThuoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_KH_CTThuoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaThuoc,
            this.SoLuong,
            this.DonViTinh,
            this.LieuDung,
            this.ThanhTien});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_KH_CTThuoc.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_KH_CTThuoc.Location = new System.Drawing.Point(156, 117);
            this.dataGridView_KH_CTThuoc.Name = "dataGridView_KH_CTThuoc";
            this.dataGridView_KH_CTThuoc.RowHeadersVisible = false;
            this.dataGridView_KH_CTThuoc.RowHeadersWidth = 62;
            this.dataGridView_KH_CTThuoc.RowTemplate.Height = 28;
            this.dataGridView_KH_CTThuoc.Size = new System.Drawing.Size(820, 338);
            this.dataGridView_KH_CTThuoc.TabIndex = 9;
            // 
            // lb_KH_CTThuoc
            // 
            this.lb_KH_CTThuoc.AutoSize = true;
            this.lb_KH_CTThuoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_CTThuoc.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_CTThuoc.Location = new System.Drawing.Point(332, 43);
            this.lb_KH_CTThuoc.Name = "lb_KH_CTThuoc";
            this.lb_KH_CTThuoc.Size = new System.Drawing.Size(498, 55);
            this.lb_KH_CTThuoc.TabIndex = 8;
            this.lb_KH_CTThuoc.Text = "CHI TIẾT TOA THUỐC";
            // 
            // pic_icon_KH_CTThuoc
            // 
            this.pic_icon_KH_CTThuoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_icon_KH_CTThuoc.Image = ((System.Drawing.Image)(resources.GetObject("pic_icon_KH_CTThuoc.Image")));
            this.pic_icon_KH_CTThuoc.Location = new System.Drawing.Point(264, 43);
            this.pic_icon_KH_CTThuoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_icon_KH_CTThuoc.Name = "pic_icon_KH_CTThuoc";
            this.pic_icon_KH_CTThuoc.Size = new System.Drawing.Size(62, 55);
            this.pic_icon_KH_CTThuoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_icon_KH_CTThuoc.TabIndex = 7;
            this.pic_icon_KH_CTThuoc.TabStop = false;
            // 
            // MaThuoc
            // 
            this.MaThuoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaThuoc.HeaderText = "Mã Thuốc";
            this.MaThuoc.MinimumWidth = 8;
            this.MaThuoc.Name = "MaThuoc";
            this.MaThuoc.Width = 125;
            // 
            // SoLuong
            // 
            this.SoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SoLuong.HeaderText = "Số Lượng";
            this.SoLuong.MinimumWidth = 8;
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ReadOnly = true;
            this.SoLuong.Width = 122;
            // 
            // DonViTinh
            // 
            this.DonViTinh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DonViTinh.HeaderText = "Đơn Vị Tính";
            this.DonViTinh.MinimumWidth = 8;
            this.DonViTinh.Name = "DonViTinh";
            this.DonViTinh.Width = 142;
            // 
            // LieuDung
            // 
            this.LieuDung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LieuDung.HeaderText = "Liều Dùng";
            this.LieuDung.MinimumWidth = 8;
            this.LieuDung.Name = "LieuDung";
            this.LieuDung.ReadOnly = true;
            this.LieuDung.Width = 127;
            // 
            // ThanhTien
            // 
            this.ThanhTien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ThanhTien.HeaderText = "Thành Tiền";
            this.ThanhTien.MinimumWidth = 8;
            this.ThanhTien.Name = "ThanhTien";
            this.ThanhTien.Width = 134;
            // 
            // UC_KH_XemToaThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_CTThuoc);
            this.Name = "UC_KH_XemToaThuoc";
            this.Size = new System.Drawing.Size(1128, 744);
            this.pnl_KH_CTThuoc.ResumeLayout(false);
            this.pnl_KH_CTThuoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_CTThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_CTThuoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_KH_CTThuoc;
        private System.Windows.Forms.Button btn_KH_QuayLai;
        private System.Windows.Forms.DataGridView dataGridView_KH_CTThuoc;
        private System.Windows.Forms.Label lb_KH_CTThuoc;
        private System.Windows.Forms.PictureBox pic_icon_KH_CTThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonViTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn LieuDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThanhTien;
    }
}
