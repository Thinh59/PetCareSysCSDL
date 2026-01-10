namespace PetCare.HoiVien
{
    partial class UC_HV_DiemLoyalty
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
            this.pnl_HV_DiemLoyalty = new System.Windows.Forms.Panel();
            this.dataGridView_HV_LSDiem = new System.Windows.Forms.DataGridView();
            this.NgayGio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MoTa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemCong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemTru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemConLai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lb_HV_LSDiem = new System.Windows.Forms.Label();
            this.textBox_HV_DiemHienTai = new System.Windows.Forms.TextBox();
            this.lb_HV_DiemHienTai = new System.Windows.Forms.Label();
            this.btn_HV_QuayLai = new System.Windows.Forms.Button();
            this.lb_HV_DiemLoyalty = new System.Windows.Forms.Label();
            this.pnl_HV_DiemLoyalty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_HV_LSDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_HV_DiemLoyalty
            // 
            this.pnl_HV_DiemLoyalty.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_HV_DiemLoyalty.Controls.Add(this.dataGridView_HV_LSDiem);
            this.pnl_HV_DiemLoyalty.Controls.Add(this.lb_HV_LSDiem);
            this.pnl_HV_DiemLoyalty.Controls.Add(this.textBox_HV_DiemHienTai);
            this.pnl_HV_DiemLoyalty.Controls.Add(this.lb_HV_DiemHienTai);
            this.pnl_HV_DiemLoyalty.Controls.Add(this.btn_HV_QuayLai);
            this.pnl_HV_DiemLoyalty.Controls.Add(this.lb_HV_DiemLoyalty);
            this.pnl_HV_DiemLoyalty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_HV_DiemLoyalty.Location = new System.Drawing.Point(0, 0);
            this.pnl_HV_DiemLoyalty.Name = "pnl_HV_DiemLoyalty";
            this.pnl_HV_DiemLoyalty.Size = new System.Drawing.Size(1000, 700);
            this.pnl_HV_DiemLoyalty.TabIndex = 2;
            // 
            // dataGridView_HV_LSDiem
            // 
            this.dataGridView_HV_LSDiem.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_HV_LSDiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_HV_LSDiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_HV_LSDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_HV_LSDiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NgayGio,
            this.HoaDon,
            this.MoTa,
            this.DiemCong,
            this.DiemTru,
            this.DiemConLai});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_HV_LSDiem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_HV_LSDiem.Location = new System.Drawing.Point(137, 213);
            this.dataGridView_HV_LSDiem.Name = "dataGridView_HV_LSDiem";
            this.dataGridView_HV_LSDiem.RowHeadersVisible = false;
            this.dataGridView_HV_LSDiem.RowHeadersWidth = 62;
            this.dataGridView_HV_LSDiem.RowTemplate.Height = 28;
            this.dataGridView_HV_LSDiem.Size = new System.Drawing.Size(818, 321);
            this.dataGridView_HV_LSDiem.TabIndex = 52;
            // 
            // NgayGio
            // 
            this.NgayGio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NgayGio.HeaderText = "Ngày Giờ";
            this.NgayGio.MinimumWidth = 8;
            this.NgayGio.Name = "NgayGio";
            this.NgayGio.ReadOnly = true;
            this.NgayGio.Width = 119;
            // 
            // HoaDon
            // 
            this.HoaDon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HoaDon.HeaderText = "Hóa đơn";
            this.HoaDon.MinimumWidth = 8;
            this.HoaDon.Name = "HoaDon";
            this.HoaDon.ReadOnly = true;
            this.HoaDon.Width = 112;
            // 
            // MoTa
            // 
            this.MoTa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MoTa.HeaderText = "Mô tả";
            this.MoTa.MinimumWidth = 8;
            this.MoTa.Name = "MoTa";
            this.MoTa.ReadOnly = true;
            this.MoTa.Width = 91;
            // 
            // DiemCong
            // 
            this.DiemCong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DiemCong.HeaderText = "Điểm cộng";
            this.DiemCong.MinimumWidth = 8;
            this.DiemCong.Name = "DiemCong";
            this.DiemCong.ReadOnly = true;
            this.DiemCong.Width = 128;
            // 
            // DiemTru
            // 
            this.DiemTru.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DiemTru.HeaderText = "Điểm trừ";
            this.DiemTru.MinimumWidth = 8;
            this.DiemTru.Name = "DiemTru";
            this.DiemTru.ReadOnly = true;
            this.DiemTru.Width = 115;
            // 
            // DiemConLai
            // 
            this.DiemConLai.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DiemConLai.HeaderText = "Điểm còn lại";
            this.DiemConLai.MinimumWidth = 8;
            this.DiemConLai.Name = "DiemConLai";
            this.DiemConLai.ReadOnly = true;
            this.DiemConLai.Width = 142;
            // 
            // lb_HV_LSDiem
            // 
            this.lb_HV_LSDiem.AutoSize = true;
            this.lb_HV_LSDiem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_HV_LSDiem.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_HV_LSDiem.Location = new System.Drawing.Point(137, 178);
            this.lb_HV_LSDiem.Name = "lb_HV_LSDiem";
            this.lb_HV_LSDiem.Size = new System.Drawing.Size(258, 22);
            this.lb_HV_LSDiem.TabIndex = 51;
            this.lb_HV_LSDiem.Text = "Lịch sử giao dịch điểm Loyalty:";
            // 
            // textBox_HV_DiemHienTai
            // 
            this.textBox_HV_DiemHienTai.Enabled = false;
            this.textBox_HV_DiemHienTai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox_HV_DiemHienTai.Location = new System.Drawing.Point(365, 121);
            this.textBox_HV_DiemHienTai.Name = "textBox_HV_DiemHienTai";
            this.textBox_HV_DiemHienTai.Size = new System.Drawing.Size(198, 28);
            this.textBox_HV_DiemHienTai.TabIndex = 50;
            // 
            // lb_HV_DiemHienTai
            // 
            this.lb_HV_DiemHienTai.AutoSize = true;
            this.lb_HV_DiemHienTai.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_HV_DiemHienTai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_HV_DiemHienTai.Location = new System.Drawing.Point(137, 124);
            this.lb_HV_DiemHienTai.Name = "lb_HV_DiemHienTai";
            this.lb_HV_DiemHienTai.Size = new System.Drawing.Size(185, 22);
            this.lb_HV_DiemHienTai.TabIndex = 49;
            this.lb_HV_DiemHienTai.Text = "Điểm Loyalty hiện tại:";
            // 
            // btn_HV_QuayLai
            // 
            this.btn_HV_QuayLai.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_HV_QuayLai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_HV_QuayLai.ForeColor = System.Drawing.Color.Teal;
            this.btn_HV_QuayLai.Location = new System.Drawing.Point(421, 565);
            this.btn_HV_QuayLai.Name = "btn_HV_QuayLai";
            this.btn_HV_QuayLai.Size = new System.Drawing.Size(103, 43);
            this.btn_HV_QuayLai.TabIndex = 43;
            this.btn_HV_QuayLai.Text = "Quay lại";
            this.btn_HV_QuayLai.UseVisualStyleBackColor = false;
            this.btn_HV_QuayLai.Click += new System.EventHandler(this.btn_HV_QuayLai_Click);
            // 
            // lb_HV_DiemLoyalty
            // 
            this.lb_HV_DiemLoyalty.AutoSize = true;
            this.lb_HV_DiemLoyalty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_HV_DiemLoyalty.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_HV_DiemLoyalty.Location = new System.Drawing.Point(327, 42);
            this.lb_HV_DiemLoyalty.Name = "lb_HV_DiemLoyalty";
            this.lb_HV_DiemLoyalty.Size = new System.Drawing.Size(328, 50);
            this.lb_HV_DiemLoyalty.TabIndex = 8;
            this.lb_HV_DiemLoyalty.Text = "ĐIỂM LOYALTY";
            // 
            // UC_HV_DiemLoyalty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_HV_DiemLoyalty);
            this.Name = "UC_HV_DiemLoyalty";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnl_HV_DiemLoyalty.ResumeLayout(false);
            this.pnl_HV_DiemLoyalty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_HV_LSDiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_HV_DiemLoyalty;
        private System.Windows.Forms.Button btn_HV_QuayLai;
        private System.Windows.Forms.Label lb_HV_DiemLoyalty;
        private System.Windows.Forms.TextBox textBox_HV_DiemHienTai;
        private System.Windows.Forms.Label lb_HV_DiemHienTai;
        private System.Windows.Forms.Label lb_HV_LSDiem;
        private System.Windows.Forms.DataGridView dataGridView_HV_LSDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayGio;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn MoTa;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemCong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemTru;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemConLai;
    }
}
