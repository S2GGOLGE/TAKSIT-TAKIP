using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace TAKSİT_TAKİP
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private static readonly HttpClient _http = new HttpClient();
        private const string _apiUrl = "http://localhost:7026/api/Kayit";

        private void button1_Click(object sender, EventArgs e)
        {
            new Ana_Sayfa().Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        { 
            string dateText = textBox1.Text.Trim();
            string tutarText = textBox2.Text.Trim();
            string aciklama = richTextBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(dateText) ||
                string.IsNullOrWhiteSpace(tutarText) ||
                string.IsNullOrWhiteSpace(aciklama))
            {
                MessageBox.Show("Lütfen boş alan bırakmayın.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DateTime.TryParse(dateText, out DateTime tarih))
            {
                MessageBox.Show("Tarih formatı geçersiz. (Örn: 23.04.2026)",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string normalizedTutar = tutarText.Replace(".", "").Replace(",", ".");
            if (!double.TryParse(normalizedTutar,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double tutar) || tutar <= 0)
            {
                MessageBox.Show("Geçerli bir tutar girin. (Örn: 1500 veya 1500,75)",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            button2.Enabled = false;
            try
            {
                var payload = new
                {
                   Date = tarih,        // Tarih → Date
                    Tutar = tutar,
                    Açıklama = aciklama  // Aciklama → Açıklama
                };

                var response = await _http.PostAsJsonAsync(_apiUrl, payload);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kayıt başarıyla eklendi.",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    textBox2.Clear();
                    richTextBox1.Clear();
                }
                else
                {
                    string detail = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Sunucu hatası: {(int)response.StatusCode}\n{detail}",
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Bağlantı hatası:\n{ex.Message}",
                    "Ağ Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmedik hata:\n{ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button2.Enabled = true;
            }
        }
    }
}