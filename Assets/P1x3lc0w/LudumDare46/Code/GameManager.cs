using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace P1x3lc0w.LudumDare46
{
    class GameManager : MonoBehaviour
    {
        const float MAX_SPAWN_DELTA_TIME = 2.0f;
        const float MIN_SPAWN_DELTA_TIME = 1.0f;

        const float MIN_SPAWN_TIME_TIME = 30.0f;

#pragma warning disable CS0649
        public GameObject planetPrefab;
        public Transform planetContainer;
#pragma warning restore CS0649

        public float GameTime { get; private set; }

        public float TimeUntilMeteor { get; private set; }

        public float SpawnDeltaTime => Mathf.Lerp(MIN_SPAWN_DELTA_TIME, MAX_SPAWN_DELTA_TIME, Mathf.Min(GameTime / MIN_SPAWN_TIME_TIME, 1.0f));

        private List<Planet> _plantes = new List<Planet>();

        public void Start()
        {
            PrepareGame();
        }

        public void PrepareGame()
        {
            AddPlanet();
            _plantes[0].shieldManager.AddShield();
            TimeUntilMeteor = SpawnDeltaTime;
            GameTime = 0.0f;
        }

        public void AddPlanet()
        {
            GameObject planetGO = Instantiate(planetPrefab, planetContainer);
            Planet planet = planetGO.GetComponent<Planet>();

            _plantes.Add(planet);
        }

        public void Update()
        {
            GameTime += Time.deltaTime;
            TimeUntilMeteor -= Time.deltaTime;

            if(TimeUntilMeteor <= 0)
            {
                _plantes[Random.Range(0, _plantes.Count - 1)].SpawnMeteor();

                TimeUntilMeteor += SpawnDeltaTime;
            }
        }
    }
}
