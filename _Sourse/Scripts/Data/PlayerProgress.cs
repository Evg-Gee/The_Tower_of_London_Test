using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scripts
{
    public class PlayerProgress
    {
        private const string SavePath = "/player_progress.json";

        [System.Serializable]
        public class ProgressData
        {
            public List<int> completedLevels;
        }

        private static ProgressData progressData;

        public static void Initialize()
        {
            LoadProgress();
        }

        public static void SaveProgress()
        {
            string json = JsonUtility.ToJson(progressData);
            File.WriteAllText(Application.persistentDataPath + SavePath, json);
        }

        public static void LoadProgress()
        {
            if (File.Exists(Application.persistentDataPath + SavePath))
            {
                string json = File.ReadAllText(Application.persistentDataPath + SavePath);
                progressData = JsonUtility.FromJson<ProgressData>(json);
            }
            else
            {
                progressData = new ProgressData
                {
                    completedLevels = new List<int>()
                };
            }
        }

        public static bool IsLevelCompleted(int levelIndex)
        {
            return progressData.completedLevels.Contains(levelIndex);
        }

        public static void MarkLevelAsCompleted(int levelIndex)
        {
            if (!progressData.completedLevels.Contains(levelIndex))
            {
                progressData.completedLevels.Add(levelIndex);
                SaveProgress();
            }
        }
        public static void ClearProgress()
        {
            progressData = new ProgressData
            {
                completedLevels = new List<int>()
            };

            SaveProgress();
        }
    }
}
