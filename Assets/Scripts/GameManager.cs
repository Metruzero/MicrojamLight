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
    private Player player;
    public float timeToComplete;
    public float transitionTimeToShop;

    private float time;
    private GameState gameState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
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
            }
        }
    }

    public void StartGame()
    {
        gameState = GameState.Active;
        time = timeToComplete;
        player.UpdateGameState(gameState);
    }

    public void RefreshGame()
    {

    }
}
