using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace P1x3lc0w.LudumDare46.UI
{
    class EventLog : MonoBehaviour
    {
        const float MAX_SHOW_TIME = 5.0f;
        const float LERP_TIME = 1.0f;

#pragma warning disable CS0649
        public TextMeshProUGUI eventLogText;
        public Image background;

        public Color textColor;
        public Color backgroundColor;
#pragma warning restore CS0649

        private float _showTime;

        public void ShowEvent(string text)
        {
            eventLogText.text = text;

            eventLogText.color = textColor;
            background.color = backgroundColor;

            gameObject.SetActive(true);
            _showTime = 0.0f;
        }

        public void Update()
        {
            if (GameManager.Instance.GameRunning)
            {
                _showTime += Time.deltaTime;

                if(_showTime > MAX_SHOW_TIME)
                {
                    gameObject.SetActive(false);
                }
                else if(MAX_SHOW_TIME - _showTime < LERP_TIME)
                {
                    float lerpAmount = (MAX_SHOW_TIME - _showTime) / LERP_TIME;
                    eventLogText.color = Color.Lerp(Color.clear, textColor, lerpAmount);
                    background.color = Color.Lerp(Color.clear, backgroundColor, lerpAmount);
                }
            }
        }
    }
}
