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
using WMPLib;
using AxWMPLib;
using System.Xml;

namespace VideoPlayer
{
    [System.ComponentModel.Bindable(true)]
    [System.ComponentModel.Browsable(false)]


    public partial class MediaPlayer : Form
    {
        DataTable table = new DataTable("tbl");
        string[] format;
        bool ON = true;
        System.Net.Sockets.TcpClient casparClient = new System.Net.Sockets.TcpClient();
        System.Net.Sockets.TcpClient casparClient2 = new System.Net.Sockets.TcpClient();
        //private string path1;
        //private string path2;
        public string layer, namaServer;
        int menit2, o;
        int jam2;
        int detik2;
        SettingServer tes;



        public object SelectedItem { get; set; }

        public MediaPlayer()
        {
            //find folder 'Server' in my documents
            bool exists = System.IO.Directory.Exists(Environment.SpecialFolder.MyDocuments + "\\Server");
            if (!exists) Directory.CreateDirectory(Environment.SpecialFolder.MyDocuments + "\\Server");
            this.tes = new SettingServer(casparClient, casparClient2, this);
            InitializeComponent();
            tes.Hide();
#pragma warning disable CS1717 // Assignment made to same variable
            this.casparClient = casparClient;
#pragma warning restore CS1717 // Assignment made to same variable
#pragma warning disable CS1717 // Assignment made to same variable
            this.casparClient2 = casparClient2;
#pragma warning restore CS1717 // Assignment made to same variable
        }

        private void MediaPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            casparClient.Close();
            casparClient2.Close();

            //tes.saveServerToolStripMenuItem_Click(sender,e);
        }
        
        public void ListDirectory(TreeView Video, TreeView CG, string path)
        {
            var rootDirectoryInfo = new DirectoryInfo(path);

            Video.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            CG.Nodes.Add(CreateDirectoryNodeCG(rootDirectoryInfo));
        }

