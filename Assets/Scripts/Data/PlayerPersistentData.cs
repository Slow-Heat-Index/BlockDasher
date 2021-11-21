using System;
using System.Collections.Generic;
using Sources.Identification;
using Sources.Level;
using UnityEngine;

namespace Data {
    [Serializable]
    public class PlayerPersistentData {
        public static readonly int CurrentVersion = 2;

        public int version = CurrentVersion;
        public List<LevelCompletionData> completedLevels;
        public List<Identifier> availableSkins;
        public int totalStars;
        public int coins = 0;
        public Identifier skin = Identifiers.SkinDefault;
        private bool adsRemoved = false;

        public PlayerPersistentData() {
            completedLevels = new List<LevelCompletionData>();
            availableSkins = new List<Identifier>();
            totalStars = 0;
        }

        public void CheckVersion() {
            if (version < 1) {
                skin = Identifiers.SkinDefault;
            }

            if (version < 2) {
                availableSkins = new List<Identifier>();
                availableSkins.Add(Identifiers.SkinDefault);
            }

            version = CurrentVersion;
        }

        public void UpdateStars() {
            int stars = 0;
            foreach (var level in completedLevels) {
                stars += level.stars;
            }

            totalStars = stars;
        }

        public void ModifyCoins(int value) {
            coins = Math.Max(0, coins + value);
        }

        public bool GetAds() {
            return adsRemoved;
        }

        public void SetAds(bool b) {
            adsRemoved = b;
        }

        public void AddLevelCompleted(Identifier id, int steps, int goldStars, int silverStars) {
            var stars = LevelStars(goldStars, silverStars, steps);
            if (IsCompleted(id)) {
                var levelData = completedLevels.Find(x => x.level.Equals(id));
                var index = completedLevels.IndexOf(levelData);
                if (levelData.stars < stars) levelData.stars = stars;
                if (levelData.steps > steps) levelData.steps = steps;
                completedLevels[index] = levelData;
            }
            else {
                completedLevels.Add(new LevelCompletionData(id, steps, stars));
            }

            UpdateStars();
        }

        public bool IsCompleted(Identifier id) {
            return completedLevels.Exists(x => x.level.Equals(id));
        }

        public int LevelStars(int gold, int silver, int steps) {
            if (steps <= gold) return 3;
            return steps <= silver ? 2 : 1;
        }
    }
}