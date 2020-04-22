using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float wasdSpeed;
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float draggingSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private new Transform camera;

    private void Update()
    {
        // WASD movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * wasdSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * wasdSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * wasdSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * wasdSpeed * Time.deltaTime);
        }

        // Scrolling for scaling
        camera.Translate(Input.mouseScrollDelta.y * Vector3.forward * scrollSpeed * Time.deltaTime);

        // Dragging rotation
        if (Input.GetMouseButton(1))
        {
            var rot = transform.rotation.eulerAngles;
            rot.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(rot);
        }

        // Dragging movement
        // Not so dummy though
        if (Input.GetMouseButton(2))
        {
            Vector3 pos = transform.position;
            float yPart = transform.rotation.eulerAngles.y;
            // 0.0111111111 is for 90^-1
            float modifier1 = Mathf.Abs(yPart - 180) * 0.0111111111f - 1;
            // Basically this is similar to ||x - 3| - 2| - 1
            float modifier2 = Mathf.Abs(Mathf.Abs(yPart - 270) * 0.0111111111f - 2) - 1;
            pos.x -= (
                modifier1 * Input.GetAxis("Mouse X")
                - modifier2 * Input.GetAxis("Mouse Y"))
                * draggingSpeed * Time.deltaTime;
            pos.z -= (
                modifier2 * Input.GetAxis("Mouse X")
                + modifier1 * Input.GetAxis("Mouse Y"))
                * draggingSpeed * Time.deltaTime;
            transform.position = pos;
        }
    }
}