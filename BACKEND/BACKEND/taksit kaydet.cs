using Microsoft.Data.SqlClient;
using System.Data;

namespace BACKEND
{
    public class taksit_kaydet
    {
        public static string? ID { get; set; }
        public static string? Date { get; set; }
        public static double Tutar { get; set; }
        public static string? Açıklama { get; set; }

        private const string ConnString =
          "Data Source=Emree;Initial Catalog=Sıunucu;Integrated Security=True;Multiple Active Result Sets=True;Encrypt=False";

        public string Kayıt()
        {
            // Validasyonlar
            if (Tutar <= 0)
                return "Lütfen geçerli bir tutar giriniz";

            if (string.IsNullOrWhiteSpace(Date))
                return "Lütfen tarih giriniz";

            try
            {
                using var bağlantı = new SqlConnection(ConnString);
                bağlantı.Open();

                // Mükerrer kayıt kontrolü
                string kontrol = "SELECT COUNT(*) FROM Taksitler WHERE Tarih=@Date";
                using var cmdKontrol = new SqlCommand(kontrol, bağlantı);
                cmdKontrol.Parameters.Add("@Date", SqlDbType.Date).Value = Date;

                int sayı = Convert.ToInt32(cmdKontrol.ExecuteScalar());
                if (sayı > 0)
                    return "Bu tarihte zaten taksit mevcut";

                // Kayıt ekleme
                string query = @"INSERT INTO Taksitler (Tarih, Tutar, Açıklama) 
                                 VALUES (@Date, @Tutar, @Açıklama)";

                using var cmd = new SqlCommand(query, bağlantı);
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = Date;
                cmd.Parameters.Add("@Tutar", SqlDbType.Decimal).Value = Tutar;
                cmd.Parameters.Add("@Açıklama", SqlDbType.NVarChar).Value = Açıklama ?? string.Empty;

                return cmd.ExecuteNonQuery() > 0 ? "OK" : "Kayıt başarısız";
            }
            catch (Exception ex)
            {
                Console.WriteLine("KRİTİK SQL HATASI: " + ex.Message);
                return "Hata: " + ex.Message;
            }
        }
    }
}