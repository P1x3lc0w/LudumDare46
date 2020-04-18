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
#pragma warning restore CS0649

        public void Start()
        {
        }

        public void SpawnMeteor()
        {
            Instantiate(meteorPrefab, meteorContainer);
        }
    }
}