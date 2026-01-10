namespace PetCare
{
    partial class Usc_QLCT7_HSNV
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
            this.pnl_QLCT7_HSNV = new System.Windows.Forms.Panel();
            this.dgv_QLCT7_HSNV = new System.Windows.Forms.DataGridView();
            this.cmb_QLCT7_selectMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_QLCT7_XacNhan = new System.Windows.Forms.Button();
            this.txb_QLCT7_getMaCN = new System.Windows.Forms.TextBox();
            this.lbl_QLCT7_MaCN = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_QLCT7_HSNV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT7_HSNV)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_QLCT7_HSNV
            // 
            this.pnl_QLCT7_HSNV.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_QLCT7_HSNV.Controls.Add(this.dgv_QLCT7_HSNV);
            this.pnl_QLCT7_HSNV.Controls.Add(this.cmb_QLCT7_selectMode);
            this.pnl_QLCT7_HSNV.Controls.Add(this.label1);
            this.pnl_QLCT7_HSNV.Controls.Add(this.btn_QLCT7_XacNhan);
            this.pnl_QLCT7_HSNV.Controls.Add(this.txb_QLCT7_getMaCN);
            this.pnl_QLCT7_HSNV.Controls.Add(this.lbl_QLCT7_MaCN);
            this.pnl_QLCT7_HSNV.Controls.Add(this.label8);
            this.pnl_QLCT7_HSNV.Location = new System.Drawing.Point(308, 111);
            this.pnl_QLCT7_HSNV.Name = "pnl_QLCT7_HSNV";
            this.pnl_QLCT7_HSNV.Size = new System.Drawing.Size(605, 530);
            this.pnl_QLCT7_HSNV.TabIndex = 1;
            this.pnl_QLCT7_HSNV.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_QLCT7_HSNV_Paint);
            // 
            // dgv_QLCT7_HSNV
            // 
            this.dgv_QLCT7_HSNV.AllowUserToAddRows = false;
            this.dgv_QLCT7_HSNV.AllowUserToDeleteRows = false;
            this.dgv_QLCT7_HSNV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_QLCT7_HSNV.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv_QLCT7_HSNV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_QLCT7_HSNV.Location = new System.Drawing.Point(0, 238);
            this.dgv_QLCT7_HSNV.Name = "dgv_QLCT7_HSNV";
            this.dgv_QLCT7_HSNV.ReadOnly = true;
            this.dgv_QLCT7_HSNV.RowHeadersWidth = 51;
            this.dgv_QLCT7_HSNV.RowTemplate.Height = 24;
            this.dgv_QLCT7_HSNV.Size = new System.Drawing.Size(602, 292);
            this.dgv_QLCT7_HSNV.TabIndex = 37;
            this.dgv_QLCT7_HSNV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_QLCT7_HSNV_CellContentClick_1);
            // 
            // cmb_QLCT7_selectMode
            // 
            this.cmb_QLCT7_selectMode.FormattingEnabled = true;
            this.cmb_QLCT7_selectMode.Items.AddRange(new object[] {
            "Công ty",
            "Chi nhánh"});
            this.cmb_QLCT7_selectMode.Location = new System.Drawing.Point(277, 113);
            this.cmb_QLCT7_selectMode.Name = "cmb_QLCT7_selectMode";
            this.cmb_QLCT7_selectMode.Size = new System.Drawing.Size(121, 24);
            this.cmb_QLCT7_selectMode.TabIndex = 36;
            this.cmb_QLCT7_selectMode.SelectedIndexChanged += new System.EventHandler(this.cmb_QLCT7_selectMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Xem của";
            // 
            // btn_QLCT7_XacNhan
            // 
            this.btn_QLCT7_XacNhan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_QLCT7_XacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QLCT7_XacNhan.Location = new System.Drawing.Point(237, 190);
            this.btn_QLCT7_XacNhan.Name = "btn_QLCT7_XacNhan";
            this.btn_QLCT7_XacNhan.Size = new System.Drawing.Size(124, 26);
            this.btn_QLCT7_XacNhan.TabIndex = 34;
            this.btn_QLCT7_XacNhan.Text = "Xác Nhận";
            this.btn_QLCT7_XacNhan.UseVisualStyleBackColor = false;
            this.btn_QLCT7_XacNhan.Click += new System.EventHandler(this.btn_QLCT7_XacNhan_Click);
            // 
            // txb_QLCT7_getMaCN
            // 
            this.txb_QLCT7_getMaCN.Location = new System.Drawing.Point(298, 150);
            this.txb_QLCT7_getMaCN.Name = "txb_QLCT7_getMaCN";
            this.txb_QLCT7_getMaCN.Size = new System.Drawing.Size(100, 22);
            this.txb_QLCT7_getMaCN.TabIndex = 33;
            this.txb_QLCT7_getMaCN.Visible = false;
            this.txb_QLCT7_getMaCN.TextChanged += new System.EventHandler(this.txb_QLCT7_getMaCN_TextChanged_1);
            // 
            // lbl_QLCT7_MaCN
            // 
            this.lbl_QLCT7_MaCN.AutoSize = true;
            this.lbl_QLCT7_MaCN.Location = new System.Drawing.Point(207, 153);
            this.lbl_QLCT7_MaCN.Name = "lbl_QLCT7_MaCN";
            this.lbl_QLCT7_MaCN.Size = new System.Drawing.Size(85, 16);
            this.lbl_QLCT7_MaCN.TabIndex = 32;
            this.lbl_QLCT7_MaCN.Text = "Mã chi nhánh";
            this.lbl_QLCT7_MaCN.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(167, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(298, 32);
            this.label8.TabIndex = 24;
            this.label8.Text = "Hiệu Suất Nhân Viên";
            // 
            // Usc_QLCT7_HSNV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_QLCT7_HSNV);
            this.Name = "Usc_QLCT7_HSNV";
            this.Size = new System.Drawing.Size(1221, 753);
            this.pnl_QLCT7_HSNV.ResumeLayout(false);
            this.pnl_QLCT7_HSNV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT7_HSNV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_QLCT7_HSNV;
        private System.Windows.Forms.DataGridView dgv_QLCT7_HSNV;
        private System.Windows.Forms.ComboBox cmb_QLCT7_selectMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_QLCT7_XacNhan;
        private System.Windows.Forms.TextBox txb_QLCT7_getMaCN;
        private System.Windows.Forms.Label lbl_QLCT7_MaCN;
        private System.Windows.Forms.Label label8;
    }
}
