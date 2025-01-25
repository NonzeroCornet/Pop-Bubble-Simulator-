using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    public Sprite bgSprite;
    public SpriteRenderer bgImage;

    private float offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = 0f;
        bgImage.sprite = bgSprite;
    }

    // Update is called once per frame
    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        bgImage.transform.position = new Vector2(offset, 0);

        if (offset >= bgSprite.rect.width)
        {
            offset = 0f;
        }
    }
}

