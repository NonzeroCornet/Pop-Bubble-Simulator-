using UnityEngine;

public class Doggo : MonoBehaviour
{
    public GameObject[] doggoParts;

    private int snapFrame = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (snapFrame == 0)
        {
            doggoParts[0].transform.eulerAngles = new Vector3(0, 0, 357.58f);
            doggoParts[1].transform.eulerAngles = new Vector3(0, 0, 0);
            doggoParts[0].transform.localPosition = new Vector3(-0.85f, -3.66f, 0);
            doggoParts[1].transform.localPosition = new Vector3(-0.55f, -4f, 0.007f);
        }
        else if (snapFrame < 30) // Doubled the threshold
        {
            Snap();
        }
        else if (snapFrame < 230) // Doubled the threshold
        {
            Retract();
        }
        snapFrame++;
    }

    void Snap()
    {
        doggoParts[0].transform.Translate(Vector2.up * 32.5f * Time.deltaTime, Space.World); // Halved speed
        doggoParts[1].transform.Translate(Vector2.up * 32.5f * Time.deltaTime, Space.World); // Halved speed
        doggoParts[0].transform.Rotate(new Vector3(0, 0, -100) * Time.deltaTime, Space.World); // Halved rotation speed
        doggoParts[1].transform.Rotate(new Vector3(0, 0, 100) * Time.deltaTime, Space.World); // Halved rotation speed
    }

    void Retract()
    {
        doggoParts[0].transform.Translate(-Vector2.up * 25 * Time.deltaTime, Space.World); // Halved speed
        doggoParts[1].transform.Translate(-Vector2.up * 25 * Time.deltaTime, Space.World); // Halved speed
    }
}
