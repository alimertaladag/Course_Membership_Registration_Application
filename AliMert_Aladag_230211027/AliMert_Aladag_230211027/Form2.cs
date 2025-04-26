using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliMert_Aladag_230211027
{
    public partial class Form2: Form
    {

        MySqlConnection baglanti = new MySqlConnection("server=localhost;database=kurs;uid=root;pwd='';");

        private string ad;
        private string soyad;

        public Form2(string ad, string soyad)
        {
            InitializeComponent();
            label1.Text = $"Hoşgeldiniz, {ad} {soyad}";
            OgrenciListele();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void OgrenciListele()
        {
            baglanti.Open();
            string query = "SELECT * FROM ogrenci";
            MySqlDataAdapter da = new MySqlDataAdapter(query, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "INSERT INTO ogrenci (ad, soyad, tckimlik, adres, cinsiyet, kurstipi, kurszamani) VALUES (@ad, @soyad, @tckimlik, @adres, @cinsiyet, @kurstipi, @kurszamani)";
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox2.Text);
            cmd.Parameters.AddWithValue("@tckimlik", textBox3.Text);
            cmd.Parameters.AddWithValue("@adres", textBox4.Text);
            cmd.Parameters.AddWithValue("@cinsiyet", radioButton1.Checked ? "ERKEK" : "KADIN");
            cmd.Parameters.AddWithValue("@kurstipi", comboBox1.Text);
            cmd.Parameters.AddWithValue("@kurszamani", radioButton3.Checked ? "SABAH" : "AKSAM");
            cmd.ExecuteNonQuery();
            baglanti.Close();
            OgrenciListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "UPDATE ogrenci SET ad=@ad, soyad=@soyad, tckimlik=@tckimlik, adres=@adres, cinsiyet=@cinsiyet, kurstipi=@kurstipi, kurszamani=@kurszamani WHERE id=@id";
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id"].Value);
            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox2.Text);
            cmd.Parameters.AddWithValue("@tckimlik", textBox3.Text);
            cmd.Parameters.AddWithValue("@adres", textBox4.Text);
            cmd.Parameters.AddWithValue("@cinsiyet", radioButton1.Checked ? "ERKEK" : "KADIN");
            cmd.Parameters.AddWithValue("@kurstipi", comboBox1.Text);
            cmd.Parameters.AddWithValue("@kurszamani", radioButton3.Checked ? "SABAH" : "AKSAM");
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir satır seçin!");
                return;
            }
            cmd.ExecuteNonQuery();
            baglanti.Close();
            OgrenciListele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "DELETE FROM ogrenci WHERE id=@id";
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id"].Value);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            OgrenciListele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = $"UPDATE ogrenci SET {dataGridView1.SelectedCells[0].OwningColumn.Name} = '' WHERE id=@id";
            MySqlCommand cmd = new MySqlCommand(query, baglanti);
            cmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells["id"].Value);         
            cmd.ExecuteNonQuery();
            baglanti.Close();
            OgrenciListele();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            string query = "SELECT * FROM ogrenci WHERE ad LIKE @search OR soyad LIKE @search OR tckimlik LIKE @search";
            MySqlDataAdapter da = new MySqlDataAdapter(query, baglanti);
            da.SelectCommand.Parameters.AddWithValue("@search", "%" + textBox5.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
    }
}
