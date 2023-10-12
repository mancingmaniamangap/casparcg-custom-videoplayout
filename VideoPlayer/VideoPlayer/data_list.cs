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
    public partial class data_list : Form
    {
        public data_list()
        {
            InitializeComponent();
        }


        private void data_list_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("delay", typeof(string));
            table.Columns.Add("layer", typeof(string));
            table.Columns.Add("server", typeof(string));

            dataGridView1.DataSource = table;
        }
    }
}
