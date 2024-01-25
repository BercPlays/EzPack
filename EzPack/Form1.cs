using EzPack.HelperClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EzPack.HelperClasses.DirectoryManager;

namespace EzPack
{
    public partial class Form1 : Form
    {
        string dir;
        string Version = "0.1";
        bool _WorkspaceDirValid = false;
        DirectoryInfo localDir;
        bool WorkspaceDirValid 
        {
            get { return _WorkspaceDirValid; }
            set {_WorkspaceDirValid = value; newProjButton.Enabled = value; label4.Visible = value; }
        }

        public Form1()
        {
            InitializeComponent();
            
            if (File.Exists(GetJointedFile("dir.txt")) == false)
            {
                var file = File.CreateText(GetJointedFile("dir.txt"));
                file.Write(getCurrentDir().Trim());
                file.Close();
            }

            using (StreamReader sr = new StreamReader(GetJointedFile("dir.txt")))
            {
                
                dir = sr.ReadLine();
                sr.Close();
            }
            init = true;
            if (File.ReadAllText(GetJointedFile("dir.txt")) == getCurrentDir()) 
            {
                WorkspaceDirValid = false;
               
            }
            try
            {
                localDir = new DirectoryInfo(GetCurrentWorkspaceDir());

            }
            catch (Exception)
            {
                _WorkspaceDirValid=false;
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshProjects();
            if (FileExists("dir.txt"))
            {
                using (StreamReader sr = new StreamReader(GetJointedFile("dir.txt")))
                {
                    
                    if (!string.IsNullOrWhiteSpace(sr.ReadToEnd()))
                    {
                        label4.Visible = true;
                        WorkspaceDirValid = true;
                        label4.Text = $"Projektek keresése itt: {dir}";
                    }
                    sr.Close();
                }
            }
            this.Text = this.Text + " " + Version;
        }
        

        private void newProjButton_Click(object sender, EventArgs e)
        {
            NewProjForm newProjForm = new NewProjForm();
            newProjForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(GetJointedFile("dir.txt")))
                {
                    sw.WriteLine(folderBrowserDialog1.SelectedPath);
                    label4.Text = "Projektek keresése itt: ";
                    label4.Text += folderBrowserDialog1.SelectedPath;
                    WorkspaceDirValid = true;
                    sw.Close();
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e){}
        
        public void refreshProjects()
        {
            if (WorkspaceDirValid == false) {return;}
            ProjList.Controls.Clear();

            foreach (DirectoryInfo dir in localDir.GetDirectories())
            {
                ProjectListItem projectListItem = new ProjectListItem();
                projectListItem.Title = dir.Name;
                projectListItem.Desc = MCMETA.GetDescription(dir.FullName + @"\files\pack.mcmeta");
                projectListItem.Icon = dir.FullName + @"\files\pack.png";
                projectListItem.dir = dir.FullName + @"\";
                ProjList.Controls.Add(projectListItem);

            }
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            refreshProjects();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AsepriteForm asepriteForm = new AsepriteForm();
            asepriteForm.ShowDialog();
        }
    }

    
}
