using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace P1x3lc0w.LudumDare46.UI
{
    class PlanetIndicator : MonoBehaviour
    {
#pragma warning disable CS0649
        public Image backgroundSprite;
        public Color backgroundColor;
        public Image planetSprite;
#pragma warning restore CS0649

        public void SetPlanet(Planet p)
        {
            planetSprite.color = p.PlanetColor;
        }

        public void SetHighlighted(bool value)
        {
            backgroundSprite.color = value ? backgroundColor : Color.clear;
        }

    }
}
