using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb2d;

    [SerializeField]
    private Sprite[] sprites;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        rb2d = GetComponent<Rigidbody2D>();

        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);

        rb2d.AddForce(new Vector2(pushX, pushY));

    }

    void Update()
    {
        float moveX = (GameManager.Instance.worldSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);

        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }
}
