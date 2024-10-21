using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    [SerializeField] private Image content;
    [SerializeField] private TextMeshProUGUI textStats;
    private float currentValue;

    public float MyMaxValue { get; set; } = 100f;

    public float MyCurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = Mathf.Clamp(value, 0, MyMaxValue);
            UpdateBar();
        }
    }

    private void Start()
    {
        if (content == null)
        {
            content = GetComponent<Image>();
        }

        UpdateBar();
    }

    private void UpdateBar()
    {
        content.fillAmount = MyCurrentValue / MyMaxValue;
        textStats.text = $"{currentValue}/{MyMaxValue}";
    }
}
