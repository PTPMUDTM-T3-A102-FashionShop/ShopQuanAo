namespace userControl
{
    partial class ucProduct
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSP = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbOff = new System.Windows.Forms.RadioButton();
            this.rdbOn = new System.Windows.Forms.RadioButton();
            this.cbbDanhMuc = new System.Windows.Forms.ComboBox();
            this.btnEditBrand = new Guna.UI2.WinForms.Guna2Button();
            this.btnEditDetail = new Guna.UI2.WinForms.Guna2Button();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUpload = new Guna.UI2.WinForms.Guna2Button();
            this.pbImg = new System.Windows.Forms.PictureBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbSize = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbMau = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label11 = new System.Windows.Forms.Label();
            this.cbbBrand = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSP)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImg)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(260, 112);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(364, 28);
            this.txtName.TabIndex = 41;
            // 
            // txtDes
            // 
            this.txtDes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDes.Location = new System.Drawing.Point(184, 154);
            this.txtDes.Multiline = true;
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new System.Drawing.Size(440, 59);
            this.txtDes.TabIndex = 40;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(101, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 25);
            this.label6.TabIndex = 39;
            this.label6.Text = "Mô tả:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(685, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 25);
            this.label5.TabIndex = 38;
            this.label5.Text = "Danh mục:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 25);
            this.label2.TabIndex = 32;
            this.label2.Text = "Tên sản phẩm:";
            // 
            // dgvSP
            // 
            this.dgvSP.AllowUserToAddRows = false;
            this.dgvSP.AllowUserToDeleteRows = false;
            this.dgvSP.AllowUserToResizeColumns = false;
            this.dgvSP.AllowUserToResizeRows = false;
            this.dgvSP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSP.Location = new System.Drawing.Point(106, 571);
            this.dgvSP.Name = "dgvSP";
            this.dgvSP.ReadOnly = true;
            this.dgvSP.RowHeadersVisible = false;
            this.dgvSP.RowHeadersWidth = 51;
            this.dgvSP.RowTemplate.Height = 24;
            this.dgvSP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSP.Size = new System.Drawing.Size(889, 204);
            this.dgvSP.TabIndex = 28;
            this.dgvSP.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSP_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(398, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 70);
            this.label1.TabIndex = 26;
            this.label1.Text = "Products";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(685, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 25);
            this.label3.TabIndex = 43;
            this.label3.Text = "Kích hoạt:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbOff);
            this.groupBox1.Controls.Add(this.rdbOn);
            this.groupBox1.Location = new System.Drawing.Point(835, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 98);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            // 
            // rdbOff
            // 
            this.rdbOff.AutoSize = true;
            this.rdbOff.Location = new System.Drawing.Point(18, 58);
            this.rdbOff.Name = "rdbOff";
            this.rdbOff.Size = new System.Drawing.Size(136, 20);
            this.rdbOff.TabIndex = 1;
            this.rdbOff.TabStop = true;
            this.rdbOff.Text = "Ngừng kinh doanh";
            this.rdbOff.UseVisualStyleBackColor = true;
            // 
            // rdbOn
            // 
            this.rdbOn.AutoSize = true;
            this.rdbOn.Location = new System.Drawing.Point(18, 17);
            this.rdbOn.Name = "rdbOn";
            this.rdbOn.Size = new System.Drawing.Size(94, 20);
            this.rdbOn.TabIndex = 0;
            this.rdbOn.TabStop = true;
            this.rdbOn.Text = "Kinh doanh";
            this.rdbOn.UseVisualStyleBackColor = true;
            // 
            // cbbDanhMuc
            // 
            this.cbbDanhMuc.FormattingEnabled = true;
            this.cbbDanhMuc.Location = new System.Drawing.Point(831, 114);
            this.cbbDanhMuc.Name = "cbbDanhMuc";
            this.cbbDanhMuc.Size = new System.Drawing.Size(164, 24);
            this.cbbDanhMuc.TabIndex = 49;
            // 
            // btnEditBrand
            // 
            this.btnEditBrand.BorderRadius = 10;
            this.btnEditBrand.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditBrand.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditBrand.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditBrand.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditBrand.FillColor = System.Drawing.Color.SandyBrown;
            this.btnEditBrand.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditBrand.ForeColor = System.Drawing.Color.White;
            this.btnEditBrand.Image = global::userControl.Properties.Resources.wbrand;
            this.btnEditBrand.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnEditBrand.ImageOffset = new System.Drawing.Point(2, 0);
            this.btnEditBrand.ImageSize = new System.Drawing.Size(23, 23);
            this.btnEditBrand.Location = new System.Drawing.Point(791, 510);
            this.btnEditBrand.Name = "btnEditBrand";
            this.btnEditBrand.Size = new System.Drawing.Size(204, 45);
            this.btnEditBrand.TabIndex = 51;
            this.btnEditBrand.Text = "Edit Brands";
            this.btnEditBrand.TextOffset = new System.Drawing.Point(15, 0);
            this.btnEditBrand.Click += new System.EventHandler(this.btnEditBrand_Click);
            // 
            // btnEditDetail
            // 
            this.btnEditDetail.BorderRadius = 10;
            this.btnEditDetail.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditDetail.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditDetail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditDetail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditDetail.FillColor = System.Drawing.Color.SandyBrown;
            this.btnEditDetail.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDetail.ForeColor = System.Drawing.Color.White;
            this.btnEditDetail.Image = global::userControl.Properties.Resources.wde;
            this.btnEditDetail.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnEditDetail.ImageOffset = new System.Drawing.Point(2, 0);
            this.btnEditDetail.ImageSize = new System.Drawing.Size(23, 23);
            this.btnEditDetail.Location = new System.Drawing.Point(583, 510);
            this.btnEditDetail.Name = "btnEditDetail";
            this.btnEditDetail.Size = new System.Drawing.Size(202, 45);
            this.btnEditDetail.TabIndex = 42;
            this.btnEditDetail.Text = "Edit Details";
            this.btnEditDetail.TextOffset = new System.Drawing.Point(15, 0);
            this.btnEditDetail.Click += new System.EventHandler(this.btnEditDetail_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BorderRadius = 10;
            this.btnDelete.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDelete.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDelete.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDelete.FillColor = System.Drawing.Color.SandyBrown;
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = global::userControl.Properties.Resources.wd;
            this.btnDelete.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDelete.ImageOffset = new System.Drawing.Point(5, 0);
            this.btnDelete.ImageSize = new System.Drawing.Size(23, 23);
            this.btnDelete.Location = new System.Drawing.Point(260, 510);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(146, 45);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextOffset = new System.Drawing.Point(15, 0);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BorderRadius = 10;
            this.btnEdit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEdit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEdit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEdit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEdit.FillColor = System.Drawing.Color.SandyBrown;
            this.btnEdit.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Image = global::userControl.Properties.Resources.we;
            this.btnEdit.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnEdit.ImageOffset = new System.Drawing.Point(5, 0);
            this.btnEdit.ImageSize = new System.Drawing.Size(23, 23);
            this.btnEdit.Location = new System.Drawing.Point(422, 510);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(146, 45);
            this.btnEdit.TabIndex = 30;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextOffset = new System.Drawing.Point(15, 0);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BorderRadius = 10;
            this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAdd.FillColor = System.Drawing.Color.SandyBrown;
            this.btnAdd.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::userControl.Properties.Resources.wadd;
            this.btnAdd.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAdd.ImageOffset = new System.Drawing.Point(5, 0);
            this.btnAdd.ImageSize = new System.Drawing.Size(23, 23);
            this.btnAdd.Location = new System.Drawing.Point(106, 510);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(146, 45);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextOffset = new System.Drawing.Point(15, 0);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.pbImg);
            this.groupBox2.Controls.Add(this.btnUpload);
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtQuantity);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbbMau);
            this.groupBox2.Controls.Add(this.cbbSize);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(106, 271);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(883, 207);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "First detail";
            // 
            // btnUpload
            // 
            this.btnUpload.BorderRadius = 20;
            this.btnUpload.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUpload.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUpload.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnUpload.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnUpload.FillColor = System.Drawing.Color.LightGray;
            this.btnUpload.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnUpload.ForeColor = System.Drawing.Color.Black;
            this.btnUpload.Location = new System.Drawing.Point(477, 144);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(160, 42);
            this.btnUpload.TabIndex = 84;
            this.btnUpload.Text = "Chọn file ảnh";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // pbImg
            // 
            this.pbImg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pbImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbImg.Location = new System.Drawing.Point(697, 26);
            this.pbImg.Name = "pbImg";
            this.pbImg.Size = new System.Drawing.Size(160, 160);
            this.pbImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImg.TabIndex = 83;
            this.pbImg.TabStop = false;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(468, 92);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(156, 28);
            this.txtQuantity.TabIndex = 82;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(362, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 25);
            this.label4.TabIndex = 81;
            this.label4.Text = "Tồn kho:";
            // 
            // cbbSize
            // 
            this.cbbSize.FormattingEnabled = true;
            this.cbbSize.Location = new System.Drawing.Point(468, 34);
            this.cbbSize.Name = "cbbSize";
            this.cbbSize.Size = new System.Drawing.Size(156, 24);
            this.cbbSize.TabIndex = 80;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(362, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 25);
            this.label7.TabIndex = 79;
            this.label7.Text = "Size:";
            // 
            // cbbMau
            // 
            this.cbbMau.FormattingEnabled = true;
            this.cbbMau.Location = new System.Drawing.Point(124, 34);
            this.cbbMau.Name = "cbbMau";
            this.cbbMau.Size = new System.Drawing.Size(165, 24);
            this.cbbMau.TabIndex = 78;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Location = new System.Drawing.Point(154, 144);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 54);
            this.groupBox3.TabIndex = 77;
            this.groupBox3.TabStop = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(144, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(136, 20);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Ngừng kinh doanh";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(18, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(94, 20);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Kinh doanh";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 25);
            this.label8.TabIndex = 76;
            this.label8.Text = "Kích hoạt:";
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(124, 92);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(165, 28);
            this.txtPrice.TabIndex = 75;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 25);
            this.label9.TabIndex = 74;
            this.label9.Text = "Màu:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(22, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 25);
            this.label10.TabIndex = 73;
            this.label10.Text = "Giá bán:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(101, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 25);
            this.label11.TabIndex = 53;
            this.label11.Text = "First brand:";
            // 
            // cbbBrand
            // 
            this.cbbBrand.FormattingEnabled = true;
            this.cbbBrand.Location = new System.Drawing.Point(230, 232);
            this.cbbBrand.Name = "cbbBrand";
            this.cbbBrand.Size = new System.Drawing.Size(394, 24);
            this.cbbBrand.TabIndex = 66;
            // 
            // ucProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbbBrand);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnEditBrand);
            this.Controls.Add(this.cbbDanhMuc);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEditDetail);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtDes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvSP);
            this.Controls.Add(this.label1);
            this.Name = "ucProduct";
            this.Size = new System.Drawing.Size(1100, 790);
            this.Load += new System.EventHandler(this.ucProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSP)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImg)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2Button btnEdit;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private System.Windows.Forms.DataGridView dgvSP;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnEditDetail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbOn;
        private System.Windows.Forms.RadioButton rdbOff;
        private System.Windows.Forms.ComboBox cbbDanhMuc;
        private Guna.UI2.WinForms.Guna2Button btnEditBrand;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.PictureBox pbImg;
        private Guna.UI2.WinForms.Guna2Button btnUpload;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbMau;
        private System.Windows.Forms.ComboBox cbbSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbbBrand;
    }
}
