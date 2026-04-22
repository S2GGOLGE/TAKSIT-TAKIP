using System;
using System.Collections.Generic;
using System.Text;

namespace TAKSİT_TAKİP
{
    internal class Server_Settings
    {
        public static string HOST { get; private set; }
        public static int PORT { get; private set; }
        public static void serverayar(string host, int port)
        {
            HOST = host;
            PORT = port;
        }
    }
}
