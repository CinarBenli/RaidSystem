using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReponseRaidSystem
{
    public static class BarricadeDamage
    {
        internal static void OnDamageBaricade(CSteamID instigatorSteamID, Transform barricadeTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            byte x;
            byte y;

            ushort plant;
            ushort index;

            BarricadeRegion r;



            BarricadeManager.tryGetInfo(barricadeTransform, out x, out y, out plant, out index, out r);


            var sdata = r.barricades[index];


            UnturnedPlayer owner = UnturnedPlayer.FromCSteamID((CSteamID)sdata.owner);
            UnturnedPlayer instigator = UnturnedPlayer.FromCSteamID(instigatorSteamID);

            var node = LevelNodes.nodes.OfType<LocationNode>().OrderBy(k => Vector3.Distance(k.point, instigator.Position)).FirstOrDefault();
            string Tarih = DateTime.Now.ToString();

            if (sdata.barricade.health - pendingTotalDamage <= 0)
            {
                UnturnedChat.Say($" ```RAID ALERT``` \n\n **Yapı Sahibinin Bilgileri; ** \n``` Kullanıcı Adı:{owner.CharacterName} \n Kullanıcı Id:{owner.CSteamID} \n Durum:Online``` \n \n **Yapıyı Kıranın Bilgileri;** \n ``` Kullanıcı Adı:{instigator.CharacterName} \n Kullanıcı Id: {instigator.CSteamID}``` \n \n **Yapının Bilgileri;** \n ``` Konum: {node.name} \n Adı: {sdata.barricade.asset.name} \n ID: {sdata.barricade.id} \n TARİH: {Tarih}```");



            }
        }
    }
}
