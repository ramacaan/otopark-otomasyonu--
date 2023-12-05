using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Otopark_OTOMASYONU
{
   
    public partial class Form1 : Form
    {
        MySqlConnection con;  //mysql servere bağlanmamızı sağlayan kütüphane
        MySqlCommand cmd;   //mysqle sorgu gönderme kütüphane
        MySqlDataReader dr;  //gönderilen komutun neyi döndürdüğü
        public int deneme = 0;
        public Form1() //form ilk açıldığında çalışan yer
        {
            con = new MySqlConnection("Server =localhost;Database=otopark_otomasyonu;user=root;Pwd=4866;SslMode=none");
            InitializeComponent();
            //veri tabanı bağlantısı yapıldığı yer..
        }
            
        private void btnGiris_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            //girişteki kullanıcı adı ve şifre..
            cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            //cmd.CommandText = "SELECT * FROM users where userName = '" + txtUserName.Text + "'AND pwd='" + txtPassword.Text + "'";
            cmd.CommandText = "SELECT * FROM users where userName = '" + txtUserName.Text + "'AND pwd = '"+txtPassword.Text+"'";
            dr = cmd.ExecuteReader();
            if (dr.Read())   //bağlantı doğru ise 
            {
                MessageBox.Show("Hosgeldin " + username+".");   //girişten sonraki hoşgeldin ekranı..
                Form2 f2 = new Form2();      //form2ye geçiş..
                f2.Show();
                this.Hide();
                deneme = 10;
                

            }
            else    //bağlantı yanlışsa..
            {
                MessageBox.Show("Kullanici Adi veya Sifre hatali.");
            }
            con.Close();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
