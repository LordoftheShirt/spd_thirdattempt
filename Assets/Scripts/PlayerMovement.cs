using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip jumpSound, pickupSound, hurtSound, deathSound;
    [SerializeField] private GameObject cherryParticle, jumpParticles;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, orangeHealth, redHealth;
    [SerializeField] private TMP_Text cherryText;

    private float horizontalValue;
    private bool isGrounded;
    private bool canMove;
    private float rayDistance = 0.25f;
    private int startingHealth = 5;
    private int currentHealth = 0;
    public int cherryCollected = 0;

    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;
  

    // Start is called before the first frame update
    void Start()
    {
        // has the script go down the list of components until reaching "Rigidbody2D" then saves it in the rgbd variable
        canMove = true;
        currentHealth = startingHealth;
        // Below occurs int to string conversion through "" trickery! Suppose another way of casting.
        cherryText.text = "" + cherryCollected;
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // void Update updates as fast as possible. Therefore player input is wanted here.
        horizontalValue = Input.GetAxis("Horizontal");
        

        if (horizontalValue < 0) 
        { 
            FlipSprite(true);
        }
        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
    }
    // FixedUpdate updates only as fast as the speed of the game(?)
    private void FixedUpdate()
    {
        // Alternative write: !canMove
        if(canMove == false)
        {
            return;
        }

        // applys a velocity. Vector2 = 2D. Takes in horizontalvalue for x. Is told to keep its current value on y.
        rgbd.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Cherry"))
        {
            Destroy(otherObject.gameObject);
            cherryCollected++;
            cherryText.text = "" + cherryCollected;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupSound, 0.5f);

            // Particle explosion. Instantiate 5 of 11. Quaternion rotation: .identity tells it to keep its identity, add nothing.
            Instantiate(cherryParticle, otherObject.transform.position, Quaternion.identity);

            //this below gives health. Migrate to another fruit later maybe?
            if(currentHealth < 5)
            {
                currentHealth++;
                UpdateHealthBar();
            }

        }
    }

    private void FlipSprite(bool direction)
    {
        // the saved SpriteRenderer in the rend variable grabs at the bool flipX and direction parameter either turns it on or off
        rend.flipX = direction;
    }

    private void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce));
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(jumpSound, 0.3f);

        // ALTERNATIVE: swap out Quaternion.identity with jumpParticles.transform.localRotation to keep an upwards looking cone and now a z+ one.
        Instantiate(jumpParticles, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(hurtSound, 0.5f);

        if (currentHealth <= 0)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(deathSound, 0.5f);
            Respawn();
            
        }
    }

    public void TakeKnockback(float knockbackForce, float upwards)
    {
        canMove = false;
        rgbd.AddForce(new Vector2 (knockbackForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
        // Invoke function allows the called upon method in string to trigger after a specific set amount of time: 0.25 seconds.
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = spawnPosition.position;
        rgbd.velocity = Vector2.zero;
        // Get the above from killzone and removed otherObject.GetComponent<Rigidbody2D>(). from .veliocty = Vector2.zero;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if (currentHealth <= 2)
        {
            fillColor.color = orangeHealth;
            if (currentHealth == 1)
            {
                fillColor.color = redHealth;
            }
        }
        else 
        {
            fillColor.color = greenHealth;
        }
    }

    private bool CheckIfGrounded()
    {
        // Below: leftFoot shoots out something, downwards, what distance, check if it hits whatIsGround
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, new Vector2(0, -1), rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, new Vector2(0, -1), rayDistance, whatIsGround);
        // an alternative: Vector2.down rather than new vector2(0, -1)

        // Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.5f);
        // Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.5f);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }

       
    }
}
    
    


