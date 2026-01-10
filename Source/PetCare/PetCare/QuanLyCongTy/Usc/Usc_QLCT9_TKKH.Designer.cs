namespace PetCare
{
    partial class Usc_QLCT9_TKKH
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
            this.pnl_QLCT9_TKKH = new System.Windows.Forms.Panel();
            this.dgv_QLCT9_TKKH = new System.Windows.Forms.DataGridView();
            this.cmb_QLCT9_selectMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_QLCT9_XacNhan = new System.Windows.Forms.Button();
            this.txb_QLCT9_getMaCN = new System.Windows.Forms.TextBox();
            this.lbl_QLCT9_MaCN = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnl_QLCT9_TKKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT9_TKKH)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_QLCT9_TKKH
            // 
            this.pnl_QLCT9_TKKH.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_QLCT9_TKKH.Controls.Add(this.dgv_QLCT9_TKKH);
            this.pnl_QLCT9_TKKH.Controls.Add(this.cmb_QLCT9_selectMode);
            this.pnl_QLCT9_TKKH.Controls.Add(this.label1);
            this.pnl_QLCT9_TKKH.Controls.Add(this.btn_QLCT9_XacNhan);
            this.pnl_QLCT9_TKKH.Controls.Add(this.txb_QLCT9_getMaCN);
            this.pnl_QLCT9_TKKH.Controls.Add(this.lbl_QLCT9_MaCN);
            this.pnl_QLCT9_TKKH.Controls.Add(this.label8);
            this.pnl_QLCT9_TKKH.Location = new System.Drawing.Point(194, 111);
            this.pnl_QLCT9_TKKH.Name = "pnl_QLCT9_TKKH";
            this.pnl_QLCT9_TKKH.Size = new System.Drawing.Size(792, 530);
            this.pnl_QLCT9_TKKH.TabIndex = 1;
            this.pnl_QLCT9_TKKH.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_QLCT9_TKKH_Paint);
            // 
            // dgv_QLCT9_TKKH
            // 
            this.dgv_QLCT9_TKKH.AllowUserToAddRows = false;
            this.dgv_QLCT9_TKKH.AllowUserToDeleteRows = false;
            this.dgv_QLCT9_TKKH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_QLCT9_TKKH.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv_QLCT9_TKKH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_QLCT9_TKKH.Location = new System.Drawing.Point(0, 238);
            this.dgv_QLCT9_TKKH.Name = "dgv_QLCT9_TKKH";
            this.dgv_QLCT9_TKKH.ReadOnly = true;
            this.dgv_QLCT9_TKKH.RowHeadersWidth = 51;
            this.dgv_QLCT9_TKKH.RowTemplate.Height = 24;
            this.dgv_QLCT9_TKKH.Size = new System.Drawing.Size(792, 292);
            this.dgv_QLCT9_TKKH.TabIndex = 37;
            this.dgv_QLCT9_TKKH.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_QLCT9_TKKH_CellContentClick);
            // 
            // cmb_QLCT9_selectMode
            // 
            this.cmb_QLCT9_selectMode.FormattingEnabled = true;
            this.cmb_QLCT9_selectMode.Items.AddRange(new object[] {
            "Công ty",
            "Chi nhánh"});
            this.cmb_QLCT9_selectMode.Location = new System.Drawing.Point(356, 106);
            this.cmb_QLCT9_selectMode.Name = "cmb_QLCT9_selectMode";
            this.cmb_QLCT9_selectMode.Size = new System.Drawing.Size(121, 24);
            this.cmb_QLCT9_selectMode.TabIndex = 36;
            this.cmb_QLCT9_selectMode.SelectedIndexChanged += new System.EventHandler(this.cmb_QLCT9_selectMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Xem của";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_QLCT9_XacNhan
            // 
            this.btn_QLCT9_XacNhan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_QLCT9_XacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QLCT9_XacNhan.Location = new System.Drawing.Point(321, 190);
            this.btn_QLCT9_XacNhan.Name = "btn_QLCT9_XacNhan";
            this.btn_QLCT9_XacNhan.Size = new System.Drawing.Size(124, 26);
            this.btn_QLCT9_XacNhan.TabIndex = 34;
            this.btn_QLCT9_XacNhan.Text = "Xác Nhận";
            this.btn_QLCT9_XacNhan.UseVisualStyleBackColor = false;
            this.btn_QLCT9_XacNhan.Click += new System.EventHandler(this.btn_QLCT9_XacNhan_Click);
            // 
            // txb_QLCT9_getMaCN
            // 
            this.txb_QLCT9_getMaCN.Location = new System.Drawing.Point(377, 140);
            this.txb_QLCT9_getMaCN.Name = "txb_QLCT9_getMaCN";
            this.txb_QLCT9_getMaCN.Size = new System.Drawing.Size(100, 22);
            this.txb_QLCT9_getMaCN.TabIndex = 33;
            this.txb_QLCT9_getMaCN.Visible = false;
            this.txb_QLCT9_getMaCN.TextChanged += new System.EventHandler(this.txb_QLCT9_getMaCN_TextChanged);
            // 
            // lbl_QLCT9_MaCN
            // 
            this.lbl_QLCT9_MaCN.AutoSize = true;
            this.lbl_QLCT9_MaCN.Location = new System.Drawing.Point(286, 143);
            this.lbl_QLCT9_MaCN.Name = "lbl_QLCT9_MaCN";
            this.lbl_QLCT9_MaCN.Size = new System.Drawing.Size(85, 16);
            this.lbl_QLCT9_MaCN.TabIndex = 32;
            this.lbl_QLCT9_MaCN.Text = "Mã chi nhánh";
            this.lbl_QLCT9_MaCN.Visible = false;
            this.lbl_QLCT9_MaCN.Click += new System.EventHandler(this.lbl_QLCT9_MaCN_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(251, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(319, 32);
            this.label8.TabIndex = 24;
            this.label8.Text = "Thống Kê Khách Hàng";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // Usc_QLCT9_TKKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_QLCT9_TKKH);
            this.Name = "Usc_QLCT9_TKKH";
            this.Size = new System.Drawing.Size(1221, 753);
            this.pnl_QLCT9_TKKH.ResumeLayout(false);
            this.pnl_QLCT9_TKKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT9_TKKH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_QLCT9_TKKH;
        private System.Windows.Forms.DataGridView dgv_QLCT9_TKKH;
        private System.Windows.Forms.ComboBox cmb_QLCT9_selectMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_QLCT9_XacNhan;
        private System.Windows.Forms.TextBox txb_QLCT9_getMaCN;
        private System.Windows.Forms.Label lbl_QLCT9_MaCN;
        private System.Windows.Forms.Label label8;
    }
}
