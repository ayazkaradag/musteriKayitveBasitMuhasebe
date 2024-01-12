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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-A21VQ07\SQLEXPRESS;Initial Catalog=MusteriKayit;Integrated Security=True");

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from muhasebe", baglanti);
            da.Fill(dt);
            dgvMuhasebe.DataSource = dt;
        }
        void Temizle()
        {
            txtGelir.Text = "";
            txtGider.Text = "";
            txtId.Text = "";
            mskTarih.Text = "";           
            mskTarih.Focus();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("insert into muhasebe (TARIH,GELIR,GIDER) values (@P1,@P2,@P3)", baglanti);
            kmt.Parameters.AddWithValue("@P1", mskTarih.Text);
            kmt.Parameters.AddWithValue("@P2", txtGelir.Text);
            kmt.Parameters.AddWithValue("@P3", txtGider.Text);
            kmt.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarılı.", "Bilgi", MessageBoxButtons.OK);
            Listele();
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete from muhasebe where ID=" + txtId.Text, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void dgvMuhasebe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvMuhasebe.SelectedCells[0].RowIndex;

            txtId.Text = dgvMuhasebe.Rows[secilen].Cells[0].Value.ToString();
            mskTarih.Text = dgvMuhasebe.Rows[secilen].Cells[1].Value.ToString();
            txtGelir.Text = dgvMuhasebe.Rows[secilen].Cells[2].Value.ToString();
            txtGider.Text = dgvMuhasebe.Rows[secilen].Cells[3].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update muhasebe set TARIH=@P1,GELIR=@P2,GIDER=@P3 where ID=@P4", baglanti);
            komut.Parameters.AddWithValue("@P1", mskTarih.Text);
            komut.Parameters.AddWithValue("@P2", txtGelir.Text);
            komut.Parameters.AddWithValue("@P3", txtGider.Text);
            komut.Parameters.AddWithValue("@P4", txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Tablo bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnCik_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
