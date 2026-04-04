using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UIManager uiManager;

    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        uiManager.UpdateTime(time);
    }
}
