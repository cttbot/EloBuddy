using EloBuddy;
using EloBuddy.SDK;
using System;

namespace Riven
{
    class DamageManager
    {
        #region Riven: Math/Damage

        public static float ComboDamage(Obj_AI_Base target, bool checkq = false)
        {
            if (target == null)
                return 0f;

            var ad = (float)EventManager.player.GetAutoAttackDamage(target);
            var runicpassive = new[] { 0.2, 0.25, 0.3, 0.35, 0.4, 0.45, 0.5 };

            var ra = ad + (float)((+ObjectManager.Player.FlatPhysicalDamageMod + ObjectManager.Player.BaseAttackDamage) * runicpassive[Math.Min(ObjectManager.Player.Level, 18) / 3]);

            var rw = Wdmg(target);
            var rq = Qdmg(target);
            var rr = SpellManager.R.IsReady() ? Rdmg(target) : 0;        

            var damage = (rq * 3 + ra * 3 + rw + rr);

            return EventManager.xtra((float)damage);
        }


        public static double Wdmg(Obj_AI_Base target)
        {
            double dmg = 0;
            if (SpellManager.W.IsReady() && target != null)
            {
                dmg += Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                    new[] { 50, 80, 110, 150, 170 }[SpellManager.W.Level - 1] + 1 * ObjectManager.Player.FlatPhysicalDamageMod + ObjectManager.Player.BaseAttackDamage);
            }

            return dmg;
        }

        public static double Qdmg(Obj_AI_Base target)
        {
            double dmg = 0;
            if (SpellManager.Q.IsReady() && target != null)
            {
                dmg += Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                    -10 + (SpellManager.Q.Level * 20f) + (0.35f + (SpellManager.Q.Level * 0.05f)) * (ObjectManager.Player.FlatPhysicalDamageMod + ObjectManager.Player.BaseAttackDamage));
            }

            return dmg;
        }

        public static double Rdmg(Obj_AI_Base target)
        {
            double dmg = 0;

            if (SpellManager.R.IsReady() && target != null)
            {
                dmg += Player.Instance.CalculateDamageOnUnit(target, DamageType.Physical,
                    (new[] { 80, 120, 160 }[Math.Max(SpellManager.R.Level, 1) - 1] + 0.6f * ObjectManager.Player.FlatPhysicalDamageMod) *
                    (((target.MaxHealth - target.Health) / target.MaxHealth) * 2.67f + 1f));
            }

            return dmg;
        }

        #endregion
    }
}
