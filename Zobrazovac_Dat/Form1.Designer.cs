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
            this.dgvVlaky = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblSelekt = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnZapisDoSuboru = new System.Windows.Forms.Button();
            this.btnNacitaj = new System.Windows.Forms.Button();
            this.btnVytvorVyvesku = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVlaky)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxSelektProjektu
            // 
            this.cbxSelektProjektu.FormattingEnabled = true;
            this.cbxSelektProjektu.Location = new System.Drawing.Point(14, 57);
            this.cbxSelektProjektu.Margin = new System.Windows.Forms.Padding(5);
            this.cbxSelektProjektu.Name = "cbxSelektProjektu";
            this.cbxSelektProjektu.Size = new System.Drawing.Size(221, 24);
            this.cbxSelektProjektu.TabIndex = 1;
            this.cbxSelektProjektu.Text = "Vyber Selekt";
            // 
            // cbxSelektFiltra
            // 
            this.cbxSelektFiltra.FormattingEnabled = true;
            this.cbxSelektFiltra.Location = new System.Drawing.Point(245, 57);
            this.cbxSelektFiltra.Margin = new System.Windows.Forms.Padding(5);
            this.cbxSelektFiltra.Name = "cbxSelektFiltra";
            this.cbxSelektFiltra.Size = new System.Drawing.Size(221, 24);
            this.cbxSelektFiltra.TabIndex = 2;
            this.cbxSelektFiltra.Text = "Vyber Filter";
            // 
            // dgvVlaky
            // 
            this.dgvVlaky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVlaky.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVlaky.Location = new System.Drawing.Point(16, 90);
            this.dgvVlaky.Margin = new System.Windows.Forms.Padding(4);
            this.dgvVlaky.Name = "dgvVlaky";
            this.dgvVlaky.Size = new System.Drawing.Size(1032, 290);
            this.dgvVlaky.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(802, 14);
            this.btnOK.Margin = new System.Windows.Forms.Padding(5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(248, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblSelekt
            // 
            this.lblSelekt.AutoSize = true;
            this.lblSelekt.Location = new System.Drawing.Point(14, 22);
            this.lblSelekt.Margin = new System.Windows.Forms.Padding(5, 13, 13, 13);
            this.lblSelekt.Name = "lblSelekt";
            this.lblSelekt.Size = new System.Drawing.Size(118, 17);
            this.lblSelekt.TabIndex = 4;
            this.lblSelekt.Text = "Vyber z projektov";
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(242, 22);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(13);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(94, 17);
            this.lblFilter.TabIndex = 5;
            this.lblFilter.Text = "Vyber z filtrov";
            // 
            // btnZapisDoSuboru
            // 
            this.btnZapisDoSuboru.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZapisDoSuboru.Location = new System.Drawing.Point(975, 49);
            this.btnZapisDoSuboru.Margin = new System.Windows.Forms.Padding(5);
            this.btnZapisDoSuboru.Name = "btnZapisDoSuboru";
            this.btnZapisDoSuboru.Size = new System.Drawing.Size(75, 32);
            this.btnZapisDoSuboru.TabIndex = 6;
            this.btnZapisDoSuboru.Text = "Ulozit Vlaky";
            this.btnZapisDoSuboru.UseVisualStyleBackColor = true;
            this.btnZapisDoSuboru.Click += new System.EventHandler(this.btnZapisDoSuboru_Click);
            // 
            // btnNacitaj
            // 
            this.btnNacitaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNacitaj.Location = new System.Drawing.Point(802, 49);
            this.btnNacitaj.Margin = new System.Windows.Forms.Padding(5);
            this.btnNacitaj.Name = "btnNacitaj";
            this.btnNacitaj.Size = new System.Drawing.Size(163, 32);
            this.btnNacitaj.TabIndex = 7;
            this.btnNacitaj.Text = "Nacitat ulozene vlaky";
            this.btnNacitaj.UseVisualStyleBackColor = true;
            this.btnNacitaj.Click += new System.EventHandler(this.btnNacitaj_Click);
            // 
            // btnVytvorVyvesku
            // 
            this.btnVytvorVyvesku.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVytvorVyvesku.Location = new System.Drawing.Point(659, 49);
            this.btnVytvorVyvesku.Margin = new System.Windows.Forms.Padding(5, 40, 5, 5);
            this.btnVytvorVyvesku.Name = "btnVytvorVyvesku";
            this.btnVytvorVyvesku.Size = new System.Drawing.Size(133, 32);
            this.btnVytvorVyvesku.TabIndex = 8;
            this.btnVytvorVyvesku.Text = "Vytvor vyvesku";
            this.btnVytvorVyvesku.UseVisualStyleBackColor = true;
            this.btnVytvorVyvesku.Click += new System.EventHandler(this.btnGenerujVyvesku_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 395);
            this.Controls.Add(this.btnVytvorVyvesku);
            this.Controls.Add(this.btnNacitaj);
            this.Controls.Add(this.btnZapisDoSuboru);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblSelekt);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxSelektFiltra);
            this.Controls.Add(this.cbxSelektProjektu);
            this.Controls.Add(this.dgvVlaky);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVlaky)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxSelektProjektu;
        private System.Windows.Forms.ComboBox cbxSelektFiltra;
        private System.Windows.Forms.DataGridView dgvVlaky;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSelekt;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Button btnZapisDoSuboru;
        private System.Windows.Forms.Button btnNacitaj;
        private System.Windows.Forms.Button btnVytvorVyvesku;
    }
}

