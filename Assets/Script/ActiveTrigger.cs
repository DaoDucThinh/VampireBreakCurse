using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrigger : MonoBehaviour
{
    public GameObject trap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trap.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
