using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogDictionary
{
    public static Dictionary<EDialogName, string> dialogNpcName = new Dictionary<EDialogName, string>()
    {
        //TODO: Togliere mentore e mettere nome official.
        //TODO: Giusto mettere stringa statica in questo modo? o meglio fare un piccolo database-style?
        { EDialogName.Level0_Welcome,   "Mentore" },
        { EDialogName.Level0_Obstacle,  "Mentore" },
        { EDialogName.Level0_Jump,      "Mentore" },
        { EDialogName.Level0_Coin,      "Mentore" },
        { EDialogName.Level0_Attack,    "Mentore" },
        { EDialogName.Level0_EndLevel,  "Mentore" },
        { EDialogName.Level0_Vision_Start,  "Brann" },
        { EDialogName.Level0_Vision_End,    "Brann" },
    };

    public static Dictionary<EDialogName, string> dialogFile = new Dictionary<EDialogName, string>()
    {
        { EDialogName.Level0_Welcome,   "Level0_Welcome" },
        { EDialogName.Level0_Obstacle,  "Level0_Obstacle" },
        { EDialogName.Level0_Jump,      "Level0_Jump" },
        { EDialogName.Level0_Coin,      "Level0_Coin" },
        { EDialogName.Level0_Attack,    "Level0_Attack" },
        { EDialogName.Level0_EndLevel,  "Level0_EndLevel" },
        { EDialogName.Level0_Vision_Start,  "Level0_Vision_Start" },
        { EDialogName.Level0_Vision_End,    "Level0_Vision_End" },
    };
}
