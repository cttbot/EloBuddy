using EloBuddy;
using System;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Karma
{
    class DrawManager
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "qRange"))
            {
                if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "onlyRdy"))
                {
                    if (SpellsManager.Q.IsReady())
                        Circle.Draw(Color.Cyan, SpellsManager.Q.Range, Player.Instance.Position);
                }
                else
                    Circle.Draw(Color.Cyan, SpellsManager.Q.Range, Player.Instance.Position);
            }
            if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "wRange"))
            {
                if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "onlyRdy"))
                {
                    if (SpellsManager.W.IsReady())
                        Circle.Draw(Color.Orange, SpellsManager.W.Range, Player.Instance.Position);
                }
                else
                    Circle.Draw(Color.Orange, SpellsManager.W.Range, Player.Instance.Position);
            }
            if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "eRange"))
            {
                if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "onlyRdy"))
                {
                    if (SpellsManager.E.IsReady())
                        Circle.Draw(Color.White, SpellsManager.E.Range, Player.Instance.Position);
                }
                else
                    Circle.Draw(Color.White, SpellsManager.E.Range, Player.Instance.Position);
            }
        }
    }
}
