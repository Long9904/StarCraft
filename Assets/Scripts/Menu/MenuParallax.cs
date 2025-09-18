using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundImageWidth;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        // Get the actual width of the sprite in world units
        Vector3 scale = transform.lossyScale;
        backgroundImageWidth = (sprite.texture.width / sprite.pixelsPerUnit) * scale.x;
    }

    // Delta time is the completion time in seconds since the last frame
    // Delta time depends on the machine performance
    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);

        // If the background has moved completely off the screen, reset its position to create a looping effect
        if (Mathf.Abs(transform.position.x) > backgroundImageWidth)
        {
            transform.position = new Vector3(0f, transform.position.y);
        }
    }
}
