using System.Text;
using System.Text.Json;

namespace TAKSİT_TAKİP
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient _http = new HttpClient();
        private const string ApiUrl = "https://localhost:7026/api/login";

        public Form1()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !radioButton1.Checked;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button1.Enabled = false;
            button1.Text = "Giriş yapılıyor...";

            try
            {
                var body = new { Username = username, Password = password };
                var json = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync(ApiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Giriş başarılı!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ana formu aç
                    new Form2().Show();
                    // this.Hide();
                }
                else
                {
                    string mesaj = "Hata oluştu";
                    if (!string.IsNullOrWhiteSpace(responseBody))
                    {
                        var doc = JsonDocument.Parse(responseBody);
                        mesaj = doc.RootElement.GetProperty("hata").GetString() ?? mesaj;
                    }
                    MessageBox.Show(mesaj, "Giriş Hatası",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Sunucuya bağlanılamadı. API çalışıyor mu?", "Bağlantı Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (JsonException)
            {
                MessageBox.Show("Sunucudan geçersiz yanıt alındı.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
                button1.Text = "Giriş Yap";
            }
        }
    }
}