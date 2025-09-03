using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MystereDataBase", menuName = "data/MystereData", order = 1)]
public class MystereDataBase : ScriptableObject
{
    [SerializeField] private List<MystereData> datas = new();

    public MystereData GetData(int id) {
        if (id < 0 || id >= datas.Count)
            id = Random.Range(0, datas.Count);
        else
            id = Mathf.Clamp(id, 0, datas.Count - 1);
        return datas[id];
    }

    public int MystereLenght() {
        return datas.Count;
    }
}

