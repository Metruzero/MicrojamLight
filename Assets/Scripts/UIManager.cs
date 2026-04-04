using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI timeTMP;

    [SerializeField]
    private GameObject fuelBarParent, fuelBar;

    private RectTransform fuelBarParentRect, fuelBarRect;

    private void Awake()
    {
        fuelBarParentRect = fuelBarParent.GetComponent<RectTransform>();
        fuelBarRect = fuelBar.GetComponent<RectTransform>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
