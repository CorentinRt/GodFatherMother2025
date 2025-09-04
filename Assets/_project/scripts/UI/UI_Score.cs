using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace GFM2025
{
    public class UI_Score : MonoBehaviour
    {
        [Header("UI Score")]
        [SerializeField] private TextMeshProUGUI _scoreText;

        [Header("Tour count")]
        [SerializeField] private TextMeshProUGUI _tourText;

        public UnityEvent onUpdateScoreTextUnity;

        public UnityEvent onUpdateTourTextUnity;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (ScoreManager.Exist)
            {
                ScoreManager.Instance.onScorePoints += UpdateScoreText;
            }
            else
            {
                Debug.LogError("Error : No ScoreManager singleton found in scene ! Init of its UIs won't work properly !", this);
            }

            GameManager.Instance.onChangeTour += UpdateTourText;

        }

        private void OnDestroy()
        {
            if (ScoreManager.Exist)
            {
                ScoreManager.Instance.onScorePoints -= UpdateScoreText;
            }

            GameManager.Instance.onChangeTour -= UpdateTourText;
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();

            onUpdateScoreTextUnity?.Invoke();
        }

        private void UpdateTourText(int tour)
        {
            Debug.Log("Update tour text", this);

            _tourText.text = tour.ToString();

            onUpdateTourTextUnity?.Invoke();
        }
    }
}
