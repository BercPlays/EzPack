using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static EzPack.HelperClasses.ZipManager;
using static EzPack.HelperClasses.DirectoryManager;
using System.Threading;
using System.Threading.Tasks;

namespace EzPack
{
    public partial class AssetInstaller : Form
    {
        string jarFile;
        DirectoryInfo workspaceDirInfo = new DirectoryInfo(GetCurrentWorkspaceDir());
        string _destination;
        DirectoryInfo destinationInfo;
        string _packImage;

        string dest_format;


        string default_mcmeta = @"{
              ""pack"": {
                ""pack_format"": fd57g8g4ng74bsfa78BGT78F,
                ""description"": ""§aPack By EzPack""
              }
            }";

        public AssetInstaller(string packName, int packVersion, Enum packType, string packImage)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string versionFolder = appDataFolder + @"\.minecraft\versions";
            DirectoryInfo versionDir = new DirectoryInfo(versionFolder);

            string destination = GetCurrentWorkspaceDir() + @"\" + packName + @"\files";
            

            _packImage = packImage;
            _destination = destination;

            destinationInfo = new DirectoryInfo(_destination);
            default_mcmeta = default_mcmeta.Replace("fd57g8g4ng74bsfa78BGT78F", packVersion.ToString());

            dest_format = GetCurrentWorkspaceDir() + @"\" + packName + @"\";



            if (destinationInfo.Exists == false)
            {
                destinationInfo.Create();
            }
            if (Directory.Exists(GetCurrentWorkspaceDir() + @"\" + packName + @"\files") == false)
            {
                Directory.CreateDirectory(GetCurrentWorkspaceDir() + @"\" + packName + @"\files");
            }

            if (File.Exists(GetCurrentWorkspaceDir() + @"\" + packName + @"\displayname.txt") == false)
            {
                using (StreamWriter sw = new StreamWriter(GetCurrentWorkspaceDir() + @"\" + packName + @"\displayname.txt"))
                {
                    sw.WriteAsync(packName);
                    sw.Dispose();
                    sw.Close();
                }
                using (StreamWriter sw = new StreamWriter(GetCurrentWorkspaceDir() + @"\" + packName + @"\cleanExportSelection.txt"))
                {
                    sw.Dispose();
                    sw.Close();
                }
            }

            InitializeComponent();

            if (versionDir.Exists)
            {

                openFileDialog1.InitialDirectory = versionFolder;
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    cancel();
                }
                else
                {

                    jarFile = openFileDialog1.FileName;
                    backgroundWorker1.RunWorkerAsync();

                }
            }
            else
            {
                MessageBox.Show(".minecraft\\versions nem létezik!\nLe van töltve egy minecraft verzió?", "No minecraft detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    cancel();
                }
                else
                {
                    jarFile = openFileDialog1.FileName;
                    backgroundWorker1.RunWorkerAsync();
                }
            }

        }
        public void cancel()
        {
            back.Visible = true;
            status.Text = "Installáció hiba";
            status.ForeColor = Color.DarkRed;
            
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            
            FastZipUnpack(jarFile, _destination);
            int file_len = destinationInfo.GetFiles().Length - 1;
            int dir_len = destinationInfo.GetDirectories().Length - 1;
            int all_len = Math.Max(file_len, dir_len) - Math.Min(file_len, dir_len);

            int file_progress = 0;

            foreach (FileInfo file in destinationInfo.GetFiles())
            {
                
                file_progress++;
                backgroundWorker1.ReportProgress((file_progress / all_len) * 100);
                file.Delete();
            }
            foreach (DirectoryInfo subdirectory in destinationInfo.GetDirectories())
            {
                if (subdirectory.Name != "assets")
                {
                    file_progress++;
                    subdirectory.Delete(true);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            using (StreamWriter writer = File.CreateText(_destination + @"\" + "pack.mcmeta"))
            {
                writer.WriteLine(default_mcmeta);
            }
            File.Copy(_packImage, _destination + @"\" + "pack.png");
            MainBuilder mainBuilder = new MainBuilder(dest_format);
            mainBuilder.Show();
            this.Close();
            Application.OpenForms[1].Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Value = e.ProgressPercentage;
            status.Text = e.ProgressPercentage.ToString() + "%";
            
        }
    }
}
