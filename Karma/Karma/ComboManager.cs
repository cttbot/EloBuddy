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
    class ComboManager
    {

        public static void Combo()
        {
            var qTarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Magical);
            var wTarget = TargetSelector.GetTarget(SpellsManager.W.Range, DamageType.Magical);

            if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "UseW") && wTarget != null && SpellsManager.W.IsReady())
            {
                if ((ObjectManager.Player.Health / ObjectManager.Player.MaxHealth) /
                    (qTarget.Health / qTarget.MaxHealth) < 1)
                {
                    if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "UseR"))
                    {
                        SpellsManager.R.Cast();
                    }

                    if (!MenuManager.getCheckBoxItem(MenuManager.comboMenu, "UseR") || !SpellsManager.R.IsReady())
                    {
                        SpellsManager.W.Cast(wTarget);
                    }
                }
            }

            if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "UseQ") && qTarget != null && SpellsManager.Q.IsReady())
            {
                if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "UseR"))
                {
                    SpellsManager.R.Cast();
                }

                if (!MenuManager.getCheckBoxItem(MenuManager.comboMenu, "UseR") || !SpellsManager.R.IsReady())
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

            if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "UseW") && wTarget != null)
            {
                SpellsManager.W.Cast(wTarget);
            }
        }          
    }
}
