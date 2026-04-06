using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private SceneLoader sceneLoader;
    [SerializeField]
    private GameObject tutorialPanel;

    public void OnClickPlayButton()
    {
        sceneLoader.LoadPlayScene();
    }

    public void OnClickTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void OnClickMainMenu()
    {
        tutorialPanel.SetActive(false);
    }


}
