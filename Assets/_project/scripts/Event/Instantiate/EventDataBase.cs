using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EventDataBase", menuName = "data/EventData", order = 1)]
public class EventDataBase : ScriptableObject
{
    public int timer;
    [SerializeField] private List<EventData> events = new();

    public EventData GetData(int id)
    {
        if (events == null || events.Count==0)
        {
            return null;
        }

        if (id < 0 || id >= events.Count)
            id = Random.Range(0, events.Count);
        else
            id = Mathf.Clamp(id, 0, events.Count - 1);
        return events[id];
    }

    public int EventDataLenght()
    {
        return events.Count;
    }
}
