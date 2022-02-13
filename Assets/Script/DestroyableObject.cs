using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyableObject : MonoBehaviour
{
    public int heathTrap;
    public void DestroyTrap()
    {
        if(heathTrap <= 0) Destroy(gameObject);
    }
    public virtual void Hit(int damage,Action action)
    {
        heathTrap -= damage;
        if (action != null)
        {
            action();
        }
        
        if (heathTrap <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
