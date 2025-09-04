using GFM2025;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventDataBase _eventDataBase;
    private float _time;

    private void Update() {
        _time += Time.deltaTime;
        if (_time >= _eventDataBase.timer) {
            _time = 0;
            EventCallRandom();
        }
    }

    private void EventCallRandom() {
        int numbers = 0;
        for (int i = 0; i < DataBaseManager.Instance.GetNumberEventData(); i++)
        {
            numbers += DataBaseManager.Instance.GetEventData(i).pourcentage;
        }
        int number = Random.Range(0,numbers);

        for (int i = 0; i < DataBaseManager.Instance.GetNumberEventData(); i++)
        {
            int pourcentage = 0;
            for (int j = 0; j < i+1; j++)
            {
                pourcentage += DataBaseManager.Instance.GetEventData(j).pourcentage;
            }
            if (number <= pourcentage)
            {
                EventData item = DataBaseManager.Instance.GetEventData(i);
                for (int j = 0; j < item.number; j++)
                {
                    GameObject a = Instantiate(item.EventPrefab,new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), new Quaternion(0,0,0,0));
                    a.GetComponent<EventParent>().lifeTime = item.lifeTime;
                }

                Debug.Log($"Spawn event : {item.label}", this);
                break;
            }
        }
    }
}
