using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    // [SerializeField] private Transform spawnPosition;
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Player"))
        {
            // The problem I had: The Killzone simply teleported the player back to spawnpoint, however, it never did actually deal any damage to the player.
            // The player's health never went down to, or below 0, meaning, the "respawn" method never played. That's where the death audio is! NO AUDIO!

            //otherObject.transform.position = spawnPosition.position;
            //otherObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            otherObject.gameObject.GetComponent<PlayerMovement>().TakeDamage(5);
        }
    }
}
