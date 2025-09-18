using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundImageWidth;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Vector3 scale = transform.lossyScale;
        backgroundImageWidth = (sprite.texture.width / sprite.pixelsPerUnit) * scale.x;
    }

    // Delta time is the completion time in seconds since the last frame
    // Delta time depends on the machine performance
    void Update()
    {
        float moveX = (moveSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if (Mathf.Abs(transform.position.x) > backgroundImageWidth)
        {
            transform.position = new Vector3(0f, transform.position.y);
        }
    }
}
