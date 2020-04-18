using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace P1x3lc0w.LudumDare46.UI
{
    class DialougeManager : MonoBehaviour
    {
#pragma warning disable CS0649
        public TextMeshProUGUI dialougeText;
#pragma warning restore CS0649

        private string[] _dialouge;
        private int _dialougeIndex;
        private Action _callback;

        public void StartDialouge(string[] dialouge, Action callback)
        {
            _dialouge = dialouge;
            _callback = callback;
            _dialougeIndex = 0;
            dialougeText.text = _dialouge[0];
            GameManager.GameRunning = false;
            gameObject.SetActive(true);
        }

        public void AdvanceDialouge()
        {
            _dialougeIndex++;

            if(_dialougeIndex >= _dialouge.Length)
            {
                gameObject.SetActive(false);
                _callback?.Invoke();
            }
            else
            {
                dialougeText.text = _dialouge[_dialougeIndex];
            }
        }
        
        public void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                AdvanceDialouge();
            }
        }

    }
}
