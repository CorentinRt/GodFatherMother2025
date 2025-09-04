using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace GFM2025
{
    public class UI_Score : MonoBehaviour
    {
        [Header("UI Score")]
        [SerializeField] private TextMeshProUGUI _scoreText;

        public UnityEvent onUpdateScoreTextUnity;

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
        }

        private void OnDestroy()
        {
            if (ScoreManager.Exist)
            {
                ScoreManager.Instance.onScorePoints -= UpdateScoreText;
            }
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();

            onUpdateScoreTextUnity?.Invoke();
        }
    }
}
