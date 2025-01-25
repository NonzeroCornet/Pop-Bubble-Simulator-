using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePadInput : MonoBehaviour
{
    public BubbleController bubble;
    public GameObject bubbleArrow;

    public float dirThreshold;
    public float flickThreshold;

    public float gamepadForceMultiplier;

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
        // Gamepad support
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            var leftStick = gamepad.leftStick.ReadValue(); // Direction
            var rightStick = gamepad.rightStick.ReadValue(); // Flick to apply force

            Debug.Log("Left stick: " + leftStick + " Right stick: " + rightStick);

            if (leftStick.magnitude > dirThreshold)
            {
                Debug.Log("Left stick magnitude: " + leftStick.magnitude);

                var rightStickDiff = rightStick - prevRightStick;

                if (rightStickDiff.magnitude > flickThreshold)
                {
                    var force = leftStick * rightStickDiff.magnitude * gamepadForceMultiplier;
                    bubbleRb.AddForceAtPosition(force, Vector2.zero);

                    Debug.Log("Force applied: " + force);
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
}

