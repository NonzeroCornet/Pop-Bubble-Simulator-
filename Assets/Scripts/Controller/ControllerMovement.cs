using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePadInput : MonoBehaviour
{
    public BubbleController bubble;
    public GameObject bubbleArrow;

    public GameObject bubbleObject;

    public float dirThreshold;
    public float flickThreshold;

    public float gamepadForceMultiplier;

    public float impulseScale = 0.002f;
    public float velocityClamp = 1.36f;

    private Rigidbody2D bubbleRb;

    private Vector2 prevRightStick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleRb = bubble.GetComponent<Rigidbody2D>();

        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Debug.Log("Gamepad found");
        }
        else
        {
            Debug.Log("Gamepad not found");
            bubbleObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Gamepad support
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            var leftStick = gamepad.leftStick.ReadValue(); // Directaion
            var rightStick = gamepad.rightStick.ReadValue(); // Flick to apply force

            Debug.Log("Left stick: " + leftStick + " Right stick: " + rightStick);

            if (leftStick.magnitude > dirThreshold)
            {
                Debug.Log("Left stick magnitude: " + leftStick.magnitude);

                var rightStickDiff = rightStick - prevRightStick;

                if (rightStickDiff.magnitude > flickThreshold)
                {
                    var impulse = leftStick * rightStickDiff.magnitude * gamepadForceMultiplier;
                    bubbleRb.AddForce(impulse, ForceMode2D.Impulse);

                    Debug.Log("Force applied: " + impulse);
                }

                bubbleArrow.SetActive(true);
                var arrowAngle = Mathf.Atan2(leftStick.y, leftStick.x) * Mathf.Rad2Deg;
                bubbleArrow.transform.rotation = Quaternion.Euler(0f, 0f, arrowAngle);
            }
            else
            {
                bubbleArrow.SetActive(false);
            }

            prevRightStick = rightStick;
        }
        else
        {
            Debug.Log("Gamepad not found");
        }
    }

    void LateUpdate()
    {
        bubbleRb.linearVelocity = Vector2.ClampMagnitude(bubbleRb.linearVelocity, velocityClamp);
    }
}

