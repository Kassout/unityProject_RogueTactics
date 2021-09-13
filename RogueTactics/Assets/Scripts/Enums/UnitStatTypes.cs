public enum UnitStatTypes
{
    LVL, // Level;
    EXP, // Experience;
    HP, // Health Points; If it reaches 0 your unit dies.
    MHP, /// Max Hit Points; Maximum health points a unit has at any moment.
    MP, // Magic Points; A unit resource used to launch spells and special skills.
    MMP, // Max Magic Points; Maximum magic points a unit has at any moment.
    STR, // Strength; Damage you deal to opponents with physical weapons and skills but is dependant on defence.
    MAG, // Magic; Damage you deal to opponents with magic weapons and skills but is dependant on resistance.
    SKL, // Skill; Affects hit rate and critical rate.
    SPD, // Speed; Affects evasion rate and attack speed (AS), with 4+ more speed that the opponent you can double attack them.
    LUC, // Luck; Affects hit rate, evasion rate & critical evasion rate.
    DEF, // Defence; Affects the damage taken from strength-based attacks.
    RES, // Resistance; Affects the damage taken from magic-based attacks.
    EVD, // Evade; Affect evasion rate & critical evasion rate.
    TEN, // Tenacity; Affect status effect hit rate.
    MOV, // Movement; Affects how far your unit can move.
    Count
}
