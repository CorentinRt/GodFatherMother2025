using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QTEDataBase", menuName = "data/QTEData", order = 1)]
public class QTEDataBase : ScriptableObject
{
    [SerializeField] private List<QTEData> qteListe = new();

    public QTEData.TOUCHE[] GetData(int id)
    {

        if (qteListe == null || qteListe.Count == 0)
        {
            return null;
        }
        if (id < 0 || id >= qteListe.Count)
            id = Random.Range(0, qteListe.Count);
        else
            id = Mathf.Clamp(id, 0, qteListe.Count - 1);
        return qteListe[id].InputListQTE;
    }

    public int QTEDataLenght()
    {
        return qteListe.Count;
    }
}
