using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darius
{
    class ComboManager
    {
        public static AIHeroClient Player { get { return ObjectManager.Player; } }

        private static void LockQOnTarget()
        {
            var target = SpellManager.Q.GetTarget();
            if (target == null)
            {
                return;
            }
            var endPos = (Player.ServerPosition - target.ServerPosition).Normalized();
            var predPos = SpellManager.E.GetPrediction(target).CastPosition.To2D();
            var fullPoint = new Vector2(predPos.X + endPos.X * SpellManager.Q.Range, predPos.Y + endPos.Y * SpellManager.Q.Range);
            var closestPoint = Player.ServerPosition.To2D().Closest(new List<Vector2> { predPos, fullPoint });
            if (closestPoint.IsValid() && !closestPoint.IsWall() && predPos.Distance(closestPoint) > SpellManager.Q.Range)
            {
                EloBuddy.Player.IssueOrder(GameObjectOrder.MoveTo, closestPoint.To3D());
            }
            else if (fullPoint.IsValid() && !fullPoint.IsWall() && predPos.Distance(fullPoint) < SpellManager.Q.Range && predPos.Distance(fullPoint) > 100)
            {
                EloBuddy.Player.IssueOrder(GameObjectOrder.MoveTo, fullPoint.To3D());
            }
        }

        public static void CastR(AIHeroClient target)
        {
            if (!SpellManager.R.IsReady())
                return;

            if (target.HasBuffOfType(BuffType.Invulnerability)
                                && target.HasBuffOfType(BuffType.SpellShield)
                                && target.HasBuff("kindredrnodeathbuff") //Kindred Ult
                                && target.HasBuff("BlitzcrankManaBarrierCD") //Blitz Passive
                                && target.HasBuff("ManaBarrier") //Blitz Passive
                                && target.HasBuff("FioraW") //Fiora W
                                && target.HasBuff("JudicatorIntervention") //Kayle R
                                && target.HasBuff("UndyingRage") //Trynd R
                                && target.HasBuff("BardRStasis") //Bard R
                                && target.HasBuff("ChronoShift") //Zilean R
                                )
                return;

            
            if (target.IsValidTarget(SpellManager.R.Range) && !target.IsZombie)
            {
                int PassiveCounter = target.GetBuffCount("dariushemo") <= 0 ? 0 : target.GetBuffCount("dariushemo");
                if (Damage.RDamage(target, PassiveCounter) >= target.Health + Damage.PassiveDmg(target, 1))
                {
                    SpellManager.R.Cast(target);
                }
            }
        }

        public static void CastQ()
        {
            var target = TargetSelector.GetTarget(SpellManager.Q.Range, DamageType.Physical);

            if (target.IsValidTarget())
            {
                if (CanQ(target) && (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "useQ") && SpellManager.Q.CanCast(target) && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))||
                    (MenuManager.getCheckBoxItem(MenuManager.harassMenu, "useQ") && SpellManager.Q.CanCast(target) && EloBuddy.Player.Instance.ManaPercent > MenuManager.getSliderItem(MenuManager.harassMenu, "ManaHarass")))
                {
                    SpellManager.Q.Cast();
                    if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "LockQ") && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                        LockQOnTarget();
                }
                
            }              
        }

        internal static bool CanQ(Obj_AI_Base target)
        {
            if (target.HasBuffOfType(BuffType.Invulnerability)
                                && target.HasBuffOfType(BuffType.SpellShield)
                                && target.HasBuff("kindredrnodeathbuff") //Kindred Ult
                                && target.HasBuff("BlitzcrankManaBarrierCD") //Blitz Passive
                                && target.HasBuff("ManaBarrier") //Blitz Passive
                                && target.HasBuff("FioraW") //Fiora W
                                && target.HasBuff("JudicatorIntervention") //Kayle R
                                && target.HasBuff("UndyingRage") //Trynd R
                                && target.HasBuff("BardRStasis") //Bard R
                                && target.HasBuff("ChronoShift") //Zilean R
                                )
            {
                return false;
            }

            if (SpellManager.R.IsReady() && Player.Mana - SpellManager.Q.ManaCost < SpellManager.R.ManaCost)
            {
                return false;
            }

            if (SpellManager.W.IsReady() && Damage.WDamage(target) >= target.Health &&
                target.Distance(Player.ServerPosition) <= 200)
            {
                return false;
            }

            if (SpellManager.W.IsReady() && Player.HasBuff("DariusNoxonTactictsONH") && target.Distance(Player.ServerPosition) <= 225)
            {
                return false;
            }

            if (Player.Distance(target.ServerPosition) > SpellManager.Q.Range)
            {
                return false;
            }

            if (SpellManager.R.IsReady() && SpellManager.R.IsInRange(target) &&
                Damage.RDamage(target, Damage.PassiveCount(target)) - Damage.PassiveDmg(target, 1) >= target.Health)
            {
                return false;
            }

            if (Player.GetAutoAttackDamage(target) * 2 + Damage.PassiveDmg(target, Damage.PassiveCount(target)) >= target.Health &&
                Player.Distance(target.ServerPosition) <= 180)
            {
                return false;
            }

            return true;
        }

        public static void CastE()
        {
            var target = TargetSelector.GetTarget(SpellManager.E.Range, DamageType.Physical);
            if (target.IsValidTarget())
            {
                var eprediction = SpellManager.E.GetPrediction(target);
                if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "useE") && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    if (target.Distance(EloBuddy.Player.Instance.ServerPosition) > 250)
                    {
                        if (SpellManager.E.IsReady() && target != null)
                        {
                            if (eprediction.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
                            {
                                
                                if (Damage.RDamage(target, Damage.PassiveCount(target)) >= target.Health + Damage.PassiveDmg(target, 1))
                                    SpellManager.E.Cast(eprediction.CastPosition);

                                if (SpellManager.Q.IsReady() || SpellManager.W.IsReady())
                                    SpellManager.E.Cast(eprediction.CastPosition);

                                if (Player.GetAutoAttackDamage(target) + Damage.PassiveDmg(target, 3) * 3 >= target.Health)
                                    SpellManager.E.Cast(eprediction.CastPosition);
                            }
                        }
                    }
                    if (SpellManager.E.IsReady() && MenuManager.getCheckBoxItem(MenuManager.comboMenu, "Eon" + target.ChampionName) && (EloBuddy.Player.Instance.IsUnderTurret() || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)))
                    {
                        if (!EloBuddy.Player.Instance.IsInAutoAttackRange(target))
                        {
                            if (eprediction.HitChance >= EloBuddy.SDK.Enumerations.HitChance.Medium)
                            {
                                if (SpellManager.E.IsReady() && target != null)
                                {
                                    SpellManager.E.Cast(eprediction.CastPosition);
                                }
                            }
                        }

                    }
                }

            }
        }

        public static void OnProcessSpellCast(Obj_AI_Base unit, GameObjectProcessSpellCastEventArgs args)
        {
            try
            {
                if (unit.IsMe)
                {
                    if (args.SData.Name == "DariusNoxianTacticsONH")
                    {
                        Core.DelayAction(() => Orbwalker.ResetAutoAttack(), 30);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void CastR()
        {
            var target = TargetSelector.GetTarget(SpellManager.R.Range, DamageType.True);
            if (target.IsValidTarget())
            {
                if (target.HasBuffOfType(BuffType.Invulnerability)
                                                && target.HasBuffOfType(BuffType.SpellShield)
                                                && target.HasBuff("kindredrnodeathbuff") //Kindred Ult
                                                && target.HasBuff("BlitzcrankManaBarrierCD") //Blitz Passive
                                                && target.HasBuff("ManaBarrier") //Blitz Passive
                                                && target.HasBuff("FioraW") //Fiora W
                                                && target.HasBuff("JudicatorIntervention") //Kayle R
                                                && target.HasBuff("UndyingRage") //Trynd R
                                                && target.HasBuff("BardRStasis") //Bard R
                                                && target.HasBuff("ChronoShift") //Zilean R
                                                )
                    return;

                if (SpellManager.R.IsReady() && MenuManager.getKeyBindItem(MenuManager.comboMenu, "useRManual"))
                {
                    if (target.IsValidTarget())
                        SpellManager.R.Cast(target);
                }

                if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "useR") && SpellManager.R.IsReady() && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                {
                    if (target.IsValidTarget(SpellManager.R.Range) && !target.IsZombie)
                    {
                        int PassiveCounter = target.GetBuffCount("dariushemo") <= 0 ? 0 : target.GetBuffCount("dariushemo");
                        if (Damage.RDamage(target, PassiveCounter) >= target.Health + Damage.PassiveDmg(target, 1))
                        {
                            SpellManager.R.Cast(target);
                        }
                    }
                }

                if (MenuManager.getCheckBoxItem(MenuManager.comboMenu, "autoRbuff"))
                {
                    var buffTime = Damage.GetPassiveTime(Player, "dariusexecutemulticast");
                    if ((buffTime < 2 || (Player.HealthPercent < 10 && MenuManager.getCheckBoxItem(MenuManager.comboMenu, "autoRdeath"))) && buffTime > 0)
                        SpellManager.R.Cast(target);
                }
                foreach (var hero in ObjectManager.Get<AIHeroClient>().Where(hero => hero.IsValidTarget(SpellManager.R.Range)))
                {
                    if (Player.GetSpellDamage(target, SpellSlot.R) + MenuManager.getSliderItem(MenuManager.comboMenu, "adjustDmg") > hero.Health)
                    {
                        SpellManager.R.Cast(target);
                    }

                    else if (Player.GetSpellDamage(target, SpellSlot.R) + MenuManager.getSliderItem(MenuManager.comboMenu, "adjustDmg") < hero.Health)
                    {
                        foreach (var buff in hero.Buffs.Where(buff => buff.Name == "dariushemo"))
                        {
                            if (Player.GetSpellDamage(target, SpellSlot.R) * (1 + buff.Count / 5) + MenuManager.getSliderItem(MenuManager.comboMenu, "adjustDmg") > target.Health)
                            {
                                SpellManager.R.Cast(target);
                            }
                        }
                    }
                }
            }           
        }       
    }
}
