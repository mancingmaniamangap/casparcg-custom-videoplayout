using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoPlayer
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            tabPage1.Text = @"Shortcut";
            tabPage2.Text = @"Button Description";
            tabPage3.Text = @"How to Use";
        }

        private void Help_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
