namespace Zobrazovac_Dat
{
    partial class UvitacieOkno
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblSelekt = new System.Windows.Forms.Label();
            this.cbxSelektFiltra = new System.Windows.Forms.ComboBox();
            this.cbxSelektProjektu = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxMesto = new System.Windows.Forms.ComboBox();
            this.btnServer = new System.Windows.Forms.Button();
            this.btnSubor = new System.Windows.Forms.Button();
            this.chbxAktData = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(355, 214);
            this.button1.Margin = new System.Windows.Forms.Padding(40, 39, 40, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(271, 14);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(13, 14, 13, 14);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(94, 17);
            this.lblFilter.TabIndex = 9;
            this.lblFilter.Text = "Vyber z filtrov";
            // 
            // lblSelekt
            // 
            this.lblSelekt.AutoSize = true;
            this.lblSelekt.Location = new System.Drawing.Point(17, 14);
            this.lblSelekt.Margin = new System.Windows.Forms.Padding(5, 14, 13, 14);
            this.lblSelekt.Name = "lblSelekt";
            this.lblSelekt.Size = new System.Drawing.Size(118, 17);
            this.lblSelekt.TabIndex = 8;
            this.lblSelekt.Text = "Vyber z projektov";
            // 
            // cbxSelektFiltra
            // 
            this.cbxSelektFiltra.FormattingEnabled = true;
            this.cbxSelektFiltra.Location = new System.Drawing.Point(273, 48);
            this.cbxSelektFiltra.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbxSelektFiltra.Name = "cbxSelektFiltra";
            this.cbxSelektFiltra.Size = new System.Drawing.Size(221, 24);
            this.cbxSelektFiltra.TabIndex = 7;
            this.cbxSelektFiltra.Text = "Vyber Filter";
            // 
            // cbxSelektProjektu
            // 
            this.cbxSelektProjektu.FormattingEnabled = true;
            this.cbxSelektProjektu.Location = new System.Drawing.Point(17, 48);
            this.cbxSelektProjektu.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbxSelektProjektu.Name = "cbxSelektProjektu";
            this.cbxSelektProjektu.Size = new System.Drawing.Size(221, 24);
            this.cbxSelektProjektu.TabIndex = 6;
            this.cbxSelektProjektu.Text = "Vyber Selekt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 182);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Vyber stanice";
            // 
            // cbxMesto
            // 
            this.cbxMesto.FormattingEnabled = true;
            this.cbxMesto.Location = new System.Drawing.Point(13, 209);
            this.cbxMesto.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbxMesto.Name = "cbxMesto";
            this.cbxMesto.Size = new System.Drawing.Size(217, 24);
            this.cbxMesto.TabIndex = 11;
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(165, 103);
            this.btnServer.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(137, 69);
            this.btnServer.TabIndex = 12;
            this.btnServer.Text = "Aktualizovať vybrané dáta";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // btnSubor
            // 
            this.btnSubor.Location = new System.Drawing.Point(21, 103);
            this.btnSubor.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSubor.Name = "btnSubor";
            this.btnSubor.Size = new System.Drawing.Size(133, 69);
            this.btnSubor.TabIndex = 13;
            this.btnSubor.Text = "Načítať dáta";
            this.btnSubor.UseVisualStyleBackColor = true;
            this.btnSubor.Click += new System.EventHandler(this.btnSubor_Click);
            // 
            // chbxAktData
            // 
            this.chbxAktData.FormattingEnabled = true;
            this.chbxAktData.Items.AddRange(new object[] {
            "Dopravné body",
            "Dopravné druhy",
            "Dopravné úseky",
            "Poznámky",
            "Trasa body",
            "Vlaky"});
            this.chbxAktData.Location = new System.Drawing.Point(312, 82);
            this.chbxAktData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chbxAktData.Name = "chbxAktData";
            this.chbxAktData.Size = new System.Drawing.Size(183, 106);
            this.chbxAktData.TabIndex = 14;
            // 
            // UvitacieOkno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 290);
            this.Controls.Add(this.chbxAktData);
            this.Controls.Add(this.btnSubor);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.cbxMesto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblSelekt);
            this.Controls.Add(this.cbxSelektFiltra);
            this.Controls.Add(this.cbxSelektProjektu);
            this.Controls.Add(this.button1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(639, 358);
            this.MinimumSize = new System.Drawing.Size(562, 328);
            this.Name = "UvitacieOkno";
            this.Text = "Generátor vývesky";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblSelekt;
        private System.Windows.Forms.ComboBox cbxSelektFiltra;
        private System.Windows.Forms.ComboBox cbxSelektProjektu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxMesto;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Button btnSubor;
        private System.Windows.Forms.CheckedListBox chbxAktData;
    }
}