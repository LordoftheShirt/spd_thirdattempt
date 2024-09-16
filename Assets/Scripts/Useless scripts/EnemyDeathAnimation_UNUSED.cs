using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimation : MonoBehaviour
{
    private SpriteRenderer rend;


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void FlipSprite()
    {
        rend.flipX = true;
    }

}
