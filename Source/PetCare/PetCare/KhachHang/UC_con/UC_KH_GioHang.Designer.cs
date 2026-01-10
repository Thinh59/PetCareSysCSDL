namespace PetCare.KhachHang
{
    partial class UC_KH_GioHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_KH_GioHang));
            this.pnl_KH_GioHang = new System.Windows.Forms.Panel();
            this.btn_KH_ThanhToan = new System.Windows.Forms.Button();
            this.btn_KH_QuayLai = new System.Windows.Forms.Button();
            this.dataGridView_KH_GioHang = new System.Windows.Forms.DataGridView();
            this.lb_KH_GioHang = new System.Windows.Forms.Label();
            this.pic_icon_KH_GioHang = new System.Windows.Forms.PictureBox();
            this.lb_ChiNhanh = new System.Windows.Forms.Label();
            this.MaLSDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Xoa = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnl_KH_GioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_GioHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_GioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_KH_GioHang
            // 
            this.pnl_KH_GioHang.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_GioHang.Controls.Add(this.lb_ChiNhanh);
            this.pnl_KH_GioHang.Controls.Add(this.btn_KH_ThanhToan);
            this.pnl_KH_GioHang.Controls.Add(this.btn_KH_QuayLai);
            this.pnl_KH_GioHang.Controls.Add(this.dataGridView_KH_GioHang);
            this.pnl_KH_GioHang.Controls.Add(this.lb_KH_GioHang);
            this.pnl_KH_GioHang.Controls.Add(this.pic_icon_KH_GioHang);
            this.pnl_KH_GioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_GioHang.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_GioHang.Name = "pnl_KH_GioHang";
            this.pnl_KH_GioHang.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_GioHang.TabIndex = 2;
            // 
            // btn_KH_ThanhToan
            // 
            this.btn_KH_ThanhToan.AutoSize = true;
            this.btn_KH_ThanhToan.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_KH_ThanhToan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_KH_ThanhToan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_KH_ThanhToan.Location = new System.Drawing.Point(712, 550);
            this.btn_KH_ThanhToan.Name = "btn_KH_ThanhToan";
            this.btn_KH_ThanhToan.Size = new System.Drawing.Size(109, 43);
            this.btn_KH_ThanhToan.TabIndex = 21;
            this.btn_KH_ThanhToan.Text = "Thanh toán";
            this.btn_KH_ThanhToan.UseVisualStyleBackColor = false;
            this.btn_KH_ThanhToan.Click += new System.EventHandler(this.btn_KH_ThanhToan_Click);
            // 
            // btn_KH_QuayLai
            // 
            this.btn_KH_QuayLai.AutoSize = true;
            this.btn_KH_QuayLai.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_KH_QuayLai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KH_QuayLai.ForeColor = System.Drawing.Color.Teal;
            this.btn_KH_QuayLai.Location = new System.Drawing.Point(621, 550);
            this.btn_KH_QuayLai.Name = "btn_KH_QuayLai";
            this.btn_KH_QuayLai.Size = new System.Drawing.Size(85, 43);
            this.btn_KH_QuayLai.TabIndex = 10;
            this.btn_KH_QuayLai.Text = "Quay lại";
            this.btn_KH_QuayLai.UseVisualStyleBackColor = false;
            this.btn_KH_QuayLai.Click += new System.EventHandler(this.btn_KH_QuayLai_Click);
            // 
            // dataGridView_KH_GioHang
            // 
            this.dataGridView_KH_GioHang.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_KH_GioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_KH_GioHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_KH_GioHang.ColumnHeadersHeight = 34;
            this.dataGridView_KH_GioHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaLSDV,
            this.MaSP,
            this.SanPham,
            this.SoLuong,
            this.Xoa});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_KH_GioHang.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_KH_GioHang.Location = new System.Drawing.Point(143, 181);
            this.dataGridView_KH_GioHang.Name = "dataGridView_KH_GioHang";
            this.dataGridView_KH_GioHang.RowHeadersVisible = false;
            this.dataGridView_KH_GioHang.RowHeadersWidth = 62;
            this.dataGridView_KH_GioHang.RowTemplate.Height = 28;
            this.dataGridView_KH_GioHang.Size = new System.Drawing.Size(678, 274);
            this.dataGridView_KH_GioHang.TabIndex = 9;
            this.dataGridView_KH_GioHang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_KH_GioHang_CellContentClick);
            this.dataGridView_KH_GioHang.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_KH_GioHang_CellEndEdit);
            // 
            // lb_KH_GioHang
            // 
            this.lb_KH_GioHang.AutoSize = true;
            this.lb_KH_GioHang.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_GioHang.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_GioHang.Location = new System.Drawing.Point(400, 68);
            this.lb_KH_GioHang.Name = "lb_KH_GioHang";
            this.lb_KH_GioHang.Size = new System.Drawing.Size(233, 50);
            this.lb_KH_GioHang.TabIndex = 8;
            this.lb_KH_GioHang.Text = "GIỎ HÀNG";
            // 
            // pic_icon_KH_GioHang
            // 
            this.pic_icon_KH_GioHang.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_icon_KH_GioHang.Image = ((System.Drawing.Image)(resources.GetObject("pic_icon_KH_GioHang.Image")));
            this.pic_icon_KH_GioHang.Location = new System.Drawing.Point(332, 68);
            this.pic_icon_KH_GioHang.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_icon_KH_GioHang.Name = "pic_icon_KH_GioHang";
            this.pic_icon_KH_GioHang.Size = new System.Drawing.Size(62, 55);
            this.pic_icon_KH_GioHang.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_icon_KH_GioHang.TabIndex = 7;
            this.pic_icon_KH_GioHang.TabStop = false;
            // 
            // lb_ChiNhanh
            // 
            this.lb_ChiNhanh.AutoSize = true;
            this.lb_ChiNhanh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_ChiNhanh.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_ChiNhanh.Location = new System.Drawing.Point(143, 134);
            this.lb_ChiNhanh.Name = "lb_ChiNhanh";
            this.lb_ChiNhanh.Size = new System.Drawing.Size(103, 22);
            this.lb_ChiNhanh.TabIndex = 22;
            this.lb_ChiNhanh.Text = "Chi nhánh: ";
            // 
            // MaLSDV
            // 
            this.MaLSDV.HeaderText = "Mã LSDV";
            this.MaLSDV.MinimumWidth = 8;
            this.MaLSDV.Name = "MaLSDV";
            this.MaLSDV.ReadOnly = true;
            this.MaLSDV.Visible = false;
            this.MaLSDV.Width = 150;
            // 
            // MaSP
            // 
            this.MaSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaSP.HeaderText = "Mã Sản phẩm";
            this.MaSP.MinimumWidth = 8;
            this.MaSP.Name = "MaSP";
            this.MaSP.ReadOnly = true;
            this.MaSP.Width = 154;
            // 
            // SanPham
            // 
            this.SanPham.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SanPham.HeaderText = "Tên Sản phẩm";
            this.SanPham.MinimumWidth = 8;
            this.SanPham.Name = "SanPham";
            this.SanPham.ReadOnly = true;
            this.SanPham.Width = 157;
            // 
            // SoLuong
            // 
            this.SoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SoLuong.HeaderText = "Số lượng";
            this.SoLuong.MinimumWidth = 8;
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.Width = 115;
            // 
            // Xoa
            // 
            this.Xoa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Xoa.HeaderText = "Xóa";
            this.Xoa.MinimumWidth = 8;
            this.Xoa.Name = "Xoa";
            this.Xoa.ReadOnly = true;
            this.Xoa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Xoa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Xoa.Width = 75;
            // 
            // UC_KH_GioHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_GioHang);
            this.Name = "UC_KH_GioHang";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_GioHang.ResumeLayout(false);
            this.pnl_KH_GioHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_GioHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_GioHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_KH_GioHang;
        private System.Windows.Forms.Button btn_KH_QuayLai;
        private System.Windows.Forms.DataGridView dataGridView_KH_GioHang;
        private System.Windows.Forms.Label lb_KH_GioHang;
        private System.Windows.Forms.PictureBox pic_icon_KH_GioHang;
        private System.Windows.Forms.Button btn_KH_ThanhToan;
        private System.Windows.Forms.Label lb_ChiNhanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaLSDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewButtonColumn Xoa;
    }
}
