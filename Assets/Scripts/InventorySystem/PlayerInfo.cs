using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ElementInfo/PlayerInfo")]
public class PlayerInfo : ElementInfo
{
    public float Health;
    public float damage;

    public static Action OnDie;
    public static Action OnHealthChanged;

    private float maxHealth = 100.0f;
    private float defaultHealth = 100.0f;
    private float defaultDamage = 20.0f;

    private void OnEnable()
    {
        ResetValues();

        ConsumeItem.OnConsumeItem += HealthUp;
        OnHealthChanged?.Invoke();
    }

    private void OnDisable()
    {
        ConsumeItem.OnConsumeItem -= HealthUp;
        ResetValues();
    }

    public void ResetValues()
    {
        Health = defaultHealth;
        damage = defaultDamage;
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
