using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace P1x3lc0w.LudumDare46.UI
{
    class PlanetEnergyAllocationButton : MonoBehaviour
    {
#pragma warning disable CS0649
        public Image planetImage;
        public Transform energyIndicatorContainer;
        public Color energyIndicatorOffColor;
        public Button button;
        public Image backgorundImage;
#pragma warning restore CS0649

        private EnergyAllocationUIManager _manager;
        private Planet _planet;

        public void Init(Planet p, EnergyAllocationUIManager manager)
        {
            _planet = p;
            _manager = manager;

            bool canHaveMoreShields = p.shieldManager.Shields.Count < GameManager.MAX_SHIELD_COUNT;

            button.enabled = canHaveMoreShields;
            backgorundImage.enabled = canHaveMoreShields;

            planetImage.color = p.PlanetColor;

            for(int i = 0; i < energyIndicatorContainer.childCount; i++)
            {
                energyIndicatorContainer.GetChild(i).GetComponent<Image>().color = i < p.shieldManager.Shields.Count ?
                                                                                                                p.shieldManager.Shields[i].ShieldColor :
                                                                                                                energyIndicatorOffColor;
            }
        }

        public void OnClick()
        {
            _planet.shieldManager.AddShield();
            _manager.Close();
        }
    }
}
