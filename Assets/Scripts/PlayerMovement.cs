using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody rb;
    Vector3 moveDirection;
    Vector3 mousePosition;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GatherInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GatherInput()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        if (moveDirection.magnitude > 0.1f)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        if (plane.Raycast(ray, out float distance))
        {
            mousePosition = ray.GetPoint(distance);
        }
    }

    void Move()
    {
        // Movement
        rb.linearVelocity = moveDirection * moveSpeed;

        // Rotation toward cursor
        Vector3 direction = mousePosition - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = targetRotation;
        }
    }
}


