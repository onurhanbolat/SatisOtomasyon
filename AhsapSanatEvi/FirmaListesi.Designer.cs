
namespace AhsapSanatEvi
{
    partial class FirmaListesi
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblListeFirmaAd = new System.Windows.Forms.Label();
            this.LblListeFirmaId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(15, 69);
            this.panel1.TabIndex = 0;
            // 
            // LblListeFirmaAd
            // 
            this.LblListeFirmaAd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblListeFirmaAd.AutoSize = true;
            this.LblListeFirmaAd.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblListeFirmaAd.Location = new System.Drawing.Point(146, 24);
            this.LblListeFirmaAd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblListeFirmaAd.Name = "LblListeFirmaAd";
            this.LblListeFirmaAd.Size = new System.Drawing.Size(32, 20);
            this.LblListeFirmaAd.TabIndex = 6;
            this.LblListeFirmaAd.Text = "Ad:";
            // 
            // LblListeFirmaId
            // 
            this.LblListeFirmaId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblListeFirmaId.AutoSize = true;
            this.LblListeFirmaId.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblListeFirmaId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LblListeFirmaId.Location = new System.Drawing.Point(23, 24);
            this.LblListeFirmaId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblListeFirmaId.Name = "LblListeFirmaId";
            this.LblListeFirmaId.Size = new System.Drawing.Size(28, 20);
            this.LblListeFirmaId.TabIndex = 5;
            this.LblListeFirmaId.Text = "ID:";
            this.LblListeFirmaId.Click += new System.EventHandler(this.LblListeFirmaId_Click);
            // 
            // FirmaListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.LblListeFirmaAd);
            this.Controls.Add(this.LblListeFirmaId);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FirmaListesi";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(684, 69);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label LblListeFirmaAd;
        public System.Windows.Forms.Label LblListeFirmaId;
    }
}
