using UnityEngine;

public class MapController : MonoBehaviour
{
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        MoveMap(Vector2.left, speed);
    }

    public void MoveMap(Vector2 direction, float speed)
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
    }
}

