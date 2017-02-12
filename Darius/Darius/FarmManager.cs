using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darius
{
    class FarmManager
    {
        public static void Farm()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, EloBuddy.Player.Instance.Position, SpellManager.Q.Range).Where( m => !m.IsDead && m.IsValid && !m.IsInvulnerable);

            {
                foreach (var m in minions)
                {
                    if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                    {
                        if (MenuManager.getCheckBoxItem(MenuManager.farmMenu, "farmQ"))
                        {
                            SpellManager.Q.Cast();
                        }
                        if (MenuManager.getCheckBoxItem(MenuManager.farmMenu, "farmW") && Damage.WDamage(m) > m.Health)
                        {
                            SpellManager.W.Cast();
                        }
                    }                   
                }
            }

        }
    }
}
