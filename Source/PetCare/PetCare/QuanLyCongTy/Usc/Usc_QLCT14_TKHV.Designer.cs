namespace PetCare
{
    partial class Usc_QLCT14_TKHV
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
            this.dgv_QLCT14_TKHV = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_QLCT14_TKHV = new System.Windows.Forms.Panel();
            this.lbl_QLCT14_ChiTieuTB = new System.Windows.Forms.Label();
            this.txb_QLCT14_CTTB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT14_TKHV)).BeginInit();
            this.pnl_QLCT14_TKHV.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_QLCT14_TKHV
            // 
            this.dgv_QLCT14_TKHV.AllowUserToAddRows = false;
            this.dgv_QLCT14_TKHV.AllowUserToDeleteRows = false;
            this.dgv_QLCT14_TKHV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_QLCT14_TKHV.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv_QLCT14_TKHV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_QLCT14_TKHV.Location = new System.Drawing.Point(184, 115);
            this.dgv_QLCT14_TKHV.Name = "dgv_QLCT14_TKHV";
            this.dgv_QLCT14_TKHV.ReadOnly = true;
            this.dgv_QLCT14_TKHV.RowHeadersWidth = 51;
            this.dgv_QLCT14_TKHV.RowTemplate.Height = 24;
            this.dgv_QLCT14_TKHV.Size = new System.Drawing.Size(937, 549);
            this.dgv_QLCT14_TKHV.TabIndex = 37;
            this.dgv_QLCT14_TKHV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_QLCT14_TKHV_CellContentClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(442, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(269, 32);
            this.label8.TabIndex = 24;
            this.label8.Text = "Thống Kê Hội Viên";
            // 
            // pnl_QLCT14_TKHV
            // 
            this.pnl_QLCT14_TKHV.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_QLCT14_TKHV.Controls.Add(this.lbl_QLCT14_ChiTieuTB);
            this.pnl_QLCT14_TKHV.Controls.Add(this.txb_QLCT14_CTTB);
            this.pnl_QLCT14_TKHV.Controls.Add(this.dgv_QLCT14_TKHV);
            this.pnl_QLCT14_TKHV.Controls.Add(this.label8);
            this.pnl_QLCT14_TKHV.Location = new System.Drawing.Point(47, 50);
            this.pnl_QLCT14_TKHV.Name = "pnl_QLCT14_TKHV";
            this.pnl_QLCT14_TKHV.Size = new System.Drawing.Size(1124, 642);
            this.pnl_QLCT14_TKHV.TabIndex = 2;
            this.pnl_QLCT14_TKHV.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_QLCT14_TKKH_Paint);
            // 
            // lbl_QLCT14_ChiTieuTB
            // 
            this.lbl_QLCT14_ChiTieuTB.AutoSize = true;
            this.lbl_QLCT14_ChiTieuTB.Location = new System.Drawing.Point(38, 312);
            this.lbl_QLCT14_ChiTieuTB.Name = "lbl_QLCT14_ChiTieuTB";
            this.lbl_QLCT14_ChiTieuTB.Size = new System.Drawing.Size(110, 16);
            this.lbl_QLCT14_ChiTieuTB.TabIndex = 49;
            this.lbl_QLCT14_ChiTieuTB.Text = "Chi tiêu trung bình";
            // 
            // txb_QLCT14_CTTB
            // 
            this.txb_QLCT14_CTTB.Location = new System.Drawing.Point(43, 339);
            this.txb_QLCT14_CTTB.Name = "txb_QLCT14_CTTB";
            this.txb_QLCT14_CTTB.Size = new System.Drawing.Size(100, 22);
            this.txb_QLCT14_CTTB.TabIndex = 48;
            this.txb_QLCT14_CTTB.TextChanged += new System.EventHandler(this.txb_QLCT14_CTTB_TextChanged);
            // 
            // Usc_QLCT14_TKHV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_QLCT14_TKHV);
            this.Name = "Usc_QLCT14_TKHV";
            this.Size = new System.Drawing.Size(1221, 753);
            this.Load += new System.EventHandler(this.Usc_QLCT14_TKHV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT14_TKHV)).EndInit();
            this.pnl_QLCT14_TKHV.ResumeLayout(false);
            this.pnl_QLCT14_TKHV.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_QLCT14_TKHV;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnl_QLCT14_TKHV;
        private System.Windows.Forms.Label lbl_QLCT14_ChiTieuTB;
        private System.Windows.Forms.TextBox txb_QLCT14_CTTB;
    }
}
