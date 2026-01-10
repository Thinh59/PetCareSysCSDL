namespace PetCare
{
    partial class BH_TCKH
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_BH_TCKH = new System.Windows.Forms.Panel();
            this.btn_BH_TCKH = new System.Windows.Forms.Button();
            this.tbox_BH_TCKH = new System.Windows.Forms.TextBox();
            this.lab_BH_TCKH_MKH = new System.Windows.Forms.Label();
            this.data_BH_TCKH = new System.Windows.Forms.DataGridView();
            this.colMaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab_BH_TCKH = new System.Windows.Forms.Label();
            this.btn_BH_TCKH_Tr = new System.Windows.Forms.Button();
            this.btn_BH_TCKH_Next = new System.Windows.Forms.Button();
            this.pnl_BH_TCKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BH_TCKH)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_BH_TCKH
            // 
            this.pnl_BH_TCKH.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnl_BH_TCKH.Controls.Add(this.btn_BH_TCKH_Tr);
            this.pnl_BH_TCKH.Controls.Add(this.btn_BH_TCKH);
            this.pnl_BH_TCKH.Controls.Add(this.btn_BH_TCKH_Next);
            this.pnl_BH_TCKH.Controls.Add(this.tbox_BH_TCKH);
            this.pnl_BH_TCKH.Controls.Add(this.lab_BH_TCKH_MKH);
            this.pnl_BH_TCKH.Controls.Add(this.data_BH_TCKH);
            this.pnl_BH_TCKH.Controls.Add(this.lab_BH_TCKH);
            this.pnl_BH_TCKH.Location = new System.Drawing.Point(346, 159);
            this.pnl_BH_TCKH.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnl_BH_TCKH.Name = "pnl_BH_TCKH";
            this.pnl_BH_TCKH.Size = new System.Drawing.Size(1131, 859);
            this.pnl_BH_TCKH.TabIndex = 2;
            this.pnl_BH_TCKH.VisibleChanged += new System.EventHandler(this.TraCuuKhachHang_VisibleChanged);
            this.pnl_BH_TCKH.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_BH_TCKH_Paint);
            // 
            // btn_BH_TCKH
            // 
            this.btn_BH_TCKH.BackColor = System.Drawing.Color.PaleTurquoise;
            this.btn_BH_TCKH.Location = new System.Drawing.Point(478, 244);
            this.btn_BH_TCKH.Name = "btn_BH_TCKH";
            this.btn_BH_TCKH.Size = new System.Drawing.Size(150, 53);
            this.btn_BH_TCKH.TabIndex = 9;
            this.btn_BH_TCKH.Text = "Tìm Kiếm";
            this.btn_BH_TCKH.UseVisualStyleBackColor = false;
            this.btn_BH_TCKH.Click += new System.EventHandler(this.btn_BH_TCKH_Click);
            // 
            // tbox_BH_TCKH
            // 
            this.tbox_BH_TCKH.Location = new System.Drawing.Point(534, 172);
            this.tbox_BH_TCKH.Name = "tbox_BH_TCKH";
            this.tbox_BH_TCKH.Size = new System.Drawing.Size(270, 31);
            this.tbox_BH_TCKH.TabIndex = 4;
            this.tbox_BH_TCKH.TextChanged += new System.EventHandler(this.tbox_BH_TCKH_TextChanged);
            // 
            // lab_BH_TCKH_MKH
            // 
            this.lab_BH_TCKH_MKH.AutoSize = true;
            this.lab_BH_TCKH_MKH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lab_BH_TCKH_MKH.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BH_TCKH_MKH.Location = new System.Drawing.Point(273, 167);
            this.lab_BH_TCKH_MKH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_BH_TCKH_MKH.Name = "lab_BH_TCKH_MKH";
            this.lab_BH_TCKH_MKH.Size = new System.Drawing.Size(258, 38);
            this.lab_BH_TCKH_MKH.TabIndex = 3;
            this.lab_BH_TCKH_MKH.Text = "Mã Khách Hàng:";
            // 
            // data_BH_TCKH
            // 
            this.data_BH_TCKH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.data_BH_TCKH.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.data_BH_TCKH.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.data_BH_TCKH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSalmon;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data_BH_TCKH.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.data_BH_TCKH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_BH_TCKH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaKH,
            this.colTenKH,
            this.colSDT,
            this.colLoaiKH});
            this.data_BH_TCKH.EnableHeadersVisualStyles = false;
            this.data_BH_TCKH.Location = new System.Drawing.Point(92, 339);
            this.data_BH_TCKH.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.data_BH_TCKH.Name = "data_BH_TCKH";
            this.data_BH_TCKH.RowHeadersWidth = 51;
            this.data_BH_TCKH.RowTemplate.Height = 24;
            this.data_BH_TCKH.Size = new System.Drawing.Size(936, 441);
            this.data_BH_TCKH.TabIndex = 2;
            this.data_BH_TCKH.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_BH_TCKH_CellContentClick);
            // 
            // colMaKH
            // 
            this.colMaKH.DataPropertyName = "MaKH";
            this.colMaKH.HeaderText = "Mã KH";
            this.colMaKH.MinimumWidth = 6;
            this.colMaKH.Name = "colMaKH";
            // 
            // colTenKH
            // 
            this.colTenKH.DataPropertyName = "HoTen_KH";
            this.colTenKH.HeaderText = "Tên KH";
            this.colTenKH.MinimumWidth = 6;
            this.colTenKH.Name = "colTenKH";
            // 
            // colSDT
            // 
            this.colSDT.DataPropertyName = "SDT_KH";
            this.colSDT.HeaderText = "SĐT";
            this.colSDT.MinimumWidth = 6;
            this.colSDT.Name = "colSDT";
            // 
            // colLoaiKH
            // 
            this.colLoaiKH.DataPropertyName = "Loai_KH";
            this.colLoaiKH.HeaderText = "Loại KH";
            this.colLoaiKH.MinimumWidth = 6;
            this.colLoaiKH.Name = "colLoaiKH";
            // 
            // lab_BH_TCKH
            // 
            this.lab_BH_TCKH.AutoSize = true;
            this.lab_BH_TCKH.Font = new System.Drawing.Font("Tahoma", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_BH_TCKH.Location = new System.Drawing.Point(261, 70);
            this.lab_BH_TCKH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lab_BH_TCKH.Name = "lab_BH_TCKH";
            this.lab_BH_TCKH.Size = new System.Drawing.Size(648, 72);
            this.lab_BH_TCKH.TabIndex = 0;
            this.lab_BH_TCKH.Text = "Tra Cứu Khách Hàng";
            // 
            // btn_BH_TCKH_Tr
            // 
            this.btn_BH_TCKH_Tr.BackColor = System.Drawing.Color.PowderBlue;
            this.btn_BH_TCKH_Tr.Location = new System.Drawing.Point(427, 799);
            this.btn_BH_TCKH_Tr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_BH_TCKH_Tr.Name = "btn_BH_TCKH_Tr";
            this.btn_BH_TCKH_Tr.Size = new System.Drawing.Size(119, 42);
            this.btn_BH_TCKH_Tr.TabIndex = 48;
            this.btn_BH_TCKH_Tr.Text = "Trước";
            this.btn_BH_TCKH_Tr.UseVisualStyleBackColor = false;
            this.btn_BH_TCKH_Tr.Click += new System.EventHandler(this.btn_BH_TCKH_Tr_Click);
            // 
            // btn_BH_TCKH_Next
            // 
            this.btn_BH_TCKH_Next.BackColor = System.Drawing.Color.PowderBlue;
            this.btn_BH_TCKH_Next.Location = new System.Drawing.Point(597, 799);
            this.btn_BH_TCKH_Next.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_BH_TCKH_Next.Name = "btn_BH_TCKH_Next";
            this.btn_BH_TCKH_Next.Size = new System.Drawing.Size(119, 42);
            this.btn_BH_TCKH_Next.TabIndex = 47;
            this.btn_BH_TCKH_Next.Text = "Sau";
            this.btn_BH_TCKH_Next.UseVisualStyleBackColor = false;
            this.btn_BH_TCKH_Next.Click += new System.EventHandler(this.btn_BH_TCKH_Next_Click);
            // 
            // BH_TCKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_BH_TCKH);
            this.Name = "BH_TCKH";
            this.Size = new System.Drawing.Size(1822, 1177);
            this.Load += new System.EventHandler(this.BH_TCKH_Load);
            this.pnl_BH_TCKH.ResumeLayout(false);
            this.pnl_BH_TCKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_BH_TCKH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_BH_TCKH;
        private System.Windows.Forms.TextBox tbox_BH_TCKH;
        private System.Windows.Forms.Label lab_BH_TCKH_MKH;
        private System.Windows.Forms.DataGridView data_BH_TCKH;
        private System.Windows.Forms.Label lab_BH_TCKH;
        private System.Windows.Forms.Button btn_BH_TCKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiKH;
        private System.Windows.Forms.Button btn_BH_TCKH_Tr;
        private System.Windows.Forms.Button btn_BH_TCKH_Next;
    }
}
