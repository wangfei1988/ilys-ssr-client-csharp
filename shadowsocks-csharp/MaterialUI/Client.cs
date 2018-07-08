using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.Win32;

namespace MaterialUI
{
    public partial class Client : MaterialForm
    {
        public Client()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        // 单文件变量申明位置
        public class PageVaribles
        {
            // 统计模块
            public static double STATS_transfer_all = 0;
            public static double STATS_transfer_used = 0;
            public static double STATS_transfer_all_g = 0;
            public static double STATS_transfer_used_g = 0;
            // 信息模块
            public static int INFO_class = 0;
            public static int INFO_connector = 0;
            public static DateTime INFO_expire;
            public static TimeSpan INFO_expire_ts;
            // 认证模块 8 11 7 6 10
            public static string AUTH_port = "";
            public static string AUTH_pass = "";
            public static string AUTH_method = "";
            public static string AUTH_protocol = "";
            public static string AUTH_obfs = "";
            // 其他
            public static bool autoruncheck = false;
        }

        private void Client_Load(object sender, EventArgs e)
        {
            if (GlobalVaribles.GlobalVaribles.LOGIN_ifLogged == false)
            {
                Login login = new Login();
                try
                {
                    login.ShowDialog();
                }
                catch { };
                if (GlobalVaribles.GlobalVaribles.LOGIN_ifLogged == true)
                {
                    InitSettings();
                    this.Text = "欢迎回来，" + GlobalVaribles.GlobalVaribles.LOGIN_username + "。";
                    this.labelAccount.Text = GlobalVaribles.GlobalVaribles.LOGIN_email;
                    延迟加载.Enabled = true;
                    刷新信息.Enabled = true;
                    try
                    {
                        Cursor myCursor = new Cursor("Altr_Gea.cur");
                        this.Cursor = myCursor;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        Application.Exit();
                    }
                    catch { };
                }
            }
        }

        private void InitSettings()
        {
            string ifjson = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "information.json");
            JObject ifobj = JObject.Parse(ifjson);
            if (ifobj.Property("a") != null)
            {
                if (ifobj["a"].ToString() == "True")
                {
                    materialCheckBox1.Checked = true;
                    PageVaribles.autoruncheck = true;
                }
            }
            if (ifobj.Property("b") != null)
            {
                if (ifobj["b"].ToString() == "True")
                {
                    materialCheckBox2.Checked = true;
                }
            }
            if (ifobj.Property("c") != null)
            {
                if (ifobj["c"].ToString() == "True")
                {
                    materialCheckBox3.Checked = true;
                }
            }
            if (ifobj.Property("d") != null)
            {
                if (ifobj["d"].ToString() == "True")
                {
                    materialCheckBox4.Checked = true;
                }
            }
        }

        public void RefreshData()
        {
            MySqlConnection msqlConnection = new MySqlConnection("Host = " + GlobalVaribles.GlobalVaribles.DB_Adress + "; Database = " + GlobalVaribles.GlobalVaribles.DB_DBName + "; Username = " + GlobalVaribles.GlobalVaribles.DB_Username + "; Password = " + GlobalVaribles.GlobalVaribles.DB_Password + ";SslMode = none;");
            msqlConnection.Open();
            string sql = "SELECT transfer_enable, u, d, class, node_connector, class_expire FROM user WHERE email = \"" + GlobalVaribles.GlobalVaribles.LOGIN_email + "\"";
            MySqlDataAdapter result = new MySqlDataAdapter(sql, msqlConnection);
            DataSet dataset = new DataSet();
            result.Fill(dataset, "tb");
            msqlConnection.Close();
            PageVaribles.STATS_transfer_all = long.Parse(dataset.Tables[0].Rows[0][0].ToString());
            PageVaribles.STATS_transfer_used = long.Parse(dataset.Tables[0].Rows[0][1].ToString()) + long.Parse(dataset.Tables[0].Rows[0][2].ToString());
            PageVaribles.STATS_transfer_all_g = PageVaribles.STATS_transfer_all / 1024 / 1024 / 1024;
            PageVaribles.STATS_transfer_used_g = PageVaribles.STATS_transfer_used / 1024 / 1024 / 1024;
            double avaliable = PageVaribles.STATS_transfer_all_g - PageVaribles.STATS_transfer_used_g;
            PageVaribles.INFO_class = int.Parse(dataset.Tables[0].Rows[0][3].ToString());
            PageVaribles.INFO_connector = int.Parse(dataset.Tables[0].Rows[0][4].ToString());
            PageVaribles.INFO_expire = Convert.ToDateTime(dataset.Tables[0].Rows[0][5].ToString());
            labelVIP.Text = "VIP" + PageVaribles.INFO_class.ToString();
            label3.Text = "剩余流量 " + avaliable.ToString() + "G";
            PageVaribles.INFO_expire_ts = PageVaribles.INFO_expire - DateTime.Now;
            if (PageVaribles.INFO_connector == 0)
            {
                label2.Text = "剩余 " + (int)PageVaribles.INFO_expire_ts.TotalDays + " 天 | 设备数 无限制";
            }
            else
            {
                label2.Text = "剩余 " + (int)PageVaribles.INFO_expire_ts.TotalDays + " 天 | 设备数 " + PageVaribles.INFO_connector.ToString() + " 台";
            }
        }

