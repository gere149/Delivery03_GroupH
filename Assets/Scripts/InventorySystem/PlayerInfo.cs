using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ElementInfo/PlayerInfo")]
public class PlayerInfo : ElementInfo
{
    public float Health;
    public float damage;

    private float maxHealth = 100.0f;

    public static Action OnDie;
    public static Action OnHealthChanged;

    private void OnEnable()
    {
        ConsumeItem.OnConsumeItem += HealthUp;
        OnHealthChanged?.Invoke();
    }

    private void OnDisable()
    {
        ConsumeItem.OnConsumeItem -= HealthUp;
    }

    private void HealthUp(ItemIngestible item)
    {
        Health = Mathf.Min(Health + item.HealthPoints, maxHealth);
        OnHealthChanged?.Invoke();
    }

    public void GetDamage()
    {
        Health = Mathf.Max(Health - damage, 0);
        OnHealthChanged?.Invoke();

        if (Health == 0.0f)
        {
            OnDie?.Invoke();
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
