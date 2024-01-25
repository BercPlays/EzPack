namespace EzPack
{
    partial class ProjectListItem
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
            this.title1 = new System.Windows.Forms.Label();
            this.desc1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.icon1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.icon1)).BeginInit();
            this.SuspendLayout();
            // 
            // title1
            // 
            this.title1.AutoSize = true;
            this.title1.Font = new System.Drawing.Font("Segoe UI Light", 15F);
            this.title1.Location = new System.Drawing.Point(94, 3);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(46, 28);
            this.title1.TabIndex = 1;
            this.title1.Text = "Title";
            // 
            // desc1
            // 
            this.desc1.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.desc1.Location = new System.Drawing.Point(95, 31);
            this.desc1.Name = "desc1";
            this.desc1.Size = new System.Drawing.Size(229, 45);
            this.desc1.TabIndex = 2;
            this.desc1.Text = "Desc";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.Location = new System.Drawing.Point(724, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "Szerkeztés";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.SystemColors.ControlDark;
            this.back.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back.Font = new System.Drawing.Font("Segoe UI Light", 9F);
            this.back.ForeColor = System.Drawing.Color.DarkRed;
            this.back.Location = new System.Drawing.Point(638, 25);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(80, 31);
            this.back.TabIndex = 7;
            this.back.Text = "Törlés";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // icon1
            // 
            this.icon1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.icon1.Location = new System.Drawing.Point(15, 3);
            this.icon1.Name = "icon1";
            this.icon1.Size = new System.Drawing.Size(73, 73);
            this.icon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.icon1.TabIndex = 0;
            this.icon1.TabStop = false;
            // 
            // ProjectListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.back);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.desc1);
            this.Controls.Add(this.title1);
            this.Controls.Add(this.icon1);
            this.Name = "ProjectListItem";
            this.Size = new System.Drawing.Size(840, 79);
            ((System.ComponentModel.ISupportInitialize)(this.icon1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox icon1;
        private System.Windows.Forms.Label title1;
        private System.Windows.Forms.Label desc1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button back;
    }
}
