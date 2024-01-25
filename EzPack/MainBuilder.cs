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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace EzPack
{


    public partial class MainBuilder : Form
    {
        string test_dir_str;
        DirectoryInfo test_dir;
        string defaultImageDir;
        string currentySelectedFileName;
        string ExportFileStringDir;


        public MainBuilder(string dir)
        {
            
            test_dir_str = dir;
            test_dir = new DirectoryInfo(test_dir_str);
            defaultImageDir = test_dir.FullName + @"\files\pack.png";
            _imageDir = test_dir.FullName + @"files\pack.png"; 
            InitializeComponent();
            setMode(0);
            setExport(true);
            label1.Visible = false;

            ProjectName = test_dir.Name; 
            ProjectDesc = MCMETA.GetDescription(test_dir.FullName + @"\files\pack.mcmeta"); 
            ProjectVersion = MCMETA.GetVersion(test_dir.FullName + @"\files\pack.mcmeta");
            ExportFileStringDir = test_dir.FullName + @"\cleanExportSelection.txt";

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
            TreeNode tds = treeView1.Nodes.Add(di.Name);
            tds.Tag = di.FullName;

            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
        }
        private void LoadSubDirectories(string dir, TreeNode td)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
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
                    System.Drawing.Image originalImage = System.Drawing.Image.FromFile(e.Node.Tag.ToString());
                    image_string = e.Node.Tag.ToString();
                    Size sz = originalImage.Size;
                    prev_node = e.Node;
                    FileInfo fileInfo = new FileInfo(image_string);

                    label1.Text = fileInfo.Name;
                    currentySelectedFileName = fileInfo.Name;
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

        /*
         * https://itecnote.com/tecnote/c-copying-from-and-to-clipboard-loses-image-transparency/
         */
        private void SetClipboardImagePng(Bitmap image)
        {
            Clipboard.Clear();
            DataObject data = new DataObject();

            using (MemoryStream pngMemStream = new MemoryStream())
            using (MemoryStream dibMemStream = new MemoryStream())
            {
                image.Save(pngMemStream, ImageFormat.Png);
                data.SetData("PNG", false, pngMemStream);
                Byte[] dibData = ConvertToDib(image);
                dibMemStream.Write(dibData, 0, dibData.Length);
                data.SetData(DataFormats.Dib, false, dibMemStream);
                Clipboard.SetDataObject(data, true);
            }
        }
        public static Byte[] GetImageData(Bitmap sourceImage, out Int32 stride)
        {
            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            stride = sourceData.Stride;
            Byte[] data = new Byte[stride * sourceImage.Height];
            Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            sourceImage.UnlockBits(sourceData);
            return data;
        }

        public static void WriteIntToByteArray(Byte[] data, Int32 startIndex, Int32 bytes, Boolean littleEndian, UInt32 value)
        {
            Int32 lastByte = bytes - 1;
            if (data.Length < startIndex + bytes)
                throw new ArgumentOutOfRangeException("startIndex", "Data array is too small to write a " + bytes + "-byte value at offset " + startIndex + ".");
            for (Int32 index = 0; index < bytes; index++)
            {
                Int32 offs = startIndex + (littleEndian ? index : lastByte - index);
                data[offs] = (Byte)(value >> (8 * index) & 0xFF);
            }
        }

        public static Byte[] ConvertToDib(System.Drawing.Image image)
        {
            Byte[] bm32bData;
            Int32 width = image.Width;
            Int32 height = image.Height;
            using (Bitmap bm32b = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics gr = Graphics.FromImage(bm32b))
                    gr.DrawImage(image, new Rectangle(0, 0, bm32b.Width, bm32b.Height));
                bm32b.RotateFlip(RotateFlipType.Rotate180FlipX);
                Int32 stride;
                bm32bData = GetImageData(bm32b, out stride);
            }
            Int32 hdrSize = 0x28;
            Byte[] fullImage = new Byte[hdrSize + 12 + bm32bData.Length];

            WriteIntToByteArray(fullImage, 0x00, 4, true, (UInt32)hdrSize);
            WriteIntToByteArray(fullImage, 0x04, 4, true, (UInt32)width);
            WriteIntToByteArray(fullImage, 0x08, 4, true, (UInt32)height);

            WriteIntToByteArray(fullImage, 0x0C, 2, true, 1);
            WriteIntToByteArray(fullImage, 0x0E, 2, true, 32);
            WriteIntToByteArray(fullImage, 0x10, 4, true, 3);

            WriteIntToByteArray(fullImage, 0x14, 4, true, (UInt32)bm32bData.Length);

            WriteIntToByteArray(fullImage, hdrSize + 0, 4, true, 0x00FF0000);
            WriteIntToByteArray(fullImage, hdrSize + 4, 4, true, 0x0000FF00);
            WriteIntToByteArray(fullImage, hdrSize + 8, 4, true, 0x000000FF);

            Array.Copy(bm32bData, 0, fullImage, hdrSize + 12, bm32bData.Length);
            return fullImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetClipboardImagePng((Bitmap)System.Drawing.Image.FromFile(image_string));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> allLines = File.ReadAllLines(ExportFileStringDir).ToList();

                foreach (var line in allLines)
                {
                    string[] splitted = line.Split('\\');
                    string lastItem = splitted[splitted.Length - 1];
                    if (currentySelectedFileName == lastItem)
                    {
                        return;
                    }
                }

                allLines.Add(GetRelativePath(image_string, ProjectName + @"\files"));
                using (StreamWriter sw = new StreamWriter(ExportFileStringDir))
                {
                    foreach (var line in allLines)
                    {
                        sw.WriteLine(line);
                    }
                    sw.Dispose();
                    sw.Close();
                }

                File.Delete(image_string);
                File.Copy(openFileDialog1.FileName, image_string);

                treeView1.SelectedNode = null;
                treeView1.SelectedNode = prev_node;
            }
        }

        private void visszaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.OpenForms[0].Show();
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


        private bool RemovePredicate(string name)
        {
            
            string[] exportLines = File.ReadAllLines(ExportFileStringDir).ToArray();

            string spliced = name.Split('\\')[name.Split('\\').Length - 1];
            foreach (string line in exportLines)
            {
                string exportLine = line.Split('\\')[line.Split('\\').Length - 1];
                
                if (spliced != exportLine)
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateExportFolder()
        {
            DirectoryInfo ExportFolder = Directory.CreateDirectory(test_dir.FullName + @"\" + ProjectDisplayName);


            string sourceDirectory = test_dir.FullName + @"\files";
            string destinationDirectory = ExportFolder.FullName;
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }
            

            string[] fileExtensionsToFilter = { ".png", ".mcmeta",".txt"};

            string[] allFiles = Directory.GetFiles(sourceDirectory, "*.*", SearchOption.AllDirectories);

            var filteredFiles = allFiles
             .Where(file => fileExtensionsToFilter.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase) && RemovePredicate(file)))
             .ToList();
 
            // Move filtered files to the destination directory preserving the directory structure
            foreach (string filePath in filteredFiles)
            {
                string relativePath = GetRelativePath(filePath, ProjectName + @"\files");
                string destinationFilePath = Path.Combine(destinationDirectory, relativePath);

                // Create the parent directory if it doesn't exist
                string destinationFileDirectory = Path.GetDirectoryName(destinationFilePath);
                if (!Directory.Exists(destinationFileDirectory))
                {
                    Directory.CreateDirectory(destinationFileDirectory);
                }

                // Copy the file to the destination directory
                File.Copy(filePath, destinationFilePath, true); // Set the third parameter to true to overwrite if the file already exists
            }

            string[] allFiles2 = Directory.GetFiles(ExportFolder.FullName, "*.*", SearchOption.AllDirectories);
            foreach (var item in allFiles2)
            {
                if (RemovePredicate(item))
                {
                    File.Delete(item);
                }
            }


        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //export
            /*if (!Directory.Exists(test_dir.FullName + @"\" + ProjectDisplayName))
            {
                CreateExportFolder();
            }
            else
            {
                Directory.Delete(test_dir.FullName + @"\" + ProjectDisplayName,true);
                CreateExportFolder();
            }*/
            
               
            

            
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
            System.Windows.Forms.Application.OpenForms[0].Show();
        }
    }

}
