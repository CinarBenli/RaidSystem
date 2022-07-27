using ReponseRaidSystem.Model;
using Rocket.API;
using System.Collections.Generic;

namespace ReponseRaidSystem
{
    public class Config : IRocketPluginConfiguration
    {
        public string Webhook;
        public List<PlayerLists> PlayerList;

        public void LoadDefaults()
        {
            Webhook = "https://discord.com/api/webhooks/952206184373428275/L9jvxfhx4KwC0AmHXrGRmg39r_IklBGsY4jjJct4MY0wu-c3C6syfp0oYNgP7Brqg3QD";
            PlayerList = new List<PlayerLists>();


        }
    }
}