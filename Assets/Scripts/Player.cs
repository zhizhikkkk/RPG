using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Stats health;
    [SerializeField] private Stats mana;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxMana = 100f;

    private void Start()
    {
        InitializeStats(health, maxHealth);
        InitializeStats(mana, maxMana);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeStat(health, -10);
            ChangeStat(mana, -10);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeStat(health, 10);
            ChangeStat(mana, 10);
        }
    }

    private void ChangeStat(Stats stat, float value)
    {
        stat.MyCurrentValue += value;
    }

    private void InitializeStats(Stats stat, float maxValue)
    {
        stat.MyMaxValue = maxValue;
        stat.MyCurrentValue = maxValue;
    }
}
