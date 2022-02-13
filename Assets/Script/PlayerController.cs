using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float moveSpd;
    public float swingSpd;
    public float downSpd;

    Rigidbody2D rb;
    [SerializeField]bool isPress;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Swing();
    }
    private void Move()
    {
        transform.position += new Vector3(moveSpd * Time.deltaTime * Input.GetAxis("Horizontal"),0 ,0 );
    }
    private void Swing()
    {
        if (Input.GetMouseButton(0))
        {
            //người chơi đang giữ vào màn hình thì cho player bay lên
            transform.position += new Vector3(0,swingSpd * Time.fixedDeltaTime);
        }
        else
        {
            transform.position -= new Vector3(0, downSpd * Time.fixedDeltaTime);
        }
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        
    }
}
