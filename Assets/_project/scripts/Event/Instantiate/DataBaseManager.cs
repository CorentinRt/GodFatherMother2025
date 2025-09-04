using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    private static DataBaseManager _instance;
    public static DataBaseManager Instance => _instance;

    [SerializeField] private EventDataBase _eventDatabase;
    [SerializeField] private MystereDataBase _mystereDatabase;

    private void Awake() {
        if (_instance == null)
            _instance = this;
        else {
            Destroy(gameObject);
            return; // quitte pour éviter d’exécuter la suite dans un objet détruit
        }

        DontDestroyOnLoad(gameObject);

        // Optional: Log confirmation
        if (_eventDatabase == null)
            Debug.LogWarning("DataBaseManager : eventDatabase n'est pas assigné dans l'inspecteur !");
        if (_mystereDatabase == null)
            Debug.LogWarning("DataBaseManager : MystereDatabase n'est pas assigné dans l'inspecteur !");
    }

    public EventData GetEventData(int id) {
        if (_eventDatabase == null) {
            Debug.LogError("DataBaseManager: eventDatabase n'est pas assigné dans l'inspecteur !");
            return null;
        }
        EventData data = _eventDatabase.GetData(id);
        if (data == null) {
            Debug.LogError($"DataBaseManager: Aucun eventData trouvé pour l'id {id}.");
        }
        return data;
    }

    public int GetNumberEventData() {
        if (_eventDatabase == null) {
            Debug.LogError("DataBaseManager: eventDatabase n'est pas assigné dans l'inspecteur !");
            return 0;
        }
        return _eventDatabase.EventDataLenght();
    }
    public MystereData GetMystereData(int id) {
        if (_mystereDatabase == null) {
            Debug.LogError("DataBaseManager: MystereDatabase n'est pas assigné dans l'inspecteur !");
            return null;
        }
        MystereData data = _mystereDatabase.GetData(id);
        if (data == null) {
            Debug.LogError($"DataBaseManager: Aucun MystereData trouvé pour l'id {id}.");
        }
        return data;
    }
}