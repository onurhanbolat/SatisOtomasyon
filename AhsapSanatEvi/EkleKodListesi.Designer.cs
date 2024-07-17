
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
            this.BtnFirmaSec = new System.Windows.Forms.Button();
            this.BtnKodListeSil = new System.Windows.Forms.Button();
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
            // BtnFirmaSec
            // 
            this.BtnFirmaSec.BackColor = System.Drawing.Color.White;
            this.BtnFirmaSec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnFirmaSec.FlatAppearance.BorderSize = 0;
            this.BtnFirmaSec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFirmaSec.Image = ((System.Drawing.Image)(resources.GetObject("BtnFirmaSec.Image")));
            this.BtnFirmaSec.Location = new System.Drawing.Point(2, 2);
            this.BtnFirmaSec.Name = "BtnFirmaSec";
            this.BtnFirmaSec.Size = new System.Drawing.Size(23, 23);
            this.BtnFirmaSec.TabIndex = 2;
            this.BtnFirmaSec.UseVisualStyleBackColor = false;
            // 
            // BtnKodListeSil
            // 
            this.BtnKodListeSil.BackColor = System.Drawing.Color.White;
            this.BtnKodListeSil.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnKodListeSil.FlatAppearance.BorderSize = 0;
            this.BtnKodListeSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnKodListeSil.Image = ((System.Drawing.Image)(resources.GetObject("BtnKodListeSil.Image")));
            this.BtnKodListeSil.Location = new System.Drawing.Point(112, 2);
            this.BtnKodListeSil.Name = "BtnKodListeSil";
            this.BtnKodListeSil.Size = new System.Drawing.Size(23, 23);
            this.BtnKodListeSil.TabIndex = 4;
            this.BtnKodListeSil.UseVisualStyleBackColor = false;
            this.BtnKodListeSil.Click += new System.EventHandler(this.BtnKodListeSil_Click);
            // 
            // EkleKodListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnKodListeSil);
            this.Controls.Add(this.EkleKodListeLabel);
            this.Controls.Add(this.BtnFirmaSec);
            this.Name = "EkleKodListesi";
            this.Size = new System.Drawing.Size(138, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label EkleKodListeLabel;
        private System.Windows.Forms.Button BtnFirmaSec;
        private System.Windows.Forms.Button BtnKodListeSil;
    }
}
