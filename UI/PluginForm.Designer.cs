namespace XBEVMDGEN.UI
{
    partial class PluginForm
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
            this.btnMakeVMD = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMakeVMD
            // 
            this.btnMakeVMD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnMakeVMD.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnMakeVMD.FlatAppearance.BorderSize = 0;
            this.btnMakeVMD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakeVMD.Font = new System.Drawing.Font("Segoe UI", 32F);
            this.btnMakeVMD.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnMakeVMD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMakeVMD.Location = new System.Drawing.Point(12, 12);
            this.btnMakeVMD.Name = "btnMakeVMD";
            this.btnMakeVMD.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnMakeVMD.Size = new System.Drawing.Size(437, 109);
            this.btnMakeVMD.TabIndex = 138;
            this.btnMakeVMD.TabStop = false;
            this.btnMakeVMD.Tag = "color:dark2";
            this.btnMakeVMD.Text = "Generate VMDs";
            this.btnMakeVMD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMakeVMD.UseVisualStyleBackColor = false;
            this.btnMakeVMD.Click += new System.EventHandler(this.btnMakeVMD_Click);
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(461, 133);
            this.Controls.Add(this.btnMakeVMD);
            this.Name = "PluginForm";
            this.Tag = "color:dark";
            this.Text = "XBEVMDGEN";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnMakeVMD;
    }
}