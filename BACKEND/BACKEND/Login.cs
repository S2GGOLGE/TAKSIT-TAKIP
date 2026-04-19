using Microsoft.Data.SqlClient;

namespace TAKSIT_TAKIP_BACKEND
{
    public class Login
    {
        private const string ConnString =
            "Data Source=Emree;Initial Catalog=Sıunucu;Integrated Security=True;Multiple Active Result Sets=True;Encrypt=False";

        public string Giris(string username, string password)
        {
            string sonuc = BosKontrol(username, password);
            if (sonuc != "OK")
                return sonuc;

            using var baglanti = new SqlConnection(ConnString);
            baglanti.Open();

            string komut = "SELECT Sifre FROM Kullanicilar WHERE Email=@Email";
            using var islem = new SqlCommand(komut, baglanti);
            islem.Parameters.AddWithValue("@Email", username);

            using var reader = islem.ExecuteReader();
            if (!reader.Read())
                return "Kullanıcı bulunamadı";

            string dbPass = reader["Sifre"].ToString() ?? "";

            if (dbPass != password)
                return "Şifre yanlış";

            return "OK";
        }

        private static string BosKontrol(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return "Lütfen kullanıcı adınızı girin";
            if (string.IsNullOrWhiteSpace(password))
                return "Lütfen şifrenizi girin";
            return "OK";
        }
    }
}