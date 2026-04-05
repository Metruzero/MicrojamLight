using System.Resources;
using UnityEngine;

public enum GameState
{
    Active,
    LevelCompleteTransition,
    Shop,
    Pause,
    GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private UpgradeManager upgradeManager;
    [SerializeField]
    private StageManager stageManager;
    [SerializeField]
    private ResourceManager resourceManager;

    [SerializeField]
    private Player player;
    public float timeToComplete;
    public float transitionTimeToShop;

    private float time;
    private GameState gameState;

    private int level = 1;
    public float difficultyDecayRate;

    [SerializeField]
    private Transform PlayerStartPosition;

    void Start()
    {
        RefreshGame();
        StartGame();
    }

    void Update()
    {
        time -= Time.deltaTime;
        
        if (gameState == GameState.Active)
        {
            if (time <= 0)
            {
                gameState = GameState.LevelCompleteTransition;
                resourceManager.AdjustCurrency(0);
                uiManager.ShowLevelCompletePanel();
                time = transitionTimeToShop;
                UpdateGameStates();
                level++;
            }
            uiManager.UpdateTime(time);
        }
        else if (gameState == GameState.LevelCompleteTransition)
        {
            if (time <= 0)
            {
                stageManager.ClearStage();
                gameState = GameState.Shop;
                resourceManager.PushUpdateToUI();
                uiManager.HideLevelCompletePanel();
                upgradeManager.RefreshUpgrades();
                uiManager.ShowShop();
                UpdateGameStates();
            }
        }
    }

    private void UpdateGameStates()
    {
        player.UpdateGameState(gameState);
        resourceManager.UpdateGameState(gameState);
    }

    public void StartGame()
    {
        gameState = GameState.Active;
        time = timeToComplete;
        UpdateGameStates();
        player.difficultyDecayRate = 1f + (difficultyDecayRate * level);
    }

    public void RefreshGame()
    {
        player.transform.position = PlayerStartPosition.transform.position;
        stageManager.ClearStage();
        stageManager.GenerateStage();
    }

    public void TriggerGameOver()
    {
        gameState = GameState.GameOver;
        uiManager.ShowGameOverScreen();
    }
}
