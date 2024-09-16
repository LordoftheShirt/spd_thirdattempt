using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Animator animator;
    private bool hasPlayedAnimation = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.CompareTag("Player") && !hasPlayedAnimation) 
        {
            hasPlayedAnimation = true;
            animator.SetTrigger("Move");
        }
    }
}
