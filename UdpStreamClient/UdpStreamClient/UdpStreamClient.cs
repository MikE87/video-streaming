using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using DirectX.Capture;
using LumiSoft.Net.Media;
using LumiSoft.Net.Media.Codec.Audio;

namespace UdpStreamClient
{
    class UdpStreamClient
    {
        #region Pola składowe

        private Capture videoCapture;
        private Filters filters;
        private UdpClient udpClient;
        public IPEndPoint remoteEndPoint;
        public IPEndPoint localEndPoint;
        private IPHostEntry ipHostEntry;
        private Thread listenThread;
        private Thread imageSendThread;
        private Thread audioSendThread;
        private static AudioIn microphone;
        private static AudioOut speaker;
        private static AudioCodec audioCodec;
        private static string strHostName;
        private static PictureBox videoWindow;
        private static Image img;
        private static Byte[] videoBuffer;
        private static Byte[] audioBuffer;
        private static ImageCodecInfo jpegEncoder;
        private static EncoderParameters encoderParams;
        private static bool streaming;
        private static bool recieving;
        private static bool inColor;
        private static int timeInterval;
        private static int localUdpPort = 10101;
        private static int remoteUdpPort = 10100;
        private static int framesCount;

        #endregion // Pola składowe

        /// <summary>
        /// Tworzy nową instancję klienta streamingu przez udp.
        /// </summary>
        /// <param name="video_window">Picturebox, w którym będzie wyświetlany obraz z kamery/streamu.</param>
        /// <param name="remote_ip">Zdalne IP serwera lub innego klienta.</param>
        public UdpStreamClient(PictureBox video_window, string remote_ip)
        {
            videoCapture = null;
            filters = new Filters();
            microphone = null;
            speaker = null;
            streaming = false;
            recieving = false;
            inColor = true;
            timeInterval = 1000;
            framesCount = 0;
            videoWindow = video_window;
            audioCodec = new PCMU();
            
            #region Wstępne ustawienia kompresji
            //#################################################
            encoderParams = new EncoderParameters(2);

            // Ustawiamy format kompresji na JPEG
            jpegEncoder = GetEncoder(ImageFormat.Jpeg);

            // Ustawiamy jakość obrazu
            this.SetImageQuality(20L);

            // Ustawiamy głębię kolorów
            this.SetImageColorDepth(8L);
            //#################################################
            #endregion // Wstępne ustawienia kompresji


            // Ustalanie lokalnego ip.
            strHostName = Dns.GetHostName();
            this.ipHostEntry = Dns.GetHostByName(strHostName);
            this.localEndPoint = new IPEndPoint(ipHostEntry.AddressList[0], localUdpPort);

            // Ustawianie zdalnego ip.
            try
            {
                this.remoteEndPoint = new IPEndPoint(IPAddress.Parse(remote_ip), remoteUdpPort);
            }
            catch(FormatException ex) { MessageBox.Show(ex.Message, "Streaming"); }
        }

        ~UdpStreamClient()
        {
            if(this.imageSendThread != null)
                this.imageSendThread.Abort();

            if(this.audioSendThread != null)
                this.audioSendThread.Abort();

            if(this.listenThread != null)
                this.listenThread.Abort();

            if (this.udpClient != null)
            {
                this.udpClient.Close();
                this.udpClient = null;
            }

            if (this.videoCapture != null)
            {
                this.videoCapture.Dispose();
                this.videoCapture = null;
            }

            if(microphone != null)
            {
                microphone.Dispose();
                microphone = null;
            }

            if (speaker != null)
            {
                speaker.Dispose();
                speaker = null;
            }

            this.filters = null;
            this.remoteEndPoint = null;
            this.localEndPoint = null;
            jpegEncoder = null;
            encoderParams = null;
            strHostName = null;
            videoWindow = null;
            img = null;
            videoBuffer = null;
            audioBuffer = null;
            audioCodec = null;
        }

        #region Metody prywatne
        //#####################################################################################################

