using GFM2025;
using NaughtyAttributes;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventDataBase _eventDataBase;
    private float _time;
    private int tour;

    [SerializeField] private float _heightOffset = 1f;

    private void Start()
    {
        tour = 0;
    }

    private void Update() {
        _time += Time.deltaTime;
        if (_time >= _eventDataBase.timer- _eventDataBase.timer*_eventDataBase.scaleTimer*tour) {
            _time = 0;
            EventCallRandom();
        }
    }


    private void EventCallRandom()
    {
        for (int i = 0; i < DataBaseManager.Instance.GetNumberEventData(); i++)
        {
            EventData item = DataBaseManager.Instance.GetEventData(i);
            if (Random.Range(0, 100) < item.pourcentage+item.scalePourcentage*item.pourcentage*tour)
            {
                for (int j = 0; j < item.number; j++)
                {
                    GameObject a = Instantiate(item.EventPrefab, new Vector3(Random.Range(-5, 5), _heightOffset, Random.Range(-5, 5)), new Quaternion(0, 0, 0, 0), MapBehaviour.Instance.GetWaterTransform());
                    a.GetComponent<EventParent>().lifeTime = item.lifeTime;
                }
            }
        }
    }

    [Button]
    public void incremantDebugTour()
    {
        tour++;
    }
}