using UnityEngine;
using UnityEngine.UI;

namespace GFM2025
{
    public class UI_WaterLevel : MonoBehaviour
    {
        [Header("Slider")]
        [SerializeField] private Slider _sliderWater;


        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (MapBehaviour.Exist)
            {
                MapBehaviour.Instance.onUpdateWaterLevelPercent += UpdateSliderWater;
            }
            else
            {
                Debug.LogError("Error : No MapBehaviour singleton found in scene ! Init of it's UIs won't work properly !", this);
            }

            _sliderWater.value = 0;
        }

        private void OnDestroy()
        {
            if (MapBehaviour.Exist)
            {
                MapBehaviour.Instance.onUpdateWaterLevelPercent -= UpdateSliderWater;
            }

        }

        private void UpdateSliderWater(float percent)
        {
            _sliderWater.value = percent;
        }
    }
}
