using CREMOT.GameplayUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace GFM2025
{
    public class UI_Slime : GenericSingleton<UI_Slime>
    {
        [SerializeField] private SlimeDataBase _data;
        private float _time;

        [SerializeField] private Image[] _images;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _time = 0;
        }

        private void Update()
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            _time += Time.deltaTime;
            if (_time <= _data.timeFadeIn)
            {
                for (int i = 0; i < _images.Length; i++)
                {
                    _images[i].color = new Color(_images[i].color.r, _images[i].color.g, _images[i].color.b, _time / _data.timeFadeIn);
                }
            }
            if (_time >= _data.lifeTime)
            {
                gameObject.SetActive(false);
            }
        }

        public void OpendUiSlime()
        {
            gameObject.SetActive(true);
        }
    }
}
