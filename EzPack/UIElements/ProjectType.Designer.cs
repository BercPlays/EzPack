namespace EzPack
{
    partial class ProjectType
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox icon1;
        private System.Windows.Forms.Label title1;

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
            this.title1 = new System.Windows.Forms.Label();
            this.icon1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.icon1)).BeginInit();
            this.SuspendLayout();
            // 
            // title1
            // 
            this.title1.AutoSize = true;
            this.title1.Font = new System.Drawing.Font("Segoe UI Light", 15F);
            this.title1.Location = new System.Drawing.Point(52, 3);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(46, 28);
            this.title1.TabIndex = 1;
            this.title1.Text = "Title";
            this.title1.MouseEnter += new System.EventHandler(this.title1_MouseEnter);
            this.title1.MouseLeave += new System.EventHandler(this.title1_MouseLeave);
            // 
            // icon1
            // 
            this.icon1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.icon1.Location = new System.Drawing.Point(15, 3);
            this.icon1.Name = "icon1";
            this.icon1.Size = new System.Drawing.Size(31, 29);
            this.icon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.icon1.TabIndex = 0;
            this.icon1.TabStop = false;
            this.icon1.MouseEnter += new System.EventHandler(this.icon1_MouseEnter);
            this.icon1.MouseLeave += new System.EventHandler(this.icon1_MouseLeave);
            // 
            // ProjectType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.title1);
            this.Controls.Add(this.icon1);
            this.Name = "ProjectType";
            this.Size = new System.Drawing.Size(202, 35);
            this.MouseEnter += new System.EventHandler(this.ProjectType_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ProjectType_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.icon1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
    }
}
