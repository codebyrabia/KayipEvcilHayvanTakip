using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KayıpEvcilHayvanTakip
{
    public partial class Form1 : Form
    {

        List<EvcilHayvan> hayvanListesi = new List<EvcilHayvan>();
        public Form1()
        {
            

            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSahip.Text))
            {
                MessageBox.Show("Lütfen en az 'Ad' ve 'Sahip' alanlarını doldurun.");
                return;
            }

            EvcilHayvan hayvan = new EvcilHayvan()
            {
                Ad = txtAd.Text,
                Tur = txtTur.Text,
                Cins = txtCins.Text,
                Renk = txtRenk.Text,
                Sahip = txtSahip.Text,
                Telefon = txtTelefon.Text,
                KayipMi = chkKayip.Checked
            };

            hayvanListesi.Add(hayvan);

            MessageBox.Show("Hayvan başarıyla eklendi!");
            Temizle();

        }
            private void Temizle()
            {
                txtAd.Clear();
                txtTur.Clear();
                txtCins.Clear();
                txtRenk.Clear();
                txtSahip.Clear();
                txtTelefon.Clear();
                chkKayip.Checked = false;
            }

        private void btnListele_Click(object sender, EventArgs e)
        {
                lstHayvanlar.Items.Clear();

                foreach (var hayvan in hayvanListesi)
                {
                        string[] satir = {
                            hayvan.Ad,
                            hayvan.Tur,
                            hayvan.Cins,
                            hayvan.Renk,
                            hayvan.Sahip,
                            hayvan.Telefon,
                            hayvan.KayipMi ? "Evet" : "Hayır"
                        };

                    ListViewItem item = new ListViewItem(satir);
                    lstHayvanlar.Items.Add(item);
                }
           

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
           
                string aramaKelimesi = txtAra.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(aramaKelimesi))
                {
                    MessageBox.Show("Lütfen arama için bir kelime girin.");
                    return;
                }

                var bulunanlar = hayvanListesi.Where(h =>
                    h.Ad.ToLower().Contains(aramaKelimesi) ||
                    h.Sahip.ToLower().Contains(aramaKelimesi)).ToList();

                lstHayvanlar.Items.Clear();

                if (bulunanlar.Count == 0)
                {
                    MessageBox.Show("Arama kriterlerinize uygun kayıt bulunamadı.");
                    return;
                }

                foreach (var hayvan in bulunanlar)
                {
                            string[] satir = {
                                hayvan.Ad,
                                hayvan.Tur,
                                hayvan.Cins,
                                hayvan.Renk,
                                hayvan.Sahip,
                                hayvan.Telefon,
                                hayvan.KayipMi ? "Evet" : "Hayır"
                            };

                    ListViewItem item = new ListViewItem(satir);
                    lstHayvanlar.Items.Add(item);
                }
            

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
                if (lstHayvanlar.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Lütfen silmek istediğiniz hayvanı listeden seçin.");
                    return;
                }

                // İlk seçili item
                var selectedItem = lstHayvanlar.SelectedItems[0];
                string ad = selectedItem.SubItems[0].Text;
                string sahip = selectedItem.SubItems[4].Text;

                // Hayvanı listeden bul ve çıkar
                var hayvanToRemove = hayvanListesi.FirstOrDefault(h => h.Ad == ad && h.Sahip == sahip);

                if (hayvanToRemove != null)
                {
                    hayvanListesi.Remove(hayvanToRemove);
                    MessageBox.Show("Hayvan başarıyla silindi.");
                    btnListele_Click(null, null); // Listeyi güncelle
                }
            

        }

        private void lstHayvanlar_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (lstHayvanlar.SelectedItems.Count == 0)
                    return;

                var item = lstHayvanlar.SelectedItems[0];

                txtAd.Text = item.SubItems[0].Text;
                txtTur.Text = item.SubItems[1].Text;
                txtCins.Text = item.SubItems[2].Text;
                txtRenk.Text = item.SubItems[3].Text;
                txtSahip.Text = item.SubItems[4].Text;
                txtTelefon.Text = item.SubItems[5].Text;
                chkKayip.Checked = item.SubItems[6].Text == "Evet";
            

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            
                if (lstHayvanlar.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Güncellemek için önce listeden bir hayvan seçin.");
                    return;
                }

                var seciliItem = lstHayvanlar.SelectedItems[0];
                string eskiAd = seciliItem.SubItems[0].Text;
                string eskiSahip = seciliItem.SubItems[4].Text;

                // Listeden eski kaydı bul
                var guncellenecekHayvan = hayvanListesi.FirstOrDefault(h => h.Ad == eskiAd && h.Sahip == eskiSahip);

                if (guncellenecekHayvan != null)
                {
                    guncellenecekHayvan.Ad = txtAd.Text;
                    guncellenecekHayvan.Tur = txtTur.Text;
                    guncellenecekHayvan.Cins = txtCins.Text;
                    guncellenecekHayvan.Renk = txtRenk.Text;
                    guncellenecekHayvan.Sahip = txtSahip.Text;
                    guncellenecekHayvan.Telefon = txtTelefon.Text;
                    guncellenecekHayvan.KayipMi = chkKayip.Checked;

                    MessageBox.Show("Kayıt güncellendi!");
                    btnListele_Click(null, null); // Listeyi güncelle
                    Temizle(); // Formu temizle
                }
            

        }
    }
}