        public void GetAuthInfo()
        {
            MySqlConnection msqlConnection = new MySqlConnection("Host = " + GlobalVaribles.GlobalVaribles.DB_Adress + "; Database = " + GlobalVaribles.GlobalVaribles.DB_DBName + "; Username = " + GlobalVaribles.GlobalVaribles.DB_Username + "; Password = " + GlobalVaribles.GlobalVaribles.DB_Password + ";SslMode = none;");
            msqlConnection.Open();
            string sql = "SELECT port, passwd, method, protocol, obfs FROM user WHERE email = \"" + GlobalVaribles.GlobalVaribles.LOGIN_email + "\"";
            MySqlDataAdapter result = new MySqlDataAdapter(sql, msqlConnection);
            DataSet dataset = new DataSet();
            result.Fill(dataset, "tb");
            msqlConnection.Close();
            PageVaribles.AUTH_port = dataset.Tables[0].Rows[0][0].ToString();
            PageVaribles.AUTH_pass = dataset.Tables[0].Rows[0][1].ToString();
            PageVaribles.AUTH_method = dataset.Tables[0].Rows[0][2].ToString();
            PageVaribles.AUTH_protocol = dataset.Tables[0].Rows[0][3].ToString();
            PageVaribles.AUTH_obfs = dataset.Tables[0].Rows[0][4].ToString();
        }

