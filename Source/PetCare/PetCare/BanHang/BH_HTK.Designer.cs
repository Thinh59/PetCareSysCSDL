namespace PetCare
{
    partial class BH_HTK
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
            this.pnl_BH_QLSP = new System.Windows.Forms.Panel();
            this.lab_BH_HTK_Loai = new System.Windows.Forms.Label();
            this.cbBox_BH_Loai = new System.Windows.Forms.ComboBox();
            this.lab_BH_HTK_TC = new System.Windows.Forms.Label();
            this.data_BH_HTK = new System.Windows.Forms.DataGridView();
            this.BH_QLSP_MaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BH_QLSP_TenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BH_QLSP_LoaiSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BH_QLSP_GB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BH_HTK_SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab_BH_HTK_DSSP = new System.Windows.Forms.Label();
            this.lab_BH_QLSP_1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.pnl_BH_QLSP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BH_HTK)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_BH_QLSP
            // 
            this.pnl_BH_QLSP.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_BH_QLSP.Controls.Add(this.lab_BH_HTK_Loai);
            this.pnl_BH_QLSP.Controls.Add(this.cbBox_BH_Loai);
            this.pnl_BH_QLSP.Controls.Add(this.lab_BH_HTK_TC);
            this.pnl_BH_QLSP.Controls.Add(this.data_BH_HTK);
            this.pnl_BH_QLSP.Controls.Add(this.lab_BH_HTK_DSSP);
            this.pnl_BH_QLSP.Controls.Add(this.lab_BH_QLSP_1);
            this.pnl_BH_QLSP.Location = new System.Drawing.Point(346, 159);
            this.pnl_BH_QLSP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnl_BH_QLSP.Name = "pnl_BH_QLSP";
            this.pnl_BH_QLSP.Size = new System.Drawing.Size(1131, 859);
            this.pnl_BH_QLSP.TabIndex = 1;
            // 
            // lab_BH_HTK_Loai
            // 
            this.lab_BH_HTK_Loai.AutoSize = true;
            this.lab_BH_HTK_Loai.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_BH_HTK_Loai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BH_HTK_Loai.Location = new System.Drawing.Point(100, 186);
            this.lab_BH_HTK_Loai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_BH_HTK_Loai.Name = "lab_BH_HTK_Loai";
            this.lab_BH_HTK_Loai.Size = new System.Drawing.Size(57, 25);
            this.lab_BH_HTK_Loai.TabIndex = 5;
            this.lab_BH_HTK_Loai.Text = "Loại:";
            this.lab_BH_HTK_Loai.Click += new System.EventHandler(this.label2_Click);
            // 
            // cbBox_BH_Loai
            // 
            this.cbBox_BH_Loai.FormattingEnabled = true;
            this.cbBox_BH_Loai.Items.AddRange(new object[] {
            "---Tất Cả---",
            "Thức Ăn",
            "Phụ Kiện",
            "Thuốc"});
            this.cbBox_BH_Loai.Location = new System.Drawing.Point(198, 186);
            this.cbBox_BH_Loai.Name = "cbBox_BH_Loai";
            this.cbBox_BH_Loai.Size = new System.Drawing.Size(170, 33);
            this.cbBox_BH_Loai.TabIndex = 4;
            this.cbBox_BH_Loai.SelectedIndexChanged += new System.EventHandler(this.cbBox_BH_Loai_SelectedIndexChanged);
            // 
            // lab_BH_HTK_TC
            // 
            this.lab_BH_HTK_TC.AutoSize = true;
            this.lab_BH_HTK_TC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_BH_HTK_TC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BH_HTK_TC.Location = new System.Drawing.Point(100, 136);
            this.lab_BH_HTK_TC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_BH_HTK_TC.Name = "lab_BH_HTK_TC";
            this.lab_BH_HTK_TC.Size = new System.Drawing.Size(83, 25);
            this.lab_BH_HTK_TC.TabIndex = 3;
            this.lab_BH_HTK_TC.Text = "Tra Cứu";
            // 
            // data_BH_HTK
            // 
            this.data_BH_HTK.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.data_BH_HTK.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.data_BH_HTK.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.data_BH_HTK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkSalmon;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data_BH_HTK.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.data_BH_HTK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_BH_HTK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BH_QLSP_MaSP,
            this.BH_QLSP_TenSP,
            this.BH_QLSP_LoaiSP,
            this.BH_QLSP_GB,
            this.BH_HTK_SL});
            this.data_BH_HTK.EnableHeadersVisualStyles = false;
            this.data_BH_HTK.Location = new System.Drawing.Point(100, 320);
            this.data_BH_HTK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.data_BH_HTK.Name = "data_BH_HTK";
            this.data_BH_HTK.RowHeadersWidth = 51;
            this.data_BH_HTK.RowTemplate.Height = 24;
            this.data_BH_HTK.Size = new System.Drawing.Size(936, 441);
            this.data_BH_HTK.TabIndex = 2;
            this.data_BH_HTK.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_BH_HTK_CellContentClick);
            // 
            // BH_QLSP_MaSP
            // 
            this.BH_QLSP_MaSP.DataPropertyName = "Mã SP";
            this.BH_QLSP_MaSP.HeaderText = "Mã SP";
            this.BH_QLSP_MaSP.MinimumWidth = 6;
            this.BH_QLSP_MaSP.Name = "BH_QLSP_MaSP";
            // 
            // BH_QLSP_TenSP
            // 
            this.BH_QLSP_TenSP.DataPropertyName = "Tên SP";
            this.BH_QLSP_TenSP.HeaderText = "Tên SP";
            this.BH_QLSP_TenSP.MinimumWidth = 6;
            this.BH_QLSP_TenSP.Name = "BH_QLSP_TenSP";
            // 
            // BH_QLSP_LoaiSP
            // 
            this.BH_QLSP_LoaiSP.DataPropertyName = "Loại";
            this.BH_QLSP_LoaiSP.HeaderText = "Loại";
            this.BH_QLSP_LoaiSP.MinimumWidth = 6;
            this.BH_QLSP_LoaiSP.Name = "BH_QLSP_LoaiSP";
            // 
            // BH_QLSP_GB
            // 
            this.BH_QLSP_GB.DataPropertyName = "Giá Bán";
            this.BH_QLSP_GB.HeaderText = "Giá Bán";
            this.BH_QLSP_GB.MinimumWidth = 6;
            this.BH_QLSP_GB.Name = "BH_QLSP_GB";
            // 
            // BH_HTK_SL
            // 
            this.BH_HTK_SL.DataPropertyName = "Số Lượng";
            this.BH_HTK_SL.HeaderText = "Số Lượng";
            this.BH_HTK_SL.MinimumWidth = 6;
            this.BH_HTK_SL.Name = "BH_HTK_SL";
            // 
            // lab_BH_HTK_DSSP
            // 
            this.lab_BH_HTK_DSSP.AutoSize = true;
            this.lab_BH_HTK_DSSP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_BH_HTK_DSSP.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BH_HTK_DSSP.Location = new System.Drawing.Point(100, 277);
            this.lab_BH_HTK_DSSP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_BH_HTK_DSSP.Name = "lab_BH_HTK_DSSP";
            this.lab_BH_HTK_DSSP.Size = new System.Drawing.Size(189, 25);
            this.lab_BH_HTK_DSSP.TabIndex = 1;
            this.lab_BH_HTK_DSSP.Text = "Danh Sách Sản Phẩm";
            // 
            // lab_BH_QLSP_1
            // 
            this.lab_BH_QLSP_1.AutoSize = true;
            this.lab_BH_QLSP_1.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BH_QLSP_1.Location = new System.Drawing.Point(354, 75);
            this.lab_BH_QLSP_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_BH_QLSP_1.Name = "lab_BH_QLSP_1";
            this.lab_BH_QLSP_1.Size = new System.Drawing.Size(282, 45);
            this.lab_BH_QLSP_1.TabIndex = 0;
            this.lab_BH_QLSP_1.Text = "Hàng Tồn Kho";
            // 
            // BH_HTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_BH_QLSP);
            this.Name = "BH_HTK";
            this.Size = new System.Drawing.Size(1822, 1177);
            this.Load += new System.EventHandler(this.BH_HTK_Load);
            this.pnl_BH_QLSP.ResumeLayout(false);
            this.pnl_BH_QLSP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BH_HTK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_BH_QLSP;
        private System.Windows.Forms.DataGridView data_BH_HTK;
        private System.Windows.Forms.Label lab_BH_HTK_DSSP;
        private System.Windows.Forms.Label lab_BH_QLSP_1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Label lab_BH_HTK_Loai;
        private System.Windows.Forms.ComboBox cbBox_BH_Loai;
        private System.Windows.Forms.Label lab_BH_HTK_TC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BH_QLSP_MaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn BH_QLSP_TenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn BH_QLSP_LoaiSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn BH_QLSP_GB;
        private System.Windows.Forms.DataGridViewTextBoxColumn BH_HTK_SL;
    }
}
