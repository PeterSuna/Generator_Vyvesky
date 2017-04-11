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
            this.dgvVlaky = new System.Windows.Forms.DataGridView();
            this.btnGenVlaky = new System.Windows.Forms.Button();
            this.btnZapisDoSuboru = new System.Windows.Forms.Button();
            this.btnNacitaj = new System.Windows.Forms.Button();
            this.btnVytvorVyvesku = new System.Windows.Forms.Button();
            this.btnGenTrasaBody = new System.Windows.Forms.Button();
            this.btnNacitajTrasyBody = new System.Windows.Forms.Button();
            this.btnGenerujDopBod = new System.Windows.Forms.Button();
            this.btnGenerujData = new System.Windows.Forms.Button();
            this.btnNacitajDopravneBody = new System.Windows.Forms.Button();
            this.btnNastav = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVlaky)).BeginInit();
            this.SuspendLayout();
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
            this.dgvVlaky.Size = new System.Drawing.Size(1322, 556);
            this.dgvVlaky.TabIndex = 0;
            // 
            // btnGenVlaky
            // 
            this.btnGenVlaky.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenVlaky.Location = new System.Drawing.Point(797, 49);
            this.btnGenVlaky.Margin = new System.Windows.Forms.Padding(5);
            this.btnGenVlaky.Name = "btnGenVlaky";
            this.btnGenVlaky.Size = new System.Drawing.Size(120, 31);
            this.btnGenVlaky.TabIndex = 3;
            this.btnGenVlaky.Text = "Generuj Vlaky";
            this.btnGenVlaky.UseVisualStyleBackColor = true;
            this.btnGenVlaky.Click += new System.EventHandler(this.btnGenVlaky_Click);
            // 
            // btnZapisDoSuboru
            // 
            this.btnZapisDoSuboru.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZapisDoSuboru.Location = new System.Drawing.Point(1265, 49);
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
            this.btnNacitaj.Location = new System.Drawing.Point(1093, 49);
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
            this.btnVytvorVyvesku.Location = new System.Drawing.Point(927, 49);
            this.btnVytvorVyvesku.Margin = new System.Windows.Forms.Padding(5);
            this.btnVytvorVyvesku.Name = "btnVytvorVyvesku";
            this.btnVytvorVyvesku.Size = new System.Drawing.Size(155, 32);
            this.btnVytvorVyvesku.TabIndex = 8;
            this.btnVytvorVyvesku.Text = "Vytvor vyvesku";
            this.btnVytvorVyvesku.UseVisualStyleBackColor = true;
            this.btnVytvorVyvesku.Click += new System.EventHandler(this.btnGenerujVyvesku_Click);
            // 
            // btnGenTrasaBody
            // 
            this.btnGenTrasaBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenTrasaBody.Location = new System.Drawing.Point(797, 6);
            this.btnGenTrasaBody.Margin = new System.Windows.Forms.Padding(5);
            this.btnGenTrasaBody.Name = "btnGenTrasaBody";
            this.btnGenTrasaBody.Size = new System.Drawing.Size(120, 33);
            this.btnGenTrasaBody.TabIndex = 9;
            this.btnGenTrasaBody.Text = "Generuj Body";
            this.btnGenTrasaBody.UseVisualStyleBackColor = true;
            this.btnGenTrasaBody.Click += new System.EventHandler(this.btnGenTrasaBody_Click);
            // 
            // btnNacitajTrasyBody
            // 
            this.btnNacitajTrasyBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNacitajTrasyBody.Location = new System.Drawing.Point(1095, 8);
            this.btnNacitajTrasyBody.Margin = new System.Windows.Forms.Padding(5, 5, 87, 5);
            this.btnNacitajTrasyBody.Name = "btnNacitajTrasyBody";
            this.btnNacitajTrasyBody.Size = new System.Drawing.Size(163, 31);
            this.btnNacitajTrasyBody.TabIndex = 10;
            this.btnNacitajTrasyBody.Text = "Načítaj uložené body";
            this.btnNacitajTrasyBody.UseVisualStyleBackColor = true;
            this.btnNacitajTrasyBody.Click += new System.EventHandler(this.btnNacitajTrasyBody_Click);
            // 
            // btnGenerujDopBod
            // 
            this.btnGenerujDopBod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerujDopBod.Location = new System.Drawing.Point(644, 49);
            this.btnGenerujDopBod.Margin = new System.Windows.Forms.Padding(5);
            this.btnGenerujDopBod.Name = "btnGenerujDopBod";
            this.btnGenerujDopBod.Size = new System.Drawing.Size(143, 32);
            this.btnGenerujDopBod.TabIndex = 11;
            this.btnGenerujDopBod.Text = "Generuj D. Body";
            this.btnGenerujDopBod.UseVisualStyleBackColor = true;
            this.btnGenerujDopBod.Click += new System.EventHandler(this.btnGenerujDopBod_Click);
            // 
            // btnGenerujData
            // 
            this.btnGenerujData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerujData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnGenerujData.Location = new System.Drawing.Point(16, 6);
            this.btnGenerujData.Margin = new System.Windows.Forms.Padding(5);
            this.btnGenerujData.Name = "btnGenerujData";
            this.btnGenerujData.Size = new System.Drawing.Size(195, 74);
            this.btnGenerujData.TabIndex = 12;
            this.btnGenerujData.Text = "Zobraz vlaky prechádzajúce stanicov";
            this.btnGenerujData.UseVisualStyleBackColor = true;
            this.btnGenerujData.Click += new System.EventHandler(this.btnGenerujData_Click);
            // 
            // btnNacitajDopravneBody
            // 
            this.btnNacitajDopravneBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNacitajDopravneBody.Location = new System.Drawing.Point(927, 6);
            this.btnNacitajDopravneBody.Margin = new System.Windows.Forms.Padding(5);
            this.btnNacitajDopravneBody.Name = "btnNacitajDopravneBody";
            this.btnNacitajDopravneBody.Size = new System.Drawing.Size(158, 33);
            this.btnNacitajDopravneBody.TabIndex = 13;
            this.btnNacitajDopravneBody.Text = "Nacitaj dopravne body";
            this.btnNacitajDopravneBody.UseVisualStyleBackColor = true;
            this.btnNacitajDopravneBody.Click += new System.EventHandler(this.btnNacitajDopravneBody_Click);
            // 
            // btnNastav
            // 
            this.btnNastav.Location = new System.Drawing.Point(219, 49);
            this.btnNastav.Name = "btnNastav";
            this.btnNastav.Size = new System.Drawing.Size(151, 31);
            this.btnNastav.TabIndex = 14;
            this.btnNastav.Text = "Zmenit nastavenia";
            this.btnNastav.UseVisualStyleBackColor = true;
            this.btnNastav.Click += new System.EventHandler(this.btnNastav_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 661);
            this.Controls.Add(this.btnNastav);
            this.Controls.Add(this.btnNacitajDopravneBody);
            this.Controls.Add(this.btnGenerujData);
            this.Controls.Add(this.btnGenerujDopBod);
            this.Controls.Add(this.btnNacitajTrasyBody);
            this.Controls.Add(this.btnGenTrasaBody);
            this.Controls.Add(this.btnVytvorVyvesku);
            this.Controls.Add(this.btnNacitaj);
            this.Controls.Add(this.btnZapisDoSuboru);
            this.Controls.Add(this.btnGenVlaky);
            this.Controls.Add(this.dgvVlaky);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "GenVV";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVlaky)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvVlaky;
        private System.Windows.Forms.Button btnGenVlaky;
        private System.Windows.Forms.Button btnZapisDoSuboru;
        private System.Windows.Forms.Button btnNacitaj;
        private System.Windows.Forms.Button btnVytvorVyvesku;
        private System.Windows.Forms.Button btnGenTrasaBody;
        private System.Windows.Forms.Button btnNacitajTrasyBody;
        private System.Windows.Forms.Button btnGenerujDopBod;
        private System.Windows.Forms.Button btnGenerujData;
        private System.Windows.Forms.Button btnNacitajDopravneBody;
        private System.Windows.Forms.Button btnNastav;
    }
}

