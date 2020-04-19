using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace P1x3lc0w.LudumDare46
{
    class Planet : MonoBehaviour
    {
#pragma warning disable CS0649
        public ShieldManager shieldManager;
        public GameObject meteorPrefab;
        public Transform meteorContainer;
        public SpriteRenderer spriteRenderer;
#pragma warning restore CS0649

        public Color PlanetColor => spriteRenderer.color;


        public void Start()
        {
            shieldManager.AddShield();
        }

        public void SpawnMeteor()
        {
            Instantiate(meteorPrefab, meteorContainer);
        }
    }
}