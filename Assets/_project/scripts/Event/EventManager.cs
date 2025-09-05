using GFM2025;
using NaughtyAttributes;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventDataBase _eventDataBase;
    private float _time;

    [SerializeField] private float _heightOffset = 1f;


    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _eventDataBase.timer - _eventDataBase.timer * _eventDataBase.scaleTimer * GameManager.Instance.TourCount)
        {
            _time = 0;
            EventCallRandom();
        }
    }

    private void EventCallRandom()
    {
        for (int i = 0; i < DataBaseManager.Instance.GetNumberEventData(); i++)
        {
            EventData item = DataBaseManager.Instance.GetEventData(i);
            if (item.nombreTourAvantApparition >= GameManager.Instance.TourCount)
            {
                if (Random.Range(0, 100) < item.pourcentage + item.scalePourcentage * item.pourcentage * GameManager.Instance.TourCount)
                {
                    int number = Random.Range(item.numberMin, item.numberMax);
                    for (int j = 0; j < number; j++)
                    {
                        Vector3 minSpawnPos = new Vector3(0f, PlayerBehaviour.Instance.EventPosition.position.y, PlayerBehaviour.Instance.EventPosition.position.z) - new Vector3(10f, 0f, 10f);
                        Vector3 maxSpawnPos = new Vector3(0f, PlayerBehaviour.Instance.EventPosition.position.y, PlayerBehaviour.Instance.EventPosition.position.z) + new Vector3(10f, 0f, 15f);

                        Vector3 randomPos = new Vector3(Random.Range(minSpawnPos.x, maxSpawnPos.x), MapBehaviour.Instance.GetWaterTransform().position.y + _heightOffset, Random.Range(minSpawnPos.z, maxSpawnPos.z));

                        Debug.DrawLine(minSpawnPos, maxSpawnPos, Color.red, 5f);

                        if (!MapBehaviour.Instance.PositionIsInEventZone(randomPos))
                        {
                            continue;
                        }

                        GameObject a = Instantiate(item.EventPrefab, randomPos, Quaternion.identity, MapBehaviour.Instance.GetWaterTransform());
                        a.GetComponent<EventParent>().lifeTime = item.lifeTime;
                    }
                }
            }
        }
    }
}
