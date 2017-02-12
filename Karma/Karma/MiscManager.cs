using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma
{
    class MiscManager
    {
        public static void AntiGapcloser_OnEnemyGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloser)
        {
            if (gapcloser.Sender.IsValidTarget(300f) && SpellsManager.E.IsReady() && MenuManager.getCheckBoxItem(MenuManager.miscMenu, "egapclose"))
            {
                SpellsManager.E.Cast(ObjectManager.Player);
            }
            if (gapcloser.Sender.IsValidTarget(300f) && SpellsManager.Q.IsReady() && MenuManager.getCheckBoxItem(MenuManager.miscMenu, "qgapclose"))
            {
                SpellsManager.Q.Cast(gapcloser.Sender);
            }
        }

        public static void ExecuteAdditionals()
        {
            if (MenuManager.getCheckBoxItem(MenuManager.miscMenu, "ESheild"))
            {
                foreach (var hero in
                    ObjectManager.Get<AIHeroClient>()
                        .Where(
                            hero =>
                                hero.IsValidTarget(SpellsManager.E.Range, false) && hero.IsAlly &&
                                ObjectManager.Get<AIHeroClient>().Count(h => h.IsValidTarget() && h.Distance(hero) < 400) >
                                1))
                {
                    SpellsManager.E.Cast(hero);
                }
            }
        }
    }
}
