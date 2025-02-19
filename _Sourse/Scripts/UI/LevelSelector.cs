using Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class LevelSelector : MonoBehaviour
    {
        public GameController gameController;
      
        private void Start()
        {
            PlayerProgress.Initialize();

            LevelData[] levels = Resources.LoadAll<LevelData>("Levels");

            UIManager.Instance.CreateLevelButtons(new List<LevelData>(levels), this);
        }

        public void LoadLevel(int levelIndex)
        {
            LevelData level = Resources.Load<LevelData>($"Levels/Level{levelIndex}");
            if (level != null)
            {
                gameController.LoadLevel(level);
                UIManager.Instance.HideAllPanels();
            }
            else
            {
                Debug.LogError($"Level with index {levelIndex} not found.");
            }
        }
    }
}