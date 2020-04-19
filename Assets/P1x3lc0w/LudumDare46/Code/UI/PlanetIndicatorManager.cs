using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace P1x3lc0w.LudumDare46.UI
{
    class PlanetIndicatorManager : MonoBehaviour
    {
#pragma warning disable CS0649
        public GameObject indicatorPrefab;
        public Transform indicatorContainer;
#pragma warning restore CS0649

        private Dictionary<Planet, PlanetIndicator> _indicators;
        private PlanetIndicator _highlighted;
        public void Reset()
        {
            foreach (Transform t in indicatorContainer) Destroy(t.gameObject);

            _indicators = new Dictionary<Planet, PlanetIndicator>();
            _highlighted = null;
        }

        public void AddPlanet(Planet p)
        {
            GameObject indicatorGO = Instantiate(indicatorPrefab, indicatorContainer);
            PlanetIndicator planetIndicator = indicatorGO.GetComponent<PlanetIndicator>();

            planetIndicator.SetPlanet(p);

            _indicators.Add(p, planetIndicator);
        }

        public void HighlightPlanet(Planet p)
        {
            if (_highlighted)
                _highlighted.SetHighlighted(false);

            _highlighted = _indicators[p];

            _highlighted.SetHighlighted(true);
        }
    }
}
