using UnityEngine;
public interface IDamageable
{
    float MaxHealth { get; }
    void ModifyHealth(float amount);
}