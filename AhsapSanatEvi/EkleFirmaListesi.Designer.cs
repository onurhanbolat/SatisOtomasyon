
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
            this.button1 = new System.Windows.Forms.Button();
            this.EkleFirmaListeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // EkleFirmaListeLabel
            // 
            this.EkleFirmaListeLabel.AutoSize = true;
            this.EkleFirmaListeLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
            this.Controls.Add(this.button1);
            this.Name = "EkleFirmaListesi";
            this.Size = new System.Drawing.Size(165, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label EkleFirmaListeLabel;
    }
}
