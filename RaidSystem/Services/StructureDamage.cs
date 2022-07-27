using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReponseRaidSystem
{
    public static class StructureDamage
    {
        public static async Task SendBot(string URL)
        {
            WebClient reponse = new WebClient();

            var Kullanıcı = reponse.DownloadString("http://localhost:3131/send/?id=" + URL);

        }
        internal static void OnDamageStructure(CSteamID instigatorSteamID, Transform structureTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            byte x;
            byte y;

            ushort index;

            StructureRegion s;

            StructureManager.tryGetInfo(structureTransform, out x, out y, out index, out s);


            var sdata = s.structures[index];



            UnturnedPlayer owner = UnturnedPlayer.FromCSteamID(new CSteamID(sdata.owner));
            UnturnedPlayer instigator = UnturnedPlayer.FromCSteamID(instigatorSteamID);
            var değer = Class1.Instance.Configuration.Instance.PlayerList.FirstOrDefault(e => e.OyunId == owner.CSteamID);

            var node = LevelNodes.nodes.OfType<LocationNode>().OrderBy(k => Vector3.Distance(k.point, instigator.Position)).FirstOrDefault();
            string Tarih = DateTime.Now.ToString();

            if (değer == null) return;
            

                if (sdata.structure.health - pendingTotalDamage <= 0)
                {
                    değer.Text += 1;
                    UnturnedChat.Say(değer.Text.ToString());


                    if (değer.Text == 1)
                    {
                        if (değer.Onay == "Onaylı")
                        {


                            var SendBots = SendBot(değer.DcID + "&yapad=" + owner.CharacterName + "&yapıd=" + owner.CSteamID + "&kırad=" + instigator.CharacterName + "&kırıd=" + instigator.CSteamID + "&konum=" + node.name + "&yapıad=" + sdata.structure.asset.name + "&yapııd=" + sdata.structure.asset.id);
                            Task.Run(async () => await SendBots);

                        }
                        else
                        {
                            return;
                        }

                    }
                    else if (değer.Text == 4)
                    {


                        if (değer.Onay == "Onaylı")
                        {
                            değer.Text -= 4;
                            var SendBots = SendBot(değer.DcID + "&yapad=" + owner.CharacterName + "&yapıd=" + owner.CSteamID + "&kırad=" + instigator.CharacterName + "&kırıd=" + instigator.CSteamID + "&konum=" + node.name + "&yapıad=" + sdata.structure.asset.name + "&yapııd=" + sdata.structure.asset.id);
                            Task.Run(async () => await SendBots);
                        }
                        else
                        {
                            return;
                        }
                    }



                }
            
           
        }
    }
}
