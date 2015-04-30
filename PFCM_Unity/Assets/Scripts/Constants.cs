using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PFCM
{
	//Bonus/aggregatable
	public enum BONUS_TYPES : byte { BASE=0, ALCHEMY, ARMOR, CIRCUM, COMP, DEFLECT, DODGE, ENHANCE, 
                                        INHERENT, INSIGHT, LUCK, MORALE, NATARM, PROF, RACE, 
                                        RESIST, SACRED, SHIELD, SIZE, TRAIT, UNTYPED };
	public enum ABILITY_SCORES : byte { STR = 0, DEX, CON, INT, WIS, CHA };
	public enum SKILLS : byte { ACROBATICS=0, APPRAISE, BLUFF, CLIMB, CRAFT1, CRAFT2, CRAFT3, DIPLOMACY,
		DISABLE_DEVICE, DISGUISE, ESCAPE_ARTIST, FLY, HANDLE_ANIMAL,
		HEAL, INTIMIDATE, KN_ARCANA, KN_DUNGEONEERING, KN_ENGINEERING,
		KN_GEOGRAPHY, KN_HISTORY, KN_LOCAL, KN_NATURE, KN_NOBILITY,
		KN_PLANES, KN_RELIGION, LINGUISTICS, PERCEPTION, PERFORM1,
		PERFORM2, PROFESSION1, PROFESSION2, RIDE, SENSE_MOTIVE,
		SLEIGHT_OF_HAND, SPELLCRAFT, STEALTH, SURVIVAL, SWIM,
		USE_MAGIC_DEVICE};
	
	public enum ALIGNMENT : byte { LAWFUL_GOOD = 0, NEUTRAL_GOOD, CHAOTIC_GOOD,
		LAWFUL_NEUTRAL, NEUTRAL_NEUTRAL, CHAOTIC_NEUTRAL,
		LAWFUL_EVIL, NEUTRAL_EVIL, CHAOTIC_EVIL};
	public enum CONSUMABLES : byte { ARCANE_POOL=0, QI};
	public enum CASTER_TYPE : byte { PREPARED_ARCANE=0, SPONTANEOUS_ARCANE, PREPARED_DIVINE,
                                        SPONTANEOUS_DIVINE, NONE};
    public enum SPELL_PER_DAY : byte { FOUR_LEVEL=0, SIX_LEVEL, NINE_LEVEL, NONE};
    public enum CLASSES : byte { BARBARIAN, BARD, CLERIC, DRUID, FIGHTER, MONK, PALADIN, RANGER, ROGUE,
                                        SORCERER, WIZARD, ALCHEMIST, CAVALIER, GUNSLINGER, INQUISITOR,
                                        MAGUS, ORACLE, SUMMONER, WITCH, ANTIPALADIN, NINJA, SAMURAI,
                                        ARCANIST, BLOODRAGER, BRAWLER, HUNTER, INVESTIGATOR, SHAMAN,
                                        SKALD, SLAYER, SWASHBUCKLER, WARPRIEST, NONE};
	public enum RACES : byte { HUMAN = 0, DWARF, ELF, GNOME, HALF_ELF, HALF_ORC, HALFLING, CUSTOM};
	public enum ARMOR : byte {CLOTHING = 0, LEATHER, BREASTPLATE, HALFPLATE, FULLPLATE};
	public enum EQUIP : byte {ARMOR = 0, SHIELD, BELT, BODY, CHEST, EYES, FEET, HANDS, HEAD, HEADBAND,
		NECK, LRING, RRING, SHOULDERS, WRIST};
}
