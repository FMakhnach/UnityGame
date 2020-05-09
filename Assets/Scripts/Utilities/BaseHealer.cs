using UnityEngine;

[RequireComponent(typeof(Base))]
public class BaseHealer : MonoBehaviour
{
    [SerializeField]
    private int cost;
    [SerializeField]
    private float hpHealed;
    private Base @base;

    private void Awake()
    {
        @base = GetComponent<Base>();
    }
    public void Heal()
    {
        @base.ReceiveHeal(hpHealed, cost);
    }
}
