
namespace AhsapSanatEvi
{
    partial class EkleKodListesi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EkleKodListesi));
            this.EkleKodListeLabel = new System.Windows.Forms.Label();
            this.BtnKodSec = new System.Windows.Forms.Button();
            this.BtnKodListeSil = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // EkleKodListeLabel
            // 
            this.EkleKodListeLabel.AutoSize = true;
            this.EkleKodListeLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.EkleKodListeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.EkleKodListeLabel.Location = new System.Drawing.Point(26, 7);
            this.EkleKodListeLabel.Name = "EkleKodListeLabel";
            this.EkleKodListeLabel.Size = new System.Drawing.Size(47, 19);
            this.EkleKodListeLabel.TabIndex = 3;
            this.EkleKodListeLabel.Text = "LABEL";
            // 
            // BtnKodSec
            // 
            this.BtnKodSec.BackColor = System.Drawing.Color.Transparent;
            this.BtnKodSec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnKodSec.FlatAppearance.BorderSize = 0;
            this.BtnKodSec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnKodSec.Image = ((System.Drawing.Image)(resources.GetObject("BtnKodSec.Image")));
            this.BtnKodSec.Location = new System.Drawing.Point(2, 2);
            this.BtnKodSec.Name = "BtnKodSec";
            this.BtnKodSec.Size = new System.Drawing.Size(23, 23);
            this.BtnKodSec.TabIndex = 2;
            this.BtnKodSec.UseVisualStyleBackColor = false;
            this.BtnKodSec.Click += new System.EventHandler(this.BtnKodSec_Click);
            // 
            // BtnKodListeSil
            // 
            this.BtnKodListeSil.BackColor = System.Drawing.Color.Transparent;
            this.BtnKodListeSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnKodListeSil.FlatAppearance.BorderSize = 0;
            this.BtnKodListeSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnKodListeSil.Image = ((System.Drawing.Image)(resources.GetObject("BtnKodListeSil.Image")));
            this.BtnKodListeSil.Location = new System.Drawing.Point(117, 2);
            this.BtnKodListeSil.Name = "BtnKodListeSil";
            this.BtnKodListeSil.Size = new System.Drawing.Size(23, 23);
            this.BtnKodListeSil.TabIndex = 4;
            this.BtnKodListeSil.UseVisualStyleBackColor = false;
            this.BtnKodListeSil.Click += new System.EventHandler(this.BtnKodListeSil_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 2);
            this.panel1.TabIndex = 5;
            // 
            // EkleKodListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnKodListeSil);
            this.Controls.Add(this.EkleKodListeLabel);
            this.Controls.Add(this.BtnKodSec);
            this.Name = "EkleKodListesi";
            this.Size = new System.Drawing.Size(143, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label EkleKodListeLabel;
        private System.Windows.Forms.Button BtnKodListeSil;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button BtnKodSec;
    }
}
