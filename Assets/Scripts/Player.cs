using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    private Light2D lightComp;

    [SerializeField]
    private float moveSpeed;

    [Header("Light Controls")]
    public float intensityVariationSpeed;
    public float intensityVariationValue;
    public float baseIntensity;
    public float innerRadius;
    public float outerRadius;

    private InputAction moveInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        moveInput = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        lightComp = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * intensityVariationSpeed, 0);
        lightComp.intensity = baseIntensity + (noise - 0.5f) * intensityVariationValue;
        lightComp.pointLightInnerRadius = innerRadius;
        lightComp.pointLightOuterRadius = outerRadius;


        // Movement
        Vector2 moveValue = moveInput.ReadValue<Vector2>();
        rb.linearVelocity = moveValue * moveSpeed;
    }
}
