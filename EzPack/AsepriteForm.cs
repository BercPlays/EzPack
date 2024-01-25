using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EzPack.HelperClasses.DirectoryManager;
using static EzPack.HelperClasses.ZipManager;
using IWshRuntimeLibrary;

namespace EzPack
{
    public partial class AsepriteForm : Form
    {
        string dowloadURL;
        public AsepriteForm()
        {
            InitializeComponent();
            WebClient client = new WebClient();
            dowloadURL = client.DownloadString(new Uri("https://pastebin.com/raw/7Uc8hEaW"));
            if (Directory.Exists(getMyGames() + @"\Aseprite")) 
            {
                newProjButton.Enabled = false;
                button1.Enabled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newProjButton_Click(object sender, EventArgs e)
        {
           StartDownload();
        }
        private void StartDownload()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            //if (principal.IsInRole(WindowsBuiltInRole.Administrator) == false){MessageBox.Show("Futtasd Adminisztrátorként!"); return; }
            if (Directory.Exists(getMyGames() + @"\Aseprite") == true) { return; }
            try
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(dowloadURL), "Aseprite.zip");

                label2.Visible = true;
                label3.Visible = true;
                progressBar1.Visible = true;
                button1.Enabled = false;
                newProjButton.Enabled = true;

                void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
                {

                    double bytesIn = double.Parse(e.BytesReceived.ToString());
                    double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                    double percentage = bytesIn / totalBytes * 100;

                    progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
                    label2.Text = progressBar1.Value.ToString() + "%";
                    label3.Text = ((e.BytesReceived / 1048576) + 4) + "mb" + " / " + ((e.TotalBytesToReceive / 1048576) + 4) + "mb";
                }
                void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
                {
                    label2.Visible = false;
                    label3.Visible = false;
                    progressBar1.Visible = false;
                    if (Directory.Exists(getCurrentDir() + @"\Aseprite") == false)
                    {
                        Directory.CreateDirectory(getCurrentDir() + @"\Aseprite");
                    }
                    FastZipUnpack(getCurrentDir() + @"\Aseprite.zip", getMyGames() + @"\Aseprite");
                    button1.Enabled = true;
                    newProjButton.Enabled = false;
                    CreateShortcut();
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(getMyGames() + @"\Aseprite") == false) { return; }
            Process.Start(getMyGames() + @"\Aseprite\aseprite.exe");
        }
        public static void CreateShortcut()
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(getDesktop() + @"\Aseprite.lnk");

            // Set the target path of the shortcut
            shortcut.TargetPath = getMyGames() + @"\Aseprite\aseprite.exe";

            // Set other optional properties of the shortcut
            shortcut.WorkingDirectory = getMyGames() + @"\Aseprite";
            shortcut.Description = "";
            shortcut.IconLocation = getMyGames() + @"\Aseprite\aseprite.exe";
            // ... Set any other properties as needed

            // Save the shortcut
            shortcut.Save();




        }
    }
}
