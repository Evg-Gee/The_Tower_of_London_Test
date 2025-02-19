using Data;
using UnityEngine;

namespace Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public MoveManager moveManager;
        public LevelManager levelManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InitializeLevel()
        {
            moveManager.Initialize(levelManager.GetCurrentLevel().maxMoves);
        }

        public void LoadLevel(LevelData level)
        {
            UIManager.Instance.HideAllPanels();
            levelManager.LoadLevel(level);

            InitializeLevel();

            UIManager.Instance.UpdateTargetStateDisplay(level);
        }
        public void ClearGameField()
        {
            foreach (var pole in levelManager.GetPoled())
            {
                pole.Clear(); 
            }
        }

        public void MoveRing(Ring ring, IPole targetPole)
        {
            if (moveManager.TryMove(ring, targetPole))
            {
                if (levelManager.CheckWinCondition())
                {
                    CompleteLevel();
                }
            }
        }
        public void CompleteLevel()
        {
            AnimationManager.Instance.PlaySuccessAnimation();
            AudioManager.Instance.PlaySuccessSound();
            UIManager.Instance.ShowCompletionMessage();

            int levelIndex = levelManager.GetCurrentLevelIndex();
            PlayerProgress.MarkLevelAsCompleted(levelIndex);
        }
        public void RestartCurrentLevel()
        {
            LevelData currentLevel = levelManager.GetCurrentLevel();
            if (currentLevel != null)
            {
                LoadLevel(currentLevel);  
            }
        }
        public bool IsOutOfMoves()
        {
            return moveManager.IsOutOfMoves();
        }
    }
}