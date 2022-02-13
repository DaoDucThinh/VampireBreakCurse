using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroundChild : MonoBehaviour
{
    public MoveGround moveGroundParent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveGroundParent.isPlayerOn = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveGroundParent.isPlayerOn = false;
        }
    }
}
