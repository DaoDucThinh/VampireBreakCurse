using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : DestroyableObject
{
    public PlayerControl player;
    public float damage;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public float moveSpd;
    public dir direc;
    public Rigidbody2D rb2d;
    public float forceHit;
    public Vector3 originPos; //điểm gốc
    public playerState state;
    bool isTakeHit;
    bool isAttack;
    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        originPos = transform.position;
    }
    public override void Hit(int damage,Action action)
    {
        base.Hit(damage,null);
        isTakeHit = true;
        if(direc == dir.left)
        {
            rb2d.AddForce(Vector2.right * forceHit);
        }
        else
        {
            rb2d.AddForce(Vector2.left * forceHit);
        }
        anim.Play("TakeHit", LayerMask.GetMask("default"), -1);
    }
    public void ResetState()
    {
        state = playerState.Idle;
        isTakeHit = false;
        anim.Play("Idle");
    }
    public void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.left,1f,LayerMask.GetMask("Player"));
        if(hit.collider != null)
        {
            hit.collider.GetComponent<PlayerControl>().GetHit();
        }
    }
    private void Update()
    {
        if (isTakeHit) return;
        
        //Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        if (Vector2.Distance(transform.position,player.transform.position) <= 1f)//cách người chơi khoảng 0.5 thì tung đòn chém
        {
            state = playerState.Attack;
            anim.Play("Attack");
        }
        if(state != playerState.Attack) AutoChase();
    }

    private void AutoChase()
    {
        //chase player if player in the zone
        if (Vector3.Distance( player.transform.position , originPos) <= 3)
        {
            //skeleton chase and attack player
            //chạy tới gần player đứng im chém. sau khi chém xong thì lại chase tiếp
            LookAtPlayer();
        }
        else
        {
            //quay ngược đít trở về vị trí cũ
            TurnBackToOriginPos();
        }
    }

    private void TurnBackToOriginPos()
    {
        //spriteRenderer.flipX = !spriteRenderer.flipX;
        if (transform.position.x < originPos.x && Vector2.Distance(transform.position, originPos) > 0.2f)  //left
        {
            transform.position += Vector3.right * moveSpd * Time.deltaTime;
            spriteRenderer.flipX = false;
            direc = dir.right;
            anim.Play("Walk");
        }
        else if (transform.position.x > originPos.x && Vector2.Distance(transform.position, originPos) > 0.2f)
        {
            //if(transform.position.x == originPos.x)
            transform.position += Vector3.left * moveSpd * Time.deltaTime;
            spriteRenderer.flipX = true;
            direc = dir.left;
            anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }
    }

    private void LookAtPlayer()
    {
        if (transform.position.x > player.transform.position.x)//player o ben trai
        {
            spriteRenderer.flipX = true;
            direc = dir.left;
            transform.position += Vector3.left * moveSpd * Time.deltaTime;
            anim.Play("Walk");
        }
        else
        {
            spriteRenderer.flipX = false;
            direc = dir.right;
            transform.position += Vector3.right * moveSpd * Time.deltaTime;
            anim.Play("Walk");
        }

    }
}
