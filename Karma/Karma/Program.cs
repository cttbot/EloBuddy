using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;

namespace Karma
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Karma")
            {
                return;
            }
            MenuManager.LoadMenu();
            Gapcloser.OnGapcloser += MiscManager.AntiGapcloser_OnEnemyGapcloser;
            Game.OnUpdate += Game_OnGameUpdate;
            Drawing.OnDraw += DrawManager.Drawing_OnDraw;
            Chat.Print("<font color=\"#ca0711\" >CTTBOT Presents </font><font color=\"#ffffff\" >Karma </font><font color=\"#ca0711\" >Kappa</font>");
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (SpellsManager.MantraActive)
            {
                SpellsManager.Q.Range = 1000;
                SpellsManager.Q.Width = 80;
            }
            else
            {
                SpellsManager.Q.Range = 950;
                SpellsManager.Q.Width = 60;
            }

            MiscManager.ExecuteAdditionals();
            if(Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            ComboManager.Combo();

            HarassManager.Harass();

            if (MenuManager.getCheckBoxItem(MenuManager.miscMenu, "skinHack"))
            {
                Player.Instance.SetSkinId(MenuManager.getSliderItem(MenuManager.miscMenu, "SkinID"));
            }
            else { Player.Instance.SetSkinId(0); }
        }
    }
}
