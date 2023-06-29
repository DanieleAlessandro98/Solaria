using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDictionary
{
    public static Dictionary<int, ELevelName> levelNumber = new Dictionary<int, ELevelName>()
    {
        { 0,    ELevelName.Level_Castle },
        { 1,    ELevelName.Level_Plains },
        { 2,    ELevelName.Level_Forest },
        { 3,    ELevelName.Level_Cave },
        { 4,    ELevelName.Level_Sword },
    };

    public static Dictionary<ELevelName, string> levelInfoFile = new Dictionary<ELevelName, string>()
    {
        { ELevelName.Level_Castle, "CastleLevel_Information" },
        { ELevelName.Level_Plains, "PlainsLevel_Information" },
        { ELevelName.Level_Forest, "ForestLevel_Information" },
        { ELevelName.Level_Cave,   "CaveLevel_Information" },
        { ELevelName.Level_Sword,  "SwordLevel_Information" },
    };
}
