using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrap : MonoBehaviour
{
    public dir direc;
    public float force;
    public void PushPlayer()
    {
        PlayerControl.instance.rb.AddForce(Vector2.left * force);
    }
}
