using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "ElementInfo/PlayerInfo")]
public class PlayerInfo : ElementInfo
{
    public float Health;
    public float damage;

    public static Action OnDie;
    public static Action OnHealthChanged;

    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float defaultHealth = 100.0f;
    [SerializeField] private float defaultDamage = 20.0f;

    private void OnEnable()
    {
        ResetHealth();
        ConsumeItem.OnConsumeItem += HealthUp;
    }

    private void OnDisable()
    {
        ConsumeItem.OnConsumeItem -= HealthUp;
    }

    public void ResetHealth()
    {
        Health = defaultHealth;
        damage = defaultDamage;
        OnHealthChanged?.Invoke();  
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