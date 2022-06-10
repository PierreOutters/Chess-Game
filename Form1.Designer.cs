namespace Chess_on_forms
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Boardpanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Boardpanel
            // 
            this.Boardpanel.BackColor = System.Drawing.Color.Gray;
            this.Boardpanel.Location = new System.Drawing.Point(10, 10);
            this.Boardpanel.Name = "Boardpanel";
            this.Boardpanel.Size = new System.Drawing.Size(646, 596);
            this.Boardpanel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 651);
            this.Controls.Add(this.Boardpanel);
            this.Name = "Form1";
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Boardpanel;
    }
}

