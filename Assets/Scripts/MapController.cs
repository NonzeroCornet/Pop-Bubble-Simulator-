using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        MoveMap(new Vector2(horizontal, vertical), speed);
    }

    public void MoveMap(Vector2 direction, float speed)
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }
}

