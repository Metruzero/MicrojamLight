using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI timeTMP;

    [SerializeField]
    private GameObject fuelBarParent, fuelBar;

    [SerializeField]
    private GameObject gameplayPanel, shopPanel;

    private RectTransform fuelBarRect;

    private InputAction toggleAction;
    private bool displayToggle = false;

    private void Awake()
    {
        fuelBarRect = fuelBar.GetComponent<RectTransform>();
        toggleAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleAction.WasPressedThisFrame())
        {
            if (!displayToggle)
            {
                gameplayPanel.SetActive(false);
                shopPanel.SetActive(true);
            }
            else
            {
                gameplayPanel.SetActive(true);
                shopPanel.SetActive(false);
            }
            displayToggle = !displayToggle;
        }
    }

    public void UpdateTime(float time)
    {
        timeTMP.text = time.ToString("#");
    }

    public void UpdateFuel(float fuelPercentage)
    {
        fuelBarRect.localScale = new Vector3(1f, fuelPercentage, 1f);
    }
}
