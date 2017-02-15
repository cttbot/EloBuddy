using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System.Collections.Generic;
using System.Linq;

namespace Riven
{
    public static class Utility
    {
        //L#

        public static bool IsValid<T>(this GameObject obj) where T : GameObject
        {
            return obj is T && obj.IsValid;
        }

        public static List<AIHeroClient> GetEnemiesInRange(this Obj_AI_Base unit, float range)
        {
            return GetEnemiesInRange(unit.ServerPosition, range);
        }

        public static List<AIHeroClient> GetEnemiesInRange(this Vector3 point, float range)
        {
            return EntityManager.Heroes.Enemies.FindAll(x => point.Distance(x.ServerPosition, true) <= range * range);
        }

        public static bool IsValidTarget(this AttackableUnit unit, float range = float.MaxValue, bool checkTeam = true, Vector3 from = new Vector3())
        {
            if (unit == null || !unit.IsValid || !unit.IsVisible || unit.IsDead || !unit.IsTargetable || unit.IsInvulnerable)
            {
                return false;
            }

            if (checkTeam && unit.Team == EventManager.player.Team)
            {
                return false;
            }

            var baseObject = unit as Obj_AI_Base;
            if (baseObject != null && !baseObject.IsHPBarRendered)
            {
                return false;
            }

            if (unit.Name == "WardCorpse")
            {
                return false;
            }

            var @base = unit as Obj_AI_Base;

            return !(range < float.MaxValue)
                   || !(Vector2.DistanceSquared(
                       (@from.To2D().IsValid() ? @from : EventManager.player.ServerPosition).To2D(),
                       (@base != null ? @base.ServerPosition : unit.Position).To2D()) > range * range);
        }
        public static bool UnderTurret(this Obj_AI_Base unit)
        {
            return UnderTurret(unit.Position, true);
        }
        public static bool UnderTurret(this Obj_AI_Base unit, bool enemyTurretsOnly)
        {
            return UnderTurret(unit.Position, enemyTurretsOnly);
        }

        public static bool UnderTurret(this Vector3 position, bool enemyTurretsOnly)
        {
            return
                ObjectManager.Get<Obj_AI_Turret>().Any(turret => turret.IsValidTarget(950, enemyTurretsOnly, position));
        }
    }
}
