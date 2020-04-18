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
            spriteRenderer.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 0.5f, 0.5f);
        }

        public void SpawnMeteor()
        {
            Instantiate(meteorPrefab, meteorContainer);
        }
    }
}