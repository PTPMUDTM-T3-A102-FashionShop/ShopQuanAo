
namespace userControl
{
    partial class ucAnalytic
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
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbTenMau = new System.Windows.Forms.ComboBox();
            this.cbTenSize = new System.Windows.Forms.ComboBox();
            this.txtGia = new System.Windows.Forms.TextBox();
            this.txtSoLuongTonKho = new System.Windows.Forms.TextBox();
            this.lblDuDoan = new System.Windows.Forms.Label();
            this.btnDuDoan = new Guna.UI2.WinForms.Guna2Button();
            this.lbl3MoTa = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Location = new System.Drawing.Point(417, 157);
            this.dgvSanPham.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.RowHeadersWidth = 51;
            this.dgvSanPham.RowTemplate.Height = 24;
            this.dgvSanPham.Size = new System.Drawing.Size(796, 555);
            this.dgvSanPham.TabIndex = 39;
            this.dgvSanPham.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPham_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(104, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1093, 83);
            this.label1.TabIndex = 38;
            this.label1.Text = "Thống kê và dự đoán sản phẩm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "Tên màu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 44;
            this.label3.Text = "Tên size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 453);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 20);
            this.label4.TabIndex = 45;
            this.label4.Text = "Giá";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 516);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 20);
            this.label5.TabIndex = 46;
            this.label5.Text = "Số lượng tồn kho";
            // 
            // cbTenMau
            // 
            this.cbTenMau.FormattingEnabled = true;
            this.cbTenMau.Location = new System.Drawing.Point(191, 313);
            this.cbTenMau.Name = "cbTenMau";
            this.cbTenMau.Size = new System.Drawing.Size(163, 28);
            this.cbTenMau.TabIndex = 47;
            this.cbTenMau.SelectedIndexChanged += new System.EventHandler(this.cbTenMau_SelectedIndexChanged);
            // 
            // cbTenSize
            // 
            this.cbTenSize.FormattingEnabled = true;
            this.cbTenSize.Location = new System.Drawing.Point(191, 375);
            this.cbTenSize.Name = "cbTenSize";
            this.cbTenSize.Size = new System.Drawing.Size(163, 28);
            this.cbTenSize.TabIndex = 48;
            this.cbTenSize.SelectedIndexChanged += new System.EventHandler(this.cbTenSize_SelectedIndexChanged);
            // 
            // txtGia
            // 
            this.txtGia.Location = new System.Drawing.Point(191, 447);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new System.Drawing.Size(163, 26);
            this.txtGia.TabIndex = 49;
            this.txtGia.TextChanged += new System.EventHandler(this.txtGia_TextChanged);
            // 
            // txtSoLuongTonKho
            // 
            this.txtSoLuongTonKho.Location = new System.Drawing.Point(191, 516);
            this.txtSoLuongTonKho.Name = "txtSoLuongTonKho";
            this.txtSoLuongTonKho.Size = new System.Drawing.Size(163, 26);
            this.txtSoLuongTonKho.TabIndex = 50;
            this.txtSoLuongTonKho.TextChanged += new System.EventHandler(this.txtSoLuongTonKho_TextChanged);
            // 
            // lblDuDoan
            // 
            this.lblDuDoan.AutoSize = true;
            this.lblDuDoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuDoan.Location = new System.Drawing.Point(58, 755);
            this.lblDuDoan.Name = "lblDuDoan";
            this.lblDuDoan.Size = new System.Drawing.Size(229, 29);
            this.lblDuDoan.TabIndex = 51;
            this.lblDuDoan.Text = "Dự đoán sản phẩm";
            this.lblDuDoan.Click += new System.EventHandler(this.lblDuDoan_Click);
            // 
            // btnDuDoan
            // 
            this.btnDuDoan.BorderRadius = 10;
            this.btnDuDoan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDuDoan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDuDoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDuDoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDuDoan.FillColor = System.Drawing.Color.SandyBrown;
            this.btnDuDoan.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuDoan.ForeColor = System.Drawing.Color.White;
            this.btnDuDoan.Image = global::userControl.Properties.Resources.we;
            this.btnDuDoan.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDuDoan.ImageOffset = new System.Drawing.Point(5, 0);
            this.btnDuDoan.ImageSize = new System.Drawing.Size(23, 23);
            this.btnDuDoan.Location = new System.Drawing.Point(33, 657);
            this.btnDuDoan.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnDuDoan.Name = "btnDuDoan";
            this.btnDuDoan.Size = new System.Drawing.Size(362, 55);
            this.btnDuDoan.TabIndex = 41;
            this.btnDuDoan.Text = "Dự đoán sản phẩm";
            this.btnDuDoan.TextOffset = new System.Drawing.Point(15, 0);
            this.btnDuDoan.Click += new System.EventHandler(this.btnDuDoan_Click);
            // 
            // lbl3MoTa
            // 
            this.lbl3MoTa.AutoSize = true;
            this.lbl3MoTa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3MoTa.Location = new System.Drawing.Point(613, 728);
            this.lbl3MoTa.Name = "lbl3MoTa";
            this.lbl3MoTa.Size = new System.Drawing.Size(200, 29);
            this.lbl3MoTa.TabIndex = 52;
            this.lbl3MoTa.Text = "Top 3 sản phẩm";
            this.lbl3MoTa.Click += new System.EventHandler(this.lbl3MoTa_Click);
            // 
            // ucAnalytic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl3MoTa);
            this.Controls.Add(this.lblDuDoan);
            this.Controls.Add(this.txtSoLuongTonKho);
            this.Controls.Add(this.txtGia);
            this.Controls.Add(this.cbTenSize);
            this.Controls.Add(this.cbTenMau);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDuDoan);
            this.Controls.Add(this.dgvSanPham);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucAnalytic";
            this.Size = new System.Drawing.Size(1323, 920);
            this.Load += new System.EventHandler(this.ucAnalytic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbTenMau;
        private System.Windows.Forms.ComboBox cbTenSize;
        private System.Windows.Forms.TextBox txtGia;
        private System.Windows.Forms.TextBox txtSoLuongTonKho;
        private System.Windows.Forms.Label lblDuDoan;
        private Guna.UI2.WinForms.Guna2Button btnDuDoan;
        private System.Windows.Forms.Label lbl3MoTa;
    }
}
