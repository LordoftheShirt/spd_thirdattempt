using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private float ThrowForce = 0f;
    [SerializeField] private AudioClip[] trampSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Player") || otherObject.CompareTag("Enemy"))
        {
            int randomValue = Random.Range(0, trampSound.Length);
            // print(randomValue);
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(trampSound[randomValue], 0.5f);

            // Defines playerRigidBody by getting the rigidbody component of an object with tag "Player"
            Rigidbody2D playerRigidbody = otherObject.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);

            // IDEA: Add an if here to check if y velocity is negative to add up -y + jumpForce. A prior jump onto the tramp will thereby give more force than simply walking onto it

            playerRigidbody.AddForce(new Vector2(ThrowForce, jumpForce));

            // Goes into components, look for Animator, accesses an already set Trigger called "Jump" in animator
            GetComponent<Animator>().SetTrigger("Jump");
        }

    }
}