        public static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.ToString());
            foreach (var directory in directoryInfo.GetDirectories())
                if (directory.GetFiles("*.mp4").Length != 0 || directory.GetFiles("*.mxf").Length != 0 || directory.GetFiles("*.png").Length != 0 || directory.GetFiles("*.mov").Length != 0)
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
                else
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles("*.mp4"))
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            foreach (var file in directoryInfo.GetFiles("*.mxf"))
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            foreach (var file in directoryInfo.GetFiles("*.png"))
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            foreach (var file in directoryInfo.GetFiles("*.mov"))
                directoryNode.Nodes.Add(new TreeNode(file.Name));

            return directoryNode;
        }
        public static TreeNode CreateDirectoryNodeCG(DirectoryInfo directoryInfo)
        {
            var directoryNodecg = new TreeNode(directoryInfo.ToString());
            foreach (var directory in directoryInfo.GetDirectories())
                if (directory.GetFiles("*.ft").Length != 0)
                    directoryNodecg.Nodes.Add(CreateDirectoryNodeCG(directory));
            foreach (var file in directoryInfo.GetFiles("*.ft"))
                directoryNodecg.Nodes.Add(new TreeNode(file.Name));

            return directoryNodecg;
        }

        private string getIP(string path)
        {
            System.Text.RegularExpressions.Regex ip = new System.Text.RegularExpressions.Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            System.Text.RegularExpressions.MatchCollection result = ip.Matches(path);

            return result[0].ToString();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            TreeNode node2 = treeView3.SelectedNode;
            string layere;

            if (node == null && node2 == null)
            {
                MessageBox.Show("Node unknown, can't add file. ");
            }
            else if (node != null)
            {

                string result = txtShowItem.Text; //node treeview2 click
                //MessageBox.Show(node.Parent.Text+"\""+node.Text);
                /*
                if(getIP(node.Parent.Text)==tes.Host1)
                {
                    MessageBox.Show(tes.ServerName1);
                }
                else if(getIP(node.Parent.Text)==tes.Host2)
                {
                    MessageBox.Show(tes.ServerName2);
                }
                */
                //MessageBox.Show(getIP(node.Parent.Text));
                //tes.ServerName2;
                if (txtShowItem.Text == "")
                {
                    MessageBox.Show("Can't add file !!");
                }
                else
                {
                    string name = result.Split('\\').Last();
                    //MessageBox.Show(name);
                    //format = name.Split('.');
                    string format_file = name.Split('.').Last();
                    string result2 = name.Replace(Path.GetExtension(name), "");
                    string[] getip;

                    if (format_file == "ft" && format_file != "")
                    {
                        layere = "20";
                    }
                    else if(format_file == "png")
                    {
                        layere = "1";
                    }
                    else
                    {
                        layere = "10";
                    }

                    //caspar video
                    if (treeView2.SelectedNode != null)
                    {
                        if (treeView2.SelectedNode.Parent == null)
                        {
                            if (treeView1.SelectedNode.Parent.Parent == null)
                            {
                                string a = treeView1.SelectedNode.Parent.Text;
                                getip = a.Split('\\');
                            }
                            else if (treeView1.SelectedNode.Parent.Parent.Parent == null)
                            {
                                string a = treeView1.SelectedNode.Parent.Parent.Text;
                                getip = a.Split('\\');
                                //MessageBox.Show(getip[2]);
                            }
                            else if (treeView1.SelectedNode.Parent.Parent.Parent.Parent == null)
                            {
                                string a = treeView1.SelectedNode.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }
                            else if (treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent == null)
                            {
                                string a = treeView1.SelectedNode.Parent.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }
                            else if (treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent == null)
                            {
                                string a = treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }
                            else
                            {
                                string a = treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }
                            
                            if (getip[2] == tes.gridServer.Rows[0].Cells[2].Value.ToString())
                            {
                                treeView2.SelectedNode.Nodes.Add(result2);
                                table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", treeView2.SelectedNode.Text, tes.gridServer.Rows[0].Cells[1].Value.ToString());
                            }
                            else if (getip[2] == tes.gridServer.Rows[1].Cells[2].Value.ToString())
                            {
                                treeView2.SelectedNode.Nodes.Add(result2);
                                table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", treeView2.SelectedNode.Text, tes.gridServer.Rows[1].Cells[1].Value.ToString());
                            }
                            else
                            {
                                MessageBox.Show("data not showing");
                            }
                        }
                        else
                        {
                            //the node has a parent, so it must be  a child !
                            MessageBox.Show("Can't add file or group");
                        }
                    }
                    else //kalau null
                    {
                        if (treeView1.SelectedNode.Parent.Parent == null)
                        {
                            string a = treeView1.SelectedNode.Parent.Text;
                            getip = a.Split('\\');
                            //MessageBox.Show(getip[2]);
                        }
                        else if (treeView1.SelectedNode.Parent.Parent.Parent == null)
                        {
                            string a = treeView1.SelectedNode.Parent.Parent.Text;
                            getip = a.Split('\\');
                            //MessageBox.Show(getip[2]);
                        }
                        else if (treeView1.SelectedNode.Parent.Parent.Parent.Parent == null)
                        {
                            string a = treeView1.SelectedNode.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }
                        else if (treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent == null)
                        {
                            string a = treeView1.SelectedNode.Parent.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }
                        else if (treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent == null)
                        {
                            string a = treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }
                        else
                        {
                            string a = treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }


                        if (getip[2] == tes.gridServer.Rows[0].Cells[2].Value.ToString())
                        {
                            treeView2.Nodes.Add(result2);
                            table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", result2, tes.gridServer.Rows[0].Cells[1].Value.ToString());
                        }
                        else if (getip[2] == tes.gridServer.Rows[1].Cells[2].Value.ToString())
                        {
                            treeView2.Nodes.Add(result2);
                            table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", result2, tes.gridServer.Rows[1].Cells[1].Value.ToString());
                        }
                        else
                        {
                            MessageBox.Show("data not showing");
                        }
                    }
                }

                //if (treeView2.SelectedNode != null) { table.Rows.Add(result2, "0", "10",durasi(),format_file,"0",treeView2.SelectedNode.Text); }
                //else { table.Rows.Add(result2, "0", "10", durasi(), format_file, "0", ""); }

                dataGridView1.DataSource = table;
                treeView2.ExpandAll();

                //durasi();
            }
            else
            //end if
            if (node2 == null)
            {
                MessageBox.Show("Node unknown, can't add file. ");
            }
            else
            {

                string result = txtShowItem.Text; //node treeview2 click
                //MessageBox.Show(node.Parent.Text+"\""+node.Text);
                /*
                if(getIP(node.Parent.Text)==tes.Host1)
                {
                    MessageBox.Show(tes.ServerName1);
                }
                else if(getIP(node.Parent.Text)==tes.Host2)
                {
                    MessageBox.Show(tes.ServerName2);
                }
                */
                //MessageBox.Show(getIP(node.Parent.Text));
                //tes.ServerName2;
                if (txtShowItem.Text == "")
                {
                    MessageBox.Show("Can't add file !!");
                }
                else
                {
                    /* if (node.Parent.Text == tes.Path1)
                     {
                         result = result.Replace(tes.Path1 + "/", "");
                     }
                     else
                     {
                         result = result.Replace(tes.Path1 + "/", "");
                     }
                     if (node.Parent.Text == tes.Path2)
                     {
                         result = result.Replace(tes.Path2 + "/", "");
                         //MessageBox.Show(result);
                     }
                     else
                    
                         result = result.Replace(tes.Path2 + "/", "");
                     }*/
                    string name = result.Split('\\').Last();
                    //MessageBox.Show(name);
                    format = name.Split('.');
                    string result2 = Path.GetFileName(name);
                    string format_file = name.Split('.').Last();
                    result2 = result2.Replace(Path.GetExtension(name), "");
                    string[] getip;
                    
                    if (format_file == "ft" && format_file != "")
                    {
                        layere = "20";
                    }
                    else if (format_file == "png")
                    {
                        layere = "1";
                    }
                    else
                    {
                        layere = "10";
                    }

                    //caspar video
                    if (treeView2.SelectedNode != null)
                    {
                        if (treeView2.SelectedNode.Parent == null)
                        {
                            if (treeView3.SelectedNode.Parent.Parent == null)
                            {
                                string a = treeView3.SelectedNode.Parent.Text;
                                getip = a.Split('\\');
                                //MessageBox.Show(getip[2]);
                            }
                            else if (treeView3.SelectedNode.Parent.Parent.Parent == null)
                            {
                                string a = treeView3.SelectedNode.Parent.Parent.Text;
                                getip = a.Split('\\');
                                //MessageBox.Show(getip[2]);
                            }
                            else if (treeView3.SelectedNode.Parent.Parent.Parent.Parent == null)
                            {
                                string a = treeView3.SelectedNode.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }
                            else if (treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent == null)
                            {
                                string a = treeView3.SelectedNode.Parent.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }
                            else if (treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent == null)
                            {
                                string a = treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }
                            else
                            {
                                string a = treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent.Text;
                                getip = a.Split('\\');
                            }

                            if (getip[2] == tes.gridServer.Rows[0].Cells[2].Value.ToString())
                            {
                                treeView2.SelectedNode.Nodes.Add(result2);
                                table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", treeView2.SelectedNode.Text, tes.gridServer.Rows[0].Cells[1].Value.ToString());
                            }
                            else if (getip[2] == tes.gridServer.Rows[1].Cells[2].Value.ToString())
                            {
                                treeView2.SelectedNode.Nodes.Add(result2);
                                table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", treeView2.SelectedNode.Text, tes.gridServer.Rows[1].Cells[1].Value.ToString());
                            }
                            else
                            {
                                MessageBox.Show("data not showing");
                            }
                        }
                        else
                        {
                            //the node has a parent, so it must be  a child !
                            MessageBox.Show("Can't add file or group");
                        }
                    }
                    else //kalau null
                    {
                        if (treeView3.SelectedNode.Parent.Parent == null)
                        {
                            string a = treeView3.SelectedNode.Parent.Text;
                            getip = a.Split('\\');
                            //MessageBox.Show(getip[2]);
                        }
                        else if (treeView3.SelectedNode.Parent.Parent.Parent == null)
                        {
                            string a = treeView3.SelectedNode.Parent.Parent.Text;
                            getip = a.Split('\\');
                            //MessageBox.Show(getip[2]);
                        }
                        else if (treeView3.SelectedNode.Parent.Parent.Parent.Parent == null)
                        {
                            string a = treeView3.SelectedNode.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }
                        else if (treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent == null)
                        {
                            string a = treeView3.SelectedNode.Parent.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }
                        else if (treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent == null)
                        {
                            string a = treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }
                        else
                        {
                            string a = treeView3.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent.Text;
                            getip = a.Split('\\');
                        }

                        if (getip[2] == tes.gridServer.Rows[0].Cells[2].Value.ToString())
                        {
                            treeView2.Nodes.Add(result2);
                            table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", result2, tes.gridServer.Rows[0].Cells[1].Value.ToString());
                        }
                        else if (getip[2] == tes.gridServer.Rows[1].Cells[2].Value.ToString())
                        {
                            treeView2.Nodes.Add(result2);
                            table.Rows.Add(result2, "0", layere, durasi(), format_file, "0", result2, tes.gridServer.Rows[1].Cells[1].Value.ToString());
                        }
                        else
                        {
                            MessageBox.Show("data not showing");
                        }
                    }
                }

                //if (treeView2.SelectedNode != null) { table.Rows.Add(result2, "0", "10",durasi(),format_file,"0",treeView2.SelectedNode.Text); }
                //else { table.Rows.Add(result2, "0", "10", durasi(), format_file, "0", ""); }

                dataGridView1.DataSource = table;
                treeView2.ExpandAll();

                //durasi();
            }
            //end if

        }

        private string durasi()
        {
            string vid = "";
            string durasi;
            /*
            if (casparClient.Connected)
            {
                this.path1 = tes.Path1;
                vid = path1 + "\\" + txtShowItem.Text;
            }
            if (casparClient2.Connected)
            {
                this.path2 = tes.Path2;
                vid = path2 + "\\" + txtShowItem.Text;
            }
            */
            vid = txtShowItem.Text;
            string name = vid.Split('\\').Last();
            string format_file = name.Split('.').Last();
            if (format_file == "png")
            {
                return "00:00:00";
            }
            MediaInfo vidInfo = new MediaInfo();

            vidInfo.Open(vid);
            var dur = vidInfo.Get(StreamKind.Video, 0, "Duration/String3");
            vidInfo.Close();
            //MessageBox.Show(vid);
            //string durationminutesL = TimeSpan.FromMilliseconds(Convert.ToDouble(durationL)).ToString();

            //MessageBox.Show(length);
            //MessageBox.Show(durasi);

            return dur.ToString();
            //return durationL;
        }

        private int getJam()
        {
            string jam = "";
            jam = durasi().Substring(durasi().Length - 8, 2);
            return int.Parse(jam);
        }

        private int getMenit()
        {
            string menit = "";
            menit = durasi().Substring(durasi().Length - 5, 2);
            return int.Parse(menit);
        }

        private int getDetik()
        {
            string detik = "";
            detik = durasi().Substring(durasi().Length - 2);
            return int.Parse(detik);
        }

        async Task PutTaskDelay(int a)
        {
            await Task.Delay(a);
        }

        //Play
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int i, j, o,  delay;
                string layers, barisGrid, namaServer;

                //videoPreview.playlist.play();
                i = 0;
                j = 0;
                o = 0;
                TreeNode node = treeView2.SelectedNode;
                if (node == null)
                {
                    MessageBox.Show("Node unknown, Try to add some file !");
                }
                else
                {
                    //node treeview2 click
                    //data = node.Text;

                    if (node.Nodes.Count == 0)
                    {
                        if (node.Parent != null)
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }
                        else
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }


                        
                        layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                        delay = Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value);
                        namaServer = dataGridView1.Rows[j].Cells[7].Value.ToString();
                        while (namaServer != tes.gridServer.Rows[o].Cells[1].Value.ToString())
                        {
                            o++;
                        }
                        string path = "\\\\" + tes.gridServer.Rows[o].Cells[2].Value.ToString() + "\\" + tes.gridServer.Rows[o].Cells[6].Value.ToString();

                        barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();
                        
                        
                        if (barisGrid == "mp4" || barisGrid == "mxf"  || barisGrid == "png" || barisGrid == "mov")
                        {
                            if (casparClient.Connected)
                            {
                                var writer = new StreamWriter(casparClient.GetStream());
                                playingVideo(delay,  layers, treeView2.SelectedNode.Text, writer, dataGridView1.Rows[j].Cells[3].Value.ToString(), path, barisGrid);
                            }
                            if (casparClient2.Connected)
                            {
                                var writer = new StreamWriter(casparClient2.GetStream());
                                playingVideo(delay,  layers, treeView2.SelectedNode.Text, writer, dataGridView1.Rows[j].Cells[3].Value.ToString(), path, barisGrid);
                            }
                        }
                        else if (barisGrid == "ft")
                        {
                            if (casparClient.Connected)
                            {
                                var writer = new StreamWriter(casparClient.GetStream());
                                playingCG(delay,  layers, treeView2.SelectedNode.Text, writer);
                            }
                            if (casparClient2.Connected)
                            {
                                var writer = new StreamWriter(casparClient2.GetStream());
                                playingCG(delay,  layers, treeView2.SelectedNode.Text, writer);
                            }
                        }
                       
                    }
                    else if (node.Nodes.Count != 0)
                    {
                        while (i < treeView2.SelectedNode.Nodes.Count)
                        {
                            //wmp.URL = this.path1 + "\\" + treeView2.SelectedNode.Nodes[i].Text + ".mp4";
                            //layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                            if (node.Nodes[i].Parent != null)
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }
                            else
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }


                            /* buat timecode
                            if (durasi().LongCount() > 5)
                            {
                                jam = getJam();
                                menit = getMenit();
                                detik = getDetik();
                            }
                            else
                            {
                                menit = getMenit();
                                detik = getDetik();
                            }
                            */

                           
                            layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                            delay = Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value);

                            //cek apakah video atau cg
                            barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();
                            namaServer = dataGridView1.Rows[j].Cells[7].Value.ToString();
                            while (namaServer != tes.gridServer.Rows[o].Cells[1].Value.ToString())
                            {
                                o++;
                            }
                            string path = "\\\\" + tes.gridServer.Rows[o].Cells[2].Value.ToString() + "\\" + tes.gridServer.Rows[o].Cells[6].Value.ToString();

                            barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();
                            //var writer = new StreamWriter(casparClient.GetStream());
                            
                            //dataGridView1.Rows[j].Cells[3].Value = "00.00.00";
                           

                            if (barisGrid == "mp4" || barisGrid == "mxf" || barisGrid == "" ||barisGrid == "png"|| barisGrid == "mov")
                            {
                                if (casparClient.Connected)
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("PLAY 1-" + layers + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\"");
                                        writer.Flush();
                                    }
                                    else if (delay != 0)
                                    {
                                        playingVideo(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    playingVideo(delay,  layers, treeView2.SelectedNode.Nodes[i].Text, writer, dataGridView1.Rows[j].Cells[3].Value.ToString(), path, barisGrid);
                                    //playingVideoLive(path1, treeView2.SelectedNode.Nodes[i].Text, barisGrid);

                                }
                                if (casparClient2.Connected)
                                {
                                    var writer = new StreamWriter(casparClient2.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("PLAY 1-" + layers + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\"");
                                        writer.Flush();
                                    }
                                    else //if (delay != 0)
                                    {
                                        playingVideo(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    playingVideo(delay,  layers, treeView2.SelectedNode.Nodes[i].Text, writer, dataGridView1.Rows[j].Cells[3].Value.ToString(), path, barisGrid);
                                }
                            }
                            else if (barisGrid == "ft")
                            {
                                if (casparClient.Connected)
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("CG 1-" + layers + " ADD 1" + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\" 1");
                                        writer.Flush();
                                    }
                                    else //if (delay != 0)
                                    {
                                        playingCG(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    playingCG(delay,  layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                }
                                if (casparClient2.Connected)
                                {
                                    var writer = new StreamWriter(casparClient2.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("CG 1-" + layers + " ADD 1" + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\" 1");
                                        writer.Flush();
                                    }
                                    else //if (delay != 0)
                                    {
                                        playingCG(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    playingCG(delay,  layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                }
                            }
                            
                            i++;
                        } //endwhile
                    }
                } //endif
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }
        /*
        public void playingVideoLive(string path,string namafile,string format)
        {
            await PutTaskDelay(delay);
            await PutTaskDelay(startdelay);

            if (dur.LongCount() > 5)
            {
                string[] durj2 = dur.Split(':');
                jam2 = int.Parse(durj2[0]);
                menit2 = int.Parse(durj2[1]);
                string[] durd = durj2[2].Split('.');
                detik2 = int.Parse(durd[0]);
            }
            else
            {
                string[] durj2 = dur.Split(':');
                menit2 = int.Parse(durj2[1]);
                string[] durd = durj2[2].Split('.');
                detik2 = int.Parse(durd[0]);
            }

            videoLive.playlist.stop();
            videoLive.playlist.clear();
            videoLive.playlist.add("file:///" + path +"\\"+ namafile + "." + format);
            videoLive.playlist.play();
            timer1.Start();
        }
        */

        public async void playingVideo(int delay,  string layers, string namafile, StreamWriter writer, string dur, string path, string format)
        {
            //var writer = new StreamWriter(casparClient.GetStream());
            //MessageBox.Show(path);
            await PutTaskDelay(delay);
            

            if (dur.LongCount() > 5)
            {
                string[] durj2 = dur.Split(':');
                jam2 = int.Parse(durj2[0]);
                menit2 = int.Parse(durj2[1]);
                string[] durd = durj2[2].Split('.');
                detik2 = int.Parse(durd[0]);
            }
            else
            {
                string[] durj2 = dur.Split(':');
                menit2 = int.Parse(durj2[1]);
                string[] durd = durj2[2].Split('.');
                detik2 = int.Parse(durd[0]);
            }
            //play video live
            videoLive.playlist.stop();
            videoLive.playlist.clear();
            videoLive.playlist.add("file:///" + path + "\\" + namafile + "." + format);
            videoLive.playlist.play();

            //play video server
            writer.WriteLine("PLAY 1-" + layers + " \"" + namafile + "\" CUT 1 Linear RIGHT");
            writer.Flush();
            timer1.Start();
        }

        public async void playingCG(int delay, string layers, string namafile, StreamWriter writer)
        {
            //var writer = new StreamWriter(casparClient.GetStream());
            await PutTaskDelay(delay);
            

            writer.WriteLine("CG 1-" + layers + " ADD 1" + " \"" + namafile + "\" 1");
            writer.Flush();
        }

        public void stopVideo( string layers, StreamWriter writer)
        {
            videoLive.playlist.stop();
            writer.WriteLine("STOP 1-" + layers);
            writer.Flush();

            timer1.Stop();
            jamLabel.Text = "0";
            menitLabel.Text = "0";
            detikLabel.Text = "0";
        }

        public void stopCG( string layers, StreamWriter writer)
        {
            //var writer = new StreamWriter(casparClient.GetStream());

            writer.WriteLine("CG 1-" + layers + " STOP 1");
            writer.Flush();
        }

        public void pauseVideo(string layers, string namafile, StreamWriter writer)
        {
            videoLive.playlist.pause();
            writer.WriteLine("PAUSE 1-" + layers + " \"" + namafile + "\"");
            writer.Flush();

            timer1.Stop();
        }

        public void resumeVideo(string layers, string namafile, StreamWriter writer)
        {
            videoLive.playlist.play();
            writer.WriteLine("RESUME 1-" + layers + " \"" + namafile + "\"");
            writer.Flush();

            timer1.Start();
        }

        public void loadVideo(string layers, string namafile, StreamWriter writer, string path, string format)
        {
            videoLive.playlist.stop();
            videoLive.playlist.clear();
            videoLive.playlist.add("file:///" + path + "\\" + namafile + "." + format);
            writer.WriteLine("LOAD 1-" + layers + " \"" + namafile + "\"");
            writer.Flush();

        }


        //Pause
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i, j;
                string layers, barisGrid;

                //wmp.Ctlcontrols.stop();
                //videoPreview.playlist.pause();
                i = 0;
                j = 0;
                TreeNode node = treeView2.SelectedNode;
                if (node == null)
                {
                    MessageBox.Show("Node unknown, Try to add some file !");
                }
                else
                {
                    //node treeview2 click
                    //data = node.Text;

                    if (node.Nodes.Count == 0)
                    {
                        if (node.Parent != null)
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }
                        else
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }


                        layers = dataGridView1.Rows[j].Cells[2].Value.ToString();


                        barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();
                        
                        if (barisGrid == "mp4" || barisGrid == "mxf"|| barisGrid == "png")
                        {
                            if (casparClient.Connected)
                            {
                                if (ON)
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    pauseVideo(layers, treeView2.SelectedNode.Text, writer);
                                    ON = false;
                                    //btnPause.Text = "Resume";
                                }
                                else
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    resumeVideo(layers, treeView2.SelectedNode.Text, writer);
                                    ON = true;
                                    //btnPause.Text = "Pause";
                                }
                            }

                            if (casparClient2.Connected)
                            {
                                if (ON)
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    pauseVideo(layers, treeView2.SelectedNode.Text, writer);
                                    ON = false;
                                    //btnPause.Text = "Resume";
                                }
                                else
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    resumeVideo(layers, treeView2.SelectedNode.Text, writer);
                                    ON = true;
                                    //btnPause.Text = "Pause";
                                }
                            }
                        }

                    }
                    else if (node.Nodes.Count != 0)
                    {
                        while (i < treeView2.SelectedNode.Nodes.Count)
                        {
                            //wmp.URL = this.path1 + "\\" + treeView2.SelectedNode.Nodes[i].Text + ".mp4";
                            //layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                            if (node.Nodes[i].Parent != null)
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }
                            else
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }

                            layers = dataGridView1.Rows[j].Cells[2].Value.ToString();

                            //cek apakah video atau cg
                            barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();

                            //var writer = new StreamWriter(casparClient.GetStream());
                            if (barisGrid == "mp4" || barisGrid == "mxf" || barisGrid == "" || barisGrid=="png")
                            {
                                if (casparClient.Connected)
                                {
                                    if (ON)
                                    {
                                        var writer = new StreamWriter(casparClient.GetStream());
                                        pauseVideo(layers, treeView2.SelectedNode.Text, writer);
                                        ON = false;
                                        btnPause.Text = "Resume";
                                    }
                                    else
                                    {
                                        var writer = new StreamWriter(casparClient.GetStream());
                                        resumeVideo(layers, treeView2.SelectedNode.Text, writer);
                                        ON = true;
                                        btnPause.Text = "Pause";
                                    }
                                }
                                if (casparClient2.Connected)
                                {
                                    if (ON)
                                    {
                                        var writer = new StreamWriter(casparClient.GetStream());
                                        pauseVideo(layers, treeView2.SelectedNode.Text, writer);
                                        ON = false;
                                        btnPause.Text = "Resume";
                                    }
                                    else
                                    {
                                        var writer = new StreamWriter(casparClient.GetStream());
                                        resumeVideo(layers, treeView2.SelectedNode.Text, writer);
                                        ON = true;
                                        btnPause.Text = "Pause";
                                    }
                                }
                            }

                            i++;
                        } //endwhile
                    }
                } //endif
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }

        }

        //Stop
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                int i, j,  delay;
                string layers, barisGrid;

                //videoPreview.playlist.stop();
                i = 0;
                j = 0;
                TreeNode node = treeView2.SelectedNode;
                if (node == null)
                {
                    MessageBox.Show("Node unknown, Try to add some file !");
                }
                else
                {
                    //node treeview2 click
                    //data = node.Text;

                    if (node.Nodes.Count == 0)
                    {
                        if (node.Parent != null)
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }
                        else
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }

                        
                        layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                        delay = Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value);

                        barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();
                        if (barisGrid == "mp4" || barisGrid == "mxf" || barisGrid == "png")
                        {
                            if (casparClient.Connected)
                            {
                                var writer = new StreamWriter(casparClient.GetStream());
                                stopVideo(layers, writer);
                            }
                            if (casparClient2.Connected)
                            {
                                var writer = new StreamWriter(casparClient2.GetStream());
                                stopVideo(layers, writer);
                            }

                            lblVidTime2.Text = "0:00:00";
                            lblFullTime2.Text = "0:00:00";
                        }
                        else if (barisGrid == "ft")
                        {
                            if (casparClient.Connected)
                            {
                                var writer = new StreamWriter(casparClient.GetStream());
                                stopCG(  layers, writer);
                            }
                            if (casparClient2.Connected)
                            {
                                var writer = new StreamWriter(casparClient2.GetStream());
                                stopCG( layers, writer);
                            }
                        }
                    }
                    else if (node.Nodes.Count != 0)
                    {
                        while (i < treeView2.SelectedNode.Nodes.Count)
                        {
                            //wmp.URL = this.path1 + "\\" + treeView2.SelectedNode.Nodes[i].Text + ".mp4";
                            //layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                            if (node.Nodes[i].Parent != null)
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }
                            else
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }

                            /* buat timecode
                            if (durasi().LongCount() > 5)
                            {
                                jam = getJam();
                                menit = getMenit();
                                detik = getDetik();
                            }
                            else
                            {
                                menit = getMenit();
                                detik = getDetik();
                            }
                            */

                           
                            layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                            delay = Convert.ToInt32(dataGridView1.Rows[j].Cells[1].Value);

                            //cek apakah video atau cg
                            barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();

                            //var writer = new StreamWriter(casparClient.GetStream());
                            if (barisGrid == "mp4" || barisGrid == "mxf" || barisGrid == "" || barisGrid=="png")
                            {
                                if (casparClient.Connected)
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("PLAY 1-" + layers + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\"");
                                        writer.Flush();
                                    }
                                    else if (delay != 0)
                                    {
                                        playingVideo(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    stopVideo( layers, writer);
                                }
                                if (casparClient2.Connected)
                                {
                                    var writer = new StreamWriter(casparClient2.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("PLAY 1-" + layers + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\"");
                                        writer.Flush();
                                    }
                                    else //if (delay != 0)
                                    {
                                        playingVideo(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    stopVideo( layers, writer);
                                }

                                lblVidTime2.Text = "0:00:00";
                                lblFullTime2.Text = "0:00:00";
                            }
                            else if (barisGrid == "ft")
                            {
                                if (casparClient.Connected)
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("CG 1-" + layers + " ADD 1" + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\" 1");
                                        writer.Flush();
                                    }
                                    else //if (delay != 0)
                                    {
                                        playingCG(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    stopCG( layers, writer);
                                }
                                if (casparClient2.Connected)
                                {
                                    var writer = new StreamWriter(casparClient2.GetStream());
                                    /*
                                    if (delay == 0)
                                    {
                                        writer.WriteLine("CG 1-" + layers + " ADD 1" + " \"" + treeView2.SelectedNode.Nodes[i].Text + "\" 1");
                                        writer.Flush();
                                    }
                                    else //if (delay != 0)
                                    {
                                        playingCG(delay, startdelay, layers, treeView2.SelectedNode.Nodes[i].Text, writer);
                                    }
                                    */
                                    stopCG( layers, writer);
                                }
                            }
                            i++;
                        } //endwhile
                    }
                } //endif
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }


        //Load
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                int i, j;
                string layers, barisGrid;

                //wmp.Ctlcontrols.stop();
                //videoPreview.playlist.stop();
                i = 0;
                j = 0;
                TreeNode node = treeView2.SelectedNode;
                if (node == null)
                {
                    MessageBox.Show("Node unknown, Try to add some file !");
                }
                else
                {
                    //node treeview2 click
                    //data = node.Text;

                    if (node.Nodes.Count == 0)
                    {
                        if (node.Parent != null)
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }
                        else
                        {
                            while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }


                        layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                        namaServer = dataGridView1.Rows[j].Cells[7].Value.ToString();
                        while (namaServer != tes.gridServer.Rows[o].Cells[1].Value.ToString())
                        {
                            o++;
                        }
                        string path = "\\\\" + tes.gridServer.Rows[o].Cells[2].Value.ToString() + "\\" + tes.gridServer.Rows[o].Cells[6].Value.ToString();

                        barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();
                        if (barisGrid == "mp4" || barisGrid == "mxf" || barisGrid == "png")
                        {
                            if (casparClient.Connected)
                            {
                                var writer = new StreamWriter(casparClient.GetStream());
                                loadVideo(layers, treeView2.SelectedNode.Text, writer, path, barisGrid);
                            }
                            if (casparClient2.Connected)
                            {
                                var writer = new StreamWriter(casparClient2.GetStream());
                                loadVideo(layers, treeView2.SelectedNode.Text, writer, path, barisGrid);
                            }
                        }

                    }
                    else if (node.Nodes.Count != 0)
                    {
                        while (i < treeView2.SelectedNode.Nodes.Count)
                        {

                            if (node.Nodes[i].Parent != null)
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }
                            else
                            {
                                while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Nodes[i].Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                                {
                                    j++;
                                }
                            }

                            layers = dataGridView1.Rows[j].Cells[2].Value.ToString();
                            namaServer = dataGridView1.Rows[j].Cells[7].Value.ToString();
                            while (namaServer != tes.gridServer.Rows[o].Cells[1].Value.ToString())
                            {
                                o++;
                            }
                            string path = "\\\\" + tes.gridServer.Rows[o].Cells[2].Value.ToString() + "\\" + tes.gridServer.Rows[o].Cells[6].Value.ToString();

                            //cek apakah video atau cg
                            barisGrid = dataGridView1.Rows[j].Cells[4].Value.ToString();

                            //var writer = new StreamWriter(casparClient.GetStream());
                            if (barisGrid == "mp4" || barisGrid == "mxf" || barisGrid == "" || barisGrid=="png")
                            {
                                if (casparClient.Connected)
                                {
                                    var writer = new StreamWriter(casparClient.GetStream());

                                    loadVideo(layers, treeView2.SelectedNode.Text, writer, path, barisGrid);
                                }
                                if (casparClient2.Connected)
                                {
                                    var writer = new StreamWriter(casparClient2.GetStream());

                                    loadVideo(layers, treeView2.SelectedNode.Text, writer, path, barisGrid);
                                }
                            }


                            i++;
                        } //endwhile
                    }
                } //endif
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }

        }

        public  void AutoconnectServer(string id, string host, string port, string servername, string folderPath, int i)
        {
            bool terhubung = false;
            try
            {
                if (id == "1")
                {
                    casparClient.Connect(host, int.Parse(port));
                    tes.Path1 = @"\\" + host + "\\" + folderPath;
                    tes.ServerName1 = servername;
                    if (casparClient.Connected)
                    {
                        tes.lblCon.Text = "CONNECTED!!!";
                        tes.lblCon.ForeColor = Color.Green;
                        tes.gridServer.Rows[i].Cells[5].Value = "true";
                        MessageBox.Show("Caspar Server 1 CONNECTED !");
                        terhubung = true;

                        ListDirectory(treeView1, treeView3, tes.Path1);
                        treeView1.ExpandAll();
                        treeView3.ExpandAll();
                        treeView1.Refresh();
                        treeView3.Refresh();
                    }
                }

                if (id == "2")
                {
                    casparClient2.Connect(host, int.Parse(port));
                    tes.Path2 = @"\\" + host + "\\" + folderPath;
                    //MessageBox.Show(path2);
                    tes.ServerName2 = servername;
                    if (casparClient2.Connected)
                    {
                        tes.lblCon.Text = "CONNECTED!!!";
                        tes.lblCon.ForeColor = Color.Green;
                        tes.gridServer.Rows[i].Cells[5].Value = "true";
                        MessageBox.Show("Caspar Server 2 CONNECTED !");
                        terhubung = true;


                        ListDirectory(treeView1, treeView3, tes.Path2);
                        treeView1.ExpandAll();
                        treeView3.ExpandAll();
                        treeView1.Refresh();
                        treeView3.Refresh();
                    }
                }
            }
            catch (Exception)
            {
                tes.lblCon.Text = "DISCONNECTED!!!";
                tes.lblCon.ForeColor = Color.Red;
                tes.txtStats.Text = "false";
                MessageBox.Show("Caspar Server NOT CONNECTED !");
                terhubung = false;
            }
        }

            private void MediaPlayer_Load(object sender, EventArgs e)
        {
            tes.Show();
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Delay", typeof(string));
            table.Columns.Add("Layer", typeof(string));
            table.Columns.Add("Durasi", typeof(string));
            table.Columns.Add("Format", typeof(string));
            table.Columns.Add("StartDelay", typeof(string));
            table.Columns.Add("Group", typeof(string));
            table.Columns.Add("ServerName", typeof(string));
            if (tes.gridServer.Rows.Count != 0)
            {
                for (int i = 0; i < tes.gridServer.Rows.Count; i++)
                {
                    tes.gridServer.Rows[i].Cells[5].Value = "false";
                    AutoconnectServer(tes.gridServer.Rows[i].Cells[0].Value.ToString(), tes.gridServer.Rows[i].Cells[2].Value.ToString(), tes.gridServer.Rows[i].Cells[3].Value.ToString(), tes.gridServer.Rows[i].Cells[1].Value.ToString(),tes.gridServer.Rows[i].Cells[6].Value.ToString(),i);
                }
            }

            tes.Hide();
            dataGridView1.DataSource = table;
            if (radDelay.Checked == true)
            {
                labelDetik.Visible = false;
                labelMenit.Visible = false;
                labelJam.Visible = false;
                jamBox.Visible = false;
                menitBox.Visible = false;
                detikBox.Visible = false;
                delayBox.Visible = true;
            }
        }

        private void MediaPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnPlay.PerformClick();
            }

            if (e.KeyCode == Keys.F1)
            {
                btnStop.PerformClick();
            }

            if (e.KeyCode == Keys.F3)
            {
                btnLoad.PerformClick();
            }

            if (e.KeyCode == Keys.F4)
            {
                btnPause.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveToolStripMenuItem.PerformClick();
            }
            if (e.Shift && e.Control && e.KeyCode == Keys.S)
            {
                saveAsToolStripMenuItem.PerformClick();
            }
            if (e.Control && e.KeyCode == Keys.O)
            {
                loadButton.PerformClick();
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnPlay.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnStop.PerformClick();
            }
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            Font newFont = new Font(treeView2.Font.FontFamily, 10, FontStyle.Bold);
            int j = 0;
            try
            {
                if (txtGroup.Text == "" || txtGroup.Text == " ")
                {
                    throw new Exception();
                }

                bool dataFound = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    object name = row.Cells[0].Value;
                    if (name.ToString() == txtGroup.Text)
                    {
                        MessageBox.Show("Group already exists !");
                        dataFound = true;
                        break;
                    }
                }

                if (!dataFound)
                {
                    treeView2.BeginUpdate();
                    TreeNode node = new TreeNode(txtGroup.Text.ToUpper());
                    node.NodeFont = newFont;
                    treeView2.Nodes.Add(node);
                    treeView2.EndUpdate();

                    table.Rows.Add(txtGroup.Text.ToUpper(), "0", "10", "", "", "", txtGroup.Text.ToUpper());
                    dataGridView1.DataSource = table;

                    dataGridView1.ClearSelection();
                    txtGroup.Text = ""; //reset
                }
            }
            catch
            {
                MessageBox.Show("The node must be filled in");
                txtGroup.Focus();
            }
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            int j = 0;
            int i = 0;
            if (treeView2.SelectedNode != null)
            {
                if (treeView2.SelectedNode.Parent != null)
                {
                    while (treeView2.SelectedNode.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || treeView2.SelectedNode.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        j++;
                    }
                }
                else
                {
                    while (treeView2.SelectedNode.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || treeView2.SelectedNode.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        j++;
                    }
                }

                if (treeView2.SelectedNode.Nodes.Count != 0)
                {
                    while (i < treeView2.SelectedNode.Nodes.Count)
                    {
                        if (treeView2.SelectedNode.Nodes[i].Parent != null)
                        {
                            while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || treeView2.SelectedNode.Nodes[i].Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }
                        else
                        {
                            while (treeView2.SelectedNode.Nodes[i].Text != this.dataGridView1.Rows[j].Cells[0].Value.ToString() || treeView2.SelectedNode.Nodes[i].Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                            {
                                j++;
                            }
                        }
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows[j].Index);
                        treeView2.SelectedNode.Nodes[i].Remove();
                    }
                    //treeView2.Nodes.Remove(treeView2.SelectedNode);
                    i++;
                }
                else
                {
                    treeView2.Nodes.Remove(treeView2.SelectedNode);
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows[j].Index);
                    dataGridView1.ClearSelection();
                }
                i++;

            }
            else
            {
                MessageBox.Show("Can't remove node, Please select node !\n\nOR\n\n Try to add some file !");
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Path.GetExtension(e.Node.Text) == "")
            {
                txtShowItem.Text = "";
            }
            else
            {
                if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent
                       != null && e.Node.Parent.Parent.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent.Parent != null && (e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent
                    != null && e.Node.Parent.Parent.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent != null && (e.Node.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent
                    != null) && (e.Node.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && (e.Node.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && (e.Node.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Text + "\\" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Text != tes.Path2))
                {
                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && (e.Node.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Text + "\\" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null) && (e.Node.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Text != tes.Path2))
                {
                    txtShowItem.Text = e.Node.Parent.Parent.Text + "\\" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent != null && (e.Node.Parent.Text != tes.Path1 || e.Node.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Text + "\\" + e.Node.Text;
                    }
                }
                else if (e.Node.Parent != null && (e.Node.Parent.Text == tes.Path1 || e.Node.Parent.Text == tes.Path2))
                {
                    txtShowItem.Text = e.Node.Text;
                }
                else if (e.Node.Text != "*.mp4" || e.Node.Text != "*.ft" || e.Node.Text != "*.mxf")
                {
                    txtShowItem.Text = "";
                }
                else
                {
                    txtShowItem.Text = "";
                }
            }
        }
        private void treeView3_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Path.GetExtension(e.Node.Text) == "")
            {
                txtShowItem.Text = "";
            }
            else
            {
                if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent
                       != null && e.Node.Parent.Parent.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent.Parent != null && (e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent
                       != null && e.Node.Parent.Parent.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent != null && (e.Node.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && e.Node.Parent.Parent.Parent.Parent
                    != null) && (e.Node.Parent.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Parent.Text + "/" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null && (e.Node.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && (e.Node.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Text + "\\" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else if ((e.Node.Parent != null && e.Node.Parent.Parent != null && e.Node.Parent.Parent.Parent != null) && (e.Node.Parent.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Parent.Text != tes.Path2))
                {

                    txtShowItem.Text = e.Node.Parent.Parent.Parent.Text + "\\" + e.Node.Parent.Parent.Text + "/" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent.Parent != null && (e.Node.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Parent.Text + "\\" + e.Node.Parent.Text + "/" + e.Node.Text;
                    }
                }
                else
                if ((e.Node.Parent != null && e.Node.Parent.Parent != null) && (e.Node.Parent.Parent.Text != tes.Path1 || e.Node.Parent.Parent.Text != tes.Path2))
                {
                    txtShowItem.Text = e.Node.Parent.Parent.Text + "\\" + e.Node.Parent.Text + "/" + e.Node.Text;
                }
                else if (e.Node.Parent != null && (e.Node.Parent.Text != tes.Path1 || e.Node.Parent.Text != tes.Path2))
                {
                    if (Path.GetExtension(e.Node.Text) == "")
                    {
                        txtShowItem.Text = "";
                    }
                    else
                    {
                        txtShowItem.Text = e.Node.Parent.Text + "\\" + e.Node.Text;
                    }
                }
                else if (e.Node.Parent != null && (e.Node.Parent.Text == tes.Path1 || e.Node.Parent.Text == tes.Path2))
                {
                    txtShowItem.Text = e.Node.Text;
                }
                else if (e.Node.Text != "*.mp4" || e.Node.Text != "*.ft" || e.Node.Text != "*.mxf" || e.Node.Text != "*.png")
                {
                    txtShowItem.Text = "";
                }
                else
                {
                    txtShowItem.Text = "";
                }
            }
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int j = 0;
            int o = 0;
            if (treeView2.SelectedNode == null)
            {
                MessageBox.Show("File not selected !");
            }
            else
            {
                if (treeView2.SelectedNode.Parent != null) //kalau dia punya anak atau dia memiliki orang tua/group
                {
                    while (treeView2.SelectedNode.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || treeView2.SelectedNode.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        j++;
                    }
                }
                else
                {
                    while (treeView2.SelectedNode.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || treeView2.SelectedNode.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        j++;
                    }
                }
                string newPath = "";
                string namaServerFile = dataGridView1.Rows[j].Cells[7].Value.ToString();
                //MessageBox.Show(namaServerFile + "\n\n" + tes.gridServer.Rows[o].Cells[1].Value.ToString());
                if(namaServerFile==null|| namaServerFile=="")
                {
                    //kalau folder
                }
                else
                {
                    while (namaServerFile != tes.gridServer.Rows[o].Cells[1].Value.ToString())
                    {
                        o++;
                    }
                    string path = "\\\\" + tes.gridServer.Rows[o].Cells[2].Value.ToString() + "\\" + tes.gridServer.Rows[o].Cells[6].Value.ToString();
                    if (casparClient.Connected)
                    {
                        newPath = "file:///" + path + "\\" + treeView2.SelectedNode.Text + "." + dataGridView1.Rows[j].Cells[4].Value.ToString();
                    }
                    if (casparClient2.Connected)
                    {
                        newPath = "file:///" + path + "\\" + treeView2.SelectedNode.Text + "." + dataGridView1.Rows[j].Cells[4].Value.ToString();
                    }
                    videoPreview.playlist.stop();
                    videoPreview.playlist.add(newPath);
                    videoPreview.playlist.play();
                    videoPreview.playlist.clear();
                }
            }
        }

        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //MessageBox.Show();
            try
            {
                dataGridView1.ClearSelection();
                int j = 0;

                if (e.Node.Parent != null) //kalau dia punya anak atau dia memiliki orang tua/group
                {
                    while (e.Node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || e.Node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        j++;
                    }
                }
                else
                {
                    while (e.Node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || e.Node.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        j++;
                    }
                }

                //MessageBox.Show(e.Node.Parent.Text);

                //MessageBox.Show(dataGridView1.Rows.Count.ToString());
                //ini gak terlalu penting hahaha
                dataGridView1.Rows[j].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[j].Cells[0];

                NameBox.Text = e.Node.Text;
                layerBox.Text = dataGridView1.Rows[j].Cells[2].Value.ToString();
                serverBox.Text = dataGridView1.Rows[j].Cells[7].Value.ToString();
                delayBox.Text = dataGridView1.Rows[j].Cells[1].Value.ToString();
            }
            catch (Exception s) { MessageBox.Show(s.Message); }
        }

        private void set_media_Click(object sender, EventArgs e)
        {
            try
            {
                treeView2.LabelEdit = true;
                treeView2.SelectedNode.BeginEdit();
                TreeNode node = treeView2.SelectedNode;
                //string nodes = node.ToString();
                //string parse1 = nodes.Remove(0, 10);

                int j = 0;
                if (node.Parent != null)
                {
                    while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        //while (node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                        //{
                        j++;
                        //}

                    }
                }
                else
                {
                    while (node.Text != dataGridView1.Rows[j].Cells[0].Value.ToString() || node.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                    {
                        //while (node.Parent.Text != dataGridView1.Rows[j].Cells[6].Value.ToString())
                        //{
                        j++;
                        //}
                    }
                }

                int jam = int.Parse(jamBox.Text);
                int menit = int.Parse(menitBox.Text);
                int detik = int.Parse(detikBox.Text);

                int f = 0;
                jam = jam * 3600;
                menit = menit * 60;
                int hasil = (jam + menit + detik) * 1000;
                if (dataGridView1.Rows[j].Cells[4].Value.ToString() == "")
                {
                    dataGridView1.Rows[j].Cells[0].Value = NameBox.Text.ToUpper();
                    Font newFont = new Font(treeView2.Font.FontFamily, 10, FontStyle.Bold);
                    NameBox.CharacterCasing = CharacterCasing.Upper;
                    node.NodeFont = newFont;
                    for (f = 0; f < dataGridView1.Rows.Count; f++)
                    {
                        if (dataGridView1.Rows[f].Cells[6].Value.ToString() == node.Text)
                            dataGridView1.Rows[f].Cells[6].Value = NameBox.Text.ToUpper();
                    }
                    node.Text = NameBox.Text.ToUpper();
                }
                else
                {
                    //MessageBox.Show("File Not Group");
                }
                if (radStartDelay.Checked == true)
                {
                    dataGridView1.Rows[j].Cells[1].Value = 0;
                    dataGridView1.Rows[j].Cells[1].Value = hasil;
                }
                else if (radDelay.Checked == true)
                {
                    dataGridView1.Rows[j].Cells[1].Value = 0;
                    dataGridView1.Rows[j].Cells[1].Value = delayBox.Text;
                }
                else
                {
                    MessageBox.Show("Please, Choose one for your delay");

                }
                dataGridView1.Rows[j].Cells[2].Value = layerBox.Text;
                treeView2.EndUpdate();
                
            }
            catch (Exception)
            {
                //MessageBox.Show("File not found !\n\n\nLet's try to add some videos");
                //MessageBox.Show(s.Message);
                MessageBox.Show("Node not selected !!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            detik2 = detik2 - 1;

            if (detik2 == -1)
            {
                menit2 = menit2 - 1;
                detik2 = 59;
            }


            if (menit2 == -1)
            {
                jam2 = jam2 - 1;
                menit2 = 59;
            }

            if (jam2 <= 0 && menit2 <= 0 && detik2 <= 0)
            {
                timer1.Stop();
                //MessageBox.Show("Stop!");
            }

            string detiks = Convert.ToString(detik2);
            string menits = Convert.ToString(menit2);
            string jams = Convert.ToString(jam2);

            detikLabel.Text = detiks;
            menitLabel.Text = menits;
            jamLabel.Text = jams;

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                table.WriteXml(saveFile.FileName + "_Data", XmlWriteMode.WriteSchema);
                Class1 serializer = new Class1();
                serializer.SerializeTreeView(this.treeView2, saveFile.FileName);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = Application.StartupPath + "\\..\\..\\MyTreeView.xml";
            //openFile.Filter = "XML|*.xml";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                table.ReadXml(openFile.FileName + "_Data");
                dataGridView1.DataSource = table;
                this.treeView2.Nodes.Clear();
                Class1 serializer = new Class1();
                serializer.DeserializeTreeView(this.treeView2, openFile.FileName);
            }
            treeView2.ExpandAll();
            for (int k = 0; k < treeView2.Nodes.Count; k++)
            {
                treeView2.Nodes[k].NodeFont = new Font(treeView2.Font.FontFamily, 10, FontStyle.Bold);
            }
            treeView2.ExpandAll();
            for (int k = 0; k < treeView2.Nodes.Count; k++)
            {
                treeView2.Nodes[k].NodeFont = new Font(treeView2.Font.FontFamily, 10, FontStyle.Bold);
            }
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tes.Show();
            //tes.TopMost = true;
        }

        private void clearOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (casparClient.Connected)
                {
                    var writer = new StreamWriter(casparClient.GetStream());
                    writer.WriteLine("CLEAR 1");
                    writer.WriteLine("MIXER 1 CLEAR");
                    writer.Flush();
                }
                if (casparClient2.Connected)
                {
                    var writer = new StreamWriter(casparClient2.GetStream());
                    writer.WriteLine("CLEAR 1");
                    writer.WriteLine("MIXER 1 CLEAR");
                    writer.Flush();
                }
            }
            catch { }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPlay.PerformClick();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStop.PerformClick();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnLoad.PerformClick();
        }

        private void pauseResumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPause.PerformClick();
        }

        private void previewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            previewToolStripMenuItem.PerformClick();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveButton.PerformClick();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveButton.PerformClick();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            loadButton.PerformClick();
        }

        private void radDelay_Click(object sender, EventArgs e)
        {
            labelDetik.Visible = false;
            labelMenit.Visible = false;
            labelJam.Visible = false;
            jamBox.Visible = false;
            menitBox.Visible = false;
            detikBox.Visible = false;
            delayBox.Visible = true;
            radStartDelay.Checked = false;
        }

        private void radStartDelay_Click(object sender, EventArgs e)
        {
            labelDetik.Visible = true;
            labelMenit.Visible = true;
            labelJam.Visible = true;
            jamBox.Visible = true;
            menitBox.Visible = true;
            detikBox.Visible = true;
            delayBox.Visible = false;
            radDelay.Checked = false;
        }
        List<TreeView> tree = new List<TreeView>();

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView newTree = new TreeView();
            tree.Add(newTree);
            this.Controls.Add(newTree);
        }

        private string TimeFormat(int miliSec)
        {
            int totalSec = miliSec / 1000;
            int hours = (totalSec >= 3600) ? totalSec / 3600 : 0;
            int hours_res = (totalSec >= 3600) ? totalSec - (hours * 3600) : totalSec;
            int minutes = hours_res / 60;
            int minutes_res = hours_res - (minutes * 60);
            int seconds = minutes_res % 60;
            string strTime = String.Format("{0,1}:{1,2:D2}:{2,2:D2}", hours, minutes, seconds);
            return strTime;
        }

        private int TimeFormatSecond(int miliSec)
        {
            int totalSec = miliSec / 1000;
            int hours = (totalSec >= 3600) ? totalSec / 3600 : 0;
            int hours_res = (totalSec >= 3600) ? totalSec - (hours * 3600) : totalSec;
            int minutes = hours_res / 60;
            int minutes_res = hours_res - (minutes * 60);
            int seconds = minutes_res % 60;

            return seconds;
        }

        private void videoPreview_MediaPlayerPlaying(object sender, EventArgs e)
        {
            lblFullTime.Text = TimeFormat((int)videoPreview.input.Length);
            /*
            int fps = Convert.ToInt32(videoPreview.input.fps.ToString());
            int dur = TimeFormatSecond((int)videoPreview.input.Length);
            int frame = fps * dur;
            lblFrame.Text = frame.ToString();
            */

            //MessageBox.Show(TimeFormat((int)videoPreview.input.Length));
        }

        private void videoPreview_MediaPlayerTimeChanged(object sender, AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEvent e)
        {
            lblVidTime.Text = TimeFormat(e.time);
            /*
            int fps = Convert.ToInt32(videoPreview.input.fps.ToString());
            int dur = TimeFormatSecond(e.time);
            int frame = fps * dur;
            lblSFrame.Text = frame.ToString();
            */
        }

        private void videoPreview_MediaPlayerEndReached(object sender, EventArgs e)
        {
            videoPreview.playlist.stop();
            videoPreview.playlist.clear();
        }

        private void videoPreview_MediaPlayerSeekableChanged(object sender, AxAXVLC.DVLCEvents_MediaPlayerSeekableChangedEvent e)
        {
            e.seekable = true;
        }

        private void videoLive_MediaPlayerEndReached(object sender, EventArgs e)
        {
            videoLive.playlist.stop();
        }

        private void videoLive_MediaPlayerSeekableChanged(object sender, AxAXVLC.DVLCEvents_MediaPlayerSeekableChangedEvent e)
        {
            e.seekable = true;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }

        private void videoPreview_play(object sender, EventArgs e)
        {
        }

        private void videoLive_MediaPlayerTimeChanged(object sender, AxAXVLC.DVLCEvents_MediaPlayerTimeChangedEvent e)
        {
            lblVidTime2.Text = TimeFormat(e.time);
            /*
            int fps = Convert.ToInt32(videoLive.input.fps.ToString());
            int dur = TimeFormatSecond(e.time);
            int frame = fps * dur;
            lblSFrame2.Text = frame.ToString();
            */
        }

        private void videoLive_MediaPlayerPlaying(object sender, EventArgs e)
        {
            lblFullTime2.Text = TimeFormat((int)videoLive.input.Length);
            /*
            int fps = Convert.ToInt32(videoLive.input.fps.ToString());
            int dur = TimeFormatSecond((int)videoLive.input.Length);
            int frame = fps * dur;
            lblFrame2.Text = frame.ToString();
            */
        }
    }
}


    