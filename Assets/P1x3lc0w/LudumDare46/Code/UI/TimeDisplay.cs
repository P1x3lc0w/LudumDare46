using TMPro;
using UnityEngine;

namespace P1x3lc0w.LudumDare46.UI
{
    internal class TimeDisplay : MonoBehaviour
    {
#pragma warning disable CS0649
        public TextMeshProUGUI timeText;
        public GameManager gameManager;
#pragma warning restore CS0649

        public void Update()
        {
            if (GameManager.GameRunning)
                timeText.text = $"{(int)(gameManager.GameTime / 60)}:{(gameManager.GameTime % 60.0f).ToString("00")}:{((gameManager.GameTime % 1.0f) * 99.0f).ToString("00")}";
        }
    }
}