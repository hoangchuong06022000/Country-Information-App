using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Model.Model;
using Model.DAL;
using System.Threading;

namespace Server
{
    public class Program
    {
        public static SimpleTcpServer server;
        private static String host = "127.0.0.1";
        private static int port = 2000;
        private static GEOSNAME api;
        private static List<THONGTIN> listQuocGia;
        private static DAL_QuocGia dalQG = new DAL_QuocGia();
        public static void Main(string[] args)
        {
            server = new SimpleTcpServer();
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(host);
            server.Start(ip, port);
            Console.WriteLine("Server is starting...");
            server.Delimiter = 0x13;
            server.DataReceived += Receive; 
            while (server.IsStarted)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("1. Luu du lieu vao database");
                Console.WriteLine("2. Khong luu du lieu");
                try
                {
                    int select = Int32.Parse(Console.ReadLine());
                    switch (select)
                    {
                        case 1:
                            bool check = false;
                            foreach (var item in Program.api.geonames)
                            {
                                THONGTIN thongTin = new THONGTIN
                                {
                                    TENQUOCGIA = item.countryName,
                                    THUDO = item.capital,
                                    NGONNGU = item.languages,
                                    CHAULUC = item.continentName,
                                    TIENTE = item.currencyCode,
                                    DIENTICH = item.areaInSqKm,
                                    DANSO = item.population
                                };
                                check = dalQG.Insert(thongTin);
                            }
                            if (check == true)
                            {
                                Console.WriteLine("Cap nhat thanh cong!!");
                            }
                            else
                            {
                                Console.WriteLine("Cap nhat khong thanh cong!!");
                            }
                            break;
                        case 2:
                            break;
                        default:
                            Console.WriteLine("Chi duoc nhap 1 hoac 2!!");
                            break;

                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Vui long nhap vao so!!");
                }
            }
            server.Stop();
        }
        public static GEOSNAME ConvertJSONToObject(string jsonString)
        {
            GEOSNAME countries = JsonConvert.DeserializeObject<GEOSNAME>(jsonString);
            if ((countries == null | countries.geonames == null))
            {
                return null;
            }
            else
            {
                return countries;
            }
        }

        private static void Receive(object sender, Message e)
        {
            Console.WriteLine(e.MessageString);
            Send(sender, e);
        }
        private static void Send(object sender, Message e)
        {
            e.ReplyLine(GetApiData());
            Program.api = Program.ConvertJSONToObject(GetApiData());
        }

        public static string GetApiData()
        {
            string url = "http://api.geonames.org/countryInfoJSON?formatted=true&username=hoangchuong06022000";
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse ws = request.GetResponse();
                string jsonString = string.Empty;
                using (System.IO.StreamReader sreader = new System.IO.StreamReader(ws.GetResponseStream()))
                {
                    jsonString = sreader.ReadToEnd();
                }
                return jsonString;
            }
            catch (WebException wex)
            {

            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
