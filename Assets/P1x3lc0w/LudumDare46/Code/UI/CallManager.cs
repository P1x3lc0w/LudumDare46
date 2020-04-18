using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace P1x3lc0w.LudumDare46.UI
{
    class CallManager : MonoBehaviour
    {

        private Action _callback;

        public void DoCall(Action callback)
        {
            GameManager.GameRunning = false;
            _callback = callback;
            gameObject.SetActive(true);
        }

        public void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                gameObject.SetActive(false);
                _callback.Invoke();
            }
        }
    }
}
