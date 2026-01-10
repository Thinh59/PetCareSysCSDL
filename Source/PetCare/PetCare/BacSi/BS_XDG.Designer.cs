namespace PetCare
{
    partial class BS_XDG
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
            this.pnl_BS_TTT = new System.Windows.Forms.Panel();
            this.txb_BS_Time = new System.Windows.Forms.TextBox();
            this.btn_BS_XDG_Tr = new System.Windows.Forms.Button();
            this.lab_ALL_TDN = new System.Windows.Forms.Label();
            this.btn_BS_XDG_Next = new System.Windows.Forms.Button();
            this.data_BS_XDG = new System.Windows.Forms.DataGridView();
            this.BS_XDG_MaDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_XDG_MKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_XDG_DDV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_XDG_DNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_XDG_HL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BS_XDG_BL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab_BS_XDG = new System.Windows.Forms.Label();
            this.pnl_BS_XDG_L = new System.Windows.Forms.Panel();
            this.combo_BS_XDG = new System.Windows.Forms.ComboBox();
            this.tbox_BS_XDG_TC = new System.Windows.Forms.TextBox();
            this.lab_BS_XDG_TC = new System.Windows.Forms.Label();
            this.btn_BS_XDG = new System.Windows.Forms.Button();
            this.lab_BS_XDG_DV = new System.Windows.Forms.Label();
            this.pnl_BS_TTT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BS_XDG)).BeginInit();
            this.pnl_BS_XDG_L.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_BS_TTT
            // 
            this.pnl_BS_TTT.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_BS_TTT.Controls.Add(this.txb_BS_Time);
            this.pnl_BS_TTT.Controls.Add(this.btn_BS_XDG_Tr);
            this.pnl_BS_TTT.Controls.Add(this.lab_ALL_TDN);
            this.pnl_BS_TTT.Controls.Add(this.btn_BS_XDG_Next);
            this.pnl_BS_TTT.Controls.Add(this.data_BS_XDG);
            this.pnl_BS_TTT.Controls.Add(this.lab_BS_XDG);
            this.pnl_BS_TTT.Controls.Add(this.pnl_BS_XDG_L);
            this.pnl_BS_TTT.Location = new System.Drawing.Point(189, 0);
            this.pnl_BS_TTT.Name = "pnl_BS_TTT";
            this.pnl_BS_TTT.Size = new System.Drawing.Size(837, 753);
            this.pnl_BS_TTT.TabIndex = 26;
            // 
            // txb_BS_Time
            // 
            this.txb_BS_Time.Location = new System.Drawing.Point(709, 62);
            this.txb_BS_Time.Multiline = true;
            this.txb_BS_Time.Name = "txb_BS_Time";
            this.txb_BS_Time.Size = new System.Drawing.Size(94, 29);
            this.txb_BS_Time.TabIndex = 66;
            this.txb_BS_Time.TextChanged += new System.EventHandler(this.txb_BS_Time_TextChanged);
            // 
            // btn_BS_XDG_Tr
            // 
            this.btn_BS_XDG_Tr.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_BS_XDG_Tr.Location = new System.Drawing.Point(331, 712);
            this.btn_BS_XDG_Tr.Name = "btn_BS_XDG_Tr";
            this.btn_BS_XDG_Tr.Size = new System.Drawing.Size(79, 27);
            this.btn_BS_XDG_Tr.TabIndex = 44;
            this.btn_BS_XDG_Tr.Text = "Trước";
            this.btn_BS_XDG_Tr.UseVisualStyleBackColor = false;
            this.btn_BS_XDG_Tr.Click += new System.EventHandler(this.btn_BS_XDG_Tr_Click);
            // 
            // lab_ALL_TDN
            // 
            this.lab_ALL_TDN.AutoSize = true;
            this.lab_ALL_TDN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_ALL_TDN.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_ALL_TDN.Location = new System.Drawing.Point(709, 31);
            this.lab_ALL_TDN.Name = "lab_ALL_TDN";
            this.lab_ALL_TDN.Size = new System.Drawing.Size(67, 25);
            this.lab_ALL_TDN.TabIndex = 65;
            this.lab_ALL_TDN.Text = "Time: ";
            // 
            // btn_BS_XDG_Next
            // 
            this.btn_BS_XDG_Next.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_BS_XDG_Next.Location = new System.Drawing.Point(444, 712);
            this.btn_BS_XDG_Next.Name = "btn_BS_XDG_Next";
            this.btn_BS_XDG_Next.Size = new System.Drawing.Size(79, 27);
            this.btn_BS_XDG_Next.TabIndex = 43;
            this.btn_BS_XDG_Next.Text = "Sau";
            this.btn_BS_XDG_Next.UseVisualStyleBackColor = false;
            this.btn_BS_XDG_Next.Click += new System.EventHandler(this.btn_BS_XDG_Next_Click);
            // 
            // data_BS_XDG
            // 
            this.data_BS_XDG.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.data_BS_XDG.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.data_BS_XDG.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.data_BS_XDG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkSalmon;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data_BS_XDG.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.data_BS_XDG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_BS_XDG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BS_XDG_MaDV,
            this.BS_XDG_MKH,
            this.BS_XDG_DDV,
            this.BS_XDG_DNV,
            this.BS_XDG_HL,
            this.BS_XDG_BL});
            this.data_BS_XDG.EnableHeadersVisualStyles = false;
            this.data_BS_XDG.Location = new System.Drawing.Point(64, 323);
            this.data_BS_XDG.Name = "data_BS_XDG";
            this.data_BS_XDG.RowHeadersWidth = 51;
            this.data_BS_XDG.RowTemplate.Height = 24;
            this.data_BS_XDG.Size = new System.Drawing.Size(712, 376);
            this.data_BS_XDG.TabIndex = 35;
            this.data_BS_XDG.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_BS_XDG_CellContentClick);
            // 
            // BS_XDG_MaDV
            // 
            this.BS_XDG_MaDV.DataPropertyName = "Mã Dịch Vụ";
            this.BS_XDG_MaDV.HeaderText = "Mã Dịch Vụ";
            this.BS_XDG_MaDV.MinimumWidth = 6;
            this.BS_XDG_MaDV.Name = "BS_XDG_MaDV";
            // 
            // BS_XDG_MKH
            // 
            this.BS_XDG_MKH.DataPropertyName = "Mã Khách Hàng";
            this.BS_XDG_MKH.HeaderText = "Mã Khách Hàng";
            this.BS_XDG_MKH.MinimumWidth = 6;
            this.BS_XDG_MKH.Name = "BS_XDG_MKH";
            // 
            // BS_XDG_DDV
            // 
            this.BS_XDG_DDV.DataPropertyName = "Điểm Dịch Vụ";
            this.BS_XDG_DDV.HeaderText = "Điểm Dịch Vụ";
            this.BS_XDG_DDV.MinimumWidth = 6;
            this.BS_XDG_DDV.Name = "BS_XDG_DDV";
            // 
            // BS_XDG_DNV
            // 
            this.BS_XDG_DNV.DataPropertyName = "Điểm Nhân Viên";
            this.BS_XDG_DNV.HeaderText = "Điểm Nhân Viên";
            this.BS_XDG_DNV.MinimumWidth = 6;
            this.BS_XDG_DNV.Name = "BS_XDG_DNV";
            // 
            // BS_XDG_HL
            // 
            this.BS_XDG_HL.DataPropertyName = "Hài Lòng";
            this.BS_XDG_HL.HeaderText = "Hài Lòng";
            this.BS_XDG_HL.MinimumWidth = 6;
            this.BS_XDG_HL.Name = "BS_XDG_HL";
            // 
            // BS_XDG_BL
            // 
            this.BS_XDG_BL.DataPropertyName = "Bình Luận";
            this.BS_XDG_BL.HeaderText = "Bình Luận";
            this.BS_XDG_BL.MinimumWidth = 6;
            this.BS_XDG_BL.Name = "BS_XDG_BL";
            // 
            // lab_BS_XDG
            // 
            this.lab_BS_XDG.AutoSize = true;
            this.lab_BS_XDG.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BS_XDG.Location = new System.Drawing.Point(313, 31);
            this.lab_BS_XDG.Name = "lab_BS_XDG";
            this.lab_BS_XDG.Size = new System.Drawing.Size(284, 45);
            this.lab_BS_XDG.TabIndex = 0;
            this.lab_BS_XDG.Text = "Xem Đánh Giá";
            this.lab_BS_XDG.Click += new System.EventHandler(this.lab_BS_TCVC_Click);
            // 
            // pnl_BS_XDG_L
            // 
            this.pnl_BS_XDG_L.BackColor = System.Drawing.Color.PeachPuff;
            this.pnl_BS_XDG_L.Controls.Add(this.combo_BS_XDG);
            this.pnl_BS_XDG_L.Controls.Add(this.tbox_BS_XDG_TC);
            this.pnl_BS_XDG_L.Controls.Add(this.lab_BS_XDG_TC);
            this.pnl_BS_XDG_L.Controls.Add(this.btn_BS_XDG);
            this.pnl_BS_XDG_L.Controls.Add(this.lab_BS_XDG_DV);
            this.pnl_BS_XDG_L.Location = new System.Drawing.Point(156, 94);
            this.pnl_BS_XDG_L.Name = "pnl_BS_XDG_L";
            this.pnl_BS_XDG_L.Size = new System.Drawing.Size(523, 214);
            this.pnl_BS_XDG_L.TabIndex = 36;
            this.pnl_BS_XDG_L.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_BS_XDG_L_Paint);
            // 
            // combo_BS_XDG
            // 
            this.combo_BS_XDG.FormattingEnabled = true;
            this.combo_BS_XDG.Items.AddRange(new object[] {
            "---Tất cả---",
            "Khám Bệnh",
            "Tiêm Phòng"});
            this.combo_BS_XDG.Location = new System.Drawing.Point(192, 94);
            this.combo_BS_XDG.Margin = new System.Windows.Forms.Padding(2);
            this.combo_BS_XDG.Name = "combo_BS_XDG";
            this.combo_BS_XDG.Size = new System.Drawing.Size(155, 24);
            this.combo_BS_XDG.TabIndex = 37;
            this.combo_BS_XDG.SelectedIndexChanged += new System.EventHandler(this.combo_BS_XDG_SelectedIndexChanged);
            // 
            // tbox_BS_XDG_TC
            // 
            this.tbox_BS_XDG_TC.Location = new System.Drawing.Point(192, 48);
            this.tbox_BS_XDG_TC.Margin = new System.Windows.Forms.Padding(2);
            this.tbox_BS_XDG_TC.Name = "tbox_BS_XDG_TC";
            this.tbox_BS_XDG_TC.Size = new System.Drawing.Size(147, 22);
            this.tbox_BS_XDG_TC.TabIndex = 36;
            this.tbox_BS_XDG_TC.TextChanged += new System.EventHandler(this.tbox_BS_XDG_TC_TextChanged);
            // 
            // lab_BS_XDG_TC
            // 
            this.lab_BS_XDG_TC.AutoSize = true;
            this.lab_BS_XDG_TC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_BS_XDG_TC.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BS_XDG_TC.Location = new System.Drawing.Point(26, 45);
            this.lab_BS_XDG_TC.Name = "lab_BS_XDG_TC";
            this.lab_BS_XDG_TC.Size = new System.Drawing.Size(122, 25);
            this.lab_BS_XDG_TC.TabIndex = 35;
            this.lab_BS_XDG_TC.Text = "Khách Hàng:";
            // 
            // btn_BS_XDG
            // 
            this.btn_BS_XDG.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_BS_XDG.Location = new System.Drawing.Point(214, 149);
            this.btn_BS_XDG.Name = "btn_BS_XDG";
            this.btn_BS_XDG.Size = new System.Drawing.Size(92, 37);
            this.btn_BS_XDG.TabIndex = 32;
            this.btn_BS_XDG.Text = "Tìm Kiếm";
            this.btn_BS_XDG.UseVisualStyleBackColor = false;
            this.btn_BS_XDG.Click += new System.EventHandler(this.btn_BS_XDG_Click);
            // 
            // lab_BS_XDG_DV
            // 
            this.lab_BS_XDG_DV.AutoSize = true;
            this.lab_BS_XDG_DV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_BS_XDG_DV.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BS_XDG_DV.Location = new System.Drawing.Point(26, 97);
            this.lab_BS_XDG_DV.Name = "lab_BS_XDG_DV";
            this.lab_BS_XDG_DV.Size = new System.Drawing.Size(86, 25);
            this.lab_BS_XDG_DV.TabIndex = 33;
            this.lab_BS_XDG_DV.Text = "Dịch Vụ:";
            // 
            // BS_XDG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_BS_TTT);
            this.Name = "BS_XDG";
            this.Size = new System.Drawing.Size(1215, 753);
            this.Load += new System.EventHandler(this.BS_XDG_Load);
            this.pnl_BS_TTT.ResumeLayout(false);
            this.pnl_BS_TTT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BS_XDG)).EndInit();
            this.pnl_BS_XDG_L.ResumeLayout(false);
            this.pnl_BS_XDG_L.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_BS_TTT;
        private System.Windows.Forms.DataGridView data_BS_XDG;
        private System.Windows.Forms.Label lab_BS_XDG;
        private System.Windows.Forms.Panel pnl_BS_XDG_L;
        private System.Windows.Forms.Button btn_BS_XDG;
        private System.Windows.Forms.Label lab_BS_XDG_DV;
        private System.Windows.Forms.TextBox tbox_BS_XDG_TC;
        private System.Windows.Forms.Label lab_BS_XDG_TC;
        private System.Windows.Forms.ComboBox combo_BS_XDG;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_XDG_MaDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_XDG_MKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_XDG_DDV;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_XDG_DNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_XDG_HL;
        private System.Windows.Forms.DataGridViewTextBoxColumn BS_XDG_BL;
        private System.Windows.Forms.Button btn_BS_XDG_Tr;
        private System.Windows.Forms.Button btn_BS_XDG_Next;
        private System.Windows.Forms.TextBox txb_BS_Time;
        private System.Windows.Forms.Label lab_ALL_TDN;
    }
}
