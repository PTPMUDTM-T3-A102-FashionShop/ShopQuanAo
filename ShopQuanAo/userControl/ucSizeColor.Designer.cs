namespace userControl
{
    partial class ucSizeColor
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.dgvSize = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.dgvColor = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDeleteColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnEditColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddColor = new Guna.UI2.WinForms.Guna2Button();
            this.btnDeleteSize = new Guna.UI2.WinForms.Guna2Button();
            this.btnEditSize = new Guna.UI2.WinForms.Guna2Button();
            this.btnAddSize = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(377, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = "Size - Color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(58, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 29);
            this.label2.TabIndex = 13;
            this.label2.Text = "Size:";
            // 
            // txtSize
            // 
            this.txtSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSize.Location = new System.Drawing.Point(147, 146);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(366, 34);
            this.txtSize.TabIndex = 9;
            // 
            // dgvSize
            // 
            this.dgvSize.AllowUserToAddRows = false;
            this.dgvSize.AllowUserToResizeColumns = false;
            this.dgvSize.AllowUserToResizeRows = false;
            this.dgvSize.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSize.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSize.Location = new System.Drawing.Point(588, 146);
            this.dgvSize.Name = "dgvSize";
            this.dgvSize.ReadOnly = true;
            this.dgvSize.RowHeadersVisible = false;
            this.dgvSize.RowHeadersWidth = 51;
            this.dgvSize.RowTemplate.Height = 24;
            this.dgvSize.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSize.Size = new System.Drawing.Size(450, 231);
            this.dgvSize.TabIndex = 8;
            this.dgvSize.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSize_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 433);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 29);
            this.label3.TabIndex = 19;
            this.label3.Text = "Color:";
            // 
            // txtColor
            // 
            this.txtColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColor.Location = new System.Drawing.Point(147, 433);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(366, 34);
            this.txtColor.TabIndex = 15;
            // 
            // dgvColor
            // 
            this.dgvColor.AllowUserToAddRows = false;
            this.dgvColor.AllowUserToResizeColumns = false;
            this.dgvColor.AllowUserToResizeRows = false;
            this.dgvColor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColor.Location = new System.Drawing.Point(588, 433);
            this.dgvColor.Name = "dgvColor";
            this.dgvColor.ReadOnly = true;
            this.dgvColor.RowHeadersVisible = false;
            this.dgvColor.RowHeadersWidth = 51;
            this.dgvColor.RowTemplate.Height = 24;
            this.dgvColor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColor.Size = new System.Drawing.Size(450, 231);
            this.dgvColor.TabIndex = 14;
            this.dgvColor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColor_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(583, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 29);
            this.label4.TabIndex = 20;
            this.label4.Text = "Sizes list:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(583, 401);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 29);
            this.label5.TabIndex = 21;
            this.label5.Text = "Color list:";
            // 
            // btnDeleteColor
            // 
            this.btnDeleteColor.BorderRadius = 10;
            this.btnDeleteColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeleteColor.FillColor = System.Drawing.Color.SandyBrown;
            this.btnDeleteColor.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteColor.ForeColor = System.Drawing.Color.White;
            this.btnDeleteColor.Image = global::userControl.Properties.Resources.wd;
            this.btnDeleteColor.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDeleteColor.ImageOffset = new System.Drawing.Point(1, 0);
            this.btnDeleteColor.ImageSize = new System.Drawing.Size(23, 23);
            this.btnDeleteColor.Location = new System.Drawing.Point(198, 557);
            this.btnDeleteColor.Name = "btnDeleteColor";
            this.btnDeleteColor.Size = new System.Drawing.Size(195, 45);
            this.btnDeleteColor.TabIndex = 18;
            this.btnDeleteColor.Text = "Delete color";
            this.btnDeleteColor.TextOffset = new System.Drawing.Point(15, 0);
            this.btnDeleteColor.Click += new System.EventHandler(this.btnDeleteColor_Click);
            // 
            // btnEditColor
            // 
            this.btnEditColor.BorderRadius = 10;
            this.btnEditColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditColor.FillColor = System.Drawing.Color.SandyBrown;
            this.btnEditColor.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditColor.ForeColor = System.Drawing.Color.White;
            this.btnEditColor.Image = global::userControl.Properties.Resources.we;
            this.btnEditColor.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnEditColor.ImageOffset = new System.Drawing.Point(3, 0);
            this.btnEditColor.ImageSize = new System.Drawing.Size(23, 23);
            this.btnEditColor.Location = new System.Drawing.Point(199, 619);
            this.btnEditColor.Name = "btnEditColor";
            this.btnEditColor.Size = new System.Drawing.Size(194, 45);
            this.btnEditColor.TabIndex = 17;
            this.btnEditColor.Text = "Edit color";
            this.btnEditColor.TextOffset = new System.Drawing.Point(15, 0);
            this.btnEditColor.Click += new System.EventHandler(this.btnEditColor_Click);
            // 
            // btnAddColor
            // 
            this.btnAddColor.BorderRadius = 10;
            this.btnAddColor.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddColor.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddColor.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddColor.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddColor.FillColor = System.Drawing.Color.SandyBrown;
            this.btnAddColor.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddColor.ForeColor = System.Drawing.Color.White;
            this.btnAddColor.Image = global::userControl.Properties.Resources.wadd;
            this.btnAddColor.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAddColor.ImageOffset = new System.Drawing.Point(3, 0);
            this.btnAddColor.ImageSize = new System.Drawing.Size(23, 23);
            this.btnAddColor.Location = new System.Drawing.Point(198, 496);
            this.btnAddColor.Name = "btnAddColor";
            this.btnAddColor.Size = new System.Drawing.Size(195, 45);
            this.btnAddColor.TabIndex = 16;
            this.btnAddColor.Text = "Add color";
            this.btnAddColor.TextOffset = new System.Drawing.Point(15, 0);
            this.btnAddColor.Click += new System.EventHandler(this.btnAddColor_Click);
            // 
            // btnDeleteSize
            // 
            this.btnDeleteSize.BorderRadius = 10;
            this.btnDeleteSize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteSize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDeleteSize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDeleteSize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDeleteSize.FillColor = System.Drawing.Color.SandyBrown;
            this.btnDeleteSize.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSize.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSize.Image = global::userControl.Properties.Resources.wd;
            this.btnDeleteSize.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDeleteSize.ImageOffset = new System.Drawing.Point(1, 0);
            this.btnDeleteSize.ImageSize = new System.Drawing.Size(23, 23);
            this.btnDeleteSize.Location = new System.Drawing.Point(198, 270);
            this.btnDeleteSize.Name = "btnDeleteSize";
            this.btnDeleteSize.Size = new System.Drawing.Size(195, 45);
            this.btnDeleteSize.TabIndex = 12;
            this.btnDeleteSize.Text = "Delete size";
            this.btnDeleteSize.TextOffset = new System.Drawing.Point(15, 0);
            this.btnDeleteSize.Click += new System.EventHandler(this.btnDeleteSize_Click);
            // 
            // btnEditSize
            // 
            this.btnEditSize.BorderRadius = 10;
            this.btnEditSize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEditSize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEditSize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEditSize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEditSize.FillColor = System.Drawing.Color.SandyBrown;
            this.btnEditSize.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditSize.ForeColor = System.Drawing.Color.White;
            this.btnEditSize.Image = global::userControl.Properties.Resources.we;
            this.btnEditSize.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnEditSize.ImageOffset = new System.Drawing.Point(3, 0);
            this.btnEditSize.ImageSize = new System.Drawing.Size(23, 23);
            this.btnEditSize.Location = new System.Drawing.Point(199, 332);
            this.btnEditSize.Name = "btnEditSize";
            this.btnEditSize.Size = new System.Drawing.Size(194, 45);
            this.btnEditSize.TabIndex = 11;
            this.btnEditSize.Text = "Edit size";
            this.btnEditSize.TextOffset = new System.Drawing.Point(15, 0);
            this.btnEditSize.Click += new System.EventHandler(this.btnEditSize_Click);
            // 
            // btnAddSize
            // 
            this.btnAddSize.BorderRadius = 10;
            this.btnAddSize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddSize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddSize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddSize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddSize.FillColor = System.Drawing.Color.SandyBrown;
            this.btnAddSize.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSize.ForeColor = System.Drawing.Color.White;
            this.btnAddSize.Image = global::userControl.Properties.Resources.wadd;
            this.btnAddSize.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAddSize.ImageOffset = new System.Drawing.Point(3, 0);
            this.btnAddSize.ImageSize = new System.Drawing.Size(23, 23);
            this.btnAddSize.Location = new System.Drawing.Point(198, 209);
            this.btnAddSize.Name = "btnAddSize";
            this.btnAddSize.Size = new System.Drawing.Size(195, 45);
            this.btnAddSize.TabIndex = 10;
            this.btnAddSize.Text = "Add size";
            this.btnAddSize.TextOffset = new System.Drawing.Point(15, 0);
            this.btnAddSize.Click += new System.EventHandler(this.btnAddSize_Click);
            // 
            // ucSizeColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDeleteColor);
            this.Controls.Add(this.btnEditColor);
            this.Controls.Add(this.btnAddColor);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.dgvColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeleteSize);
            this.Controls.Add(this.btnEditSize);
            this.Controls.Add(this.btnAddSize);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.dgvSize);
            this.Controls.Add(this.label1);
            this.Name = "ucSizeColor";
            this.Size = new System.Drawing.Size(1100, 700);
            this.Load += new System.EventHandler(this.ucSizeColor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnDeleteSize;
        private Guna.UI2.WinForms.Guna2Button btnEditSize;
        private Guna.UI2.WinForms.Guna2Button btnAddSize;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.DataGridView dgvSize;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button btnDeleteColor;
        private Guna.UI2.WinForms.Guna2Button btnEditColor;
        private Guna.UI2.WinForms.Guna2Button btnAddColor;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.DataGridView dgvColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
