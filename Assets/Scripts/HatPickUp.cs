using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatPickUp : MonoBehaviour
{
    private Sprite localhatRenderer;
    [SerializeField] private GameObject hatSlot;


    void Start()
    {
       localhatRenderer = GetComponent<SpriteRenderer>().sprite;
    }

    public void HatInteract()
    {
        print("PRESSED");
        hatSlot.gameObject.GetComponent<SpriteRenderer>().sprite = localhatRenderer;
        Destroy(this.gameObject);
    }
}
