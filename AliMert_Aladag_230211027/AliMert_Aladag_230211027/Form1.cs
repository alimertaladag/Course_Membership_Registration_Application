using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliMert_Aladag_230211027
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        MySqlConnection baglanti = new MySqlConnection("server=localhost;database=kurs;uid=root;pwd='';");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string kontrolQuery = "SELECT COUNT(*) FROM admin WHERE kullanici_adi = @kullaniciAdi";
            MySqlCommand kontrolCmd = new MySqlCommand(kontrolQuery, baglanti);
            kontrolCmd.Parameters.AddWithValue("@kullaniciAdi", textBox3.Text);
            int count = Convert.ToInt32(kontrolCmd.ExecuteScalar());

            if (count > 0)
            {
                MessageBox.Show("Kullanıcı zaten kayıtlı!");
                baglanti.Close();
                return;
            }

            string ekleQuery = "INSERT INTO admin (ad, soyad, kullanici_adi, sifre) VALUES (@ad, @soyad, @kullaniciAdi, @sifre)";
            MySqlCommand ekleCmd = new MySqlCommand(ekleQuery, baglanti);
            ekleCmd.Parameters.AddWithValue("@ad", textBox1.Text);
            ekleCmd.Parameters.AddWithValue("@soyad", textBox2.Text);
            ekleCmd.Parameters.AddWithValue("@kullaniciAdi", textBox3.Text);
            ekleCmd.Parameters.AddWithValue("@sifre", textBox4.Text);
            ekleCmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Yeni kullanıcı kaydedildi!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "SELECT ad, soyad FROM admin WHERE kullanici_adi=@kullaniciAdi AND sifre=@sifre";
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.Parameters.AddWithValue("@kullaniciAdi", textBox3.Text);
            cmd.Parameters.AddWithValue("@sifre", textBox4.Text);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string ad = reader["ad"].ToString();
                string soyad = reader["soyad"].ToString();
                reader.Close();
                baglanti.Close();

                Form2 form2 = new Form2(ad, soyad);
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Yok!");
            }
            baglanti.Close();
        }
        
    }
}
