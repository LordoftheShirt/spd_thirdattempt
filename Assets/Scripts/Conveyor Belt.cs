using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    Rigidbody2D rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        Vector2 pos = rBody.position;
        rBody.position += Vector2.right * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }
}