        public void GetAnnounce()
        {
            MySqlConnection msqlConnection = new MySqlConnection("Host = " + GlobalVaribles.GlobalVaribles.DB_Adress + "; Database = " + GlobalVaribles.GlobalVaribles.DB_DBName + "; Username = " + GlobalVaribles.GlobalVaribles.DB_Username + "; Password = " + GlobalVaribles.GlobalVaribles.DB_Password + ";SslMode = none;");
            msqlConnection.Open();
            string sql = "SELECT markdown FROM announcement";
            MySqlDataAdapter result = new MySqlDataAdapter(sql, msqlConnection);
            DataSet dataset = new DataSet();
            result.Fill(dataset, "tb");
            msqlConnection.Close();
            if (dataset.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                     if (i != 0) materialLabel6.Text = materialLabel6.Text + "\n\n";
                     materialLabel6.Text = materialLabel6.Text + dataset.Tables[0].Rows[i][0];
                }
            }
        }

        public void RefreshNode()
        {
            MySqlConnection msqlConnection = new MySqlConnection("Host = " + GlobalVaribles.GlobalVaribles.DB_Adress + "; Database = " + GlobalVaribles.GlobalVaribles.DB_DBName + "; Username = " + GlobalVaribles.GlobalVaribles.DB_Username + "; Password = " + GlobalVaribles.GlobalVaribles.DB_Password + ";SslMode = none;");
            msqlConnection.Open();
            string sql = "SELECT id, name, server, info, type FROM ss_node";
            MySqlDataAdapter result = new MySqlDataAdapter(sql, msqlConnection);
            DataSet dataset = new DataSet();
            result.Fill(dataset, "tb");
            msqlConnection.Close();
            foreach (ListViewItem lvi in materialListView1.SelectedItems)
            {
                materialListView1.Items.RemoveAt(lvi.Index);
            }
            materialListView1.Clear();
            this.materialListView1.Columns.Add("国家", 100, HorizontalAlignment.Left);
            this.materialListView1.Columns.Add("服务器名称", 256, HorizontalAlignment.Left);
            this.materialListView1.Columns.Add("延迟", 60, HorizontalAlignment.Left);
            this.materialListView1.Columns.Add("IP", 0, HorizontalAlignment.Left);
            this.materialListView1.BeginUpdate();
            if (dataset.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    // 0是id，1是服务器名称，2是连接地址，3是说明，4是是否隐藏（0隐藏）。
                    if (int.Parse(dataset.Tables[0].Rows[i][0].ToString()) == 1 || int.Parse(dataset.Tables[0].Rows[i][0].ToString()) == 2) continue; //保留节点
                    if (int.Parse(dataset.Tables[0].Rows[i][4].ToString()) == 0) continue; //隐藏
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = dataset.Tables[0].Rows[i][1].ToString().Split(' ')[0]; //0是国家
                    lvi.SubItems.Add(dataset.Tables[0].Rows[i][1].ToString().Split(' ')[1]); //1服务器名称
                    lvi.SubItems.Add("999"); //2延迟
                    lvi.SubItems.Add(dataset.Tables[0].Rows[i][2].ToString()); //3连接地址
                    this.materialListView1.Items.Add(lvi);
                }
            }
            this.materialListView1.EndUpdate();
        }

        public void RefreshPing()
        {
            if (materialListView1.Items.Count > 0)
            {
                foreach (ListViewItem lt in materialListView1.Items)
                {
                    long lping = ping(lt.SubItems[3].Text);
                    lt.SubItems[2].Text = lping.ToString();
                }
            }
        }

        private void Relay_Tick(object sender, EventArgs e)
        {
            延迟加载.Enabled = false;
            RefreshData();
            GetAuthInfo();
            GetAnnounce();
            RefreshNode();
            RefreshPing();
            if(materialCheckBox2.Checked == true)
            {
                if (materialListView1.Items.Count > 0)
                {
                    string ip = "";
                    string name = "";
                    int min = 999;
                    foreach (ListViewItem lt in materialListView1.Items)
                    {
                        if(int.Parse(lt.SubItems[2].Text) < min)
                        {
                            ip = lt.SubItems[3].Text;
                            name = lt.SubItems[1].Text;
                            min = int.Parse(lt.SubItems[2].Text);
                        }
                    }
                    if (min != 999)
                    {
                        if (PageVaribles.STATS_transfer_all - PageVaribles.STATS_transfer_used < 0)
                        {
                            MessageBox.Show("你的流量已经耗尽，请联系管理员获取！", "提示");
                            return;
                        }
                        if (PageVaribles.INFO_expire_ts.TotalSeconds < 0)
                        {
                            MessageBox.Show("你的账户等级已到期，请联系管理员获取！", "提示");
                            return;
                        }
                        int mode = 0;
                        if (materialCheckBox3.Checked == true)
                            mode = 1;
                        else
                            mode = 2;
                        string ipaddress = JsonConfigGenerate(ip, mode);
                        File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "gui-config.json", ipaddress, Encoding.UTF8);
                        Shadowsocks.GlobalVaribles.InitController();
                        Shadowsocks.GlobalVaribles._controller.Start();
                        label1.Text = "已连接至：" + name;
                        materialListView1.Enabled = false;
                        materialCheckBox3.Enabled = false;
                        materialRaisedButton1.Visible = false;
                        materialRaisedButton2.Visible = true;
                        if (materialCheckBox4.Checked == true)
                            this.WindowState = FormWindowState.Minimized;
                    }
                    else
                    {
                        MessageBox.Show("所有节点检测均超时。", "提示：");
                    }
                }
                else
                {
                    MessageBox.Show("当前没有节点可以连接。", "提示：");
                }
            }
        }

        private void materialListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = this.materialListView1.Columns[e.ColumnIndex].Width;
        }

        public static long ping(string adress)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "ping test data";
            byte[] buf = Encoding.ASCII.GetBytes(data);
            PingReply reply = pingSender.Send(adress, 999, buf, options);
            int time = 0;
            while (time < 3)
            {
                if (reply.Status == IPStatus.Success)
                {
                    return reply.RoundtripTime;
                }
                time++;
            }
            return 999;
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            RefreshData();
            GetAuthInfo();
            GetAnnounce();
            RefreshNode();
            RefreshPing();
            if (materialRaisedButton1.Visible == false)
            {
                if (PageVaribles.STATS_transfer_all - PageVaribles.STATS_transfer_used < 0)
                {
                    MessageBox.Show("你的流量已经耗尽，请联系管理员获取！", "提示");
                    Shadowsocks.GlobalVaribles._controller.Stop();
                    materialListView1.Enabled = true;
                    materialCheckBox3.Enabled = true;
                    materialRaisedButton1.Visible = true;
                    materialRaisedButton2.Visible = false;
                }
                if (PageVaribles.INFO_expire_ts.TotalSeconds < 0)
                {
                    MessageBox.Show("你的账户等级已到期，请联系管理员获取！", "提示");
                    Shadowsocks.GlobalVaribles._controller.Stop();
                    materialListView1.Enabled = true;
                    materialCheckBox3.Enabled = true;
                    materialRaisedButton1.Visible = true;
                    materialRaisedButton2.Visible = false;
                }
            }
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            RefreshPing();
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(materialCheckBox1.Checked == true)
            {
                string acjson = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "account.json");
                JObject acobj = JObject.Parse(acjson);
                if (acobj.Property("password") == null)
                {
                    MessageBox.Show("你需要在登录时保存密码才可以使用此功能！", "提示：");
                    materialCheckBox1.Checked = false;
                }
            }
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "information.json",
                "{\"a\":\"" + materialCheckBox1.Checked.ToString() + "\"," +
                "\"b\":\"" + materialCheckBox2.Checked.ToString() + "\"," +
                "\"c\":\"" + materialCheckBox3.Checked.ToString() + "\"," +
                "\"d\":\"" + materialCheckBox4.Checked.ToString() + "\"}", Encoding.UTF8);
            try
            {
                if (materialCheckBox1.Checked)
                {
                    string path = Application.ExecutablePath;
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.SetValue("ILYSClientAutorun", path);
                    rk2.Close();
                    rk.Close();
                }
                else
                {
                    string path = Application.ExecutablePath;
                    RegistryKey rk = Registry.LocalMachine;
                    RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                    rk2.DeleteValue("ILYSClientAutorun", false);
                    rk2.Close();
                    rk.Close();
                }
            }
            catch
            {
                if(PageVaribles.autoruncheck != materialCheckBox1.Checked)
                if(PageVaribles.autoruncheck == false)
                {
                    MessageBox.Show("程序无权访问注册表，已取消开机自动启动选项。\n请尝试以管理员模式启动程序！", "警告");
                    File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "information.json",
                        "{\"a\":\"False\"," +
                        "\"b\":\"" + materialCheckBox2.Checked.ToString() + "\"," +
                        "\"c\":\"" + materialCheckBox3.Checked.ToString() + "\"," +
                        "\"d\":\"" + materialCheckBox4.Checked.ToString() + "\"}", Encoding.UTF8);
                    materialCheckBox1.Checked = false;
                }
                else
                {
                    MessageBox.Show("程序无权访问注册表，已重新勾选开机自动启动选项。\n请尝试以管理员模式启动程序！", "警告");
                    File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "information.json",
                        "{\"a\":\"True\"," +
                        "\"b\":\"" + materialCheckBox2.Checked.ToString() + "\"," +
                        "\"c\":\"" + materialCheckBox3.Checked.ToString() + "\"," +
                        "\"d\":\"" + materialCheckBox4.Checked.ToString() + "\"}", Encoding.UTF8);
                    materialCheckBox1.Checked = true;
                }
            }
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (PageVaribles.STATS_transfer_all - PageVaribles.STATS_transfer_used < 0)
            {
                MessageBox.Show("你的流量已经耗尽，请联系管理员获取！", "提示");
                return;
            }
            if (PageVaribles.INFO_expire_ts.TotalSeconds < 0)
            {
                MessageBox.Show("你的账户等级已到期，请联系管理员获取！", "提示");
                return;
            }
            if (this.materialListView1.SelectedItems.Count > 0)
            {
                if (GetMiddleString(this.materialListView1.SelectedItems[0].SubItems[2].ToString(), "{", "}") == "999")
                {
                    MessageBox.Show("该节点目前无法正常访问，请更换一个！", "提示");
                    return;
                }
                int mode = 0;
                if (materialCheckBox3.Checked == true)
                    mode = 1;
                else
                    mode = 2;
                string ipaddress = JsonConfigGenerate(GetMiddleString(this.materialListView1.SelectedItems[0].SubItems[3].ToString(), "{", "}"), mode);
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "gui-config.json", ipaddress, Encoding.UTF8);
                Shadowsocks.GlobalVaribles.InitController();
                Shadowsocks.GlobalVaribles._controller.Start();
                label1.Text = "已连接至：" + GetMiddleString(this.materialListView1.SelectedItems[0].SubItems[1].ToString(), "{", "}");
                materialListView1.Enabled = false;
                materialCheckBox3.Enabled = false;
                materialRaisedButton1.Visible = false;
                materialRaisedButton2.Visible = true;
                if (materialCheckBox4.Checked == true)
                    this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                MessageBox.Show("你确定你选择服务器了？", "提示");
            }
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Shadowsocks.GlobalVaribles._controller.Stop();
            materialListView1.Enabled = true;
            materialCheckBox3.Enabled = true;
            materialRaisedButton1.Visible = true;
            materialRaisedButton2.Visible = false;
        }

        public string JsonConfigGenerate(string ip, int mode)
        {
            string json = "{\"configs\" : " +
                "[{\"remarks\" : \"\"," +
                "\"id\" : \"\"," +
                "\"server\" : \"" + ip + "\"," +
                "\"server_port\" : " + PageVaribles.AUTH_port + "," +
                "\"server_udp_port\" : 0," +
                "\"password\" : \"" + PageVaribles.AUTH_pass + "\"," +
                "\"method\" : \"" + PageVaribles.AUTH_method + "\"," +
                "\"protocol\" : \"" + PageVaribles.AUTH_protocol + "\"," +
                "\"protocolparam\" : \"\"," +
                "\"obfs\" : \"" + PageVaribles.AUTH_obfs + "\"," +
                "\"obfsparam\" : \"\"," +
                "\"remarks_base64\" : \"\"," +
                "\"group\" : \"G41代理\"," +
                "\"enable\" : true,\"udp_over_tcp\" : false}]," +
                "\"index\" : 0,\"random\" : true," +
                "\"sysProxyMode\" : 3," +
                "\"shareOverLan\" : false," +
                "\"localPort\" : 1088," +
                "\"localAuthPassword\" : \"null\"," +
                "\"dnsServer\" : \"\"," +
                "\"reconnectTimes\" : 2," +
                "\"randomAlgorithm\" : 3," +
                "\"randomInGroup\" : false," +
                "\"TTL\" : 0,\"connectTimeout\" : 5," +
                "\"proxyRuleMode\" : " + mode + "," +
                "\"proxyEnable\" : false," +
                "\"pacDirectGoProxy\" : false," +
                "\"proxyType\" : 0,\"proxyHost\" : null," +
                "\"proxyPort\" : 0,\"proxyAuthUser\" : null," +
                "\"proxyAuthPass\" : null," +
                "\"proxyUserAgent\" : null," +
                "\"authUser\" : null," +
                "\"authPass\" : null," +
                "\"autoBan\" : false," +
                "\"sameHostForSameTarget\" : false," +
                "\"keepVisitTime\" : 180," +
                "\"isHideTips\" : false," +
                "\"nodeFeedAutoUpdate\" : true," +
                "\"serverSubscribes\" : []," +
                "\"token\" : {}," +
                "\"portMap\" : {}}";
            return json;
        }

        public static string GetMiddleString(string SumString, string LeftString, string RightString)
        {
            if (string.IsNullOrEmpty(SumString)) return "";
            if (string.IsNullOrEmpty(LeftString)) return "";
            if (string.IsNullOrEmpty(RightString)) return "";

            int LeftIndex = SumString.IndexOf(LeftString);
            if (LeftIndex == -1) return "";
            LeftIndex = LeftIndex + LeftString.Length;
            int RightIndex = SumString.IndexOf(RightString, LeftIndex);
            if (RightIndex == -1) return "";
            return SumString.Substring(LeftIndex, RightIndex - LeftIndex);
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定要退出本程序吗？\n退出之后您将不能享受漫游之旅！", "提示：", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (materialRaisedButton1.Visible == false)
            {
                Shadowsocks.GlobalVaribles._controller.Stop();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (materialRaisedButton1.Visible == false)
            {
                Shadowsocks.GlobalVaribles._controller.Stop();
                materialListView1.Enabled = true;
                materialCheckBox3.Enabled = true;
                materialRaisedButton1.Visible = true;
                materialRaisedButton2.Visible = false;
            }
            Login lg = new Login();
            lg.ShowDialog();
            if (GlobalVaribles.GlobalVaribles.LOGIN_ifLogged == true)
            {
                InitSettings();
                this.Text = "欢迎回来，" + GlobalVaribles.GlobalVaribles.LOGIN_username + "。";
                this.labelAccount.Text = GlobalVaribles.GlobalVaribles.LOGIN_email;
                延迟加载.Enabled = true;
            }
            else
            {
                try
                {
                    Application.Exit();
                }
                catch { };
            }
        }

        private void materialTabControl1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch { }
        }

        private void tabPage1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch { }
        }

        private void tabPage2_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch { }
        }

        private void Client_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch { }
        }

        private void materialListView1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch { }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://proxy.g41.moe/");
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.illyasviel.me/");
        }
    }
}
