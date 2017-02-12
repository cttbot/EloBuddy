using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Karma
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

        public static Menu Main, drawMenu, comboMenu, harassMenu, miscMenu;

        public static void LoadMenu()
        {
            Main = MainMenu.AddMenu("Karma", "Karma");

            comboMenu = Main.AddSubMenu("Combo");
            comboMenu.Add("UseQ", new CheckBox("Use Q", true));
            comboMenu.Add("UseW", new CheckBox("Use W", true));
            comboMenu.Add("UseR", new CheckBox("Use R", true));          

            harassMenu = Main.AddSubMenu("Harass");
            harassMenu.Add("UseQ", new CheckBox("Use Q", true));
            harassMenu.Add("UseR", new CheckBox("Use R", true));
            harassMenu.Add("ManaHarass", new Slider("ManaPercent Harass", 60, 0, 100));

            miscMenu = Main.AddSubMenu("Misc");
            miscMenu.Add("ESheild", new CheckBox("Make E Shield"));
            miscMenu.Add("egapclose", new CheckBox("Use E on Gapclosers"));
            miscMenu.Add("qgapclose", new CheckBox("Use Q on Gapclosers"));
            miscMenu.Add("skinHack", new CheckBox("Skin Change"));
            miscMenu.Add("SkinID", new Slider("Skin", 0, 0, 8));

            drawMenu = Main.AddSubMenu("Draw");
            drawMenu.AddGroupLabel("Draw Spell");
            drawMenu.Add("qRange", new CheckBox("Q range", false));
            drawMenu.Add("wRange", new CheckBox("W range", false));
            drawMenu.Add("eRange", new CheckBox("E range", false));
            drawMenu.Add("onlyRdy", new CheckBox("Draw when skill ready", true));
        }
    }
}
