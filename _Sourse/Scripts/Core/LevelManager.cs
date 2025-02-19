using Data;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Pole> poles;
        private LevelData currentLevel;
        public void LoadLevel(LevelData level)
        {
            currentLevel = level;

            foreach (var pole in poles)
            {
                pole.Clear();
            }

            IPole startPole = poles[0];
            for (int i = 0; i < currentLevel.initialColors.Count; i++)
            {
                Ring ring = Instantiate(Resources.Load<Ring>("RingPrefab"));
                ring.ringColor = currentLevel.initialColors[i];
                ring.GetComponent<Renderer>().material.color = ring.ringColor;
                startPole.AddRing(ring);
            }

            for (int i = 0; i < poles.Count; i++)
            {
                poles[i].SetTargetState(currentLevel.targetStates[i]);
            }
        }
        public bool CheckWinCondition()
        {
            foreach (var pole in poles)
            {
                if (!pole.IsInTargetState()) return false;
            }
            return true;
        }

        public LevelData GetCurrentLevel()
        {
            return currentLevel;
        }

        public int GetCurrentLevelIndex()
        {
            return currentLevel.levelIndex;
        }

        public List<Pole> GetPoled()
        {
            return poles;
        }
    }
}