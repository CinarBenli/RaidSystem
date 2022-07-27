using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace SalakBot
{
    class Program
    {
        public static DiscordSocketClient _client;

        static void Main(string[] args)
            => new Program()
                .MainAsync()
                .GetAwaiter()
                .GetResult();
        
        public Program()
        {
            _client = new DiscordSocketClient();
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            var token = "OTYyMjU5MTc1NjA0Mzc1NTcy.Gs5GpO.txkEDSVW1R_U9OOTyK6SfSShv3hAmZk3OTpy54";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Connected += Client_Connected;

            await Task.Delay(-1);
        }

        private async Task Client_Connected()
        {
            await Task.Delay(0);

            await Task.Run(() =>
            {
                HttpListener mainRoute = new HttpListener();

                mainRoute.Prefixes.Add("http://localhost:3131/");

                mainRoute.Start();
                Console.WriteLine("Main route up!");
                mainRoute.BeginGetContext(MainRoute, mainRoute);
            });

            await Task.Run(() =>
            {
                HttpListener addRoute = new HttpListener();

                addRoute.Prefixes.Add("http://localhost:3131/add/");

                addRoute.Start();
                Console.WriteLine("Add route up!");
                addRoute.BeginGetContext(AddRoute, addRoute);
            });


            await Task.Run(() =>
            {
                HttpListener SendRoute = new HttpListener();

                SendRoute.Prefixes.Add("http://localhost:3131/send/");

                SendRoute.Start();
                Console.WriteLine("Add route up!");
                SendRoute.BeginGetContext(Send, SendRoute);
            });

            Console.ReadKey();

        }

        public static async void MainRoute(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;

            HttpListenerContext context = listener.EndGetContext(result);

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            byte[] value = Encoding.UTF8.GetBytes("<h1><center>Reponse Emek</center></h1>");

            response.AddHeader("Content-Type", "text/html; charset=utf-8");
            response.OutputStream.Write(value, 0, value.Length);
            response.Close();

            listener.BeginGetContext(AddRoute, listener);
        }

        public static async void Send(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;

            HttpListenerContext context = listener.EndGetContext(result);

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            var id = request.QueryString["id"];
            var yapısahibiad = request.QueryString["yapad"];
            var yapısahibiID = request.QueryString["yapıd"];

            var KıranAd = request.QueryString["kırad"];
            var KıranId = request.QueryString["kırıd"];

            var Konum = request.QueryString["konum"];

            var yapıad = request.QueryString["yapıad"];
            var yapııd = request.QueryString["yapııd"];

            var ids = Convert.ToUInt64(id);

            byte[] value = Encoding.UTF8.GetBytes(" ");

            if (ids == 0) goto end;
            var user = await _client.GetUserAsync(ids);

            if (user == null)
            {
                value = Encoding.UTF8.GetBytes("313");
            }

            else
            {
                try
                {
                    var dc = new EmbedBuilder();
                    dc.WithTitle("Reponse Emek Raid Alert");
                    dc.WithAuthor("Reponse Emek Raid Alert", "https://cdn.discordapp.com/attachments/952346861526720584/952905653024882738/1055096_caution_alert_attention_danger_error_icon.png");
                    dc.WithDescription($"**Yapın Hasar Yiyor;** \n  ``` Kullanıcı Adı: {yapısahibiad} \n Kullanıcı Id: {yapısahibiID}``` \n \n **Base Adresi;** \n ``` {Konum}``` \n **Yapı Bilgileri;** \n ``` Yapı Adı: {yapıad} \n Yapı Id: {yapııd}``` \n **Raid Atan Bilgileri;** \n``` Kullanıcı Adı: {KıranAd} \n Kullanıcı Id: {KıranId}```");
                    dc.WithThumbnailUrl("https://cdn.discordapp.com/attachments/952346861526720584/952905653024882738/1055096_caution_alert_attention_danger_error_icon.png");
                    dc.WithColor(Color.Purple);
                    dc.WithImageUrl("https://cdn.discordapp.com/attachments/951519411112644608/952556279308623933/reponse.png");
                    dc.WithFooter("Reponse Emek Raid Alert", "https://cdn.discordapp.com/attachments/952346861526720584/952905653024882738/1055096_caution_alert_attention_danger_error_icon.png");
                    await user.SendMessageAsync("", false, dc.Build());
                    value = Encoding.UTF8.GetBytes("312");
                }
                catch (Exception)
                {

                    value = Encoding.UTF8.GetBytes("314");

                }



            }

            response.AddHeader("Content-Type", "text/html; charset=utf-8");
            response.OutputStream.Write(value, 0, value.Length);
            response.Close();

        end:
            listener.BeginGetContext(Send, listener);
        }

        public static async void AddRoute(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;

            HttpListenerContext context = listener.EndGetContext(result);

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            var id = request.QueryString["id"];
            var verif = request.QueryString["verif"];
            var ids = Convert.ToUInt64(id);

            byte[] value = Encoding.UTF8.GetBytes(" ");

            if (ids == 0) goto end;
            var user = await _client.GetUserAsync(ids);

            if (user == null)
            {
                value = Encoding.UTF8.GetBytes("313");
            }
            
            else
            {
                try
                {
                    var dc = new EmbedBuilder();
                    dc.WithTitle("Reponse Emek Verify");
                    dc.WithAuthor("Reponse Emek Verify", "https://cdn.discordapp.com/attachments/951519411112644608/952556005756133436/122.png");
                    dc.WithDescription($"Doğrulama Kodun: **{verif}**");
                    dc.WithThumbnailUrl("https://cdn.discordapp.com/attachments/951519411112644608/952556005756133436/122.png");
                    dc.AddField("Hesabımı Nasıl Onaylamalıyım?", "/hesapeşle `<Size Verilen Doğrulama Kodu>`", true);
                    dc.AddField("Ne İşime Yarayacak?", "**1-** Raid Yediğinizde Özelden Bildirim Gelecektir. \n \n **2-** Uyarı Veya Sorun Olduğunda Direk Dmden İletişime Geçilecektir.", true);
                    dc.AddField("Hata Alıyorum Ne Yapmalıyım?", "<#950448035069067274> Kanalından Yetkili Ekibimizle İletişim Kurabilirsiniz.", true);
                    dc.WithColor(Color.Purple);
                    dc.WithImageUrl("https://cdn.discordapp.com/attachments/951519411112644608/952556279308623933/reponse.png");
                    dc.WithFooter("Reponse Emek Verify", "https://cdn.discordapp.com/attachments/951519411112644608/952556005756133436/122.png");
                    await user.SendMessageAsync("", false, dc.Build());
                    value = Encoding.UTF8.GetBytes("312");
                }
                catch (Exception)
                {

                    value = Encoding.UTF8.GetBytes("314");

                }



            }

            response.AddHeader("Content-Type", "text/html; charset=utf-8");
            response.OutputStream.Write(value, 0, value.Length);
            response.Close();

        end:
            listener.BeginGetContext(AddRoute, listener);
        }
    }
}






