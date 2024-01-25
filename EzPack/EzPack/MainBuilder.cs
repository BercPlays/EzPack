using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using EzPack.HelperClasses;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static EzPack.Locals.LocalVars;
using static EzPack.HelperClasses.DirectoryManager;
using static EzPack.HelperClasses.ZipManager;
using static EzPack.HelperClasses.DesktopHelper;

namespace EzPack
{


    public partial class MainBuilder : Form
    {
        string test_dir_str;
        DirectoryInfo test_dir;
        string defaultImageDir;
        public MainBuilder(string dir)
        {
            
            test_dir_str = dir;
            test_dir = new DirectoryInfo(test_dir_str);
            defaultImageDir = test_dir.FullName + @"\files\pack.png";
            _imageDir = test_dir.FullName + @"files\pack.png"; 
            InitializeComponent();
            setMode(0);
            toolStripStatusLabel1.Text = "Készen áll az exportálásra ✓";
            setExport(true);
            label1.Visible = false;

            ProjectName = test_dir.Name; 
            ProjectDesc = MCMETA.GetDescription(test_dir.FullName + @"\files\pack.mcmeta"); 
            ProjectVersion = MCMETA.GetVersion(test_dir.FullName + @"\files\pack.mcmeta");

            using (StreamReader sr = new StreamReader(test_dir.FullName + @"\displayname.txt"))
            {
                ProjectDisplayName = sr.ReadToEnd();
                sr.Close();
            }






        }
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
        }

        private void MainBuilder_Load(object sender, EventArgs e)
        {

        }
        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            //Setting ProgressBar Maximum Value
            TreeNode tds = treeView1.Nodes.Add(di.Name);
            tds.Tag = di.FullName;

            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }
        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            // Loop through them to see if they have any other subdirectories
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);

                tds.Tag = di.FullName;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);
            }
        }
        private void LoadFiles(string dir, TreeNode td)
        {

            string[] Files = Directory.GetFiles(dir, "*.*");
            // Loop through them to see files
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e) { setMode(toolStripComboBox1.SelectedIndex); }

        private int prev_mode_index = 1;
        private void setMode(int mode_index)
        {
            if (mode_index == prev_mode_index) { return; }
            switch (mode_index)
            {
                case 0:
                    treeView1.Nodes.Clear();
                    LoadDirectory(test_dir_str + @"files\assets\minecraft\textures");
                    break;
                case 1:
                    treeView1.Nodes.Clear();
                    LoadDirectory(test_dir_str);
                    break;
            }
            prev_mode_index = mode_index;
        }
        private void setExport(bool export)
        {
            exportToolStripMenuItem.Enabled = export;
        }
        string image_string = null;
        TreeNode prev_node = null;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Text.Contains(".png") && e.Node.Text.Contains(".mcmeta") == false)
            {
                try
                {
                    panel1.Visible = true;
                    Image originalImage = Image.FromFile(e.Node.Tag.ToString());
                    image_string = e.Node.Tag.ToString();
                    Size sz = originalImage.Size;
                    prev_node = e.Node;
                    FileInfo fileInfo = new FileInfo(image_string);

                    label1.Text = fileInfo.Name;
                    label1.Visible = true;
                    Bitmap zoomed = (Bitmap)pictureBox1.Image;
                    if (zoomed != null) zoomed.Dispose();

                    float zoom = (float)(50 / 4f + 1);
                    zoomed = new Bitmap((int)(sz.Width * zoom), (int)(sz.Height * zoom));

                    using (Graphics g = Graphics.FromImage(zoomed))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = PixelOffsetMode.Half;

                        g.DrawImage(originalImage, new Rectangle(Point.Empty, zoomed.Size));
                        g.Dispose();
                    }

                    pictureBox1.Image = zoomed;
                    originalImage.Dispose();

                }
                catch (OutOfMemoryException)
                {
                }

            }
            else
            {
                panel1.Visible = false;
                label1.Visible = false;
            }
        }
        private TreeNode FindNode(TreeNodeCollection nodes, string searchTerm)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text.Contains(searchTerm))
                    return node;

                TreeNode matchedNode = FindNode(node.Nodes, searchTerm);
                if (matchedNode != null)
                    return matchedNode;
            }

            return null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                TreeNode matchedNode = FindNode(treeView1.Nodes, textBox1.Text);
                if (matchedNode != null)
                {
                    treeView1.CollapseAll();
                    treeView1.SelectedNode = matchedNode;
                }
                else
                {
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Image image = Image.FromFile(image_string);
                if (image == null) { return; }
                Clipboard.SetImage(image);
                image.Dispose();
                ms.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.Delete(image_string);
                File.Copy(openFileDialog1.FileName, image_string);
                treeView1.SelectedNode = null;
                treeView1.SelectedNode = prev_node;
            }
        }

        private void visszaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Close();
        }

        private void susToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectSettings projectSettings = new ProjectSettings();

            if (projectSettings.ShowDialog() == DialogResult.OK)
            {

                
                
                MCMETA.Root root = new MCMETA.Root()
                {
                    Pack = new MCMETA.Pack()
                    {
                        PackFormat = ProjectVersion,
                        Description = ProjectDesc,

                    }
                };
                using (StreamWriter sw = new StreamWriter(test_dir_str + @"\files\pack.mcmeta"))
                {
                    sw.WriteAsync(JsonConvert.SerializeObject(root));
                    sw.Close();
                }
                using (StreamWriter sw = new StreamWriter(test_dir_str + @"\displayname.txt"))
                {
                    sw.WriteAsync(ProjectDisplayName);
                    sw.Close();
                }
                if (test_dir.Name != ProjectName)
                {
                    test_dir_str = GetCurrentWorkspaceDir() + @"\" + ProjectName;
                    test_dir.MoveTo(test_dir_str);

                }
                defaultImageDir = test_dir.FullName + @"\files\pack.png";

                if (_imageDir != defaultImageDir)
                {
                    if (imageChanged == false) {_imageDir = defaultImageDir; return; }
                    File.Delete(defaultImageDir);
                    File.Copy(_imageDir, defaultImageDir);
                }
                imageChanged = false;

                
            }
            else
            {
                _imageDir = null;
            }
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(getDesktop() + @"\" + ProjectDisplayName + ".zip") == false)
            {
                FastZipPack(test_dir.FullName + @"\files", ProjectDisplayName);
                Directory.Move(getCurrentDir() + @"\" + ProjectDisplayName + ".zip", getDesktop() + @"\" + ProjectDisplayName + ".zip");
                RefreshDesktop();

            }
            else
            {
                File.Delete(getDesktop() + @"\" + ProjectDisplayName + ".zip");
                FastZipPack(test_dir.FullName + @"\files", ProjectDisplayName);
                Directory.Move(getCurrentDir() + @"\" + ProjectDisplayName + ".zip", getDesktop() + @"\" + ProjectDisplayName + ".zip");
                RefreshDesktop();
            }
           
        }

        private void MainBuilder_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.OpenForms[0].Show();
        }
    }

}
