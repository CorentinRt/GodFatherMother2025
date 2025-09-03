using UnityEngine;
using UnityEngine.SceneManagement;

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
        print("eventStart");
        int numbers = 0;
        for (int i = 0; i < DataBaseManager.Instance.GetNumberEventData(); i++) {
            numbers += DataBaseManager.Instance.GetEventData(i).pourcentage;
        }
        int number = Random.Range(0,numbers);
        print(number);
        for (int i = 0; i < DataBaseManager.Instance.GetNumberEventData(); i++) {
            int pourcentage = 0;
            for (int j = 0; j < i+1; j++) {
                pourcentage += DataBaseManager.Instance.GetEventData(j).pourcentage;
            }
            print("pourcentage : " + pourcentage);
            if (number <= pourcentage) {
                for (int j = 0; j < DataBaseManager.Instance.GetEventData(i).number; j++) {
                    Instantiate(DataBaseManager.Instance.GetEventData(0).EventPrefab,new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), new Quaternion(0,0,0,0));
                }
                print(DataBaseManager.Instance.GetEventData(i).label);
                break;
            }
        }
    }
}
