using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace P1x3lc0w.LudumDare46
{
    class ShieldManager : MonoBehaviour
    {
#pragma warning disable CS0649
        public GameObject shieldPrefab;
        public LineRenderer indicatorCircle;
#pragma warning restore CS0649

        public bool InputActive { get; set; }

        private Shield _activeShield;

        public Shield ActiveShield
        {
            get => _activeShield;
            set
            {
                _activeShield = value;
                if(_activeShield != null)
                {
                    indicatorCircle.gameObject.SetActive(true);
                    Color color = Color.Lerp(_activeShield.ShieldColor, Color.black, 0.5f);
                    indicatorCircle.startColor = color;
                    indicatorCircle.endColor = color;
                    float scale = _activeShield.Height;
                    indicatorCircle.transform.localScale = new Vector3(scale, scale);
                }
                else
                {
                    indicatorCircle.gameObject.SetActive(false);
                }
            }
        }

        private List<Shield> _shields = new List<Shield>();

        public void AddShield()
        {
            GameObject shieldGO = Instantiate(shieldPrefab, transform);
            Shield shield = shieldGO.GetComponent<Shield>();

            shield.SetSize(_shields.Count + 1);
            shield.SetColor();

            if(_shields.Count == 0)
            {
                ActiveShield = shield;
            }

            _shields.Add(shield);
        }

        public void Update()
        {
            if (InputActive && _shields.Count > 0)
            {
                if (ActiveShield != null)
                {
                    ActiveShield.Move(Input.GetAxis("Horizontal"));
                }

                if (Input.GetButtonDown("Up"))
                {
                    ActiveShield = _shields[(_shields.IndexOf(ActiveShield) + 1) % _shields.Count];
                }
                else if (Input.GetButtonDown("Down"))
                {
                    ActiveShield = _shields[(_shields.IndexOf(ActiveShield) - 1 + _shields.Count) % _shields.Count];
                }
            }
        }
    }
}
