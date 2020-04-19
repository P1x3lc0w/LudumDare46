﻿using P1x3lc0w.LudumDare46.UI;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace P1x3lc0w.LudumDare46
{
    internal class GameManager : MonoBehaviour
    {
        private const float PLANET_DISTANCE = 22.69f;

        private const float TOTAL_CAMERA_MOVE_TIME = 0.4f;

        private const float EVENT_INTERVAL = 30.0f;

        public const int MAX_SHIELD_COUNT = 3;

        const float GAME_LENGTH = 300.0f;
        const float GAME_END_TIME = 10.0f;

#pragma warning disable CS0649
        public GameObject planetPrefab;
        public DialougeManager dialougeManager;
        public EnergyAllocationUIManager energyAllocationUIManager;
        public CallManager callManager;
        public Transform planetContainer;
        public GameObject mainMenu;
        public EventLog eventLog;
#pragma warning restore CS0649

        public static bool GameRunning { get; set; }

        public float GameTime { get; private set; }

        public float TimeUntilMeteor { get; private set; }

        public float SpawnDeltaTime => Mathf.Lerp(GameDifficultyInfo.MaxSpawnInterval, GameDifficultyInfo.MinSpawnInterval, Mathf.Min(GameTime / GameDifficultyInfo.DifficultyWindupTime, 1.0f));

        public GameDifficultyInfo GameDifficultyInfo { get; private set; }

        private List<Planet> _planets = new List<Planet>();

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

        private int _eventCounter;

        private bool _addedPlanet;
        private bool _addedShield;

        public void Start()
        {
        }

        public void StartGame(int difficulty)
        {
            mainMenu.SetActive(false);
            GameDifficultyInfo = GameDifficultyInfo.GetDifficultyInfo((GameDifficulty)difficulty);
            PrepareGame();
            callManager.DoCall(() =>
            {
                dialougeManager.StartDialouge(new string[] {
                        "This is Galactic Command to Remote Control Outpost RCO-556479.",
                        "Our systems have detected an incoming meteor shower near colony 477659.",
                        "However the automatic energy shield control systems for your sector are currently out of order.",
                        "Should a meteor impact occur, the colonist's lives are in danger.",
                        "You will need to manually control the colony's energy shield using [A] and [D].",
                        "Galactic Command out."
                    },
                () =>
                {
                    GameRunning = true;
                }); ;
            });
        }

        public void PrepareGame()
        {
            AddPlanet();
            TimeUntilMeteor = 0.0f;
            GameTime = 0.0f;
            _eventCounter = 1;
        }

        public void AddPlanet()
        {
            bool firstPlanet = _planets.Count == 0;
            GameObject planetGO = Instantiate(planetPrefab, new Vector3(_planets.Count * PLANET_DISTANCE, firstPlanet ? 0.0f : Random.Range(-20.0f, 20.0f)), Quaternion.identity, planetContainer);
            Planet planet = planetGO.GetComponent<Planet>();

            if (firstPlanet)
            {
                ActivePlanet = planet;
            }

            _planets.Add(planet);
        }

        public void DoEvent()
        {
            if(_eventCounter % 2 == 1)
            {
                if (_planets.Count < GameDifficultyInfo.MaxPlanets)
                {
                    AddPlanet();
                    if (!_addedPlanet)
                    {
                        callManager.DoCall(() =>
                        {
                            dialougeManager.StartDialouge(new string[] {
                                "This is Galactic Command to Remote Control Outpost RCO-556479.",
                                "Our systems have detected that another colony in your sector may be affected by the meteor storm.",
                                "We will be sending you the coordinates.",
                                "Switch between colonies using [Q] and [E].",
                                "Galactic Command out."
                            },
                            () =>
                            {
                                GameRunning = true;
                                _addedPlanet = true;
                            });
                        });
                    }
                    else
                    {
                        eventLog.ShowEvent("The another colony may be affected by the storm. The coordinates have been added.");
                    }
                }
            }
            else
            {
                if (!_addedShield)
                {
                    callManager.DoCall(() =>
                    {
                        dialougeManager.StartDialouge(new string[] {
                                "This is Galactic Command to Remote Control Outpost RCO-556479.",
                                "We managed to re-route some power from other systems.",
                                "You may assign power for an additional energy shield for one of the colonies currently under your watch.",
                                "Switch between shields using [W] and [S].",
                                "Galactic Command out."
                    },
                        () =>
                        {
                            energyAllocationUIManager.Open(_planets);
                            _addedShield = true;
                        });
                    });
                }
                else
                {
                    energyAllocationUIManager.Open(_planets);
                }
            }

            _eventCounter++;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                GameTime += 30.0f;

            if (GameRunning)
            {
                GameTime += Time.deltaTime;
                TimeUntilMeteor -= Time.deltaTime;

                _cameraMoveTime += Time.deltaTime;
                Vector2 camPos = Vector2.Lerp(_cameraStartPos, _cameraEndPos, _cameraMoveTime / TOTAL_CAMERA_MOVE_TIME);
                Camera.main.transform.position = new Vector3(camPos.x, camPos.y, Camera.main.transform.position.z);



                if (_planets.Count > 1)
                {
                    if (Input.GetButtonDown("PlanetRight"))
                    {
                        ActivePlanet = _planets[(_planets.IndexOf(ActivePlanet) + 1) % _planets.Count];
                    }
                    else if (Input.GetButtonDown("PlanetLeft"))
                    {
                        ActivePlanet = _planets[(_planets.IndexOf(ActivePlanet) - 1 + _planets.Count) % _planets.Count];
                    }
                }

                if(GameTime < GAME_LENGTH)
                {
                    if (TimeUntilMeteor <= 0)
                    {
                        _planets[Random.Range(0, _planets.Count)].SpawnMeteor();

                        TimeUntilMeteor += SpawnDeltaTime;
                    }

                    if (GameTime / EVENT_INTERVAL > _eventCounter)
                    {
                        DoEvent();
                    }
                }
                else if (GameTime >= GAME_LENGTH + GAME_END_TIME)
                {
                    Win();
                }
            }
        }

        public void Win()
        {

        }

        public void Loose()
        {

        }
    }
}