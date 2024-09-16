using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHat : MonoBehaviour, IInteractable
{
    private SpriteRenderer hatRender;

    void Start()
    {
        hatRender = GetComponent<SpriteRenderer>();
    }


  public void Interact()
    {



        print(5);
    }
}
