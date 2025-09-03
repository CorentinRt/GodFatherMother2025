using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EventDataBase eventDataBase;
    private float time;

    private void Update() {
        time += Time.deltaTime;
        if (time >= eventDataBase.timer) {
            time = 0;
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
                //GameObject a = Instantiate(DataBaseManager.Instance.GetEventData(i).EventPrefab);
                print(DataBaseManager.Instance.GetEventData(i).label);
                break;
            }
        }
    }
}
