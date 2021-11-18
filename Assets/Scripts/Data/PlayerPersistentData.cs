﻿using System;
using System.Collections.Generic;
using Sources.Level;

namespace Data
{
    [Serializable]
    public class PlayerPersistentData
    {
        public List<LevelCompletionData> completedLevels;
        public int totalStars;

        public PlayerPersistentData()
        {
            completedLevels = new List<LevelCompletionData>();
            totalStars = 0;
        }

        public void UpdateStars()
        {
            int stars = 0;
            foreach (var level in completedLevels)
            {
                stars += level.stars;
            }
            totalStars = stars;
        }

        public void AddLevelCompleted(string path, int steps, int stars)
        {
            if (IsCompleted(path))
            {
                var levelData = completedLevels.Find(x => x.level.Equals(path));
                var index = completedLevels.IndexOf(levelData);
                if (levelData.stars < stars) levelData.stars = stars;
                if (levelData.steps > steps) levelData.steps = steps;
                completedLevels[index] = levelData;
            }
            else
            {
                completedLevels.Add(new LevelCompletionData(path,steps,stars));
            }
            UpdateStars();
        }

        public bool IsCompleted(string path)
        {
            return completedLevels.Exists(x => x.level.Equals(path));
        }
    }
}