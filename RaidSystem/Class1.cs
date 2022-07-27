using Newtonsoft.Json;
using Rocket.API;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace ReponseRaidSystem
{
    public class Class1 : RocketPlugin<Config>
    {
        public static Class1 Instance { get; private set; }
        public string Warn { get; private set; }

        public static async Task SendVerif(string URL)
        {
            WebClient reponse = new WebClient();

            var Kullanıcı = reponse.DownloadString("http://localhost:3131/add/?id=" + URL);

            switch (Kullanıcı)
            {
                case "312":
                    Class1.Instance.Warn = "312";


                    break;
                case "313":
                    Class1.Instance.Warn = "313";

                    break;
                case "314":
                    Class1.Instance.Warn = "314";
                    break;
            }
        }

        protected override void Load()
        {

            Instance = this;
            base.Load();
            U.Events.OnPlayerConnected += PlayerConnected.Join;
            StructureManager.onDamageStructureRequested += StructureDamage.OnDamageStructure;
            BarricadeManager.onDamageBarricadeRequested += BarricadeDamage.OnDamageBaricade;

        }

        [RocketCommand("esle", "Discord İstek Yollar", "/esle <Dc ID>", AllowedCaller.Both)]
        public void ExecuteRulesCommand(IRocketPlayer caller, string[] args)
        {
            string logo = "https://cdn.discordapp.com/attachments/949058399155400824/950704886813712434/gfhjjf.png";

            Random rnd = new Random();
            UnturnedPlayer pl = caller as UnturnedPlayer;
            WebClient reponse = new WebClient();
            int verif = rnd.Next(000000, 999999);

            if (args[0].Length == 0)
            {
                ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> Yanlış Kullanım; <color=orange>/esle <Discord Id>.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

                return;
            }


            ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> Yükleniyor...", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

            var SendBots = SendVerif(args[0] + "&verif=" + verif);
            Task.Run(async () => await SendBots);

            if (Warn == "312")
            {

                var değer = Configuration.Instance.PlayerList.FirstOrDefault(x => x.OyunId == pl.CSteamID);
                if (değer == null)
                {
                    var değers = Convert.ToUInt64(args[0]);
                    Configuration.Instance.PlayerList.Add(new Model.PlayerLists { OyunId = pl.CSteamID, DcID = değers, VerifCode = verif, Onay = "Onaysız", Text = 0 }); ;
                    Configuration.Save();

                    ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> İşlem Başarılı. [Discord Dm Kutunu Kontrol Et]", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);


                }
                else
                {
                    ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> Kayıtlı Gözüküyorsun. Bir Sorun Olduğunu Düşünüyor İsen Destek Talebi Açabilirsin.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                }
            }
            else if (Warn == "313")
            {
                ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> <color=orange>{args[0]}</color> İle Hiç Bir Hesap Eşleşmediği İçin İşlem Başarısız.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
            }
            else if (Warn == "314")
            {
                ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> <color=orange>{args[0]}</color> İdsine Ait Hesabın <color=red>DM</color> Kapalı Olduğu İçin İşlem Başarısız.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);


            }

        }
        [RocketCommand("dogrula", "Discord İstek Yollar", "/esle <Discord ID>", AllowedCaller.Both)]
        public void ExecuteRulesCommands(IRocketPlayer caller, string[] args)
        {
            string logo = "https://cdn.discordapp.com/attachments/949058399155400824/950704886813712434/gfhjjf.png";

            UnturnedPlayer pl = caller as UnturnedPlayer;
            WebClient reponse = new WebClient();

            if (args[0].Length == 0)
            {
                ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> Yanlış Kullanım; <color=orange>/dogrula <Doğrulama Kodu>.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

                return;
            }


            var değers = Convert.ToInt64(args[0]);

            var değer = Configuration.Instance.PlayerList.FirstOrDefault(x => x.VerifCode == değers);

            if (değer == null)
            {
                ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> İlk Önce Eşleşme Yapmanız Gerekmektedir.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

            }
            else
            {
                
                if (değer.Onay == "Onaysız")
                {
                    değer.VerifCode -= değer.VerifCode;

                    değer.Onay = "Onaylı";
                    Configuration.Save();
                    ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> İşlem Başarılı.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);
                }
                else
                {
                    ChatManager.serverSendMessage($"<size=15><color=#5865F2> DİSCORD |</color></size> Hesabın Onaylı Gözüküyor.", Color.grey, null, pl.SteamPlayer(), EChatMode.SAY, logo, true);

                }
            }


        }

    }






}


