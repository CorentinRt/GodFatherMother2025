using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QTEDataBase", menuName = "data/QTEData", order = 1)]
public class QTEDataBase : ScriptableObject
{
    [SerializeField] private List<QTEData> datas = new();

    public QTEData.TOUCHE[] GetData(int id)
    {
        id = Mathf.Clamp(id, 0, datas.Count - 1);
        return datas[id].InputListQTE;
    }

    public int QTEDataLenght()
    {
        return datas.Count;
    }
}
