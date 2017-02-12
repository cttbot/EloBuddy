using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karma
{
    class HarassManager
    {
        public static void Harass()
        {
            var qTarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);

            if (MenuManager.getCheckBoxItem(MenuManager.harassMenu, "UseQ") && qTarget != null && SpellsManager.Q.IsReady() && Player.Instance.ManaPercent > MenuManager.getSliderItem(MenuManager.harassMenu, "ManaHarass"))
            {
                if (MenuManager.getCheckBoxItem(MenuManager.harassMenu, "UseR"))
                {
                    SpellsManager.R.Cast();
                }

                if (!MenuManager.getCheckBoxItem(MenuManager.harassMenu, "UseR") || !SpellsManager.R.IsReady())
                {
                    var qPrediction = SpellsManager.Q.GetPrediction(qTarget);
                    if (qPrediction.HitChance >= HitChance.High)
                    {
                        SpellsManager.Q.Cast(qPrediction.CastPosition);
                    }
                    else if (qPrediction.HitChance == HitChance.Collision)
                    {
                        var minionsHit = qPrediction.CollisionObjects;
                        var closest =
                            minionsHit.Where(m => m.NetworkId != ObjectManager.Player.NetworkId)
                                .OrderBy(m => m.Distance(ObjectManager.Player))
                                .FirstOrDefault();

                        if (closest != null && closest.Distance(qPrediction.UnitPosition) < 200)
                        {
                            SpellsManager.Q.Cast(qPrediction.CastPosition);
                        }
                    }
                }
            }
        }
    }
}
