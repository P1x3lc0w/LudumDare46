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
#pragma warning restore CS0649

        public Shield ActiveShield { get; private set; }

        private List<Shield> _shields;

        public void Start()
        {
            _shields = new List<Shield>();
        }

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
            if(ActiveShield != null)
            {
                ActiveShield.Move(Input.GetAxis("Horizontal"));
            }
        }
    }
}
