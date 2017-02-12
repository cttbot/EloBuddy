using EloBuddy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Darius
{
    public class DrawManager
    {
        public static void Drawing_OnDraw(EventArgs args)
        {
            if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "qRange"))
            {
                if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "onlyRdy"))
                {
                    if (SpellManager.Q.IsReady())
                        Circle.Draw(Color.Cyan, SpellManager.Q.Range, Player.Instance.Position);
                }
                else
                    Circle.Draw(Color.Cyan, SpellManager.Q.Range, Player.Instance.Position);
            }
            if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "eRange"))
            {
                if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "onlyRdy"))
                {
                    if (SpellManager.E.IsReady())
                        Circle.Draw(Color.Orange, SpellManager.E.Range, Player.Instance.Position);
                }
                else
                    Circle.Draw(Color.Orange, SpellManager.E.Range, Player.Instance.Position);
            }
            if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "rRange"))
            {
                if (MenuManager.getCheckBoxItem(MenuManager.drawMenu, "onlyRdy"))
                {
                    if (SpellManager.R.IsReady())
                        Circle.Draw(Color.Red, SpellManager.R.Range, Player.Instance.Position);
                }
                else
                    Circle.Draw(Color.Red, SpellManager.R.Range, Player.Instance.Position);
            }           
        }
    }
}
