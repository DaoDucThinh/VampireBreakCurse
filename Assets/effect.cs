using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
