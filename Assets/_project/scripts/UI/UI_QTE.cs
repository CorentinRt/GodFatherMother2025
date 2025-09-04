using UnityEngine;
using CREMOT.GameplayUtilities;
using TMPro;
using System.Linq;

namespace GFM2025
{
    public class UI_QTE : GenericSingleton<UI_QTE>
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        private QTEData.TOUCHE[] _inputListQTE;
        private int _index = 0;

        private void Start()
        {
            CloseUi();

            PlayerBehaviour.Instance.onPressQTEOne += OnPressInputOne;
            PlayerBehaviour.Instance.onPressQTETwo += OnPressInputTwo;
            PlayerBehaviour.Instance.onPressQTEThree += OnPressInputThree;
            PlayerBehaviour.Instance.onPressQTEFour += OnPressInputFour;
        }
        private void Destroy()
        {
            PlayerBehaviour.Instance.onPressQTEOne -= OnPressInputOne;
            PlayerBehaviour.Instance.onPressQTETwo -= OnPressInputTwo;
            PlayerBehaviour.Instance.onPressQTEThree -= OnPressInputThree;
            PlayerBehaviour.Instance.onPressQTEFour -= OnPressInputFour;
        }

        private void OnPressInputOne()
        {
            if (gameObject.activeInHierarchy)
            {
                VerifPressTouche(1);
            }
        }
        private void OnPressInputTwo()
        {
            if (gameObject.activeInHierarchy)
            {
                VerifPressTouche(2);
            }
                
        }
        private void OnPressInputThree()
        {
            if (gameObject.activeInHierarchy)
            {
                VerifPressTouche(3);
            }
        }
        private void OnPressInputFour()
        {
            if (gameObject.activeInHierarchy)
            {
                VerifPressTouche(4);
            }
        }


        bool VerifPressTouche(int touche)
        {
            if (touche == 1 && _inputListQTE[_index] == QTEData.TOUCHE.Gauche)
            {
                _index++;
                CheckQTEEnd();
                return true;
            }
            else if (touche == 2 && _inputListQTE[_index] == QTEData.TOUCHE.Droite)
            {
                _index++;
                CheckQTEEnd();
                return true;
            }
            else if (touche == 3 && _inputListQTE[_index] == QTEData.TOUCHE.Top)
            {
                _index++;
                CheckQTEEnd();
                return true;
            }
            else if (touche == 4 && _inputListQTE[_index] == QTEData.TOUCHE.Bot)
            {
                _index++;
                CheckQTEEnd();
                return true;
            }
            else
                return false;
        }

        void CheckQTEEnd()
        {
            if (_index >= _inputListQTE.Length)
            {
                _index = 0;
                EndQTE();
            }
        }

        public void StartQTE()
        {
            if (!gameObject.activeInHierarchy)
            {
                _inputListQTE = DataBaseManager.Instance.GetGTEData(UnityEngine.Random.Range(0, DataBaseManager.Instance.GetQTEEventData()));
                UpdateText();
                OpenUi();
            }
        }

        void EndQTE()
        {
            CloseUi();
        }
        public void OpenUi()
        {
            gameObject.SetActive(true);
        }

        public void CloseUi()
        {
            gameObject.SetActive(false);
        }

        void UpdateText()
        {
            string text = "";
            for (int i = 0; i < _inputListQTE.Count(); i++)
            {
                switch (_inputListQTE[i]) {
                    case QTEData.TOUCHE.Gauche:
                        text += "C ";
                        break;
                    case QTEData.TOUCHE.Droite:
                        text += "B ";
                        break;
                    case QTEData.TOUCHE.Top:
                        text += "F ";
                        break;
                    case QTEData.TOUCHE.Bot:
                        text += "V ";
                        break;
                }
                textMeshProUGUI.text = text;
            }
        }
    }
}
