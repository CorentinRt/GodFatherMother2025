using System;
using UnityEngine;

[Serializable]
public class QTEData
{
    [Serializable]
    public enum TOUCHE
    {
        Gauche,
        Droite,
        Top,
        Bot
    }

    [SerializeField] public TOUCHE[] InputListQTE;
}




