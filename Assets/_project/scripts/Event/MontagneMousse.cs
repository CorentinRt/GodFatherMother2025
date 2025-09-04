using System;
using UnityEngine;

namespace GFM2025
{
    public class MontagneMousse : EventParent
    {
        private QTEData.TOUCHE[] _inputListQTE;
        private int _index = 0;
        private bool istarted;


        private void Start()
        {
            _inputListQTE = DataBaseManager.Instance.GetGTEData(UnityEngine.Random.Range(0, DataBaseManager.Instance.GetQTEEventData()));

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
            if (istarted)
                VerifPressTouche(1);
        }
        private void OnPressInputTwo()
        {
            if (istarted)
                VerifPressTouche(2);
        }
        private void OnPressInputThree()
        {
            if (istarted)
                VerifPressTouche(3);
        }
        private void OnPressInputFour()
        {
            if (istarted)
                VerifPressTouche(4);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent<IPlayerBehaviour>(out IPlayerBehaviour player)) {
                Debug.Log("lance QTE");
                StartQTE();
                //InputActionReference
            }
        }


        bool VerifPressTouche(int touche) {
            if (touche == 1 && _inputListQTE[_index] == QTEData.TOUCHE.Gauche) {
                _index++;
                CheckQTEEnd();
                return true;
            } else if (touche == 2 && _inputListQTE[_index] == QTEData.TOUCHE.Droite) {
                _index++;
                CheckQTEEnd();
                return true;
            } else if (touche == 3 && _inputListQTE[_index] == QTEData.TOUCHE.Top) {
                _index++;
                CheckQTEEnd();
                return true;
            } else if (touche == 4 && _inputListQTE[_index] == QTEData.TOUCHE.Bot) {
                _index++;
                CheckQTEEnd();
                return true;
            } else
                return false;
        }

        void CheckQTEEnd()
        {
            if (_index >= _inputListQTE.Length) {
                EndQTE();
            }
        }

        void StartQTE() {
            istarted = true;
            UI_QTE.Instance.OpenUi();
        }

        void EndQTE() {
            istarted = false;
            UI_QTE.Instance.CloseUi();
        }
    }
 }
