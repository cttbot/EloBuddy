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
    class SpellsManager
    {
        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Targeted E;
        public static Spell.Active R;


        static SpellsManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Linear, 250, 1500, 100);
            W = new Spell.Targeted(SpellSlot.W, 675);
            E = new Spell.Targeted(SpellSlot.E, 800);
            R = new Spell.Active(SpellSlot.R, 1100);
        }

        public static bool MantraActive
        {
            get { return ObjectManager.Player.HasBuff("KarmaMantra"); }
        }
    }
}
