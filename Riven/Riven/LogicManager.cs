using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riven
{
    class LogicManager : MenuBase
    {
        private static bool InWRange(GameObject target)
        {
            if(Player.HasBuff("RivenFengShuiEngine") && target != null)
            {
                return 330 >= player.Distance(target.Position);
            }           
            return 265 >= player.Distance(target.Position);
        }

        public static void Dodge_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsEnemy && sender.Type == player.Type && getCheckBoxItem(miscMenu, "dodge"))
            {
                var epos = player.ServerPosition + (player.ServerPosition - sender.ServerPosition).Normalized() * 300;

                var a = EntityManager.Heroes.Enemies.Where(x => x.IsValidTarget(player.AttackRange + 360));

                var targets = a as AIHeroClient[] ?? a.ToArray();

                foreach (var target in targets)
                {
                    if (player.Distance(sender.ServerPosition) <= args.SData.CastRange)
                    {
                        if (target.HasBuff("IreliaEquilibriumStrike"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.W.IsReady() && InWRange(sender)) SpellManager.W.Cast();
                                else
                                if (SpellManager.E.IsReady()) SpellManager.E.Cast(epos);
                            }
                        }
                        if (target.HasBuff("TalonCutthroat"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.W.IsReady()) SpellManager.W.Cast();
                            }
                        }
                        if (target.HasBuff("RenektonPreExecute"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.W.IsReady()) SpellManager.W.Cast();
                            }
                        }
                        if (target.HasBuff("GarenRPreCast"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady()) SpellManager.E.Cast(epos);
                            }
                        }
                        if (target.HasBuff("GarenQAttack"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                Chat.Print("GarenQAttack");
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("XenZhaoThrust3"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                Chat.Print("xingzao cast");
                                if (SpellManager.W.IsReady()) SpellManager.W.Cast();
                            }
                        }
                        if (target.HasBuff("RengarQ"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("RengarPassiveBuffDash"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("RengarPassiveBuffDashAADummy"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("TwitchEParticle"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("FizzPiercingStrike"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("HungeringStrike"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("YasuoDash"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("KatarinaRTrigger"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.W.IsReady() && InWRange(sender)) SpellManager.W.Cast();
                                else if (SpellManager.E.IsReady()) SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            }
                        }
                        if (target.HasBuff("YasuoDash"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("KatarinaE"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.W.IsReady()) SpellManager.W.Cast();
                            }
                        }
                        if (target.HasBuff("MonkeyKingSpinToWin"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady()) SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                else if (SpellManager.W.IsReady()) SpellManager.W.Cast();
                            }
                        }
                        if (target.HasBuff("MonkeyKingQAttack"))
                        {
                            if (args.Target.NetworkId == player.NetworkId)
                            {
                                if (SpellManager.E.IsReady())
                                    SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                                return;
                            }
                        }
                        if (target.HasBuff("DariusR"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("GarenQ"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("GarenR"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                        }

                        if (target.HasBuff("IreliaE"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("LeeSinR"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("OlafE"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("RenektonW"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("RenektonPreExecute"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("RengarQ"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("VeigarR"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("VolibearW"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("XenZhaoThrust3"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                            return;
                        }

                        if (target.HasBuff("attack") && args.Target.IsMe &&
                            sender.Buffs.Any(
                                buff =>
                                    buff.Name == "BlueCardAttack" || buff.Name == "GoldCardAttack" ||
                                    buff.Name == "RedCardAttack"))
                        {
                            SpellManager.E.Cast(Me.Position.Extend(Game.CursorPos, SpellManager.E.Range).To3DWorld());
                        }
                    }
                }
            }
        }                   
    }
}
