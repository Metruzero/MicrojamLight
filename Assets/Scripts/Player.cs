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

    
    public UIManager uiManager;

    private InputAction moveInput;
    private InputAction previousInput;
    private InputAction nextInput;
    private int lightRadiusLevel;
    private Rigidbody2D rb;
    

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
        lightRadiusLevel = 1;
    }

    void Update()
    {
        // This will "randomly" change the intensity of the light to create a flicker effect
        float noise = Mathf.PerlinNoise(Time.time * intensityVariationSpeed, 0);
        lightComp.intensity = baseIntensity + (noise - 0.5f) * intensityVariationValue;
        lightComp.pointLightInnerRadius = innerRadius * (1f + (lightRadiusLevel * increasedRadiusMulti));
        lightComp.pointLightOuterRadius = outerRadius * (1f + (lightRadiusLevel * increasedRadiusMulti));


        // Movement
        Vector2 moveValue = moveInput.ReadValue<Vector2>();
        rb.linearVelocity = moveValue * moveSpeed;

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
        fuelValue -= Mathf.Clamp(fuelDecayRate * (1f + (lightRadiusLevel * fuelDecayGrowthRate)) * Time.deltaTime, 0, 100);

        uiManager.UpdateFuel(fuelValue / 100);


    }
}
