
namespace AhsapSanatEvi
{
    partial class EkleFirmaListesi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EkleFirmaListesi));
            this.BtnFirmaSec = new System.Windows.Forms.Button();
            this.EkleFirmaListeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.BtnFirmaSec.TabIndex = 0;
            this.BtnFirmaSec.UseVisualStyleBackColor = false;
            this.BtnFirmaSec.Click += new System.EventHandler(this.BtnFirmaSec_Click);
            // 
            // EkleFirmaListeLabel
            // 
            this.EkleFirmaListeLabel.AutoSize = true;
            this.EkleFirmaListeLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.EkleFirmaListeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.EkleFirmaListeLabel.Location = new System.Drawing.Point(26, 7);
            this.EkleFirmaListeLabel.Name = "EkleFirmaListeLabel";
            this.EkleFirmaListeLabel.Size = new System.Drawing.Size(47, 19);
            this.EkleFirmaListeLabel.TabIndex = 1;
            this.EkleFirmaListeLabel.Text = "LABEL";
            // 
            // EkleFirmaListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EkleFirmaListeLabel);
            this.Controls.Add(this.BtnFirmaSec);
            this.Name = "EkleFirmaListesi";
            this.Size = new System.Drawing.Size(165, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnFirmaSec;
        public System.Windows.Forms.Label EkleFirmaListeLabel;
    }
}
