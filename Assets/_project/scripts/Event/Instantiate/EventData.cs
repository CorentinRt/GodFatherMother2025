using System;
using UnityEngine;

[Serializable]
public class EventData
{
    public string label;
    public GameObject EventPrefab;
    public float pourcentage;
    public float scalePourcentage;
    public int nombreTourAvantApparition;
    public int numberMin;
    public int numberMax;
    public float lifeTime;
}
