using UnityEngine;

public class CollidersGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject tileCollider;

    private void Start()
    {
        for (int i = -25; i <= 25; i++)
        {
            for (int j = -25; j <= 25; j++)
            {
                Instantiate(tileCollider, 
                    new Vector3(1.5f * i, 0, (1.73205f * j) + 0.86602f * (i % 2)), 
                    Quaternion.identity,
                    gameObject.transform);
            }
        }
    }
}
