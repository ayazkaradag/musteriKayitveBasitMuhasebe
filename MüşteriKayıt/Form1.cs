using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MüşteriKayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-A21VQ07\SQLEXPRESS;Initial Catalog=MusteriKayit;Integrated Security=True");


        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("select * from müsteriler",baglanti);
            da.Fill(dt);
            dgvMüsteriler.DataSource = dt;
        }

        void Temizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtIs.Text = "";
            txtAdres.Text = "";
            txtId.Text = "";
            mskTarih.Text = "";
            mskTel.Text = "";
            txtAd.Focus();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut=new SqlCommand("insert into müsteriler (AD,SOYAD,TELEFON,ADRES,YAPILAN,TARIH) values (@p1, @p2, @p3, @p4, @p5, @p6)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTel.Text);
            komut.Parameters.AddWithValue("@p4", txtAdres.Text);
            komut.Parameters.AddWithValue("@p5", txtIs.Text);
            komut.Parameters.AddWithValue("@p6", mskTarih.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri kaydı başarılı.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut=new SqlCommand("Delete from müsteriler where ID="+txtId.Text,baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri kaydı başarıyla silindi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele() ;
            Temizle() ;
        }

        private void dgvMüsteriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvMüsteriler.SelectedCells[0].RowIndex;

            txtId.Text = dgvMüsteriler.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dgvMüsteriler.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dgvMüsteriler.Rows[secilen].Cells[2].Value.ToString();
            mskTel.Text = dgvMüsteriler.Rows[secilen].Cells[3].Value.ToString();
            txtAdres.Text = dgvMüsteriler.Rows[secilen].Cells[4].Value.ToString();
            txtIs.Text = dgvMüsteriler.Rows[secilen].Cells[5].Value.ToString();
            mskTarih.Text = dgvMüsteriler.Rows[secilen].Cells[6].Value.ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update müsteriler set AD=@P1,SOYAD=@P2,TELEFON=@P3,ADRES=@P4,YAPILAN=@P5,TARIH=@P6 where ID=@P7", baglanti);
            komut.Parameters.AddWithValue("@P1",txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTel.Text);
            komut.Parameters.AddWithValue("@P4", txtAdres.Text);
            komut.Parameters.AddWithValue("@P5", txtIs.Text);
            komut.Parameters.AddWithValue("@P6", mskTarih.Text);
            komut.Parameters.AddWithValue("@P7", txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi bilgisi güncellendi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void dgvMüsteriler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
