using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePadInput : MonoBehaviour
{
    public BubbleController bubble;
    public GameObject bubbleArrow;

    public float dirThreshold;
    public float flickThreshold;
    public float flickThresholdMouse;

    public float forceMultiplier;

    private Rigidbody2D bubbleRb;

    private Vector2 prevMousePos;
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

            if (leftStick.magnitude > dirThreshold)
            {
                //Debug.Log("Left stick: " + leftStick + " Right stick: " + rightStick);

            var rightStickDiff = (Vector2)rightStick - prevRightStick;

            if (rightStickDiff.magnitude > flickThreshold)
            {
                var force = (Vector2)leftStick * rightStickDiff.magnitude * forceMultiplier;
                bubbleRb.AddForce(force, ForceMode2D.Impulse);

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

        // Mouse support
        if (Mouse.current != null)
        {
            var mousePos = Mouse.current.position.ReadValue();

            if (IsMouseOverBubble())
            {
                var mousePosDiff = (Vector2)mousePos - prevMousePos;

                Debug.Log("Right stick diff: " + mousePosDiff);

                if (mousePosDiff.magnitude > flickThresholdMouse)
                {
                    var force = mousePosDiff * forceMultiplier;
                    bubbleRb.AddForceAtPosition(force, Vector2.zero);

                    Debug.Log("Force applied: " +  force);
                }
            }

            prevMousePos = (Vector2)mousePos;
        }
    }

    private bool IsMouseOverBubble()
    {
        var mousePos = Mouse.current.position.ReadValue();
        var bubblePos = bubble.transform.position;

        return Vector2.Distance(mousePos, bubblePos) < 100f;
    }
}

