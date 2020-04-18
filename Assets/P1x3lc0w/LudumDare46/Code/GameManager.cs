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
        const float MAX_SPAWN_DELTA_TIME = 20.0f;
        const float MIN_SPAWN_DELTA_TIME = 10.0f;

        const float MIN_SPAWN_TIME_TIME = 30.0f;

        const float PLANET_DISTANCE = 22.69f;

        const float TOTAL_CAMERA_MOVE_TIME = 0.4f;

#pragma warning disable CS0649
        public GameObject planetPrefab;
        public Transform planetContainer;
#pragma warning restore CS0649

        public float GameTime { get; private set; }

        public float TimeUntilMeteor { get; private set; }

        public float SpawnDeltaTime => Mathf.Lerp(MIN_SPAWN_DELTA_TIME, MAX_SPAWN_DELTA_TIME, Mathf.Min(GameTime / MIN_SPAWN_TIME_TIME, 1.0f));

        private List<Planet> _plantes = new List<Planet>();

        private Planet _activePlanet;

        public Planet ActivePlanet
        {
            get => _activePlanet;
            set
            {
                if(_activePlanet != null)
                    _activePlanet.shieldManager.InputActive = false;

                _activePlanet = value;
                _activePlanet.shieldManager.InputActive = true;

                _cameraStartPos = Camera.main.transform.position;
                _cameraEndPos = _activePlanet.transform.position;
                _cameraMoveTime = 0.0f;
            }
        }

        private Vector2 _cameraStartPos;
        private Vector2 _cameraEndPos;
        private float _cameraMoveTime;

        public void Start()
        {
            PrepareGame();
        }

        public void PrepareGame()
        {
            AddPlanet();
            AddPlanet();
            _plantes[0].shieldManager.AddShield();
            TimeUntilMeteor = SpawnDeltaTime;
            GameTime = 0.0f;
        }

        public void AddPlanet()
        {
            GameObject planetGO = Instantiate(planetPrefab, new Vector3(_plantes.Count * PLANET_DISTANCE, 0.0f), Quaternion.identity, planetContainer);
            Planet planet = planetGO.GetComponent<Planet>();


            if (_plantes.Count == 0)
            {
                ActivePlanet = planet;
            }

            _plantes.Add(planet);        
        }

        public void Update()
        {
            GameTime += Time.deltaTime;
            TimeUntilMeteor -= Time.deltaTime;

            _cameraMoveTime += Time.deltaTime;
            Vector2 camPos = Vector2.Lerp(_cameraStartPos, _cameraEndPos, _cameraMoveTime / TOTAL_CAMERA_MOVE_TIME);
            Camera.main.transform.position = new Vector3(camPos.x, camPos.y, Camera.main.transform.position.z); 

            if(TimeUntilMeteor <= 0)
            {
                _plantes[Random.Range(0, _plantes.Count)].SpawnMeteor();

                TimeUntilMeteor += SpawnDeltaTime;
            }

            if(_plantes.Count > 1)
            {
                if (Input.GetButtonDown("PlanetRight"))
                {
                    ActivePlanet = _plantes[(_plantes.IndexOf(ActivePlanet) + 1) % _plantes.Count];
                }
                else if (Input.GetButtonDown("PlanetLeft"))
                {
                    ActivePlanet = _plantes[(_plantes.IndexOf(ActivePlanet) - 1 + _plantes.Count) % _plantes.Count];
                }

            }
        }
    }
}
