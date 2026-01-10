namespace PetCare
{
    partial class Usc_QLCT3_HSCN
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
            this.pnl_QLCT3_HSCN = new System.Windows.Forms.Panel();
            this.dgv_QLCT3_HSCN = new System.Windows.Forms.DataGridView();
            this.cmb_QLCT3_selectMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_QLCT3_XacNhan = new System.Windows.Forms.Button();
            this.txb_QLCT3_getMaCN = new System.Windows.Forms.TextBox();
            this.lbl_QLCT3_MaCN = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_QLCT3_HSCN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT3_HSCN)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_QLCT3_HSCN
            // 
            this.pnl_QLCT3_HSCN.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_QLCT3_HSCN.Controls.Add(this.dgv_QLCT3_HSCN);
            this.pnl_QLCT3_HSCN.Controls.Add(this.cmb_QLCT3_selectMode);
            this.pnl_QLCT3_HSCN.Controls.Add(this.label1);
            this.pnl_QLCT3_HSCN.Controls.Add(this.btn_QLCT3_XacNhan);
            this.pnl_QLCT3_HSCN.Controls.Add(this.txb_QLCT3_getMaCN);
            this.pnl_QLCT3_HSCN.Controls.Add(this.lbl_QLCT3_MaCN);
            this.pnl_QLCT3_HSCN.Controls.Add(this.label8);
            this.pnl_QLCT3_HSCN.Location = new System.Drawing.Point(334, 119);
            this.pnl_QLCT3_HSCN.Name = "pnl_QLCT3_HSCN";
            this.pnl_QLCT3_HSCN.Size = new System.Drawing.Size(605, 530);
            this.pnl_QLCT3_HSCN.TabIndex = 0;
            this.pnl_QLCT3_HSCN.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dgv_QLCT3_HSCN
            // 
            this.dgv_QLCT3_HSCN.AllowUserToAddRows = false;
            this.dgv_QLCT3_HSCN.AllowUserToDeleteRows = false;
            this.dgv_QLCT3_HSCN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_QLCT3_HSCN.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv_QLCT3_HSCN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_QLCT3_HSCN.Location = new System.Drawing.Point(0, 238);
            this.dgv_QLCT3_HSCN.Name = "dgv_QLCT3_HSCN";
            this.dgv_QLCT3_HSCN.ReadOnly = true;
            this.dgv_QLCT3_HSCN.RowHeadersWidth = 51;
            this.dgv_QLCT3_HSCN.RowTemplate.Height = 24;
            this.dgv_QLCT3_HSCN.Size = new System.Drawing.Size(602, 292);
            this.dgv_QLCT3_HSCN.TabIndex = 37;
            // 
            // cmb_QLCT3_selectMode
            // 
            this.cmb_QLCT3_selectMode.AutoCompleteCustomSource.AddRange(new string[] {
            "Chi nhánh",
            "Công ty"});
            this.cmb_QLCT3_selectMode.FormattingEnabled = true;
            this.cmb_QLCT3_selectMode.Items.AddRange(new object[] {
            "Công ty",
            "Chi nhánh"});
            this.cmb_QLCT3_selectMode.Location = new System.Drawing.Point(277, 113);
            this.cmb_QLCT3_selectMode.Name = "cmb_QLCT3_selectMode";
            this.cmb_QLCT3_selectMode.Size = new System.Drawing.Size(121, 24);
            this.cmb_QLCT3_selectMode.TabIndex = 36;
            this.cmb_QLCT3_selectMode.SelectedIndexChanged += new System.EventHandler(this.cmb_QLCT3_selectMode_SelectedIndexChanged);
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
            // btn_QLCT3_XacNhan
            // 
            this.btn_QLCT3_XacNhan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_QLCT3_XacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QLCT3_XacNhan.Location = new System.Drawing.Point(237, 190);
            this.btn_QLCT3_XacNhan.Name = "btn_QLCT3_XacNhan";
            this.btn_QLCT3_XacNhan.Size = new System.Drawing.Size(124, 26);
            this.btn_QLCT3_XacNhan.TabIndex = 34;
            this.btn_QLCT3_XacNhan.Text = "Xác Nhận";
            this.btn_QLCT3_XacNhan.UseVisualStyleBackColor = false;
            this.btn_QLCT3_XacNhan.Click += new System.EventHandler(this.btn_QLCT3_XacNhan_Click);
            // 
            // txb_QLCT3_getMaCN
            // 
            this.txb_QLCT3_getMaCN.Location = new System.Drawing.Point(298, 150);
            this.txb_QLCT3_getMaCN.Name = "txb_QLCT3_getMaCN";
            this.txb_QLCT3_getMaCN.Size = new System.Drawing.Size(100, 22);
            this.txb_QLCT3_getMaCN.TabIndex = 33;
            this.txb_QLCT3_getMaCN.Visible = false;
            // 
            // lbl_QLCT3_MaCN
            // 
            this.lbl_QLCT3_MaCN.AutoSize = true;
            this.lbl_QLCT3_MaCN.Location = new System.Drawing.Point(207, 153);
            this.lbl_QLCT3_MaCN.Name = "lbl_QLCT3_MaCN";
            this.lbl_QLCT3_MaCN.Size = new System.Drawing.Size(85, 16);
            this.lbl_QLCT3_MaCN.TabIndex = 32;
            this.lbl_QLCT3_MaCN.Text = "Mã chi nhánh";
            this.lbl_QLCT3_MaCN.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(167, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(299, 32);
            this.label8.TabIndex = 24;
            this.label8.Text = "Hiệu Suất Chi Nhánh";
            // 
            // Usc_QLCT3_HSCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_QLCT3_HSCN);
            this.Name = "Usc_QLCT3_HSCN";
            this.Size = new System.Drawing.Size(1221, 753);
            this.pnl_QLCT3_HSCN.ResumeLayout(false);
            this.pnl_QLCT3_HSCN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT3_HSCN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_QLCT3_HSCN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txb_QLCT3_getMaCN;
        private System.Windows.Forms.Label lbl_QLCT3_MaCN;
        private System.Windows.Forms.Button btn_QLCT3_XacNhan;
        private System.Windows.Forms.ComboBox cmb_QLCT3_selectMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_QLCT3_HSCN;
    }
}
