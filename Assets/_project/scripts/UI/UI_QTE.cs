using UnityEngine;
using CREMOT.GameplayUtilities;
using TMPro;
using System.Linq;
using UnityEngine.UI;

namespace GFM2025
{
    public class UI_QTE : GenericSingleton<UI_QTE>
    {
        [SerializeField] private TextMeshProUGUI[] _textMesh;
        [SerializeField] private Sprite[] _spriteTouch;
        [SerializeField] private Image[] _zoneSprite;

        private GameObject _itemMousse;

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


        void VerifPressTouche(int touche)
        {
            if (touche == 1 && _inputListQTE[_index] == QTEData.TOUCHE.Gauche)
            {
                UpdateTextQTE();
                _index++;
                CheckQTEEnd();
            }
            else if (touche == 2 && _inputListQTE[_index] == QTEData.TOUCHE.Droite)
            {
                UpdateTextQTE();
                _index++;
                CheckQTEEnd();
            }
            else if (touche == 3 && _inputListQTE[_index] == QTEData.TOUCHE.Top)
            {
                UpdateTextQTE();
                _index++;
                CheckQTEEnd();
            }
            else if (touche == 4 && _inputListQTE[_index] == QTEData.TOUCHE.Bot)
            {
                UpdateTextQTE();
                _index++;
                CheckQTEEnd();
            }
        }

        void CheckQTEEnd()
        {
            if (_index >= _inputListQTE.Length)
            {
                _index = 0;
                EndQTE();
            }
        }

        public bool StartQTE(GameObject itemMousse)
        {
            _itemMousse = itemMousse;
            if (!gameObject.activeInHierarchy)
            {
                if (DataBaseManager.Instance.GetNumberQTEData() == 0)
                {
                    Debug.LogError($"UI_QTE: Aucun qteData trouvé");
                    return false;
                }
                    
                _inputListQTE = DataBaseManager.Instance.GetGTEData(UnityEngine.Random.Range(0, DataBaseManager.Instance.GetNumberQTEData()));

                if (_inputListQTE.Length == 0)
                {
                    Debug.LogError($"UI_QTE: Aucun input trouvé dans la qteData");
                    return false;
                }
                UpdateTextStartQTE();
                OpenUi();
                return true;
            }
            return false;
        }

        void EndQTE()
        {
            CloseUi();
            Destroy(_itemMousse);
        }
        public void OpenUi()
        {
            gameObject.SetActive(true);
        }

        public void CloseUi()
        {
            gameObject.SetActive(false);
        }

        void UpdateTextStartQTE()
        {
            for (int i = 0; i < _inputListQTE.Count(); i++)
            {
                switch (_inputListQTE[i]) {
                    case QTEData.TOUCHE.Gauche:
                        _zoneSprite[i].sprite = _spriteTouch[0];
                        break;
                    case QTEData.TOUCHE.Droite:
                        _zoneSprite[i].sprite = _spriteTouch[1];
                        break;
                    case QTEData.TOUCHE.Top:
                        _zoneSprite[i].sprite = _spriteTouch[2];
                        break;
                    case QTEData.TOUCHE.Bot:
                        _zoneSprite[i].sprite = _spriteTouch[3];
                        break;
                }
            }
        }

        private void UpdateTextQTE()
        {
            Debug.Log("change color");
            _textMesh[_index].color = Color.green;
        }
    }
}
