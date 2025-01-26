using UnityEngine;

public class MaterialColorCycler : MonoBehaviour
{
    public Color startColor;
    public Color endColor;

    private MeshRenderer meshRenderer;

    private float currT = 0;
    private bool increasing = true;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        var material = meshRenderer.material;

        if (increasing)
        {
            currT += Time.deltaTime;
            if (currT > 1f)
            {
                currT = 1f;
                increasing = false;
            }
        } else
        {
            currT -= Time.deltaTime;
            if (currT < 0f)
            {
                currT = 0f;
                increasing = true;
            }
        }

        material.color = Color.Lerp(startColor, endColor, currT);
    }
}