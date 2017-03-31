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
            this.btnGenTrasaBody = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVlaky)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxSelektProjektu
            // 
            this.cbxSelektProjektu.FormattingEnabled = true;
            this.cbxSelektProjektu.Location = new System.Drawing.Point(10, 46);
            this.cbxSelektProjektu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxSelektProjektu.Name = "cbxSelektProjektu";
            this.cbxSelektProjektu.Size = new System.Drawing.Size(167, 21);
            this.cbxSelektProjektu.TabIndex = 1;
            this.cbxSelektProjektu.Text = "Vyber Selekt";
            // 
            // cbxSelektFiltra
            // 
            this.cbxSelektFiltra.FormattingEnabled = true;
            this.cbxSelektFiltra.Location = new System.Drawing.Point(184, 46);
            this.cbxSelektFiltra.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxSelektFiltra.Name = "cbxSelektFiltra";
            this.cbxSelektFiltra.Size = new System.Drawing.Size(167, 21);
            this.cbxSelektFiltra.TabIndex = 2;
            this.cbxSelektFiltra.Text = "Vyber Filter";
            // 
            // dgvVlaky
            // 
            this.dgvVlaky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVlaky.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVlaky.Location = new System.Drawing.Point(12, 73);
            this.dgvVlaky.Name = "dgvVlaky";
            this.dgvVlaky.Size = new System.Drawing.Size(784, 248);
            this.dgvVlaky.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(612, 11);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(186, 20);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblSelekt
            // 
            this.lblSelekt.AutoSize = true;
            this.lblSelekt.Location = new System.Drawing.Point(10, 18);
            this.lblSelekt.Margin = new System.Windows.Forms.Padding(4, 11, 10, 11);
            this.lblSelekt.Name = "lblSelekt";
            this.lblSelekt.Size = new System.Drawing.Size(89, 13);
            this.lblSelekt.TabIndex = 4;
            this.lblSelekt.Text = "Vyber z projektov";
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(182, 18);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(10, 11, 10, 11);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(70, 13);
            this.lblFilter.TabIndex = 5;
            this.lblFilter.Text = "Vyber z filtrov";
            // 
            // btnZapisDoSuboru
            // 
            this.btnZapisDoSuboru.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZapisDoSuboru.Location = new System.Drawing.Point(741, 40);
            this.btnZapisDoSuboru.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnZapisDoSuboru.Name = "btnZapisDoSuboru";
            this.btnZapisDoSuboru.Size = new System.Drawing.Size(56, 26);
            this.btnZapisDoSuboru.TabIndex = 6;
            this.btnZapisDoSuboru.Text = "Ulozit Vlaky";
            this.btnZapisDoSuboru.UseVisualStyleBackColor = true;
            this.btnZapisDoSuboru.Click += new System.EventHandler(this.btnZapisDoSuboru_Click);
            // 
            // btnNacitaj
            // 
            this.btnNacitaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNacitaj.Location = new System.Drawing.Point(612, 40);
            this.btnNacitaj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNacitaj.Name = "btnNacitaj";
            this.btnNacitaj.Size = new System.Drawing.Size(122, 26);
            this.btnNacitaj.TabIndex = 7;
            this.btnNacitaj.Text = "Nacitat ulozene vlaky";
            this.btnNacitaj.UseVisualStyleBackColor = true;
            this.btnNacitaj.Click += new System.EventHandler(this.btnNacitaj_Click);
            // 
            // btnVytvorVyvesku
            // 
            this.btnVytvorVyvesku.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVytvorVyvesku.Location = new System.Drawing.Point(504, 40);
            this.btnVytvorVyvesku.Margin = new System.Windows.Forms.Padding(4, 32, 4, 4);
            this.btnVytvorVyvesku.Name = "btnVytvorVyvesku";
            this.btnVytvorVyvesku.Size = new System.Drawing.Size(100, 26);
            this.btnVytvorVyvesku.TabIndex = 8;
            this.btnVytvorVyvesku.Text = "Vytvor vyvesku";
            this.btnVytvorVyvesku.UseVisualStyleBackColor = true;
            this.btnVytvorVyvesku.Click += new System.EventHandler(this.btnGenerujVyvesku_Click);
            // 
            // btnGenTrasaBody
            // 
            this.btnGenTrasaBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenTrasaBody.Location = new System.Drawing.Point(422, 44);
            this.btnGenTrasaBody.Name = "btnGenTrasaBody";
            this.btnGenTrasaBody.Size = new System.Drawing.Size(75, 23);
            this.btnGenTrasaBody.TabIndex = 9;
            this.btnGenTrasaBody.Text = "GenTrasaBody";
            this.btnGenTrasaBody.UseVisualStyleBackColor = true;
            this.btnGenTrasaBody.Click += new System.EventHandler(this.btnGenTrasaBody_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 333);
            this.Controls.Add(this.btnGenTrasaBody);
            this.Controls.Add(this.btnVytvorVyvesku);
            this.Controls.Add(this.btnNacitaj);
            this.Controls.Add(this.btnZapisDoSuboru);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblSelekt);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxSelektFiltra);
            this.Controls.Add(this.cbxSelektProjektu);
            this.Controls.Add(this.dgvVlaky);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button btnGenTrasaBody;
    }
}

