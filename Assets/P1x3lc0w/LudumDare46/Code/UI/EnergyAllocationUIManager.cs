using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace P1x3lc0w.LudumDare46.UI
{
    class EnergyAllocationUIManager : MonoBehaviour
    {
#pragma warning disable CS0649
        public GameObject planetEnergyAllocationButtonPrefab;
        public Transform planetEnergyAllocationButtonContainer;
#pragma warning restore CS0646

        public void Open(IEnumerable<Planet> planets)
        {
            GameManager.Instance.GameRunning = false;

            foreach (Transform t in planetEnergyAllocationButtonContainer)
                Destroy(t.gameObject);

            foreach(Planet p in planets)
            {
                Instantiate(planetEnergyAllocationButtonPrefab, planetEnergyAllocationButtonContainer).GetComponent<PlanetEnergyAllocationButton>().Init(p, this);
            }

            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameRunning = true;
        }
    }
}
