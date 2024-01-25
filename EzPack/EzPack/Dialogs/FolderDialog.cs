using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EzPack
{
    public partial class FolderDialog : Form
    {
        private string title_;
        private string desc_;
        public string File;
        public FolderDialog(string title,string desc,DirectoryInfo dir)
        {
            InitializeComponent();
            this.title_ = title;
            this.desc_ = desc;
            label1.Text = title_;
            label2.Text = desc_;
            


            flowLayoutPanel1.Controls.Clear();
            FileDisplay[] listItems = new FileDisplay[1];
            int ind = 0;
            foreach (var item in dir.GetDirectories())
            {
                try
                {
                    listItems[ind] = new FileDisplay();
                    listItems[ind].Title = item.Name;
                    listItems[ind].File = item.FullName;
                    listItems[ind].Function = FolderDialog_Click;

                    Array.Resize(ref listItems, listItems.Length + 1);

                    void FolderDialog_Click(object sender, EventArgs e)
                    {
                        File = listItems[ind]._file;
                        
                    }
                    flowLayoutPanel1.Controls.Add(listItems[ind]);
                    ind++;
                }
                catch (Exception)
                {
                    
                }
                
            }
            ind -= 1;
            Array.Resize(ref listItems, listItems.Length - 1);
        }

        
    }
}
