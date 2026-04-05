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
    private Player player;
    public float timeToComplete;
    public float transitionTimeToShop;

    private float time;
    private GameState gameState;

    private int level = 1;
    public float difficultyDecayRate;

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
                uiManager.ShowLevelCompletePanel();
                time = transitionTimeToShop;
                player.UpdateGameState(gameState);
                level++;
            }
            uiManager.UpdateTime(time);
        }
        else if (gameState == GameState.LevelCompleteTransition)
        {
            if (time <= 0)
            {
                gameState = GameState.Shop;
                uiManager.HideLevelCompletePanel();
                upgradeManager.RefreshUpgrades();
                uiManager.ShowShop();
                player.UpdateGameState(gameState);
            }
        }
    }

    public void StartGame()
    {
        gameState = GameState.Active;
        time = timeToComplete;
        player.UpdateGameState(gameState);
        player.difficultyDecayRate = 1f + (difficultyDecayRate * level);
    }

    public void RefreshGame()
    {
        stageManager.GenerateStage();
    }
}
