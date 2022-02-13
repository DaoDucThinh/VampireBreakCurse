using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBatControl : MonoBehaviour
{
    public float spdFly;
    public SpriteRenderer batSprite;

    private void Update()
    {
        if (DialogueUI.instance.isDialog) return;
        CalculateFly();
    }

    private void CalculateFly()
    {
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * spdFly * Time.deltaTime;
        transform.position += Vector3.up * Input.GetAxis("Vertical") * spdFly * Time.deltaTime;
        if (Input.GetAxis("Horizontal") > 0)
        {
            batSprite.flipX = true;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            batSprite.flipX = false;
        }
    }
}
