using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "QTEDataBase", menuName = "data/QTEData", order = 1)]
public class QTEDataBase : ScriptableObject
{
    [SerializeField] private List<QTEData> datas = new();

    public QTEData.TOUCHE[] GetData(int id)
    {

        if (datas == null || datas.Count == 0)
        {
            return null;
        }
        if (id < 0 || id >= datas.Count)
            id = Random.Range(0, datas.Count);
        else
            id = Mathf.Clamp(id, 0, datas.Count - 1);
        return datas[id].InputListQTE;
    }

    public int QTEDataLenght()
    {
        return datas.Count;
    }
}
