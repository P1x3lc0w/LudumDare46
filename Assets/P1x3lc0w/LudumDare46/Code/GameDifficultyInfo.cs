using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1x3lc0w.LudumDare46
{
    class GameDifficultyInfo
    {
        public float MaxSpawnInterval { get; private set; }
        public float MinSpawnInterval { get; private set; }
        public float DifficultyWindupTime { get; private set; }
        public int MaxPlanets { get; private set; }

        public static GameDifficultyInfo GetDifficultyInfo(GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.EASY:
                    return new GameDifficultyInfo(7.0f, 3.0f, 180.0f, 3);

                case GameDifficulty.NORMAL:
                    return new GameDifficultyInfo(5.0f, 2.0f, 200.0f, 4);

                case GameDifficulty.HARD:
                    return new GameDifficultyInfo(4.0f, 2.0f, 220.0f, 5);

                default:
                    throw new NotImplementedException("Unimplemented Game Difficulty.");
            }
        }

        public GameDifficultyInfo(float maxSpawnInterval, float minSpawnInterval, float difficultyWindupTime, int maxPlanets)
        {
            MaxSpawnInterval = maxSpawnInterval;
            MinSpawnInterval = minSpawnInterval;
            DifficultyWindupTime = difficultyWindupTime;
            MaxPlanets = maxPlanets;
        }
    }
}
