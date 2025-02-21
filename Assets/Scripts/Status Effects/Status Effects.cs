using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    // Correlates with monster ID
    public static List<bool> causesParalysis = new List<bool>();
    public static List<bool> hasParalysis = new List<bool>();
    public static List<bool> immuneToParalysis = new List<bool>();
    public static List<bool> burnList = new List<bool>();

    public static void SetUpNewMonsterStatusEffects(bool paralysis, bool immuneParalysis)
    {
        causesParalysis.Add(paralysis);
        hasParalysis.Add(false);
        immuneToParalysis.Add(immuneParalysis);
    }
}