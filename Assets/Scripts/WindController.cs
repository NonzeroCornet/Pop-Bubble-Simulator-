using UnityEngine;

public class WindController : MonoBehaviour
{
    public float speed;
    public float windAmount;

    public GameObject bubble;

    void Update()
    {
        // move to the mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // the distance from the camera to the object
        Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = (objectPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // rotate to the cursor
        Vector3 targetDirection = objectPosition - transform.position;
        targetDirection.z = 0f; // ignore the z-axis
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, speed * Time.deltaTime);

        // only rotate in the xz plane
        Vector3 euler = transform.eulerAngles;
        euler.x = 0f;
        euler.y = 0f;
        transform.eulerAngles = euler;
    }
}



