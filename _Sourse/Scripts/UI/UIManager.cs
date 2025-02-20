using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Data;
using System.Text;

namespace Scripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject mainMenuPanel; 
        [SerializeField] private GameObject resultsPanel; 
        [SerializeField] private GameObject levelSelectionPanel; 

        [SerializeField] private GameObject levelButtonPrefab; 
        [SerializeField] private Transform levelButtonsParent; 
        [SerializeField] private TextMeshProUGUI moveCounterText; 
        [SerializeField] private GameObject completionPanel; 
        [SerializeField] private GameObject losePanel; 
        [SerializeField] private Sprite completedLevelSprite;

        [SerializeField] private Transform[] poleContainers; 
        [SerializeField] private GameObject ringPrefab;

        [SerializeField] private TextMeshProUGUI resultsText; 

       
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
        private void Start()
        {
            ShowMainMenu();
        }
        public void UpdateTargetStateDisplay(LevelData level)
        {
            foreach (Transform poleContainer in poleContainers)
            {
                foreach (Transform child in poleContainer)
                {
                    Destroy(child.gameObject);
                }
            }

            for (int i = 0; i < level.targetStates.Count; i++)
            {
                PoleTargetState targetState = level.targetStates[i];
                Transform poleContainer = poleContainers[i];

                foreach (Color color in targetState.colors)
                {
                    GameObject ringObject = Instantiate(ringPrefab, poleContainer);
                    Image ringImage = ringObject.GetComponent<Image>();

                    if (ringImage != null)
                    {
                        ringImage.color = color;
                    }
                }
            }
        }

public void ShowMainMenu()
        {
            mainMenuPanel.SetActive(true);
            resultsPanel.SetActive(false);
            levelSelectionPanel.SetActive(false);
            completionPanel.SetActive(false);
            losePanel.SetActive(false);
            GameController.Instance.ClearGameField();
        }
        public void HideAllPanels()
        {
            mainMenuPanel.SetActive(false);
            resultsPanel.SetActive(false);
            levelSelectionPanel.SetActive(false);
            losePanel.SetActive(false);
            completionPanel.SetActive(false);           
        }
        public void ShowLevelSelection()
        {
            mainMenuPanel.SetActive(false);
            losePanel.SetActive(false);
            levelSelectionPanel.SetActive(true);

            UpdateLevelButtonVisuals();
        }

        public void ShowResults()
        {
            mainMenuPanel.SetActive(false);
            losePanel.SetActive(false);
            resultsPanel.SetActive(true);

            LoadAndDisplayResults();
        }

        private void LoadAndDisplayResults()
        {
            LevelData[] levels = Resources.LoadAll<LevelData>("Levels");
            PlayerProgress.LoadProgress();

            StringBuilder resultsBuilder = new StringBuilder();
            foreach (var level in levels)
            {
                bool isCompleted = PlayerProgress.IsLevelCompleted(level.levelIndex);
                resultsBuilder.AppendLine($"Level {level.levelIndex}: {(isCompleted ? "is Completed!" : "Not Completed")}");                
            }

            resultsText.text = resultsBuilder.ToString();
        }

        public void ResetProgressButton()
        {
            PlayerProgress.ClearProgress();
            LoadAndDisplayResults();
        }

        public void CreateLevelButtons(List<LevelData> levels, LevelSelector levelSelector)
        {
            foreach (Transform child in levelButtonsParent)
            {
                Destroy(child.gameObject);                
            }

            for (int i = 0; i < levels.Count; i++)
            {
                int levelIndex = i + 1; 

                GameObject buttonObject = Instantiate(levelButtonPrefab, levelButtonsParent);
                Button button = buttonObject.GetComponent<Button>();
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                Image statusImage = buttonObject.transform.Find("StatusImage").GetComponent<Image>();

                if (PlayerProgress.IsLevelCompleted(levelIndex))
                {
                    buttonText.color = Color.green;
                    statusImage.sprite = completedLevelSprite;
                    buttonText.text = $"Level {levelIndex}";
                }
                else
                {
                    statusImage.enabled =false;
                    buttonText.text = $"Level {levelIndex}";
                }

                button.onClick.AddListener(() => levelSelector.LoadLevel(levelIndex));
            }
        }

        public void UpdateLevelButtonVisuals()
        {
            if (levelButtonsParent == null)
            {
                Debug.LogError("levelButtonsParent is not assigned!");
                return;
            }

            int childCount = levelButtonsParent.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform child = levelButtonsParent.GetChild(i);

                Button button = child.GetComponent<Button>();
                TextMeshProUGUI buttonText = child.GetComponentInChildren<TextMeshProUGUI>();
                Image statusImage = button.transform.Find("StatusImage")?.GetComponent<Image>();

                if (button != null && buttonText != null)
                {
                    int levelIndex = i + 1;

                    if (PlayerProgress.IsLevelCompleted(levelIndex))
                    {
                        buttonText.color = Color.green;
                        if (statusImage != null)
                        {
                            statusImage.enabled = true;
                        }
                        buttonText.text = $"Level {levelIndex}";
                    }
                    else
                    {
                        if (statusImage != null)
                        {
                            statusImage.enabled = false;
                        }
                        buttonText.color = Color.white;
                        buttonText.text = $"Level {levelIndex}";
                    }
                }
            }
        }

        public void UpdateMoveCounter(int currentMoves, int maxMoves)
        {
            moveCounterText.text = $"Moves: {currentMoves}/{maxMoves}";
        }

        public void ShowCompletionMessage()
        {
            completionPanel.SetActive(true);
        }
        public void ShowGameOverPanel()
        {
            losePanel.SetActive(true);
        }
    }
}