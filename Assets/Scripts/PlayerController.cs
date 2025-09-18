using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;


    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Animator animator;

    [SerializeField] private float moveSpeed;

    // Mana system
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    // Health system
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    // Destroy
    [SerializeField] private GameObject destroyEffect;

    // Boost system
    public float boost = 1f;
    private float boostPower = 5f;
    private bool isBoosting = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Get the Rigidbody component attached to the player GameObject
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        energy = maxEnergy;
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);

        health = maxHealth;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            // Go to Edit -> Project Settings -> Input Manager to see the input axes
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            // Check in the animator for the parameters "moveX" and "moveY"

            animator.SetFloat("moveX", directionX);
            animator.SetFloat("moveY", directionY);


            // Create a new Vector2 based on the input axes
            playerDirection = new Vector2(directionX, directionY).normalized;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                EnterBoost();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                ExitBoost();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);

        if (isBoosting)
        {
            // Consume mana 
            if (energy >= 0.2f) energy -= 0.2f;
            else ExitBoost();
        }
        else
        {
            // Recover mana
            if (energy < maxEnergy) energy += energyRegen;
        }
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    private void EnterBoost()
    {
        if (energy > 10)
        {
            animator.SetBool("boosting", true);
            boost = boostPower;
            isBoosting = true;
        }

    }

    private void ExitBoost()
    {
        animator.SetBool("boosting", false);
        boost = 1f;
        isBoosting = false;
    }


    // Placeholder for collision with enemies or obstacles
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        if (health <= 0)
        {
            boost = 1f;
            gameObject.SetActive(false);
            // Clone the destroy effect at the player's position and rotation
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }

}
