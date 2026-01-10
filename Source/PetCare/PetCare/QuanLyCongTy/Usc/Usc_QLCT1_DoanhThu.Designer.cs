using System.Windows.Forms;
namespace PetCare
{
    partial class Usc_QLCT1_DoanhThu
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
            this.cmb_QLCT1_selectScope = new System.Windows.Forms.ComboBox();
            this.txb_QLCT1_getQuarter = new System.Windows.Forms.TextBox();
            this.txb_QLCT1_getYear = new System.Windows.Forms.TextBox();
            this.txb_QLCT1_getMonth = new System.Windows.Forms.TextBox();
            this.txb_QLCT1_getDay = new System.Windows.Forms.TextBox();
            this.cmb_QLCT1_selectTimeMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_QTCT1 = new System.Windows.Forms.Panel();
            this.dgv_QLCT1_DoanhThu = new System.Windows.Forms.DataGridView();
            this.btn_QLCT1_XacNhan = new System.Windows.Forms.Button();
            this.cmb_QLCT1_selectBranch = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_QLCT1_ChiNhanh = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_QLCT1_Quy = new System.Windows.Forms.Label();
            this.lbl_QLCT1_Thang = new System.Windows.Forms.Label();
            this.lbl_QLCT1_Ngay = new System.Windows.Forms.Label();
            this.lbl_QLCT1_Nam = new System.Windows.Forms.Label();
            this.pnl_QTCT1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT1_DoanhThu)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_QLCT1_selectScope
            // 
            this.cmb_QLCT1_selectScope.FormattingEnabled = true;
            this.cmb_QLCT1_selectScope.Items.AddRange(new object[] {
            "Công ty",
            "Chi nhánh"});
            this.cmb_QLCT1_selectScope.Location = new System.Drawing.Point(174, 207);
            this.cmb_QLCT1_selectScope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_QLCT1_selectScope.Name = "cmb_QLCT1_selectScope";
            this.cmb_QLCT1_selectScope.Size = new System.Drawing.Size(166, 24);
            this.cmb_QLCT1_selectScope.TabIndex = 13;
            this.cmb_QLCT1_selectScope.SelectedIndexChanged += new System.EventHandler(this.cmb_QLCT1_selectScope_SelectedIndexChanged);
            // 
            // txb_QLCT1_getQuarter
            // 
            this.txb_QLCT1_getQuarter.Location = new System.Drawing.Point(138, 152);
            this.txb_QLCT1_getQuarter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_QLCT1_getQuarter.Name = "txb_QLCT1_getQuarter";
            this.txb_QLCT1_getQuarter.Size = new System.Drawing.Size(125, 22);
            this.txb_QLCT1_getQuarter.TabIndex = 12;
            this.txb_QLCT1_getQuarter.Visible = false;
            // 
            // txb_QLCT1_getYear
            // 
            this.txb_QLCT1_getYear.Location = new System.Drawing.Point(557, 156);
            this.txb_QLCT1_getYear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_QLCT1_getYear.Name = "txb_QLCT1_getYear";
            this.txb_QLCT1_getYear.Size = new System.Drawing.Size(125, 22);
            this.txb_QLCT1_getYear.TabIndex = 11;
            this.txb_QLCT1_getYear.Visible = false;
            this.txb_QLCT1_getYear.TextChanged += new System.EventHandler(this.txb_QLCT1_getYear_TextChanged);
            // 
            // txb_QLCT1_getMonth
            // 
            this.txb_QLCT1_getMonth.Location = new System.Drawing.Point(340, 153);
            this.txb_QLCT1_getMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_QLCT1_getMonth.Name = "txb_QLCT1_getMonth";
            this.txb_QLCT1_getMonth.Size = new System.Drawing.Size(125, 22);
            this.txb_QLCT1_getMonth.TabIndex = 10;
            this.txb_QLCT1_getMonth.Visible = false;
            this.txb_QLCT1_getMonth.TextChanged += new System.EventHandler(this.txb_QLCT1_getMonth_TextChanged);
            // 
            // txb_QLCT1_getDay
            // 
            this.txb_QLCT1_getDay.Location = new System.Drawing.Point(138, 153);
            this.txb_QLCT1_getDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txb_QLCT1_getDay.Name = "txb_QLCT1_getDay";
            this.txb_QLCT1_getDay.Size = new System.Drawing.Size(125, 22);
            this.txb_QLCT1_getDay.TabIndex = 9;
            this.txb_QLCT1_getDay.Visible = false;
            this.txb_QLCT1_getDay.TextChanged += new System.EventHandler(this.txb_QLCT1_getDay_TextChanged);
            // 
            // cmb_QLCT1_selectTimeMode
            // 
            this.cmb_QLCT1_selectTimeMode.FormattingEnabled = true;
            this.cmb_QLCT1_selectTimeMode.Items.AddRange(new object[] {
            "Ngày/Tháng",
            "Quý",
            "Toàn bộ"});
            this.cmb_QLCT1_selectTimeMode.Location = new System.Drawing.Point(288, 104);
            this.cmb_QLCT1_selectTimeMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_QLCT1_selectTimeMode.Name = "cmb_QLCT1_selectTimeMode";
            this.cmb_QLCT1_selectTimeMode.Size = new System.Drawing.Size(166, 24);
            this.cmb_QLCT1_selectTimeMode.TabIndex = 8;
            this.cmb_QLCT1_selectTimeMode.SelectedIndexChanged += new System.EventHandler(this.cmb_QLCT1_selectTimeMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(171, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Lọc thời gian theo";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // pnl_QTCT1
            // 
            this.pnl_QTCT1.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_QTCT1.Controls.Add(this.dgv_QLCT1_DoanhThu);
            this.pnl_QTCT1.Controls.Add(this.btn_QLCT1_XacNhan);
            this.pnl_QTCT1.Controls.Add(this.cmb_QLCT1_selectBranch);
            this.pnl_QTCT1.Controls.Add(this.label8);
            this.pnl_QTCT1.Controls.Add(this.lbl_QLCT1_ChiNhanh);
            this.pnl_QTCT1.Controls.Add(this.label6);
            this.pnl_QTCT1.Controls.Add(this.lbl_QLCT1_Quy);
            this.pnl_QTCT1.Controls.Add(this.cmb_QLCT1_selectScope);
            this.pnl_QTCT1.Controls.Add(this.lbl_QLCT1_Thang);
            this.pnl_QTCT1.Controls.Add(this.lbl_QLCT1_Ngay);
            this.pnl_QTCT1.Controls.Add(this.lbl_QLCT1_Nam);
            this.pnl_QTCT1.Controls.Add(this.txb_QLCT1_getMonth);
            this.pnl_QTCT1.Controls.Add(this.label1);
            this.pnl_QTCT1.Controls.Add(this.txb_QLCT1_getDay);
            this.pnl_QTCT1.Controls.Add(this.txb_QLCT1_getYear);
            this.pnl_QTCT1.Controls.Add(this.cmb_QLCT1_selectTimeMode);
            this.pnl_QTCT1.Controls.Add(this.txb_QLCT1_getQuarter);
            this.pnl_QTCT1.Location = new System.Drawing.Point(142, 51);
            this.pnl_QTCT1.Name = "pnl_QTCT1";
            this.pnl_QTCT1.Size = new System.Drawing.Size(921, 658);
            this.pnl_QTCT1.TabIndex = 16;
            this.pnl_QTCT1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dgv_QLCT1_DoanhThu
            // 
            this.dgv_QLCT1_DoanhThu.AllowUserToAddRows = false;
            this.dgv_QLCT1_DoanhThu.AllowUserToDeleteRows = false;
            this.dgv_QLCT1_DoanhThu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_QLCT1_DoanhThu.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgv_QLCT1_DoanhThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_QLCT1_DoanhThu.Location = new System.Drawing.Point(68, 297);
            this.dgv_QLCT1_DoanhThu.Name = "dgv_QLCT1_DoanhThu";
            this.dgv_QLCT1_DoanhThu.ReadOnly = true;
            this.dgv_QLCT1_DoanhThu.RowHeadersWidth = 51;
            this.dgv_QLCT1_DoanhThu.RowTemplate.Height = 24;
            this.dgv_QLCT1_DoanhThu.Size = new System.Drawing.Size(797, 322);
            this.dgv_QLCT1_DoanhThu.TabIndex = 26;
            // 
            // btn_QLCT1_XacNhan
            // 
            this.btn_QLCT1_XacNhan.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_QLCT1_XacNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QLCT1_XacNhan.Location = new System.Drawing.Point(400, 250);
            this.btn_QLCT1_XacNhan.Name = "btn_QLCT1_XacNhan";
            this.btn_QLCT1_XacNhan.Size = new System.Drawing.Size(124, 26);
            this.btn_QLCT1_XacNhan.TabIndex = 25;
            this.btn_QLCT1_XacNhan.Text = "Xác nhận";
            this.btn_QLCT1_XacNhan.UseVisualStyleBackColor = false;
            this.btn_QLCT1_XacNhan.Click += new System.EventHandler(this.btn_QLCT1_XacNhan_Click);
            // 
            // cmb_QLCT1_selectBranch
            // 
            this.cmb_QLCT1_selectBranch.FormattingEnabled = true;
            this.cmb_QLCT1_selectBranch.Items.AddRange(new object[] {
            "Công ty",
            "Chi nhánh"});
            this.cmb_QLCT1_selectBranch.Location = new System.Drawing.Point(530, 207);
            this.cmb_QLCT1_selectBranch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_QLCT1_selectBranch.Name = "cmb_QLCT1_selectBranch";
            this.cmb_QLCT1_selectBranch.Size = new System.Drawing.Size(166, 24);
            this.cmb_QLCT1_selectBranch.TabIndex = 24;
            this.cmb_QLCT1_selectBranch.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(332, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(279, 32);
            this.label8.TabIndex = 23;
            this.label8.Text = "Tra Cứu Doanh Thu";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lbl_QLCT1_ChiNhanh
            // 
            this.lbl_QLCT1_ChiNhanh.AutoSize = true;
            this.lbl_QLCT1_ChiNhanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_QLCT1_ChiNhanh.Location = new System.Drawing.Point(456, 210);
            this.lbl_QLCT1_ChiNhanh.Name = "lbl_QLCT1_ChiNhanh";
            this.lbl_QLCT1_ChiNhanh.Size = new System.Drawing.Size(68, 16);
            this.lbl_QLCT1_ChiNhanh.TabIndex = 22;
            this.lbl_QLCT1_ChiNhanh.Text = "Chi nhánh:";
            this.lbl_QLCT1_ChiNhanh.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(89, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Lọc phạm vi";
            // 
            // lbl_QLCT1_Quy
            // 
            this.lbl_QLCT1_Quy.AutoSize = true;
            this.lbl_QLCT1_Quy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_QLCT1_Quy.Location = new System.Drawing.Point(89, 159);
            this.lbl_QLCT1_Quy.Name = "lbl_QLCT1_Quy";
            this.lbl_QLCT1_Quy.Size = new System.Drawing.Size(34, 16);
            this.lbl_QLCT1_Quy.TabIndex = 19;
            this.lbl_QLCT1_Quy.Text = "Quý:";
            this.lbl_QLCT1_Quy.Visible = false;
            // 
            // lbl_QLCT1_Thang
            // 
            this.lbl_QLCT1_Thang.AutoSize = true;
            this.lbl_QLCT1_Thang.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_QLCT1_Thang.Location = new System.Drawing.Point(285, 159);
            this.lbl_QLCT1_Thang.Name = "lbl_QLCT1_Thang";
            this.lbl_QLCT1_Thang.Size = new System.Drawing.Size(49, 16);
            this.lbl_QLCT1_Thang.TabIndex = 18;
            this.lbl_QLCT1_Thang.Text = "Tháng:";
            this.lbl_QLCT1_Thang.Visible = false;
            // 
            // lbl_QLCT1_Ngay
            // 
            this.lbl_QLCT1_Ngay.AutoSize = true;
            this.lbl_QLCT1_Ngay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_QLCT1_Ngay.Location = new System.Drawing.Point(89, 156);
            this.lbl_QLCT1_Ngay.Name = "lbl_QLCT1_Ngay";
            this.lbl_QLCT1_Ngay.Size = new System.Drawing.Size(43, 16);
            this.lbl_QLCT1_Ngay.TabIndex = 17;
            this.lbl_QLCT1_Ngay.Text = "Ngày:";
            this.lbl_QLCT1_Ngay.Visible = false;
            // 
            // lbl_QLCT1_Nam
            // 
            this.lbl_QLCT1_Nam.AutoSize = true;
            this.lbl_QLCT1_Nam.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_QLCT1_Nam.Location = new System.Drawing.Point(512, 159);
            this.lbl_QLCT1_Nam.Name = "lbl_QLCT1_Nam";
            this.lbl_QLCT1_Nam.Size = new System.Drawing.Size(39, 16);
            this.lbl_QLCT1_Nam.TabIndex = 16;
            this.lbl_QLCT1_Nam.Text = "Năm:\r\n";
            this.lbl_QLCT1_Nam.Visible = false;
            this.lbl_QLCT1_Nam.Click += new System.EventHandler(this.label2_Click);
            // 
            // Usc_QLCT1_DoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PetCare.Properties.Resources.BG_Glue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.pnl_QTCT1);
            this.Name = "Usc_QLCT1_DoanhThu";
            this.Size = new System.Drawing.Size(1221, 753);
            this.Load += new System.EventHandler(this.Usc_QLCT1_DoanhThu_Load);
            this.pnl_QTCT1.ResumeLayout(false);
            this.pnl_QTCT1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QLCT1_DoanhThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComboBox cmb_QLCT1_selectScope;
        private TextBox txb_QLCT1_getQuarter;
        private TextBox txb_QLCT1_getYear;
        private TextBox txb_QLCT1_getMonth;
        private TextBox txb_QLCT1_getDay;
        private ComboBox cmb_QLCT1_selectTimeMode;
        private Label label1;
        private Panel pnl_QTCT1;
        private Label lbl_QLCT1_Nam;
        private Label lbl_QLCT1_Ngay;
        private Label lbl_QLCT1_Thang;
        private Label lbl_QLCT1_Quy;
        private Label label6;
        private Label label8;
        private Label lbl_QLCT1_ChiNhanh;
        private ComboBox cmb_QLCT1_selectBranch;
        private Button btn_QLCT1_XacNhan;
        private DataGridView dgv_QLCT1_DoanhThu;
    }
}
