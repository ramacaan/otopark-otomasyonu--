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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace Otopark_OTOMASYONU
{

    public partial class Form2 : Form
    {
        Form1 f1 = new Form1();  //form1 form2 bağlantı
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader dr;
        Button[,] buttons = new Button[5, 5];//2boyutlu dizi 20 buton dizisi
        List<String> icerdekiArabalar = new List<String>();
        public Form2()  //form2 açılıs
        {


            InitializeComponent();
            con = new MySqlConnection("Server =localhost;Database=otopark_otomasyonu;user=root;Pwd=4866;SslMode=none");

            x.Enabled = false;
            x.Visible = false;

            con.Close();
            buttons[0, 0] = A1; buttons[0, 1] = A2; buttons[0, 2] = A3; buttons[0, 3] = A4; buttons[0, 4] = A5;
            buttons[1, 0] = B1; buttons[1, 1] = B2; buttons[1, 2] = B3; buttons[1, 3] = B4; buttons[1, 4] = B5;
            buttons[2, 0] = C1; buttons[2, 1] = C2; buttons[2, 2] = C3; buttons[2, 3] = C4; buttons[2, 4] = C5;
            buttons[3, 0] = D1; buttons[3, 1] = D2; buttons[3, 2] = D3; buttons[3, 3] = D4; buttons[3, 4] = D5;


        }
        void txtMusteri_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void A1_Click(object sender, EventArgs e)
        {
            Button buttonn = new Button();

        }
        private void B1_Click(object sender, EventArgs e)
        {
            Button button = new Button();
        }

        

        private void btnAracGiris_Click(object sender, EventArgs e)
        {
            if (txtPlaka.Text !="" && txtMusteri.Text !="")
            {
                if (comboBox1.SelectedIndex>0) //seçim varmı yokmu
                {
                    DateTime girisSaat = DateTime.Now;
                    string plaka, nerede;
                    string[] adSoyad;   

                    bool dogrumu = false;
                    try
                    {
                        plaka = txtPlaka.Text;
                        if (!System.Text.RegularExpressions.Regex.IsMatch(txtMusteri.Text, "^[a-zA-Z ]"))
                        {
                            MessageBox.Show("Sadece Karakter Giriniz");
                            txtMusteri.Text.Remove(txtMusteri.Text.Length - 1);
                        }
                        else
                        {
                            //asd
                            int control = 0;
                            foreach (var item in icerdekiArabalar)
                            {
                                if (item == txtPlaka.Text)
                                {
                                    control = 1;
                                }
                            }
                            if (control == 0)
                            {
                                dogrumu = true;
                            }
                            else
                            {
                                MessageBox.Show("Araba zaten icerde.");
                            }

                        }
                        adSoyad = txtMusteri.Text.ToString().Split(' ');
                        nerede = comboBox1.SelectedItem.ToString();
                        

                    }
                    catch (Exception) //hata yakalama
                    {

                        MessageBox.Show("Gerekli Alanlari Dogru verilerle doldurunuz.");
                    }
                    if (dogrumu)
                    {
                        
                        //Butun veriler kontrol edildi.
                        label4.Text = girisSaat.ToString();
                        try
                        {
                            int aracSayisi = 0;
                            cmd = new MySqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "SELECT count(MNo) FROM arabalar";
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                aracSayisi = Convert.ToInt32(dr.GetString(0));
                            }
                            con.Close();
                            cmd = new MySqlCommand();
                            con.Open();
                            int totalAracSayisi = aracSayisi + 1;
                            cmd.Connection = con;   //insert into=veri tabanına veri ekleme 
                            cmd.CommandText = "INSERT INTO arabalar(MNo,Plaka,OdenenMiktar,BulunduguYer,GirisTarihi) VALUES('"+ totalAracSayisi + "','"+txtPlaka.Text+"','"+20+"','"+ comboBox1.SelectedItem.ToString()+"','"+DateTime.Now.ToString()+"')";
                            object sonuc = cmd.ExecuteNonQuery(); 
                            if (sonuc !=null)
                            {
                                foreach (var button in buttons)  //foreach=hem döngü hem de yazı tipi renk vs değiştirme..
                                {  //tüm butonları dolaşır

                                    try
                                    {
                                        if (comboBox1.SelectedItem.ToString() == button.Text)
                                        {
                                            button.Text = txtPlaka.Text;
                                            button.BackColor = Color.Red;
                                            
                                            comboAraclar.Items.Add(comboBox1.SelectedItem.ToString() + "-" + txtPlaka.Text);
                                            break;
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        
                                    }
                                }
                                MessageBox.Show("Arac Girisi Yapildi.","Giris Yapildi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                icerdekiArabalar.Add(txtPlaka.Text);

                            }
                            else
                            {
                                MessageBox.Show("Sisteme eklenemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            con.Close();

}
                        catch (Exception HataYakala) //hataya bakar hata yoksa bu kod es geçilir..
                        {

                            MessageBox.Show("Hata: " + HataYakala.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                    //MessageBox.Show("Arac Girisi Basarili.");
                }
                else
                {
                    MessageBox.Show("Lutfen Aracin Park Edildigi Yeri seciniz.");
                }
                
            }
            else
            {
                MessageBox.Show("Gerekli Alanlari Bos Birakmayiniz.");
            }
        }

        private void comboAraclar_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Tarih;
            cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;

        }


        private void btnAracCikisi_Click(object sender, EventArgs e)
        {
            string[] plakaYer = comboAraclar.SelectedItem.ToString().Split('-');
            string bulunduguYer = plakaYer[0];
            string plaka = plakaYer[1];
            string cikisTarihi = DateTime.Now.ToString();

            cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO cikis(Plaka,BulunduguYer,CikisTarihi) VALUES('" + plaka + "','"+bulunduguYer+"','"+cikisTarihi+"')";
            object sonuc = cmd.ExecuteNonQuery();
            if (sonuc!=null)
            {
                foreach (var button in buttons)
                {
                    if (button.Text == plaka)
                    {
                        button.Text = bulunduguYer;
                        button.BackColor = buttons[0,2].BackColor;
                        comboAraclar.Items.Remove(comboAraclar.SelectedItem);
                        comboAraclar.Text = "";
                        MessageBox.Show("Arac Cikisi Yapildi.");
                        icerdekiArabalar.Remove(txtPlaka.Text);
                        break;
                    }
                }
            }
        
            con.Close();

           
        }

        private void btnAlanEkle_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand komut = new MySqlCommand("insert into arabalar(BulunduguYer) values(@p1)");
            komut.Parameters.AddWithValue("@p1", txtMusteri.Text);
            komut.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Alan eklendi. ");
        }

        private void btnAlanSil_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand komut = new MySqlCommand("insert into arabalar(BulunduguYer) values(@p1)");
            komut.Parameters.AddWithValue("@p1", txtMusteri.Text);
            komut.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Alan silindi. ");
        }

        private void btnAlanEkle_Click_1(object sender, EventArgs e)
        {
            Button newBtn = new Button();
            int aralik = Convert.ToInt32(D1.Location.Y) - Convert.ToInt32(C1.Location.Y);
            newBtn = x;
            newBtn.Name = "E1";
            newBtn.Location = new System.Drawing.Point(D1.Location.X, D1.Location.Y + aralik);
            newBtn.Text = "E1";
            newBtn.Visible = true;
            newBtn.Enabled = true;
            comboBox1.Items.Add(newBtn.Text);
            
            buttons[4,0] = newBtn;

        }

        private void btnAlanSil_Click_1(object sender, EventArgs e)
        {
            buttons[4, 0].Visible = false;
        }
    }
}
