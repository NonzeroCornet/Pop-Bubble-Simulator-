using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float impulseScale = 0.002f;
    public float velocityClamp = 1.36f;

    private Vector3 lastMousePosition;
    private Vector3 mouseVelocity;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        mouseVelocity = (currentMousePosition - lastMousePosition) / Time.deltaTime;
        lastMousePosition = currentMousePosition;
    }

    void LateUpdate()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, velocityClamp);
    }

    void OnMouseOver()
    {
        Vector2 impulse = (Vector2)mouseVelocity * impulseScale;
        rb.AddForce(impulse, ForceMode2D.Impulse);
    }
}