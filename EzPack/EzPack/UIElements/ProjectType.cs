using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzPack
{
    public partial class ProjectType : UserControl
    {
        public ProjectType()
        {
            InitializeComponent();
        }

        #region Properties

        private string _title;
        private Image _icon;

        [Category("Custom Props")]
        public string Title
        {
            get { return _title; }
            set { _title = value; title1.Text = value; }
        }

        [Category("Custom Props")]
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; icon1.Image = value; }

        }
        #endregion  

        private void ProjectType_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ControlDarkDark;
        }

        #region events
        private void ProjectType_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ControlDark;
        }

        private void title1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ControlDarkDark;
        }

        private void title1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ControlDark;
        }
        

        private void icon1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ControlDarkDark;
        }

        private void icon1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SystemColors.ControlDark;
        }
        #endregion

        #region Clickevents

        #endregion

    }
}
