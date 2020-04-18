using P1x3lc0w.LudumDare46.UI;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace P1x3lc0w.LudumDare46
{
    internal class GameManager : MonoBehaviour
    {
        private const float MAX_SPAWN_DELTA_TIME = 5.0f;
        private const float MIN_SPAWN_DELTA_TIME = 2.0f;

        private const float MIN_SPAWN_TIME_TIME = 30.0f;

        private const float PLANET_DISTANCE = 22.69f;

        private const float TOTAL_CAMERA_MOVE_TIME = 0.4f;

#pragma warning disable CS0649
        public GameObject planetPrefab;
        public DialougeManager dialougeManager;
        public CallManager callManager;
        public Transform planetContainer;
#pragma warning restore CS0649

        public static bool GameRunning { get; set; }

        public float GameTime { get; private set; }

        public float TimeUntilMeteor { get; private set; }

        public float SpawnDeltaTime => Mathf.Lerp(MAX_SPAWN_DELTA_TIME, MIN_SPAWN_DELTA_TIME, Mathf.Min(GameTime / MIN_SPAWN_TIME_TIME, 1.0f));

        private List<Planet> _plantes = new List<Planet>();

        private Planet _activePlanet;

        public Planet ActivePlanet
        {
            get => _activePlanet;
            set
            {
                if (_activePlanet != null)
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
            callManager.DoCall(() =>
            {
                    dialougeManager.StartDialouge(new string[] {
                        "This is Galactic Command to Remote Control Outpost RCO-556479.",
                        "Our systems have detected an incoming meteor shower near colony 477659, however the automatic energy shield control systems for your sector are currently out of order.",
                        "You will need to manually control the colony's energy shield using [A] and [D].",
                        "Galactic Command out."
                    },
                    () =>
                    {
                        GameRunning = true;
                    });
            });
        }

        public void PrepareGame()
        {
            AddPlanet();
            _plantes[0].shieldManager.AddShield();
            TimeUntilMeteor = 0.0f;
            GameTime = 0.0f;
        }

        public void AddPlanet()
        {
            bool firstPlanet = _plantes.Count == 0;
            GameObject planetGO = Instantiate(planetPrefab, new Vector3(_plantes.Count * PLANET_DISTANCE, firstPlanet ? 0.0f : Random.Range(-20.0f, 20.0f)), Quaternion.identity, planetContainer);
            Planet planet = planetGO.GetComponent<Planet>();

            if (firstPlanet)
            {
                ActivePlanet = planet;
            }

            _plantes.Add(planet);
        }

        public void Update()
        {
            if (GameRunning)
            {
                GameTime += Time.deltaTime;
                TimeUntilMeteor -= Time.deltaTime;

                _cameraMoveTime += Time.deltaTime;
                Vector2 camPos = Vector2.Lerp(_cameraStartPos, _cameraEndPos, _cameraMoveTime / TOTAL_CAMERA_MOVE_TIME);
                Camera.main.transform.position = new Vector3(camPos.x, camPos.y, Camera.main.transform.position.z);

                if (TimeUntilMeteor <= 0)
                {
                    _plantes[Random.Range(0, _plantes.Count)].SpawnMeteor();

                    TimeUntilMeteor += SpawnDeltaTime;
                }

                if (_plantes.Count > 1)
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
}