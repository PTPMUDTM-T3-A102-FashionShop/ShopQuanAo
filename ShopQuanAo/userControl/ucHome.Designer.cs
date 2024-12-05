namespace userControl
{
    partial class ucHome
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbTime = new System.Windows.Forms.Label();
            this.lbDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Segoe UI", 79.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbTime.Location = new System.Drawing.Point(273, 145);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(564, 175);
            this.lbTime.TabIndex = 0;
            this.lbTime.Text = "00:00:00";
            // 
            // lbDate
            // 
            this.lbDate.AutoSize = true;
            this.lbDate.Font = new System.Drawing.Font("Segoe UI Semibold", 34.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDate.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbDate.Location = new System.Drawing.Point(3, 374);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(996, 76);
            this.lbDate.TabIndex = 1;
            this.lbDate.Text = "Chủ nhật, ngày 19 tháng 11 năm 2024";
            // 
            // ucHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lbTime);
            this.Name = "ucHome";
            this.Size = new System.Drawing.Size(1100, 700);
            this.Load += new System.EventHandler(this.ucHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbDate;
    }
}
