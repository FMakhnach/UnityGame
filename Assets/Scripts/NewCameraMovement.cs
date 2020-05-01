using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class NewCameraMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private float wasdSpeed;
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float draggingSpeed;
    [SerializeField]
    private float rotationSpeed;
    private Vector3 move;
    [SerializeField]
    private LayerMask floorMask;
    [SerializeField]
    private NavMeshAgent agent;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
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
        move = move.normalized * wasdSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.transform.position + move);

        // Scrolling for scaling
        if (Input.mouseScrollDelta.y > 0.1f && camera.transform.position.y > 10f
         || Input.mouseScrollDelta.y < -0.1f && camera.transform.position.y < 23f)
        {
            rigidbody.MovePosition(rigidbody.transform.position + Input.mouseScrollDelta.y * Vector3.down * scrollSpeed * Time.fixedDeltaTime);
        }

        // Dragging rotation
        if (Input.GetMouseButton(1))
        {
            var rot = rigidbody.rotation.eulerAngles;
            rot.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rigidbody.MoveRotation(Quaternion.Euler(rot));
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, floorMask))
            {
                Vector3 move = hit.point;
                move.y = transform.position.y;
                agent.SetDestination(move);
            }
        }*/
    }
    private Vector3 CalculateNewPositionForClickMove(Vector3 clickPoint)
    {
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, 1000f, floorMask))
        {
            Vector3 moveVector = clickPoint - hit.point;
            moveVector.y = 0f;
            return moveVector;
        }
        return Vector3.zero;
    }
    private IEnumerator NiceMovement(Vector3 moveVector)
    {
        long numOfIterations = (int)(moveVector.magnitude / (wasdSpeed * Time.fixedDeltaTime) + 1);
        //print(numOfIterations);
        Vector3 delta = moveVector * 1f * Time.fixedDeltaTime;
        for (long i = 0; i < numOfIterations; i++)
        {
            transform.Translate(delta);
            //rigidbody.MovePosition(rigidbody.transform.position + Vector3.forward);
            yield return null;
        }
    }
}
