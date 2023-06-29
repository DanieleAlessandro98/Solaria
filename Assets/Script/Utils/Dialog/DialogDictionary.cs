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
        { EDialogName.Level0_Jump,      "Mentore" },
        { EDialogName.Level0_Attack,    "Mentore" },
    };

    public static Dictionary<EDialogName, string> dialogFile = new Dictionary<EDialogName, string>()
    {
        { EDialogName.Level0_Welcome,   "Level0_Welcome" },
        { EDialogName.Level0_Jump,      "Level0_Jump" },
        { EDialogName.Level0_Attack,    "Level0_Attack" },
    };
}