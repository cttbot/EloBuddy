using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darius
{
    class MenuManager
    {
        public static bool getCheckBoxItem(Menu m, string item)
        {
            return m[item].Cast<CheckBox>().CurrentValue;
        }

        public static int getSliderItem(Menu m, string item)
        {
            return m[item].Cast<Slider>().CurrentValue;
        }

        public static bool getKeyBindItem(Menu m, string item)
        {
            return m[item].Cast<KeyBind>().CurrentValue;
        }

        public static int getBoxItem(Menu m, string item)
        {
            return m[item].Cast<ComboBox>().CurrentValue;
        }

        public static Menu Main, drawMenu, comboMenu, harassMenu, farmMenu, miscMenu;

        public static void LoadMenu()
        {
            Main = MainMenu.AddMenu("Darius", "Darius");
                      
            comboMenu = Main.AddSubMenu("Combo");
            comboMenu.AddGroupLabel("Q Config");
            comboMenu.Add("useQ", new CheckBox("Use Q", true));
            comboMenu.Add("LockQ", new CheckBox("Lock target Q", true));
            comboMenu.AddGroupLabel("W Config");
            comboMenu.Add("useW", new CheckBox("Use W", true));
            comboMenu.Add("autoW", new CheckBox("Auto W", true));
            comboMenu.AddGroupLabel("E Config");
            comboMenu.Add("useE", new CheckBox("Use E", true));
            foreach (var enemy in ObjectManager.Get<AIHeroClient>().Where(enemy => enemy.Team != ComboManager.Player.Team))
                comboMenu.Add("Eon" + enemy.ChampionName, new CheckBox("E to : " + enemy.ChampionName));
            
            comboMenu.AddGroupLabel("R Config");
            comboMenu.Add("autoR", new CheckBox("Auto R", true));
            comboMenu.Add("useR", new CheckBox("Use R", true));
            comboMenu.Add("useRManual", new KeyBind("Semi-manual cast R key", false, KeyBind.BindTypes.HoldActive, 'T'));
            comboMenu.Add("autoRbuff", new CheckBox("Auto R if darius execute multi cast time out ", true));
            comboMenu.Add("autoRdeath", new CheckBox("Auto R if darius execute multi cast and under 10 % hp", true));
            comboMenu.Add("adjustDmg", new Slider("Adjust ultimate dmg", 0, -150, 150));

            harassMenu = Main.AddSubMenu("Harass");
            harassMenu.AddGroupLabel("Q Config");
            harassMenu.Add("useQ", new CheckBox("Use Q", true));
            harassMenu.Add("ManaHarass", new Slider("ManaPercent Harass", 60, 0, 100));

            miscMenu = Main.AddSubMenu("Misc");
            miscMenu.Add("InterruptEQ", new CheckBox("Interrupt Spells using E?"));
            miscMenu.Add("R.KillSteal", new CheckBox("KillSteal R"));
            miscMenu.Add("skinHack", new CheckBox("Skin Change"));
            miscMenu.Add("SkinID", new Slider("Skin", 0, 0, 8));


            farmMenu = Main.AddSubMenu("Farm");
            farmMenu.Add("farmW", new CheckBox("Farm W", true));
            farmMenu.Add("farmQ", new CheckBox("Farm Q", true));

            drawMenu = Main.AddSubMenu("Draw");
            drawMenu.AddGroupLabel("Draw Spell");
            drawMenu.Add("qRange", new CheckBox("Q range", false));
            drawMenu.Add("eRange", new CheckBox("E range", false));
            drawMenu.Add("rRange", new CheckBox("R range", false));
            drawMenu.Add("onlyRdy", new CheckBox("Draw when skill rdy", true));
        }
    }
}
