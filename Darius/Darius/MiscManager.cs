using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using SharpDX;
using System;
using System.Linq;

namespace Darius
{
    class MiscManager
    {
        public static void InterrupterOnOnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs args)
        {
            if (!sender.IsEnemy || !(sender is AIHeroClient) || Player.Instance.IsRecalling())
            {
                return;
            }
            if (MenuManager.getCheckBoxItem(MenuManager.miscMenu, "InterruptEQ") && SpellManager.E.IsReady() && SpellManager.E.IsInRange(sender))
            {
                SpellManager.E.Cast(sender);
            }
        }

        public static void OnPostAttack(AttackableUnit target, EventArgs args)
        {
            if (!MenuManager.getCheckBoxItem(MenuManager.comboMenu, "useW"))
                return;
            var t = TargetSelector.GetTarget(SpellManager.W.Range, DamageType.Physical);
            if (t.IsValidTarget() && (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "autoW")) && SpellManager.W.IsReady() && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                SpellManager.W.Cast();
            }
        }

        //Killsteal
        public static void ExecuteAdditionals()
        {
            foreach (var ksTarget in ObjectManager.Get<AIHeroClient>().Where(ksTarget => SpellManager.R.CanCast(ksTarget)))
                ComboManager.CastR(ksTarget);
        }

        public static bool InAutoAttackRange(AttackableUnit target)
        {
            if (!target.IsValidTarget())
            {
                return false;
            }
            var myRange = Player.Instance.GetAutoAttackRange(target);
            return
                Vector2.DistanceSquared(
                    target is Obj_AI_Base ? ((Obj_AI_Base)target).ServerPosition.To2D() : target.Position.To2D(),
                    ComboManager.Player.ServerPosition.To2D()) <= myRange * myRange;
        }
    }
}
