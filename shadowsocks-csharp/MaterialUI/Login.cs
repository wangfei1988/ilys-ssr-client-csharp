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
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaterialUI
{
    public partial class Login : MaterialForm
    {
        public Login()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.materialSingleLineTextFieldPassword.Select();
            string acjson;
            try
            {
                acjson = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "account.json");
            }
            catch
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "account.json", "{}");
                acjson = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "account.json");
            }
            JObject obj = JObject.Parse(acjson);
            if (obj.Property("account") != null)
            {
                materialSingleLineTextFieldAccount.Text = obj["account"].ToString();
            }
            if (obj.Property("password") != null)
            {
                materialSingleLineTextFieldPassword.Text = obj["password"].ToString();
                materialCheckBox1.Checked = true;
            }
            string ifjson;
            try
            {
                ifjson = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "information.json");
            }
            catch
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "information.json", "{}");
                ifjson = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "information.json");
            }
            JObject ifobj = JObject.Parse(ifjson);
            if (ifobj.Property("a") != null)
            {
                if (ifobj["a"].ToString() == "True")
                {
                    materialFlatButtonLogin_Click(sender, e);
                }
            }
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch{}
        }

        private void Login_Closed(object sender, FormClosedEventArgs e)
        {
            if (GlobalVaribles.GlobalVaribles.LOGIN_ifLogged == false)
            {
                Application.Exit();
            }
        }

        private void Login_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch { }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Cursor myCursor = new Cursor("Altr_Gea.cur");
                this.Cursor = myCursor;
            }
            catch { }
        }

        private void materialFlatButtonLogin_Click(object sender, EventArgs e)
        {
            //检测是否记住密码
            if(materialCheckBox1.Checked == false)
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "account.json", "{\"account\":\"" + materialSingleLineTextFieldAccount.Text + "\"}", Encoding.UTF8);
            }
            else
            {
                File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "account.json", "{\"account\":\"" + materialSingleLineTextFieldAccount.Text + "\",\"password\":\"" + materialSingleLineTextFieldPassword.Text + "\"}", Encoding.UTF8);
            }
            //连接mysql数据库进行密码验证
            MySqlConnection msqlConnection = new MySqlConnection("Host = " + GlobalVaribles.GlobalVaribles.DB_Adress + "; Database = " + GlobalVaribles.GlobalVaribles.DB_DBName + "; Username = " + GlobalVaribles.GlobalVaribles.DB_Username + "; Password = " + GlobalVaribles.GlobalVaribles.DB_Password + ";SslMode = none;");
            msqlConnection.Open();
            string sql = "SELECT pass, user_name, email FROM user WHERE email = \"" + materialSingleLineTextFieldAccount.Text + "\"";
            MySqlDataAdapter result = new MySqlDataAdapter(sql, msqlConnection);
            DataSet dataset = new DataSet();
            result.Fill(dataset, "tb");
            msqlConnection.Close();
            if (dataset.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("用户名错误，请检查您的用户名是否正确！", "提示");
            }
            else if (dataset.Tables[0].Rows[0][0].ToString() != GetSHA256HashFromString(materialSingleLineTextFieldPassword.Text))
            {
                MessageBox.Show("密码错误，请检查您的密码是否正确！", "提示");
            }
            else
            {
                GlobalVaribles.GlobalVaribles.LOGIN_ifLogged = true;
                GlobalVaribles.GlobalVaribles.LOGIN_username = dataset.Tables[0].Rows[0][1].ToString();
                GlobalVaribles.GlobalVaribles.LOGIN_email = dataset.Tables[0].Rows[0][2].ToString();
                this.Close();
            }
        }

        private void materialSingleLineTextFieldPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                materialFlatButtonLogin_Click(sender, e);
            }
        }

        public string GetSHA256HashFromString(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://proxy.g41.moe/");
        }
    }
}
