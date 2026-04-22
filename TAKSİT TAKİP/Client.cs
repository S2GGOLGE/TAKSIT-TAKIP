using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TAKSİT_TAKİP
{
    internal class Client
    {
        public static TcpClient client;

        public static void Start()
        {
            while (true)
            {
                try
                {
                    client = new TcpClient();

                    Console.WriteLine("Sunucuya bağlanılıyor...");

                    client.Connect(Server_Settings.HOST, Server_Settings.PORT);

                    if (client.Connected)
                    {
                        Console.WriteLine("Bağlantı başarılı!");
                        MessageBox.Show("Sunucuya bağlanıldı", "Sistem",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break; // bağlantı olunca çık
                    }
                }
                catch
                {
                    Console.WriteLine("Bağlantı başarısız, tekrar deneniyor...");
                    Thread.Sleep(2000); // 2 saniye bekle ve tekrar dene
                }
            }
        }
    }
}