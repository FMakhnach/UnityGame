/// <summary>
/// Represents entities that can be damaged.
/// </summary>
public interface IDamageable
{
    void RecieveDamage(float damage);
    Alignment Alignment { get; }
}
