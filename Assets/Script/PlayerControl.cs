using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum playerState
{
    Idle,Attack,Death,Jump,Shield,Run,None
}
public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public Game_Manager gm;
    private void Awake()
    {
        instance = this;
    }
    public playerState state;
    public dir faceTo;
    public float spdMove;
    public float spdMoveAttack;
    public float spdMoveJump;
    public SpriteRenderer playerGraphic;
    public Animator playerAnim;
    public Rigidbody2D rb;
    public float forceJump;
    public bool isGround;
    public float heath;
    public bool isInvincible;

    float timeAttack;
    private void Update()
    {
        if (DialogueUI.instance.isDialog)
        {
            ChangeState(playerState.Idle);
            return;
        }
        if (state == playerState.Death) return;
        CalculateMove();
        timeAttack += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C))
        {
            timeAttack = 0;
            ChangeState(playerState.Attack);
        }
        else if (!Input.GetKeyDown(KeyCode.C) && timeAttack > 0.3f)
        {
            if(state == playerState.Attack) ChangeState(playerState.None);

        }

        if (Input.GetKeyDown(KeyCode.V) && isGround)
        {
            rb.AddForce(Vector3.up * forceJump);
            ChangeState(playerState.Jump);
            isGround = false;
        }
        else if (!Input.GetKeyDown(KeyCode.V) && isGround)
        {
            if(state == playerState.Jump) ChangeState(playerState.Idle);
        }
    }
    private void CalculateMove()
    {
        if(Input.GetAxis("Horizontal") < 0f)
        {
            playerGraphic.flipX = true;
            faceTo = dir.left;
            if (state == playerState.Attack || state == playerState.Jump )
            {
                
            }
            else
            {
                ChangeState(playerState.Run);
            }
        }
        else if (Input.GetAxis("Horizontal") > 0f)
        {
            playerGraphic.flipX = false;
            faceTo = dir.right;
            if (state == playerState.Attack || state == playerState.Jump )
            {
                
            }
            else
            {
                ChangeState(playerState.Run);
            }
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            if (state == playerState.Attack || state == playerState.Jump )
            {
                
            }
            else
            {
                ChangeState(playerState.Idle);
            }
        }

        if(state == playerState.Attack) transform.position += Vector3.right * Input.GetAxis("Horizontal") * spdMoveAttack * Time.deltaTime;
        else if(state == playerState.Jump) transform.position += Vector3.right * Input.GetAxis("Horizontal") * spdMoveJump * Time.deltaTime;
        else transform.position += Vector3.right * Input.GetAxis("Horizontal") * spdMove * Time.deltaTime;

    }
    public void ChangeState(playerState ps)//change animation
    {
        state = ps;
        playerAnim.Play(state.ToString());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("groundMove"))
        {
            isGround = true;
            ChangeState(playerState.Idle);
        }
        if (collision.gameObject.CompareTag("groundMove"))
        {
            //hàm này để phát hiện xem người chơi đã đứng lên trên ground move chưa

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("trap"))
        {
            GetHit();
        }
        else if (collision.CompareTag("TriggerBossFight"))
        {
            collision.gameObject.SetActive(false);
            CameraFollow.instance.isFollow = false;
            
            CameraFollow.instance.GetComponent<Camera>().fieldOfView = 30.86f;
            
            
            MapManaegr.instance.DoorToBoss.SetActive(true);
            gm.InitBossFight();
        }
    }
    IEnumerator I_Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }
    public void GetHit()
    {
        if (isInvincible) return;
        Debug.Log("gethit");
        //ChangeState(playerState.Death);
        playerAnim.Play("Shield");
        PlayerManager.instance.health -= 1;
        heathPlayerUI.instance.updateHeathUI();
        if (faceTo == dir.left)
        {
            rb.AddForce(Vector2.right * 150);
        }
        else
        {
            rb.AddForce(Vector2.left * 150);
        }
        StartCoroutine(I_Invincible());
    }
}
