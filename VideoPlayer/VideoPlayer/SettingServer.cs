using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoPlayer
{
    public partial class SettingServer : Form
    {

        DataTable table = new DataTable("list");
        TcpClient caspar1;
        TcpClient caspar2;
        int selectedRow;
        string path1, path2;
        string servername1="", servername2="";
        private MediaPlayer mp;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public SettingServer(TcpClient c1, TcpClient c2, MediaPlayer form1)
        {
            InitializeComponent();
            reset();
            txtStats.Enabled = false;
            caspar1 = c1;
            caspar2 = c2;
            mp = form1;
            //this.FormBorderStyle = FormBorderStyle.None;
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void SettingServer_Load(object sender, EventArgs e)
        {
            try
            {
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Host", typeof(string));
                table.Columns.Add("Port", typeof(string));
                table.Columns.Add("Desc", typeof(string));
                table.Columns.Add("Stats", typeof(string));
                table.Columns.Add("Folder", typeof(string));
                gridServer.DataSource = table;
                reset();
                table.Rows.Clear(); //mereset table yang telah tersedia
                if (Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Server.xml" != null)
                {
                    table.ReadXml(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Server.xml");
                } 
                this.KeyPreview = true;
                this.KeyDown += new KeyEventHandler(menuStrip1_KeyDown);
            }
            catch { }
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool dataFound = false;
                foreach (DataGridViewRow row in gridServer.Rows)
                {
                    object id = row.Cells[0].Value;
                    if (id != null && id.ToString() == txtId.Text)
                    {
                        MessageBox.Show("Server already exist, with same ID\n\nTry to Change ID with : 1 or 2");
                        dataFound = true;
                        break;
                    }
                }
                
                if (!dataFound)
                {
                    if ((Convert.ToInt32(txtId.Text) > 2) || (Convert.ToInt32(txtId.Text) < 1))
                    {
                        MessageBox.Show("Try to change ID with : 1 or 2");
                        //break;
                    }
                    else
                    {
                        table.Rows.Add(txtId.Text, txtName.Text, txtHost.Text, txtPort.Text, txtDesc.Text, txtStats.Text, folderPath.Text);
                        gridServer.DataSource = table;
                        reset();
                        MessageBox.Show("Data has been saved !");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Fill in the blank box !");
                txtId.Focus();
            }
        }

        public void reset()
        {
            txtId.Enabled = true;
            txtStats.Text = "false";
            //txtId.Text = "";
            txtName.Text = "";
            txtHost.Text = "";
            txtPort.Text = "";
            txtDesc.Text = "";
            folderPath.Text = "";
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnReset.Enabled = false;
        }

        private void gridServer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedRow = e.RowIndex;
                DataGridViewRow row = gridServer.Rows[selectedRow];
                txtId.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtHost.Text = row.Cells[2].Value.ToString();
                txtPort.Text = row.Cells[3].Value.ToString();
                txtDesc.Text = row.Cells[4].Value.ToString();
                txtStats.Text = row.Cells[5].Value.ToString();
                folderPath.Text = row.Cells[6].Value.ToString();

                txtId.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Enabled = false;
                btnReset.Enabled = true;
                if (row.Cells[5].Value.ToString() == "true")
                {
                    this.lblCon.Text = "CONNECTED!!!";
                    this.lblCon.ForeColor = Color.Green;
                }else
                {
                    this.lblCon.Text = "DISCONNECTED!!!";
                    this.lblCon.ForeColor = Color.Red;
                }
            }
            catch { }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow newData = gridServer.Rows[selectedRow];
            newData.Cells[0].Value = txtId.Text;
            newData.Cells[1].Value = txtName.Text;
            newData.Cells[2].Value = txtHost.Text;
            newData.Cells[3].Value = txtPort.Text;
            newData.Cells[4].Value = txtDesc.Text;
            newData.Cells[5].Value = txtStats.Text;
            newData.Cells[6].Value = folderPath.Text;
            
            reset();
            MessageBox.Show("Data has been edited !");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            selectedRow = gridServer.CurrentCell.RowIndex;
            gridServer.Rows.RemoveAt(selectedRow);
            gridServer.Rows[0].Cells[0].Value = "1";
            reset();
            MessageBox.Show("Data has been deleted !");
        }

        public void saveServerToolStripMenuItem_Click(object sender, EventArgs e) //saved file server
        {
            try
            {
                table.WriteXml(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Server.xml");

                    MessageBox.Show("File has been saved !!");
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void SettingServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;

        }

        public string Path1 { set { path1 = value; } get { return path1; } }
        public string Path2 { set { path2 = value; } get { return path2; } }

        public string ServerName1 { set { servername1 = value; } get { return servername1; } }

        public string ServerName2 { set { servername2 = value; } get { return servername2; } }

        public bool connectServer(string id, string host, string port,string servername,string folderPath)
        {
            bool terhubung = false;
            try
            {
                if (id == "1")
                {
                    caspar1.Connect(host, int.Parse(port));
                    this.path1 = @"\\" + host + "\\"+folderPath;
                    this.servername1 = servername;
                    if (caspar1.Connected)
                    {
                        this.lblCon.Text = "CONNECTED!!!";
                        this.lblCon.ForeColor = Color.Green;
                        gridServer.Rows[selectedRow].Cells[5].Value = "true";
                        MessageBox.Show("Caspar Server 1 CONNECTED !");
                        terhubung = true;
                        
                        mp.ListDirectory(mp.treeView1, mp.treeView3, path1);
                        mp.treeView1.ExpandAll();
                        mp.treeView3.ExpandAll();
                        mp.treeView1.Refresh();
                        mp.treeView3.Refresh();
                    }
                }

                if (id == "2")
                {
                    caspar2.Connect(host, int.Parse(port));
                    this.path2 = @"\\" + host + "\\"+folderPath;
                    //MessageBox.Show(path2);
                    this.servername2 = servername;
                    if (caspar2.Connected)
                    {
                        this.lblCon.Text = "CONNECTED!!!";
                        this.lblCon.ForeColor = Color.Green;
                        gridServer.Rows[selectedRow].Cells[5].Value = "true";
                        MessageBox.Show("Caspar Server 2 CONNECTED !");
                        terhubung = true;


                        mp.ListDirectory(mp.treeView1, mp.treeView3, path2);
                        mp.treeView1.ExpandAll();
                        mp.treeView3.ExpandAll();
                        mp.treeView1.Refresh();
                        mp.treeView3.Refresh();
                    }
                }
            }
            catch (Exception)
            {
                this.lblCon.Text = "DISCONNECTED!!!";
                this.lblCon.ForeColor = Color.Red;
                txtStats.Text = "false";
                MessageBox.Show("Caspar Server NOT CONNECTED !");
                terhubung = false;
            }

            return terhubung;
        }


        private void menuStrip1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                connectServer(txtId.Text, txtHost.Text, txtPort.Text,txtName.Text,folderPath.Text);
                //connectToolStripMenuItem.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                disconnectToolStripMenuItem.PerformClick();
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectServer(txtId.Text, txtHost.Text, txtPort.Text,txtName.Text,folderPath.Text);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "1")
            {
                try
                {
                    int i = 0;
                    caspar1.Close();
                    caspar1 = new System.Net.Sockets.TcpClient();
                    MessageBox.Show("Caspar Server 1 Disconnected !");
                    while (mp.treeView1.Nodes[i].Text != @"\\" + txtHost.Text + "\\" + folderPath.Text)
                    {
                        i++;
                    }
                    mp.treeView1.Nodes[i].Remove();
                    mp.treeView3.Nodes[i].Remove();
                }
                catch
                {
                    //MessageBox.Show("Server Disconnected !");
                }
            }

            if (txtId.Text == "2")
            {
                try
                {
                    int i = 0;
                    caspar2.Close();
                    caspar2 = new System.Net.Sockets.TcpClient();
                    MessageBox.Show("Caspar Server 2 Disconnected !");
                    while (mp.treeView1.Nodes[i].Text != @"\\" + txtHost.Text + "\\" + folderPath.Text)
                    {
                        i++;
                    }
                    mp.treeView1.Nodes[i].Remove();
                    mp.treeView3.Nodes[i].Remove();
                }
                catch
                {
                    //MessageBox.Show("Server Disconnected !");
                }
            }

            txtStats.Text = "false";
            this.lblCon.Text = "DISCONNECTED!!!";
            this.lblCon.ForeColor = Color.Red;
            btnEdit.PerformClick();
        }

        public void ListDirectory(TreeView treeView, string path)
        {
            var rootDirectoryInfo = new DirectoryInfo(path);

            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.TopMost = false;
        }

        private void btnMinimize_MouseEnter(object sender, EventArgs e)
        {
            //var tah = new Bitmap(VideoPlayer.Properties.Resources.BackIcon);
            //btnMinimize.BackgroundImage = Image.FromFile(tah);
            btnMinimze.BackgroundImage = VideoPlayer.Properties.Resources.BackIconHover;
        }

        private void btnMinimize_MouseLeave(object sender, EventArgs e)
        {
            //var tah = new Bitmap(VideoPlayer.Properties.Resources.BackIconHover);
            //btnMinimize.BackgroundImage = Image.FromFile(taha);
            btnMinimze.BackgroundImage = VideoPlayer.Properties.Resources.BackIcon;
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));

            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));

            return directoryNode;
        }

        private void btnMinimize_MouseDown(object sender, MouseEventArgs e)
        {
            btnMinimze.BackgroundImage = VideoPlayer.Properties.Resources.BackIconDown;
        }

        private void serverToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            serverToolStripMenuItem.ForeColor = Color.Black;
        }

        private void serverToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            serverToolStripMenuItem.ForeColor = Color.White;
        }

        private void conditionToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            conditionToolStripMenuItem.ForeColor = Color.Black;
        }

        private void conditionToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            conditionToolStripMenuItem.ForeColor = Color.White;
        }

        private void serverToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            serverToolStripMenuItem.ForeColor = Color.White;
        }

        private void serverToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            serverToolStripMenuItem.ForeColor = Color.Black;
        }

        private void conditionToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            conditionToolStripMenuItem.ForeColor = Color.Black;
        }

        private void conditionToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            conditionToolStripMenuItem.ForeColor = Color.White;
        }


        //borderless
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            WinApi.Dwm.DWMNCRENDERINGPOLICY Policy = WinApi.Dwm.DWMNCRENDERINGPOLICY.Enabled;
            WinApi.Dwm.WindowSetAttribute(this.Handle, WinApi.Dwm.DWMWINDOWATTRIBUTE.NCRenderingPolicy, (int)Policy);
            if (DWNCompositionEnabled()) { WinApi.Dwm.WindowBorderlessDropShadow(this.Handle, 3); }
            //if (DWNCompositionEnabled()) { WinApi.Dwm.WindowEnableBlurBehind(this.Handle); }
            //if (DWNCompositionEnabled()) { WinApi.Dwm.WindowSheetOfGlass(this.Handle); }
        }

        private bool DWNCompositionEnabled() => (Environment.OSVersion.Version.Major >= 6)
                                             ? WinApi.Dwm.IsCompositionEnabled()
                                             : false;

        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.PerformClick();
        }

        private void btnBrowseFolderPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                //fbd.SelectedPath = "192.168.1.140";

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string namePath = fbd.SelectedPath; //Directory.GetFiles(fbd.SelectedPath);
                    //hasil namePath : \\COMP\folder
                    if(namePath.StartsWith("\\"))
                    {
                        var host = namePath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                        string newPath = namePath.Replace(host, getAddress(host));
                        //newPath = newPath.Substring(2);
                        //MessageBox.Show(newPath);

                        string[] str2 = newPath.Split('\\');
                        string b = "";
                        //MessageBox.Show(str2[2]);
                        if (txtHost.Text == str2[2])
                        {
                            b = newPath.Replace(@"\\" + str2[2] + "\\", "");
                            //MessageBox.Show(b);
                            newPath = b;
                        }
                        else
                        {
                            newPath = "media";
                            MessageBox.Show("Host and target are not same. Default set to '\\media' ");
                        }
                        //MessageBox.Show(newPath);
                        folderPath.Text = newPath;
                    }
                    else
                    {
                        folderPath.Text = "media";
                        MessageBox.Show("Can't reach folder. must share in network !");
                    }
                }
            }
        }

        public string getAddress(string hostname)
        {
            IPAddress ip = System.Net.Dns.GetHostEntry(hostname).AddressList.Where(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First();
            
            return ip.ToString();
        }

        private void btnTes_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(System.Net.Dns.GetHostEntry(txtHost.Text).HostName);
            IPAddress ip = System.Net.Dns.GetHostEntry("DESKTOP-BB2NJ59").AddressList.Where(o => o.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First();
            MessageBox.Show(ip.ToString());
        }

        private void disconnectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            disconnectToolStripMenuItem.PerformClick();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WinApi.WinMessage.WM_DWMCOMPOSITIONCHANGED:
                    {
                        WinApi.Dwm.DWMNCRENDERINGPOLICY Policy = WinApi.Dwm.DWMNCRENDERINGPOLICY.Enabled;
                        WinApi.Dwm.WindowSetAttribute(this.Handle, WinApi.Dwm.DWMWINDOWATTRIBUTE.NCRenderingPolicy, (int)Policy);
                        WinApi.Dwm.WindowBorderlessDropShadow(this.Handle, 2);
                        m.Result = (IntPtr)0;
                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
