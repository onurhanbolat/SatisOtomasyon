
namespace AhsapSanatEvi
{
    partial class MusteriListesiSatis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusteriListesiSatis));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblListeSatisMusteriAd = new System.Windows.Forms.Label();
            this.LblListeSatisMusteriId = new System.Windows.Forms.Label();
            this.BtnMusteriSec = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(18, 53);
            this.panel1.TabIndex = 2;
            // 
            // LblListeSatisMusteriAd
            // 
            this.LblListeSatisMusteriAd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblListeSatisMusteriAd.AutoSize = true;
            this.LblListeSatisMusteriAd.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblListeSatisMusteriAd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.LblListeSatisMusteriAd.Location = new System.Drawing.Point(108, 17);
            this.LblListeSatisMusteriAd.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.LblListeSatisMusteriAd.Name = "LblListeSatisMusteriAd";
            this.LblListeSatisMusteriAd.Size = new System.Drawing.Size(82, 20);
            this.LblListeSatisMusteriAd.TabIndex = 9;
            this.LblListeSatisMusteriAd.Text = "Ad Soyad: ";
            // 
            // LblListeSatisMusteriId
            // 
            this.LblListeSatisMusteriId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LblListeSatisMusteriId.AutoSize = true;
            this.LblListeSatisMusteriId.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LblListeSatisMusteriId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LblListeSatisMusteriId.Location = new System.Drawing.Point(28, 17);
            this.LblListeSatisMusteriId.Margin = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.LblListeSatisMusteriId.Name = "LblListeSatisMusteriId";
            this.LblListeSatisMusteriId.Size = new System.Drawing.Size(28, 20);
            this.LblListeSatisMusteriId.TabIndex = 10;
            this.LblListeSatisMusteriId.Text = "ID:";
            // 
            // BtnMusteriSec
            // 
            this.BtnMusteriSec.BackColor = System.Drawing.Color.Transparent;
            this.BtnMusteriSec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnMusteriSec.FlatAppearance.BorderSize = 0;
            this.BtnMusteriSec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMusteriSec.Image = ((System.Drawing.Image)(resources.GetObject("BtnMusteriSec.Image")));
            this.BtnMusteriSec.Location = new System.Drawing.Point(355, 14);
            this.BtnMusteriSec.Margin = new System.Windows.Forms.Padding(6);
            this.BtnMusteriSec.Name = "BtnMusteriSec";
            this.BtnMusteriSec.Size = new System.Drawing.Size(25, 25);
            this.BtnMusteriSec.TabIndex = 11;
            this.BtnMusteriSec.UseVisualStyleBackColor = false;
            this.BtnMusteriSec.Click += new System.EventHandler(this.BtnMusteriSec_Click);
            // 
            // MusteriListesiSatis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.BtnMusteriSec);
            this.Controls.Add(this.LblListeSatisMusteriId);
            this.Controls.Add(this.LblListeSatisMusteriAd);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MusteriListesiSatis";
            this.Size = new System.Drawing.Size(390, 53);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label LblListeSatisMusteriAd;
        public System.Windows.Forms.Label LblListeSatisMusteriId;
        public System.Windows.Forms.Button BtnMusteriSec;
    }
}
