using UnityEngine;
using UnityEngine.InputSystem;

public class GamePadInput : MonoBehaviour
{
    public BubbleController bubble;

    public float flickThreshold;

    public float forceMultiplier;

    private Rigidbody2D bubbleRb;

    private Vector2 prevRightStick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleRb = bubble.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            var leftStick = gamepad.leftStick.ReadValue(); // Direction
            var rightStick = gamepad.rightStick.ReadValue(); // Flick to apply force

            //Debug.Log("Left stick: " + leftStick + " Right stick: " + rightStick);

            var rightStickDiff = rightStick.magnitude - prevRightStick.magnitude;

            if (rightStickDiff > flickThreshold)
            {
                var force = leftStick * rightStickDiff * forceMultiplier;
                bubbleRb.AddForceAtPosition(force, Vector2.zero);

                Debug.Log("Force applied: " +  force);
            }

            prevRightStick = rightStick;
        }
    }
}
