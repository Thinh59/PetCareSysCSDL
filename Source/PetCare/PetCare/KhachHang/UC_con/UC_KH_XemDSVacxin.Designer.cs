namespace PetCare.KhachHang
{
    partial class UC_KH_XemDSVacxin
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
            this.pnl_KH_XemDSVacxin = new System.Windows.Forms.Panel();
            this.dataGridView_KH_DSVacxin = new System.Windows.Forms.DataGridView();
            this.MaVacxin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenVacxin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoMui = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_KH_QuayLai = new System.Windows.Forms.Button();
            this.lb_KH_DSVacxin = new System.Windows.Forms.Label();
            this.pnl_KH_XemDSVacxin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_DSVacxin)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_KH_XemDSVacxin
            // 
            this.pnl_KH_XemDSVacxin.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_KH_XemDSVacxin.Controls.Add(this.dataGridView_KH_DSVacxin);
            this.pnl_KH_XemDSVacxin.Controls.Add(this.btn_KH_QuayLai);
            this.pnl_KH_XemDSVacxin.Controls.Add(this.lb_KH_DSVacxin);
            this.pnl_KH_XemDSVacxin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_KH_XemDSVacxin.Location = new System.Drawing.Point(0, 0);
            this.pnl_KH_XemDSVacxin.Name = "pnl_KH_XemDSVacxin";
            this.pnl_KH_XemDSVacxin.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_XemDSVacxin.TabIndex = 3;
            // 
            // dataGridView_KH_DSVacxin
            // 
            this.dataGridView_KH_DSVacxin.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView_KH_DSVacxin.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView_KH_DSVacxin.BackgroundColor = System.Drawing.Color.AntiqueWhite;
            this.dataGridView_KH_DSVacxin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_KH_DSVacxin.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_KH_DSVacxin.ColumnHeadersHeight = 34;
            this.dataGridView_KH_DSVacxin.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaVacxin,
            this.TenVacxin,
            this.GiaTien,
            this.SoMui});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_KH_DSVacxin.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_KH_DSVacxin.Location = new System.Drawing.Point(219, 119);
            this.dataGridView_KH_DSVacxin.Name = "dataGridView_KH_DSVacxin";
            this.dataGridView_KH_DSVacxin.RowHeadersVisible = false;
            this.dataGridView_KH_DSVacxin.RowHeadersWidth = 62;
            this.dataGridView_KH_DSVacxin.RowTemplate.Height = 28;
            this.dataGridView_KH_DSVacxin.Size = new System.Drawing.Size(599, 425);
            this.dataGridView_KH_DSVacxin.TabIndex = 44;
            // 
            // MaVacxin
            // 
            this.MaVacxin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaVacxin.HeaderText = "Mã Vacxin";
            this.MaVacxin.MinimumWidth = 8;
            this.MaVacxin.Name = "MaVacxin";
            this.MaVacxin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MaVacxin.Width = 128;
            // 
            // TenVacxin
            // 
            this.TenVacxin.HeaderText = "Tên Vacxin";
            this.TenVacxin.MinimumWidth = 8;
            this.TenVacxin.Name = "TenVacxin";
            this.TenVacxin.Width = 131;
            // 
            // GiaTien
            // 
            this.GiaTien.HeaderText = "Giá Tiền";
            this.GiaTien.MinimumWidth = 8;
            this.GiaTien.Name = "GiaTien";
            this.GiaTien.Width = 112;
            // 
            // SoMui
            // 
            this.SoMui.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SoMui.HeaderText = "Số mũi sẵn có";
            this.SoMui.MinimumWidth = 8;
            this.SoMui.Name = "SoMui";
            this.SoMui.ReadOnly = true;
            this.SoMui.Width = 153;
            // 
            // btn_KH_QuayLai
            // 
            this.btn_KH_QuayLai.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_KH_QuayLai.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_KH_QuayLai.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_KH_QuayLai.Location = new System.Drawing.Point(420, 550);
            this.btn_KH_QuayLai.Name = "btn_KH_QuayLai";
            this.btn_KH_QuayLai.Size = new System.Drawing.Size(103, 43);
            this.btn_KH_QuayLai.TabIndex = 42;
            this.btn_KH_QuayLai.Text = "Quay lại";
            this.btn_KH_QuayLai.UseVisualStyleBackColor = false;
            this.btn_KH_QuayLai.Click += new System.EventHandler(this.btn_KH_QuayLai_Click);
            // 
            // lb_KH_DSVacxin
            // 
            this.lb_KH_DSVacxin.AutoSize = true;
            this.lb_KH_DSVacxin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_KH_DSVacxin.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lb_KH_DSVacxin.Location = new System.Drawing.Point(272, 48);
            this.lb_KH_DSVacxin.Name = "lb_KH_DSVacxin";
            this.lb_KH_DSVacxin.Size = new System.Drawing.Size(433, 50);
            this.lb_KH_DSVacxin.TabIndex = 8;
            this.lb_KH_DSVacxin.Text = "DANH SÁCH VACXIN";
            // 
            // UC_KH_XemDSVacxin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_KH_XemDSVacxin);
            this.Name = "UC_KH_XemDSVacxin";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnl_KH_XemDSVacxin.ResumeLayout(false);
            this.pnl_KH_XemDSVacxin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_KH_DSVacxin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_KH_XemDSVacxin;
        private System.Windows.Forms.DataGridView dataGridView_KH_DSVacxin;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaVacxin;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenVacxin;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoMui;
        private System.Windows.Forms.Button btn_KH_QuayLai;
        private System.Windows.Forms.Label lb_KH_DSVacxin;
    }
}
