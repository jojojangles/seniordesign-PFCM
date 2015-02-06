using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFCM
{
    class Player
    {
        List<Tuple<Archetype, int>> myClasses; //List of <class object,level>
        List<Tuple<PSkill, int>> mySkills; //List of <skill object,total>
        PRace myRace; //Player race object
        AbilityScores scores; //Player ability score tracker
        Dictionary<PBody,PGear> myEquipped; //List of <body part object,gear object>
        List<Tuple<PGear, int>> myBackpack; //List of <gear object,qty>
        int AC, armor, shield, deflection, size, natarmor, miscAC, touchAC, flatfootAC, baseAC;
        int initiative, initMisc;
        int totalHP, damresist, wounds, nonlethal;
        List<PGear> myWeapons; //specifically, what attacks can this character make
        List<PSpell> mySpellbook; //strictly spell-caster spells
        List<PAbilities> myAbilities; //all spell-likes or extra combat-affecting or skill boosting abilities
        List<PFeat> myFeats; //all feats, including activatables (which should appear in abilities as well)

        public Player()
        {
            myRace = new PRace();
            baseAC = 10;
            scores = new AbilityScores();
            myClasses = new List<Tuple<Archetype,int>>();
            mySkills = new List<Tuple<PSkill, int>>();
            myEquipped = new Dictionary<PBody, PGear>();
            myBackpack = new List<Tuple<PGear, int>>();
            myWeapons = new List<PGear>();
            mySpellbook = new List<PSpell>();
            myAbilities = new List<PAbilities>();
            myFeats = new List<PFeat>();
        }
    }
}
