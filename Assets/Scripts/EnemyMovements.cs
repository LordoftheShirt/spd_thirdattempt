using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class EnemyMovements : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float bounciness = 100;
    [SerializeField] private float knockbackForce = 480f;
    [SerializeField] private float upwardForce = 200f;
    [SerializeField] private int damageGiven = 1;
    [SerializeField] private GameObject spawnDeath;

    EnemyDeathSound NeededScript;
    //EnemyDeathAnimation enemyDeathAnimation;
    

    private SpriteRenderer rend;
    
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        
        NeededScript = GameObject.FindGameObjectWithTag("EnemyOverlord").GetComponent<EnemyDeathSound>();
        //enemyDeathAnimation = GameObject.FindGameObjectWithTag("EnemyDies").GetComponent<EnemyDeathAnimation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed < 0)
        {
            rend.flipX = true;
        }

        if (moveSpeed > 0)
        {
            rend.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if(otherObject.gameObject.CompareTag("enemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }

        if (otherObject.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (otherObject.gameObject.CompareTag("Player"))
        {
            otherObject.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven);

            if(otherObject.transform.position.x > transform.position.x)
            {
                otherObject.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
            }
            else
            {
                otherObject.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Audio doesn't work (I think because gameObject gets destroyed so fast it doesn't even have the time to play) 
            // audioSource.pitch = Random.Range(0.8f, 1.2f);
            // audioSource.PlayOneShot(poppedSound, 0.5f);
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounciness));
            Destroy(gameObject);
            NeededScript.enemyDies();

            // calls upon script in gameObject "DeathAnimation" and runs FlipSprite script depending on enemies movement (where he's going, facing.)
            Instantiate(spawnDeath, transform.position, Quaternion.identity);
            //if (moveSpeed < 0)
            //{
            //    enemyDeathAnimation.FlipSprite();
            //}
        }
    }

}
