using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public List<Transform> bubbles;
    public float smoothTime = 0.5f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    public float targetZoom;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
        if (bubbles.Count == 0)
            return;

        RemoveMissingBubbles();

        // Move();
        Zoom();
    }

    void RemoveMissingBubbles()
    {
        for (int i = bubbles.Count - 1; i >= 0; i--)
        {
            if (bubbles[i] == null || !bubbles[i].gameObject.activeSelf)
            {
                bubbles.RemoveAt(i);
            }
        }
    }
    
    // Move the camera
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = new Vector3(centerPoint.x, centerPoint.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    public void SetTargetZoom(float zoom)
    {
        targetZoom = Mathf.Clamp(zoom, minZoom, maxZoom);
    }

    // Zoom the camera
    void Zoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(bubbles[0].position, Vector3.zero);
        foreach (Transform bubble in bubbles)
        {
            bounds.Encapsulate(bubble.position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (bubbles.Count == 1)
        {
            return bubbles[0].position;
        }

        var bounds = new Bounds(bubbles[0].position, Vector3.zero);
        foreach (Transform bubble in bubbles)
        {
            bounds.Encapsulate(bubble.position);
        }

        return bounds.center;
    }
}

