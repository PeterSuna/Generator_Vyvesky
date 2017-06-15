namespace Zobrazovac_Dat
{
    partial class HlavneOkno
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
            this.btnNacitaj = new System.Windows.Forms.Button();
            this.btnNacitajTrasyBody = new System.Windows.Forms.Button();
            this.btnNacitajDopravneBody = new System.Windows.Forms.Button();
            this.btnNastav = new System.Windows.Forms.Button();
            this.btnDocx = new System.Windows.Forms.Button();
            this.btnGenerujOdchody = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVlaky)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVlaky
            // 
            this.dgvVlaky.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVlaky.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVlaky.Location = new System.Drawing.Point(12, 73);
            this.dgvVlaky.Name = "dgvVlaky";
            this.dgvVlaky.Size = new System.Drawing.Size(829, 407);
            this.dgvVlaky.TabIndex = 0;
            // 
            // btnNacitaj
            // 
            this.btnNacitaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNacitaj.Location = new System.Drawing.Point(615, 39);
            this.btnNacitaj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNacitaj.Name = "btnNacitaj";
            this.btnNacitaj.Size = new System.Drawing.Size(99, 26);
            this.btnNacitaj.TabIndex = 7;
            this.btnNacitaj.Text = "Zobraz vlaky";
            this.btnNacitaj.UseVisualStyleBackColor = true;
            this.btnNacitaj.Click += new System.EventHandler(this.btnNacitaj_Click);
            // 
            // btnNacitajTrasyBody
            // 
            this.btnNacitajTrasyBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNacitajTrasyBody.Location = new System.Drawing.Point(722, 4);
            this.btnNacitajTrasyBody.Margin = new System.Windows.Forms.Padding(4, 4, 65, 4);
            this.btnNacitajTrasyBody.Name = "btnNacitajTrasyBody";
            this.btnNacitajTrasyBody.Size = new System.Drawing.Size(118, 25);
            this.btnNacitajTrasyBody.TabIndex = 10;
            this.btnNacitajTrasyBody.Text = "Zobraz T. body";
            this.btnNacitajTrasyBody.UseVisualStyleBackColor = true;
            this.btnNacitajTrasyBody.Click += new System.EventHandler(this.btnNacitajTrasyBody_Click);
            // 
            // btnNacitajDopravneBody
            // 
            this.btnNacitajDopravneBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNacitajDopravneBody.Location = new System.Drawing.Point(722, 39);
            this.btnNacitajDopravneBody.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNacitajDopravneBody.Name = "btnNacitajDopravneBody";
            this.btnNacitajDopravneBody.Size = new System.Drawing.Size(118, 26);
            this.btnNacitajDopravneBody.TabIndex = 13;
            this.btnNacitajDopravneBody.Text = "Zobraz D. body";
            this.btnNacitajDopravneBody.UseVisualStyleBackColor = true;
            this.btnNacitajDopravneBody.Click += new System.EventHandler(this.btnNacitajDopravneBody_Click);
            // 
            // btnNastav
            // 
            this.btnNastav.Location = new System.Drawing.Point(175, 4);
            this.btnNastav.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNastav.Name = "btnNastav";
            this.btnNastav.Size = new System.Drawing.Size(82, 63);
            this.btnNastav.TabIndex = 14;
            this.btnNastav.Text = "Zmenit nastavenia";
            this.btnNastav.UseVisualStyleBackColor = true;
            this.btnNastav.Click += new System.EventHandler(this.btnNastav_Click);
            // 
            // btnDocx
            // 
            this.btnDocx.Location = new System.Drawing.Point(13, 40);
            this.btnDocx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDocx.Name = "btnDocx";
            this.btnDocx.Size = new System.Drawing.Size(154, 26);
            this.btnDocx.TabIndex = 15;
            this.btnDocx.Text = "Generuj vývesku príchodov";
            this.btnDocx.UseVisualStyleBackColor = true;
            this.btnDocx.Click += new System.EventHandler(this.btnDocx_Click);
            // 
            // btnGenerujOdchody
            // 
            this.btnGenerujOdchody.Location = new System.Drawing.Point(13, 4);
            this.btnGenerujOdchody.Name = "btnGenerujOdchody";
            this.btnGenerujOdchody.Size = new System.Drawing.Size(154, 27);
            this.btnGenerujOdchody.TabIndex = 16;
            this.btnGenerujOdchody.Text = "Generuj vývesku odchodov";
            this.btnGenerujOdchody.UseVisualStyleBackColor = true;
            this.btnGenerujOdchody.Click += new System.EventHandler(this.btnGenerujOdchody_Click);
            // 
            // HlavneOkno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 492);
            this.Controls.Add(this.btnGenerujOdchody);
            this.Controls.Add(this.btnDocx);
            this.Controls.Add(this.btnNastav);
            this.Controls.Add(this.btnNacitajDopravneBody);
            this.Controls.Add(this.btnNacitajTrasyBody);
            this.Controls.Add(this.btnNacitaj);
            this.Controls.Add(this.dgvVlaky);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "HlavneOkno";
            this.Text = "Generátor dokumentu";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVlaky)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvVlaky;
        private System.Windows.Forms.Button btnNacitaj;
        private System.Windows.Forms.Button btnNacitajTrasyBody;
        private System.Windows.Forms.Button btnNacitajDopravneBody;
        private System.Windows.Forms.Button btnNastav;
        private System.Windows.Forms.Button btnDocx;
        private System.Windows.Forms.Button btnGenerujOdchody;
    }
}

