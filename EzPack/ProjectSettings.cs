using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EzPack.Locals.LocalVars;
using static EzPack.HelperClasses.PictureTools;
using static EzPack.HelperClasses.DirectoryManager;

namespace EzPack
{
    public partial class ProjectSettings : Form
    {
        bool nameValid = false;
        bool displaynameValid = false;
        bool descValid = false;
        bool versionValid = false;

        public ProjectSettings()
        {
            InitializeComponent();
            if (ProjectName != null) { textBox1.Text = ProjectName; }
            if (ProjectDisplayName != null) { textBox2.Text = ProjectDisplayName; }
            if (ProjectVersion != 0) { textBox3.Text = ProjectVersion.ToString(); }
            if (ProjectDesc != null) { textBox4.Text = ProjectDesc; }

            if (_imageDir != null)
            {
                DrawPixelModePictureBox(Image.FromFile(_imageDir), pictureBox1, 75);
            }
            else
            {
                DrawPixelModePictureBox(Image.FromFile(GetCurrentWorkspaceDir() + @"\" + ProjectName + @"\files\pack.png"), pictureBox1, 75);
            }
            

            
        }

        private void newProjButton_Click(object sender, EventArgs e)
        {
            if (displaynameValid == false) { return; }
            if (nameValid == false) { return; }
            if (versionValid == false) { return; }
            if (descValid == false) { return; }

            ProjectName = textBox1.Text;
            ProjectDisplayName = textBox2.Text;
            ProjectVersion = int.Parse(textBox3.Text);
            ProjectDesc = textBox4.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                nameValid = true;
                textBox1.BackColor = Color.DarkGray;
            }
            else
            {
                nameValid = false;
                textBox1.BackColor = Color.IndianRed;
            }
            checkValid();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                displaynameValid = true;
                textBox2.BackColor = Color.DarkGray;
            }
            else
            {
                displaynameValid = false;
                textBox2.BackColor = Color.IndianRed;
            }
            checkValid();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (AreDigitsOnly(textBox3.Text) && textBox3.Text.Length <= 2)
            {
                versionValid = true;
                textBox3.BackColor = Color.DarkGray;
            }
            else
            {
                versionValid = false;
                textBox3.BackColor = Color.IndianRed;
            }
            checkValid();
        }
        private bool AreDigitsOnly(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            foreach (char character in text)
            {
                if (character < '0' || character > '9')
                    return false;
            }

            return true;

        }

        private void ProjectSettings_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                descValid = true;
                textBox4.BackColor = Color.DarkGray;
            }
            else
            {
                descValid = false;
                textBox4.BackColor = Color.IndianRed;
            }
            checkValid();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.digminecraft.com/lists/color_list_pc.php");
        }
        private void checkValid()
        {
            newProjButton.Enabled = false;
            if (displaynameValid == false) { return; }
            if (nameValid == false) { return; }
            if (versionValid == false) { return; }
            if (descValid == false) { return; }
            newProjButton.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _imageDir = openFileDialog1.FileName;
                FileInfo imageFile = new FileInfo(_imageDir);
                Image image = Image.FromFile(imageFile.FullName);
                DrawPixelModePictureBox(image, pictureBox1, 75);
                imageChanged = true;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
