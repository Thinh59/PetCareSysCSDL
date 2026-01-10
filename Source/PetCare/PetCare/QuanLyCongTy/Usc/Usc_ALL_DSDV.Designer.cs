namespace PetCare
{
    partial class Usc_ALL_DSDV
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
            this.cmb_ALL_DSDV_selectScope = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_QLCT1_ChiNhanh = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_ALL_DSDV_selectBranch = new System.Windows.Forms.ComboBox();
            this.btn_ALL_DSDV_XacNhan = new System.Windows.Forms.Button();
            this.dgv_ALL_DSDV = new System.Windows.Forms.DataGridView();
            this.pnl_ALL_DSDV = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ALL_DSDV)).BeginInit();
            this.pnl_ALL_DSDV.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_ALL_DSDV_selectScope
            // 
            this.cmb_ALL_DSDV_selectScope.FormattingEnabled = true;
            this.cmb_ALL_DSDV_selectScope.Items.AddRange(new object[] {
            "Công ty",
            "Chi nhánh"});
            this.cmb_ALL_DSDV_selectScope.Location = new System.Drawing.Point(174, 91);
            this.cmb_ALL_DSDV_selectScope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ALL_DSDV_selectScope.Name = "cmb_ALL_DSDV_selectScope";
            this.cmb_ALL_DSDV_selectScope.Size = new System.Drawing.Size(166, 24);
            this.cmb_ALL_DSDV_selectScope.TabIndex = 13;
            this.cmb_ALL_DSDV_selectScope.SelectedIndexChanged += new System.EventHandler(this.cmb_ALL_DSDV_selectScope_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(89, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Lọc phạm vi";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // lbl_QLCT1_ChiNhanh
            // 
            this.lbl_QLCT1_ChiNhanh.AutoSize = true;
            this.lbl_QLCT1_ChiNhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_QLCT1_ChiNhanh.Location = new System.Drawing.Point(456, 94);
            this.lbl_QLCT1_ChiNhanh.Name = "lbl_QLCT1_ChiNhanh";
            this.lbl_QLCT1_ChiNhanh.Size = new System.Drawing.Size(68, 16);
            this.lbl_QLCT1_ChiNhanh.TabIndex = 22;
            this.lbl_QLCT1_ChiNhanh.Text = "Chi nhánh:";
            this.lbl_QLCT1_ChiNhanh.Visible = false;
            this.lbl_QLCT1_ChiNhanh.Click += new System.EventHandler(this.lbl_QLCT1_ChiNhanh_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(332, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(236, 32);
            this.label8.TabIndex = 23;
            this.label8.Text = "Tra Cứu Dịch Vụ";
            // 
            // cmb_ALL_DSDV_selectBranch
            // 
            this.cmb_ALL_DSDV_selectBranch.FormattingEnabled = true;
            this.cmb_ALL_DSDV_selectBranch.Location = new System.Drawing.Point(530, 91);
            this.cmb_ALL_DSDV_selectBranch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_ALL_DSDV_selectBranch.Name = "cmb_ALL_DSDV_selectBranch";
            this.cmb_ALL_DSDV_selectBranch.Size = new System.Drawing.Size(166, 24);
            this.cmb_ALL_DSDV_selectBranch.TabIndex = 24;
            this.cmb_ALL_DSDV_selectBranch.Visible = false;
            this.cmb_ALL_DSDV_selectBranch.SelectedIndexChanged += new System.EventHandler(this.cmb_ALL_DSDV_selectBranch_SelectedIndexChanged);
            // 
            // btn_ALL_DSDV_XacNhan
            // 
            this.btn_ALL_DSDV_XacNhan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_ALL_DSDV_XacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ALL_DSDV_XacNhan.Location = new System.Drawing.Point(400, 160);
            this.btn_ALL_DSDV_XacNhan.Name = "btn_ALL_DSDV_XacNhan";
            this.btn_ALL_DSDV_XacNhan.Size = new System.Drawing.Size(124, 26);
            this.btn_ALL_DSDV_XacNhan.TabIndex = 25;
            this.btn_ALL_DSDV_XacNhan.Text = "Xác nhận";
            this.btn_ALL_DSDV_XacNhan.UseVisualStyleBackColor = false;
            this.btn_ALL_DSDV_XacNhan.Click += new System.EventHandler(this.btn_ALL_DSDV_XacNhan_Click);
            // 
            // dgv_ALL_DSDV
            // 
            this.dgv_ALL_DSDV.AllowUserToAddRows = false;
            this.dgv_ALL_DSDV.AllowUserToDeleteRows = false;
            this.dgv_ALL_DSDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ALL_DSDV.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv_ALL_DSDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ALL_DSDV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_ALL_DSDV.Location = new System.Drawing.Point(37, 229);
            this.dgv_ALL_DSDV.Name = "dgv_ALL_DSDV";
            this.dgv_ALL_DSDV.ReadOnly = true;
            this.dgv_ALL_DSDV.RowHeadersWidth = 51;
            this.dgv_ALL_DSDV.RowTemplate.Height = 24;
            this.dgv_ALL_DSDV.Size = new System.Drawing.Size(854, 410);
            this.dgv_ALL_DSDV.TabIndex = 26;
            // 
            // pnl_ALL_DSDV
            // 
            this.pnl_ALL_DSDV.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_ALL_DSDV.Controls.Add(this.dgv_ALL_DSDV);
            this.pnl_ALL_DSDV.Controls.Add(this.btn_ALL_DSDV_XacNhan);
            this.pnl_ALL_DSDV.Controls.Add(this.cmb_ALL_DSDV_selectBranch);
            this.pnl_ALL_DSDV.Controls.Add(this.label8);
            this.pnl_ALL_DSDV.Controls.Add(this.lbl_QLCT1_ChiNhanh);
            this.pnl_ALL_DSDV.Controls.Add(this.label6);
            this.pnl_ALL_DSDV.Controls.Add(this.cmb_ALL_DSDV_selectScope);
            this.pnl_ALL_DSDV.Location = new System.Drawing.Point(150, 47);
            this.pnl_ALL_DSDV.Name = "pnl_ALL_DSDV";
            this.pnl_ALL_DSDV.Size = new System.Drawing.Size(921, 658);
            this.pnl_ALL_DSDV.TabIndex = 17;
            // 
            // Usc_ALL_DSDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.Controls.Add(this.pnl_ALL_DSDV);
            this.Name = "Usc_ALL_DSDV";
            this.Size = new System.Drawing.Size(1221, 753);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ALL_DSDV)).EndInit();
            this.pnl_ALL_DSDV.ResumeLayout(false);
            this.pnl_ALL_DSDV.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_ALL_DSDV_selectScope;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_QLCT1_ChiNhanh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_ALL_DSDV_selectBranch;
        private System.Windows.Forms.Button btn_ALL_DSDV_XacNhan;
        private System.Windows.Forms.DataGridView dgv_ALL_DSDV;
        private System.Windows.Forms.Panel pnl_ALL_DSDV;
    }
}
