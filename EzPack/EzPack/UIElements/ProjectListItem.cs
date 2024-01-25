using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzPack
{
    public partial class ProjectListItem : UserControl
    {
        public ProjectListItem()
        {
            InitializeComponent();
        }

        #region Properties

        private string _title;
        private string _desc;
        private string _icon;
        private string _dir;

        [Category("Custom Props")]
        public string Title
        {
            get { return _title; }
            set { _title = value; title1.Text = value; }
        }

        [Category("Custom Props")]
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; desc1.Text = value; }

        }

        [Category("Custom Props")]
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; icon1.ImageLocation = value; }

        }
        [Category("Custom Props")]
        public string dir
        {
            get { return _dir; }
            set { _dir = value; }

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            MainBuilder mainBuilder = new MainBuilder(_dir);
            mainBuilder.Show();
            Application.OpenForms[0].Hide();
            
        }

        private void back_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_dir);
            DeletePrompt deletePrompt = new DeletePrompt(_dir);
            if (deletePrompt.ShowDialog() == DialogResult.OK)
            {
                icon1.Image = null;
                icon1.ImageLocation = null;
                icon1.Dispose();
                icon1.Invalidate();
                foreach (Control item in this.Controls)
                {
                    this.Controls.Remove(item);
                }
                directoryInfo.Delete(true);
                Application.OpenForms[0].Hide();
                Application.OpenForms[0].Show();

            }
        }
    }
}
