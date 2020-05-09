using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private new Camera camera;
    [SerializeField]
    private float wasdSpeed;
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float draggingSpeed;
    private Vector3 move;
    private float speedModifier;

    private Vector3 initPosition;
    [SerializeField]
    private float lowerXBound = -1000;
    [SerializeField]
    private float upperXBound = 1000;
    [SerializeField]
    private float lowerYBound = -1000;
    [SerializeField]
    private float upperYBound = 1000;
    [SerializeField]
    private float lowerZBound = -1000;
    [SerializeField]
    private float upperZBound = 1000;
    [SerializeField]
    private float minSlope = -20f;
    [SerializeField]
    private float maxSlope = 20f;


    public void ScaleSpeed(float modifier)
    {
        float mod = modifier / speedModifier;
        speedModifier = modifier;
        wasdSpeed *= mod;
        scrollSpeed *= mod;
        rotationSpeed *= mod;
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main;
        speedModifier = 1f;
        initPosition = transform.position;

        // Slight optimization.
        draggingSpeed *= Time.fixedDeltaTime;
        rotationSpeed *= Time.fixedDeltaTime;
        wasdSpeed *= Time.fixedDeltaTime;
        scrollSpeed *= Time.fixedDeltaTime;
    }
    private void FixedUpdate()
    {
        CheckForOutOfLevel();
        // WASD movement
        move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move += rigidbody.transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move -= rigidbody.transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move -= rigidbody.transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += rigidbody.transform.right;
        }
        move = move.normalized * wasdSpeed;
        rigidbody.MovePosition(rigidbody.transform.position + move);

        // Scrolling for scaling
        if (Input.mouseScrollDelta.y > 0.1f && camera.transform.position.y > lowerYBound
         || Input.mouseScrollDelta.y < -0.1f && camera.transform.position.y < upperYBound)
        {
            rigidbody.MovePosition(rigidbody.transform.position + Input.mouseScrollDelta.y * Vector3.down * scrollSpeed);
        }

        // Dragging rotation
        if (Input.GetMouseButton(1))
        {
            var rot = rigidbody.rotation.eulerAngles;
            rot.y += Input.GetAxis("Mouse X") * rotationSpeed;
            rigidbody.MoveRotation(Quaternion.Euler(rot));

            rot = camera.transform.rotation.eulerAngles;
            rot.x = Mathf.Clamp(
                rot.x - Input.GetAxis("Mouse Y") * rotationSpeed * 0.25f,
                minSlope, maxSlope);
            camera.transform.rotation = Quaternion.Euler(rot);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 pos = rigidbody.position;
            float yPart = rigidbody.rotation.eulerAngles.y;
            // 0.0111111111 is for 90^-1
            float modifier1 = Mathf.Abs(yPart - 180) * 0.0111111111f - 1;
            // Basically this is similar to ||x - 3| - 2| - 1
            float modifier2 = Mathf.Abs(Mathf.Abs(yPart - 270) * 0.0111111111f - 2) - 1;
            // Magic
            float xInput = Mathf.Clamp(Input.GetAxis("Mouse X"), -4f, 4f);
            float yInput = Mathf.Clamp(Input.GetAxis("Mouse Y"), -4f, 4f);
            float xModifier = modifier1 * xInput - modifier2 * yInput;
            float yModifier = modifier2 * xInput + modifier1 * yInput;
            pos.x -= xModifier * draggingSpeed;
            pos.z -= yModifier * draggingSpeed;
            rigidbody.MovePosition(pos);
        }
    }
    private void CheckForOutOfLevel()
    {
        Vector3 p = transform.position;
        if (p.x >= upperXBound || p.x <= lowerXBound
            || p.z >= upperZBound || p.z <= lowerZBound)
        {
            transform.position = initPosition;
        }
        if (p.y >= upperYBound)
        {
            p.y = upperYBound;
            transform.position = p;
        }
    }
}
