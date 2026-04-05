using System;
using System.Collections.Generic;
using System.Resources;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    private Light2D lightComp;

    [Header("Player")]
    public float moveSpeed;
    public float fuelValue;
    public float fuelDecayRate;
    public float fuelDecayGrowthRate;
    public float increasedRadiusMulti;

    [Header("Light Controls")]
    public float intensityVariationSpeed;
    public float intensityVariationValue;
    public float baseIntensity;
    public float innerRadius;
    public float outerRadius;

    public ResourceManager resourceManager;

    private InputAction moveInput;
    private InputAction previousInput;
    private InputAction nextInput;
    private int lightRadiusLevel;
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameState gameState;

    private Vector2 lastMove;

    private Dictionary<UpgradeType, int> upgradeLevels;
    private Dictionary<UpgradeType, float> upgradeValues;
    [SerializeField]
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        moveInput = InputSystem.actions.FindAction("Move");
        previousInput = InputSystem.actions.FindAction("Previous");
        nextInput = InputSystem.actions.FindAction("Next");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        lightComp = GetComponentInChildren<Light2D>();
        lightRadiusLevel = 0;
        upgradeLevels = new Dictionary<UpgradeType, int>();
        upgradeValues = new Dictionary<UpgradeType, float>();
        foreach (UpgradeType uType in Enum.GetValues(typeof(UpgradeType)))
        {
            upgradeLevels.Add(uType, 0);
            upgradeValues.Add(uType, 0);
        }
    }

    private void Animate(Vector2 moveValue)
    {

        spriteRenderer.flipX = lastMove.x > 0 ? true : false;
        animator.SetFloat("MoveX", moveValue.x);
        animator.SetFloat("MoveY", moveValue.y);
        animator.SetFloat("MoveMag", moveValue.magnitude);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    void Update()
    {
        if (gameState == GameState.Active)
        {
            // This will "randomly" change the intensity of the light to create a flicker effect
            float noise = Mathf.PerlinNoise(Time.time * intensityVariationSpeed, 0);
            lightComp.intensity = baseIntensity + (noise - 0.5f) * intensityVariationValue;
            lightComp.pointLightInnerRadius = innerRadius * (1f + (lightRadiusLevel * increasedRadiusMulti));
            lightComp.pointLightOuterRadius = outerRadius * (1f + (lightRadiusLevel * increasedRadiusMulti));


            // Movement
            Vector2 moveValue = moveInput.ReadValue<Vector2>();
            float moveUpgradeModifier = 1f + (upgradeValues[UpgradeType.MovementSpeed] * upgradeLevels[UpgradeType.MovementSpeed]);
            rb.linearVelocity = moveValue * moveSpeed * moveUpgradeModifier;

            if (moveValue.magnitude > 0.1)
            {
                lastMove = moveValue;
            }

            Animate(moveValue);

            // Light Manip
            if (previousInput.WasPressedThisFrame() && lightRadiusLevel > 0)
            {
                lightRadiusLevel--;
            }

            if (nextInput.WasPressedThisFrame() && lightRadiusLevel < 2)
            {
                lightRadiusLevel++;
            }

            // Reduce fuel
            float upgradeReductionValue = (1f - (upgradeLevels[UpgradeType.FuelEfficiency] * upgradeValues[UpgradeType.FuelEfficiency]));

            float growthRateUpgradeModification = (1f - (upgradeLevels[UpgradeType.LargerLightEfficiency] * upgradeValues[UpgradeType.LargerLightEfficiency]));
            Debug.Log("growthRateUpgradeModification: " + growthRateUpgradeModification);
            float lightRadiusFuelModifier = (1f + (lightRadiusLevel * fuelDecayGrowthRate * growthRateUpgradeModification));

            fuelValue -= fuelDecayRate * upgradeReductionValue * lightRadiusFuelModifier * Time.deltaTime;
            resourceManager.ReduceFuel(fuelDecayRate * upgradeReductionValue * lightRadiusFuelModifier * Time.deltaTime);
        }
    }

    public void UpdateUpgrades(List<UpgradeDetails> upgradeDetails)
    {
        foreach (var upgrade in upgradeDetails)
        {
            if (upgradeLevels.ContainsKey(upgrade.type))
            {
                upgradeLevels[upgrade.type] = upgrade.currentLevel;
            }
            else
            {
                upgradeLevels.Add(upgrade.type, upgrade.currentLevel);
            }

            if (upgradeValues.ContainsKey(upgrade.type))
            {
                upgradeValues[upgrade.type] = upgrade.value;
            }
            else
            {
                upgradeValues.Add(upgrade.type, upgrade.value);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void UpdateGameState(GameState gameState)
    {
        this.gameState = gameState;
    }
}
