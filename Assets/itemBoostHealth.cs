using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBoostHealth : MonoBehaviour
{
    public int health;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerManager.instance.health += health;
        heathPlayerUI.instance.updateHeathUI();
        if (PlayerManager.instance.health > 5) PlayerManager.instance.health = 5;

        Destroy(this.gameObject);
    }
}
