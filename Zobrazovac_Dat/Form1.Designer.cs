namespace Zobrazovac_Dat
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
            this.cbxSelektProjektu = new System.Windows.Forms.ComboBox();
            this.cbxSelektFiltra = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxSelektProjektu
            // 
            this.cbxSelektProjektu.FormattingEnabled = true;
            this.cbxSelektProjektu.Location = new System.Drawing.Point(12, 12);
            this.cbxSelektProjektu.Name = "cbxSelektProjektu";
            this.cbxSelektProjektu.Size = new System.Drawing.Size(243, 24);
            this.cbxSelektProjektu.TabIndex = 0;
            // 
            // cbxSelektFiltra
            // 
            this.cbxSelektFiltra.FormattingEnabled = true;
            this.cbxSelektFiltra.Location = new System.Drawing.Point(262, 10);
            this.cbxSelektFiltra.Name = "cbxSelektFiltra";
            this.cbxSelektFiltra.Size = new System.Drawing.Size(121, 24);
            this.cbxSelektFiltra.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 349);
            this.Controls.Add(this.cbxSelektFiltra);
            this.Controls.Add(this.cbxSelektProjektu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxSelektProjektu;
        private System.Windows.Forms.ComboBox cbxSelektFiltra;
    }
}

