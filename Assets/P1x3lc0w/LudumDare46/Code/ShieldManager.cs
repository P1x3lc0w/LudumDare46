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
                    indicatorCircle.startColor = Color.red;
                    indicatorCircle.transform.localScale = new Vector3(_activeShield.transform.position.y, _activeShield.transform.position.y);
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
            if (_shields.Count > 0)
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
