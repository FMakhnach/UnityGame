using UnityEngine;

public class RoadPlacingScript : MonoBehaviour
{
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private RoadNode node;
    private int i = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                var nd = Instantiate(node, parent);
                var newPos = hit.point;
                newPos.y = 0.01f;
                nd.transform.position = newPos;
                nd.name = "Node" + i++;
            }
        }
    }
}