        /// <summary>
        /// Obsługa wątku nasłuchiwania na dane przychodzące.
        /// </summary>
        private void ListenForData()
        {
            Byte[] decodedAudio, recievedData;
            while (true)
            {
                try
                {
                    recievedData = this.udpClient.Receive(ref this.remoteEndPoint);

                    if (recievedData.Length == 200)
                    {
                        decodedAudio = audioCodec.Decode(recievedData, 0, recievedData.Length);
                        speaker.Write(decodedAudio, 0, decodedAudio.Length);
                    }
                    else
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ms.Write(recievedData, 0, recievedData.Length);

                            videoWindow.Image = Image.FromStream(ms);
                            framesCount++;
                        }
                    }
                }
                catch 
                { 
                    decodedAudio = null; 
                    recievedData = null; 
                    break; 
                }
            }
        }

        /// <summary>
        /// Obsługa wątku wysyłania obrazu.
        /// </summary>
        private void SendImage()
        {
            if (inColor)
            {
                while (streaming)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, jpegEncoder, encoderParams);
                            videoBuffer = ms.ToArray();
                        }
                        this.udpClient.Send(videoBuffer, videoBuffer.Length, this.remoteEndPoint);
                        framesCount++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Błąd wysyłania danych video",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                    }
                    // Kontrola liczby klatek na sekundę.
                    Thread.Sleep(timeInterval);
                }
            }
            else
            {
                Image image;
                while (streaming)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image = this.MakeGrayscale(img);
                            image.Save(ms, jpegEncoder, encoderParams);
                            videoBuffer = ms.ToArray();
                        }
                        this.udpClient.Send(videoBuffer, videoBuffer.Length, this.remoteEndPoint);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Błąd wysyłania danych video",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                    }

                    Thread.Sleep(timeInterval);
                }
            }

            this.udpClient.Close();
        }

        /// <summary>
        /// Obsługa wątku wysyłania dźwięku.
        /// </summary>
        private void SendSound()
        {
            Byte[] deviceBuffer = new Byte[400];

            while (streaming)
            {
                try
                {
                    if (microphone.Available < deviceBuffer.Length)
                    {
                        Thread.Sleep(1);
                    }
                    else
                    {
                        microphone.Read(deviceBuffer, 0, deviceBuffer.Length);
                        audioBuffer = audioCodec.Encode(deviceBuffer, 0, deviceBuffer.Length);
                        this.udpClient.Send(audioBuffer, audioBuffer.Length, this.remoteEndPoint);
                    }
                }
                catch { break; }
            }
            deviceBuffer = null;
        }

        /// <summary>
        /// Zwraca kodek dla podanego formatu.
        /// </summary>
        /// <param name="format">Format kompresji jaka ma być wyszukana.</param>
        /// <returns>Jeśli nie znajdzie kodeka do podanego formatu to zwróci null.</returns>
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        /// <summary>
        /// Konwertuje podany kolorowy obraz na odcienie szarości.
        /// </summary>
        /// <param name="original">Kolorowy obraz do konwersji.</param>
        /// <returns>Zwraca podany obraz w odcieniach szarości.</returns>
        private Image MakeGrayscale(Image original)
        {
            Image newImage = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newImage);

            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
                          {
                             new float[] {.3f, .3f, .3f, 0, 0},
                             new float[] {.59f, .59f, .59f, 0, 0},
                             new float[] {.11f, .11f, .11f, 0, 0},
                             new float[] {0, 0, 0, 1, 0},
                             new float[] {0, 0, 0, 0, 1}
                          });

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            g.Dispose();
            return newImage;
        }

        /// <summary>
        /// Obsługa zdarzenia występującego podczas przechwycenia klatki.
        /// </summary>
        /// <param name="e">Przechwycona klatka.</param>
        private void CapturedFrame(System.Drawing.Bitmap e)
        {
            img = e;
        }

        /// <summary>
        /// Uruchamia przechwytywanie obrazu za pomocą pierwszego dostępnego urządzenia.
        /// </summary>
        private void StartVideoCapture()
        {
            if (this.filters.VideoInputDevices.Count == 0)
                throw new Exception("Brak urządzeń do przechwytywania obrazu!");

            videoCapture = new Capture(filters.VideoInputDevices[0], null);
            videoCapture.FrameSize = new Size(320, 240);
            videoCapture.FrameRate = 30.0;
            videoCapture.PreviewWindow = videoWindow;
            videoCapture.FrameEvent2 += new Capture.HeFrame(CapturedFrame);
            videoCapture.GrapImg();

            // pauza pozwalająca na kompletne zainicjowanie kamery
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Zatrzymuje przechwytywanie obrazu.
        /// </summary>
        private void StopVideoCapture()
        {
            videoCapture.FrameEvent2 -= new Capture.HeFrame(CapturedFrame);
            videoCapture.PreviewWindow = null;
            videoCapture.Dispose();
            videoCapture = null;
        }

        /// <summary>
        /// Uruchamia przechwytywanie dźwięku za pomocą pierwszego dostępnego urządzenia.
        /// </summary>
        private void StartAudioCapture()
        {
            if (AudioIn.Devices.Length == 0)
                throw new Exception("Brak urządzeń do przechwytywania dźwięku!");

            microphone = new AudioIn(AudioIn.Devices[0], 8000, 16, 1);

        }

        /// <summary>
        /// Zatrzymuje przechwytywanie dźwięku.
        /// </summary>
        private void StopAudioCapture()
        {
            microphone.Dispose();
            microphone = null;
        }

        //####################################################################################################
        #endregion // Metody Prywatne


        #region Metody publiczne
        //###########################################################################################
        
        /// <summary>
        /// Uruchamia wysyłanie streamu jeżeli wcześniej nie zostało uruchomione.
        /// </summary>
        public void StartStreaming()
        {
            if(!streaming)
            {
                if (this.remoteEndPoint != null)
                {
                    try
                    {
                        framesCount = 0;
                        this.udpClient = new UdpClient(this.localEndPoint);
                        this.StartVideoCapture();
                        this.StartAudioCapture();

                        this.imageSendThread = new Thread(new ThreadStart(SendImage));
                        this.imageSendThread.Start();
                        this.audioSendThread = new Thread(new ThreadStart(SendSound));
                        this.audioSendThread.Start();
                        streaming = true;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); return; }
                }
                else
                {
                    MessageBox.Show("Nie określono adresu serwera/odbiorcy.", "Błąd", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Zatrzymuje wysłanie streamu jeżeli zostało uruchomione.
        /// </summary>
        public void StopStreaming()
        {
            if (streaming)
            {
                try
                {
                    this.imageSendThread.Abort();
                    this.audioSendThread.Abort();
                    this.udpClient.Close();
                    this.StopVideoCapture();
                    this.StopAudioCapture();
                    streaming = false;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
            }
        }

        /// <summary>
        /// Uruchamia odbieranie streamu jeżeli wcześniej nie zostało uruchomione.
        /// </summary>
        public void StartRecievingStream()
        {
            if(!recieving)
            {
                try
                {
                    framesCount = 0;
                    speaker = new AudioOut(AudioOut.Devices[0], 8000, 16, 1);
                    this.localEndPoint.Port = localUdpPort;
                    this.udpClient = new UdpClient(localUdpPort);
                    this.listenThread = new Thread(new ThreadStart(ListenForData));
                    this.listenThread.Start();
                    recieving = true;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }
            }
        }

        /// <summary>
        /// Zatrzymuje odbieranie streamu jeżeli było wcześniej uruchomione.
        /// </summary>
        public void StopRecievingStream()
        {
            if (recieving)
            {
                this.listenThread.Abort();
                this.udpClient.Close();
                speaker.Dispose();
                speaker = null;
                recieving = false;
            }
        }

        /// <summary>
        /// Ustawia odstęp czasu pomiędzy klatkami video.
        /// </summary>
        /// <param name="time">Odstęp czasu w ms</param>
        public void SetTimeInterval(int time)
        {
            timeInterval = time;
        }

        /// <summary>
        /// Ustawia jakość wysyłanego obrazu.
        /// </summary>
        /// <param name="imageQuality">Wartość jakości obrazu. Czym mniejsza tym słabsza jakość obrazu. MAX=50, MIN=0</param>
        public void SetImageQuality(long imageQuality)
        {
            if (imageQuality > 50)
                imageQuality = 50;
            else if (imageQuality < 0)
                imageQuality = 0;

            System.Drawing.Imaging.Encoder encoderQuality = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter encoderParam = new EncoderParameter(encoderQuality, imageQuality);
            encoderParams.Param[0] = encoderParam;
        }

        /// <summary>
        /// Ustawia głębię kolorów wysyłanego obrazu.
        /// </summary>
        /// <param name="imageColorDepth">Ilość kolorów.</param>
        public void SetImageColorDepth(long imageColorDepth)
        {
            System.Drawing.Imaging.Encoder encoderColorDepth = System.Drawing.Imaging.Encoder.ColorDepth;
            EncoderParameter encoderParam = new EncoderParameter(encoderColorDepth, imageColorDepth);
            encoderParams.Param[1] = encoderParam;
        }

        /// <summary>
        /// Ustawia czy obraz ma być wysyłany w kolorze czy w odcieniach szarości.
        /// </summary>
        /// <param name="in_color">True jeśli obraz ma być kolorowy.</param>
        public void SetImageInColor(bool in_color)
        {
            inColor = in_color;
        }

        /// <summary>
        /// Ustawia zdalny adres IP serwera lub innego klienta.
        /// </summary>
        /// <param name="ip">Adres IPv4 serwera lub klienta, do wysyłania lub odbierania streamu.</param>
        public void SetRemoteIP(string ip)
        {
            try
            {
                this.remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), remoteUdpPort);
            }
            catch
            {
                MessageBox.Show("Podano niewłaściwy adres ip serwera/odbiorcy.", "Błąd",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Zwraca lokalny adres ip.
        /// </summary>
        /// <returns>Lokalny adres ip w postaci stringu.</returns>
        public string GetLocalIP()
        {
            return this.localEndPoint.Address.ToString();
        }

        /// <summary>
        /// Ustawia lokalny port udp.
        /// </summary>
        /// <param name="port">Lokalny port UDP.</param>
        public void SetlocalUdpPort(int port)
        {
            localUdpPort = port;
        }

        /// <summary>
        /// Zwraca ilość wysłanych lub odebranych klatek video.
        /// </summary>
        /// <returns>Liczba wysłanych klatek video.</returns>
        public string GetFramesCount()
        {
            return framesCount.ToString();
        }

        /// <summary>
        /// Resetuje wartość zmiennej przechowującej liczbę wysłanych lub odebranych klatek video.
        /// </summary>
        public void ResetFramesCount()
        {
            framesCount = 0;
        }

        //#####################################################################################################
        #endregion // Metody publiczne
    }
}
