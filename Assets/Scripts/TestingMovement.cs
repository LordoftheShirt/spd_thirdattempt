using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rb;
    public Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // It is rb.velocity.y that is fucking up the gravity. Fix it.
        movement = new Vector2(Input.GetAxis("Horizontal"), rb.velocity.y);
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
