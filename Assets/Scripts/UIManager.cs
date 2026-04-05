using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public class UpgradeButton
{
    public GameObject buttonObject;
    public TextMeshProUGUI upgradeTitleTMP;
    public TextMeshProUGUI upgradeDescriptionTMP;
    public TextMeshProUGUI upgradeCostTMP;
}
public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI timeTMP;

    [SerializeField]
    private GameObject fuelBarParent, fuelBar;

    [SerializeField]
    private GameObject gameplayPanel, shopPanel;

    [SerializeField]
    private List<UpgradeButton> upgradeButtons;

    private RectTransform fuelBarRect;

    private InputAction toggleAction;
    private bool displayToggle = false;

    [SerializeField]
    private GameObject dialogueContainerPanel, upgradePanel, sellPanel, levelCompletePanel;

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
                ShowShop();
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

    public void UpdateUpgradeButton(string upgradeTitle, string upgradeDescription, int cost, bool enable, int index)
    {
        UpgradeButton currentButton = upgradeButtons[index];
        currentButton.upgradeTitleTMP.text = upgradeTitle;
        currentButton.upgradeDescriptionTMP.text = upgradeDescription;
        currentButton.upgradeCostTMP.text = cost.ToString("#");

        currentButton.buttonObject.SetActive(enable);


    }

    public void ResetUpgradeButtons()
    {
        foreach (var button in upgradeButtons)
        {
            button.buttonObject.SetActive(false);
            button.buttonObject.GetComponent<Button>().interactable = true;
        }
    }

    public void UpgradeCompleteDisableButton(int ind)
    {
        Debug.Log("upgradeButtons[ind].buttonObject.GetComponent<Button>().interactable" + upgradeButtons[ind].buttonObject.GetComponent<Button>().interactable);
        upgradeButtons[ind].buttonObject.GetComponent<Button>().interactable = false;
    }

    public void HideLeftShopBoxes()
    {
        dialogueContainerPanel.SetActive(false);
        upgradePanel.SetActive(false);
        sellPanel.SetActive(false);
    }

    public void ShowUpgradeBox()
    {
        upgradePanel.SetActive(true);
    }

    public void ShowSellBox()
    {
        sellPanel.SetActive(true);
    }

    public void ShowDialogueBox()
    {
        dialogueContainerPanel.SetActive(true);
    }

    public void ShowLevelCompletePanel()
    {
        levelCompletePanel.SetActive(true);
    }

    public void HideLevelCompletePanel()
    {
        levelCompletePanel.SetActive(false);
    }

    public void ShowShop()
    {
        gameplayPanel.SetActive(false);
        shopPanel.SetActive(true);
    }
}
