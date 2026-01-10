namespace PetCare.KhachHang.UC
{
    partial class UC_KH_ChiNhanh
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_KH_ChiNhanh));
            this.pnl_KH_Content = new System.Windows.Forms.Panel();
            this.pnl_KH_ChiNhanh = new System.Windows.Forms.Panel();
            this.dataGridView_ChiNhanh = new System.Windows.Forms.DataGridView();
            this.TenCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TGMo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TGDong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DVCungCap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lb_KH_ChiNhanh = new System.Windows.Forms.Label();
            this.pic_icon_KH_DSChiNhanh = new System.Windows.Forms.PictureBox();
            this.pnl_KH_Content.SuspendLayout();
            this.pnl_KH_ChiNhanh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ChiNhanh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_DSChiNhanh)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_KH_Content
            // 
            this.pnl_KH_Content.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.pnl_KH_Content.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_KH_Content.Controls.Add(this.pnl_KH_ChiNhanh);
            this.pnl_KH_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_Content.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_Content.Name = "pnl_KH_Content";
            this.pnl_KH_Content.Size = new System.Drawing.Size(1128, 744);
            this.pnl_KH_Content.TabIndex = 12;
            // 
            // pnl_KH_ChiNhanh
            // 
            this.pnl_KH_ChiNhanh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnl_KH_ChiNhanh.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_ChiNhanh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_KH_ChiNhanh.Controls.Add(this.dataGridView_ChiNhanh);
            this.pnl_KH_ChiNhanh.Controls.Add(this.lb_KH_ChiNhanh);
            this.pnl_KH_ChiNhanh.Controls.Add(this.pic_icon_KH_DSChiNhanh);
            this.pnl_KH_ChiNhanh.Location = new System.Drawing.Point(164, 49);
            this.pnl_KH_ChiNhanh.Name = "pnl_KH_ChiNhanh";
            this.pnl_KH_ChiNhanh.Size = new System.Drawing.Size(810, 656);
            this.pnl_KH_ChiNhanh.TabIndex = 0;
            // 
            // dataGridView_ChiNhanh
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView_ChiNhanh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_ChiNhanh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView_ChiNhanh.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_ChiNhanh.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_ChiNhanh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_ChiNhanh.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_ChiNhanh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_ChiNhanh.ColumnHeadersHeight = 60;
            this.dataGridView_ChiNhanh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenCN,
            this.TGMo,
            this.TGDong,
            this.DiaChi,
            this.SDT,
            this.DVCungCap});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_ChiNhanh.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_ChiNhanh.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridView_ChiNhanh.Location = new System.Drawing.Point(3, 142);
            this.dataGridView_ChiNhanh.Name = "dataGridView_ChiNhanh";
            this.dataGridView_ChiNhanh.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_ChiNhanh.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_ChiNhanh.RowHeadersVisible = false;
            this.dataGridView_ChiNhanh.RowHeadersWidth = 62;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dataGridView_ChiNhanh.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_ChiNhanh.RowTemplate.Height = 28;
            this.dataGridView_ChiNhanh.Size = new System.Drawing.Size(810, 273);
            this.dataGridView_ChiNhanh.TabIndex = 25;
            // 
            // TenCN
            // 
            this.TenCN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TenCN.HeaderText = "Tên Chi nhánh";
            this.TenCN.MinimumWidth = 8;
            this.TenCN.Name = "TenCN";
            this.TenCN.ReadOnly = true;
            this.TenCN.Width = 148;
            // 
            // TGMo
            // 
            this.TGMo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TGMo.HeaderText = "Thời gian mở cửa";
            this.TGMo.MinimumWidth = 8;
            this.TGMo.Name = "TGMo";
            this.TGMo.ReadOnly = true;
            this.TGMo.Width = 142;
            // 
            // TGDong
            // 
            this.TGDong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TGDong.HeaderText = "Thời gian đóng cửa";
            this.TGDong.MinimumWidth = 8;
            this.TGDong.Name = "TGDong";
            this.TGDong.ReadOnly = true;
            this.TGDong.Width = 153;
            // 
            // DiaChi
            // 
            this.DiaChi.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DiaChi.HeaderText = "Địa chỉ";
            this.DiaChi.MinimumWidth = 8;
            this.DiaChi.Name = "DiaChi";
            this.DiaChi.ReadOnly = true;
            this.DiaChi.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DiaChi.Width = 72;
            // 
            // SDT
            // 
            this.SDT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SDT.HeaderText = "Số điện thoại";
            this.SDT.MinimumWidth = 8;
            this.SDT.Name = "SDT";
            this.SDT.ReadOnly = true;
            this.SDT.Width = 136;
            // 
            // DVCungCap
            // 
            this.DVCungCap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DVCungCap.HeaderText = "Dịch vụ cung cấp";
            this.DVCungCap.MinimumWidth = 8;
            this.DVCungCap.Name = "DVCungCap";
            this.DVCungCap.ReadOnly = true;
            this.DVCungCap.Width = 140;
            // 
            // lb_KH_ChiNhanh
            // 
            this.lb_KH_ChiNhanh.AutoSize = true;
            this.lb_KH_ChiNhanh.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_ChiNhanh.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_ChiNhanh.Location = new System.Drawing.Point(273, 52);
            this.lb_KH_ChiNhanh.Name = "lb_KH_ChiNhanh";
            this.lb_KH_ChiNhanh.Size = new System.Drawing.Size(288, 55);
            this.lb_KH_ChiNhanh.TabIndex = 6;
            this.lb_KH_ChiNhanh.Text = "CHI NHÁNH";
            // 
            // pic_icon_KH_DSChiNhanh
            // 
            this.pic_icon_KH_DSChiNhanh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_icon_KH_DSChiNhanh.Image = ((System.Drawing.Image)(resources.GetObject("pic_icon_KH_DSChiNhanh.Image")));
            this.pic_icon_KH_DSChiNhanh.Location = new System.Drawing.Point(205, 52);
            this.pic_icon_KH_DSChiNhanh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_icon_KH_DSChiNhanh.Name = "pic_icon_KH_DSChiNhanh";
            this.pic_icon_KH_DSChiNhanh.Size = new System.Drawing.Size(62, 55);
            this.pic_icon_KH_DSChiNhanh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_icon_KH_DSChiNhanh.TabIndex = 5;
            this.pic_icon_KH_DSChiNhanh.TabStop = false;
            // 
            // UC_KH_ChiNhanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_Content);
            this.Name = "UC_KH_ChiNhanh";
            this.Size = new System.Drawing.Size(1128, 744);
            this.pnl_KH_Content.ResumeLayout(false);
            this.pnl_KH_ChiNhanh.ResumeLayout(false);
            this.pnl_KH_ChiNhanh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ChiNhanh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_icon_KH_DSChiNhanh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_KH_Content;
        private System.Windows.Forms.Panel pnl_KH_ChiNhanh;
        private System.Windows.Forms.DataGridView dataGridView_ChiNhanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TGMo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TGDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DVCungCap;
        private System.Windows.Forms.Label lb_KH_ChiNhanh;
        private System.Windows.Forms.PictureBox pic_icon_KH_DSChiNhanh;
    }
}
