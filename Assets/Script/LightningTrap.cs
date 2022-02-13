using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrap : MonoBehaviour
{
    public Animator animTrap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) animTrap.Play("Active");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) animTrap.Play("idle");
    }
}
