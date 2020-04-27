/// <summary>
/// Represents entities that can be damaged.
/// </summary>
public interface IDamageable
{
    void ReceiveDamage(float damage);
    PlayerManager Owner { get; }
}