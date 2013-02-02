using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDP_serwer
{
    /// <summary>
    /// Delegat umożliwiający wystawianie komunikatów serwera streamingu.
    /// </summary>
    /// <param name="msg">Komunikat</param>
    public delegate void MessageHandler(string msg);

    /// <summary>
    /// Umożliwia odbiór danych od klienta i rozsyłanie ich do pozostałych klientów.
    /// </summary>
    class UdpServer
    {
        #region Pola składowe

        /// <summary>
        /// Obiekt umożliwiający komunikację w sieci Internet za pomocą protokołu UDP.
        /// </summary>
        private UdpClient server;
        /// <summary>
        /// Wątek odbierający i rozsyłający dane.
        /// </summary>
        private Thread listenThread;
        /// <summary>
        /// Bufor odbieranych danych.
        /// </summary>
        private static Byte[] dataBuffer;
        /// <summary>
        /// Nazwa lokalnego hosta.
        /// </summary>
        private static string strHostName;
        /// <summary>
        /// Lokalny port UDP.
        /// </summary>
        private static int localUdpPort = 10100;
        /// <summary>
        /// Zdalny port UDP.
        /// </summary>
        private static int remoteUdpPort = 10101;
        /// <summary>
        /// Klasa udostępniająca podstawowe informacje o lokalnym hoście, wykorzystywane do komunikacji z innymi komputerami w sieci.
        /// </summary>
        private IPHostEntry ipHostEntry;
        /// <summary>
        /// Zdalny adres IP i port UDP.
        /// </summary>
        private static IPEndPoint remoteEndPoint;
        /// <summary>
        /// Lokalny adres IP i port UDP.
        /// </summary>
        private static IPEndPoint localEndPoint;
        /// <summary>
        /// Lista klientów do których jest rozsyłany stream.
        /// </summary>
        private List<IPEndPoint> clients;
        /// <summary>
        /// Zwraca true jeśli serwer jest uruchomiony.
        /// </summary>
        public bool isRunning = false;
        /// <summary>
        /// Występuje gdy serwer wystawia komunikat.
        /// </summary>
        public event MessageHandler Message;

        #endregion // Pola składowe

        /// <summary>
        /// Tworzy nową instancję klasy z podanymi portami udp.
        /// </summary>
        /// <param name="local_udp_port">Lokalny port UDP.</param>
        /// <param name="remote_udp_port">Zdalny port UDP.</param>
        public UdpServer(int local_udp_port, int remote_udp_port)
        {
            this.clients = new List<IPEndPoint>();

            localUdpPort = local_udp_port;
            remoteUdpPort = remote_udp_port;

            // Ustalanie lokalnego ip.
            strHostName = Dns.GetHostName();
            this.ipHostEntry = Dns.GetHostByName(strHostName);
        }

        /// <summary>
        /// Tworzy nową instancję klasy z domyślnymi portami udp. Port lokalny: 10100, zdalny: 10101.
        /// </summary>
        public UdpServer()
        {
            this.clients = new List<IPEndPoint>();

            // Ustalanie lokalnego ip.
            strHostName = Dns.GetHostName();
            this.ipHostEntry = Dns.GetHostByName(strHostName);
        }

        ~UdpServer()
        {
            this.clients = null;
            this.ipHostEntry = null;
            dataBuffer = null;
            strHostName = null;

            if (this.listenThread != null)
                this.listenThread.Abort();
            if (this.server != null)
                this.server.Close();

            this.listenThread = null;
            this.server = null;
        }

        /// <summary>
        /// Czeka na dane od klienta, po czym rozsyła je do pozostałych klientów.
        /// </summary>
        private void ListenForData()
        {
            this.Message("Pomyślnie uruchomiono serwer streamingu.");

            while (true)
            {
                try
                {
                    dataBuffer = this.server.Receive(ref remoteEndPoint);
                    
                    // * komunikaty o odbieranych danych
                    if(dataBuffer.Length > 200)
                        this.Message("Odebrano " + dataBuffer.Length.ToString() + " bajtów video.");
                    else if(dataBuffer.Length == 200)
                        this.Message("Odebrano " + dataBuffer.Length.ToString() + " bajtów audio.");
                    else
                        this.Message("Odebrano " + dataBuffer.Length.ToString() + " bajtów.");
                     //*/
                }
                catch { break; }
                
                
                for (int i = 0; i < this.clients.Count; ++i)
                {
                    try
                    {
                        UdpClient u = new UdpClient();
                        u.Send(dataBuffer, dataBuffer.Length, this.clients[i]);
                    }
                    catch { this.Message("Nie można wysłać danych do " + this.clients[i].ToString()); }
                }
                
            }
        }

        /// <summary>
        /// Tworzy nowy wątek obsługujący odbieranie i rozsyłanie danych.
        /// </summary>
        /// <param name="ip">Adres ip klienta nadającego stream.</param>
        public void Start(string ip)
        {
            if (!this.isRunning)
            {
                this.Message("Uruchamianie serwera streamingu...");
                remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), remoteUdpPort);

                try
                {
                    // Ustawienie adresu pierwszej aktywnej karty sieciowej jako adresu lokalnego.
                    localEndPoint = new IPEndPoint(this.ipHostEntry.AddressList[0], localUdpPort);
                }
                catch { this.Message("Błąd ustawiania adresu ip."); return; }
                
                try
                {
                    this.server = new UdpClient(localEndPoint);
                }
                catch { this.Message("Port UDP jest zajęty przez inny program."); return; }

                this.Message("Adres serwera streamu: " + this.server.Client.LocalEndPoint.ToString());

                this.listenThread = new Thread(new ThreadStart(ListenForData));
                this.listenThread.Start();
                this.isRunning = true;
            }
        }

        /// <summary>
        /// Przerywa wątek odbierania/rozsyłania danych i zamyka serwer streamingu.
        /// </summary>
        public void Stop()
        {
            if (this.isRunning)
            {
                this.listenThread.Abort();
                this.server.Close();
                this.isRunning = false;
                this.Message("Serwer streamingu został zatrzymany.");
            }
        }

        /// <summary>
        /// Dodaje podany adres ip do listy adresów, na które serwer rozsyła dane.
        /// </summary>
        /// <param name="ip">Adres IP klienta, do którego będą wysyłane dane.</param>
        public void AddClientIP(string ip)
        {
            try
            {
                this.clients.Add(new IPEndPoint(IPAddress.Parse(ip), remoteUdpPort));
            }
            catch { this.Message("Błędny adres klienta odbiorcy!"); return; }
            this.Message("Dodano nowego klienta odbierającego stream.");
        }

        /// <summary>
        /// Usuwa podany adres ip z listy adresów, na które serwer rozsyła dane.
        /// </summary>
        /// <param name="ip">Adres IP klienta, do którego nie będą już wysyłane dane.</param>
        public void RemoveClientIP(string ip)
        {
            IPEndPoint ipe;
            try
            {
                ipe = new IPEndPoint(IPAddress.Parse(ip), remoteUdpPort);
            }
            catch { this.Message("Błędny adres klienta!"); return; }
            try
            {
                this.clients.Remove(ipe);
            }
            catch { this.Message("Brak klienta o podanym ip!"); return; }
            this.Message("Usunięto klienta z listy odbiorców.");
        }

        /// <summary>
        /// Ustawia lokalny adres IP i port UDP. Jest to adres i port serwera streamingu.
        /// </summary>
        /// <param name="ip">Lokalny adres IP.</param>
        /// <param name="port">Lokalny port UDP.</param>
        public void SetLocalEndPoint(string ip, int port)
        {
            try
            {
                localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            }
            catch { this.Message("Błędny adres ip lub zajęty port udp do odbioru streamu!"); }
            this.Message("Pomyślnie ustawiono adres ip i port udp do odbioru streamu.");
        }

        /// <summary>
        /// Ustawia zdalny adres IP i port UDP. Jest to adres i port klienta nadającego stream.
        /// </summary>
        /// <param name="ip">Zdalny adres IP.</param>
        /// <param name="port">Zdalny port UDP.</param>
        public void SetRemoteEndPoint(string ip, int port)
        {
            try
            {
                remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            }
            catch { this.Message("Podano błędny adres ip lub zajęty port udp do wysyłania streamu!"); }
            this.Message("Pomyślnie ustawiono adres ip i port udp do wysyłania streamu.");
        }
    }
}
