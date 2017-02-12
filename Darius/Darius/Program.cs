using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darius
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }
        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Hero != Champion.Darius)
            {
                return;
            }

            MenuManager.LoadMenu();

            Interrupter.OnInterruptableSpell += MiscManager.InterrupterOnOnInterruptableSpell;
            Orbwalker.OnPostAttack += MiscManager.OnPostAttack;
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += DrawManager.Drawing_OnDraw;
            Chat.Print("<font color=\"#ca0711\" >CTTBOT Presents </font><font color=\"#ffffff\" >Darius </font><font color=\"#ca0711\" >Kappa</font>");
        }

        public static void Game_OnTick(EventArgs args)
        {
            ComboManager.CastE();

            ComboManager.CastQ();

            ComboManager.CastR();

            if (MenuManager.getCheckBoxItem(MenuManager.miscMenu, "R.KillSteal"))
                MiscManager.ExecuteAdditionals();

            Obj_AI_Base.OnProcessSpellCast += ComboManager.OnProcessSpellCast;

            FarmManager.Farm();

            if (MenuManager.getCheckBoxItem(MenuManager.miscMenu, "skinHack"))
            {
                Player.Instance.SetSkinId(MenuManager.getSliderItem(MenuManager.miscMenu, "SkinID"));
            }
            else { Player.Instance.SetSkinId(0); }
        }
    }
}
