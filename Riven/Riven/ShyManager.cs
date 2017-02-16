using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Riven
{
    class ShyManager : MenuBase
    {
        public static void Burst()
        {
            if (EventManager.riventarget() != null && EventManager.riventarget().IsValidTarget())
            {
                ShyBurst(EventManager.riventarget());
            }
        }

        private static void ShyBurst(AIHeroClient target)
        {
            if (SpellManager.E.IsReady() && SpellManager.R.IsReady() && SpellManager.W.IsReady() && !CheckUlt())
            {
                if (target.IsValidTarget(SpellManager.E.Range + Me.BoundingRadius - 30))
                {
                    SpellManager.E.Cast(target.Position);
                    Core.DelayAction(() => SpellManager.R.Cast(), 10);
                    Core.DelayAction(() => SpellManager.W.Cast(), 60);
                    return;
                }

                if (getCheckBoxItem(comboMenu, "flashb") && SpellManager.Flash.IsReady())
                {
                    if (target.IsValidTarget(SpellManager.E.Range + Me.BoundingRadius + SpellManager.Flash.Range - 50))
                    {
                        SpellManager.E.Cast(target.Position);
                        Core.DelayAction(() => SpellManager.R.Cast(), 10);
                        Core.DelayAction(() => SpellManager.W.Cast(), 60);
                        Core.DelayAction(() => SpellManager.Flash.Cast(target.Position), 61);
                        return;
                    }
                }
            }
            else
            {
                if (SpellManager.W.IsReady() && target.IsValidTarget(SpellManager.W.Range))
                {
                    SpellManager.W.Cast();
                }
            }
        }
    }
}
