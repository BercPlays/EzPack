using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static EzPack.Globals.Enums;
using static EzPack.HelperClasses.DirectoryManager;

namespace EzPack
{
    public partial class NewProjForm : Form
    {
        Enum selectedType = WorkspaceType.Vanilla;
        bool NameValid = false;
        bool versionValid = false;
        bool packtextureValid = false;

        public NewProjForm()
        {
            InitializeComponent();
            
        }
        
        private void SetType(ProjectType cont)
        {
            workspaceType.Text = "Projekt tipus: ";
            workspaceType.Text = workspaceType.Text + cont.Title;
            pictureBox1.Image = cont.Icon;
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


        private void NewProjForm_Load(object sender, EventArgs e)
        {

        }

        #region clickEvents
        private void projectType1_MouseClick(object sender, MouseEventArgs e)
        {
            SetType(projectType1);
            selectedType = WorkspaceType.Vanilla;
            
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = openFileDialog1.FileName;
                packtextureValid = true;
                button1.ForeColor = Color.Black;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NameValid && versionValid && packtextureValid)
            {
                Application.OpenForms[0].Hide();

                AssetInstaller assetInstaller = new AssetInstaller(textBox1.Text, int.Parse(textBox2.Text),selectedType,pictureBox2.ImageLocation);
                assetInstaller.ShowDialog();
            }
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                if (Directory.Exists(GetCurrentWorkspaceDir() + @"\" + textBox1.Text) == false)
                {
                    NameValid = true;
                    textBox1.BackColor = Color.DarkGray;
                }
                else
                {
                    NameValid = false;
                    textBox1.BackColor = Color.IndianRed;
                }
            }
            else
            {
                NameValid = false;
                textBox1.BackColor = Color.IndianRed;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (AreDigitsOnly(textBox2.Text) && textBox2.Text.Length <= 2)
            {
                versionValid = true;
                textBox2.BackColor = Color.DarkGray;

            }
            else
            {
                versionValid = false;
                textBox2.BackColor = Color.IndianRed;
            }
        }
    }
}
