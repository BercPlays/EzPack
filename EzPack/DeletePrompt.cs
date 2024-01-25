using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzPack
{
    public partial class DeletePrompt : Form
    {
        DirectoryInfo directoryInfo;
        public DeletePrompt(string dir)
        {
             directoryInfo = new DirectoryInfo(dir);
            InitializeComponent();
            label1.Text = directoryInfo.Name + " törlése?";
        }

        private void back_Click(object sender, EventArgs e)
        {
            
        }
    }
}
