using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    public float rollSpd;
    

    private void Update()
    {
        transform.position += Vector3.left * rollSpd * Time.deltaTime;
        transform.eulerAngles += Vector3.forward * rollSpd * Time.deltaTime * 40;
    }
}
