using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzPack
{
    public partial class FileDisplay : UserControl
    {
        public FileDisplay()
        {
            InitializeComponent();
        }

        #region Properties

        private string _title;
        public string _file;
        private System.EventHandler _func;

        [Category("Custom Props")]
        public string Title
        {
            get { return _title; }
            set { _title = value; title1.Text = value; }
        }

        [Category("Custom Props")]
        public System.EventHandler Function
        {
            get { return _func; }
            set { _func = value; title1.Click += value; }
        }

        [Category("Custom Props")]
        public string File
        {
            get { return _file; }
            set { _file = value;}
        }

        #endregion

    }
}
