using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum point
{
    posA,posB
}
public class MoveGround : MonoBehaviour
{
    public GameObject moveGround;
    public Transform posA;
    public Transform posB;
    public float timeMove;
    public point p;

    [Header("MAKE PLAYER MOVE")]
    public float spdMove;
    public Vector3 direction;
    public bool isPlayerOn;
    [HideInInspector]public PlayerControl player;
    private void Awake()
    {
        Move();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
    }

    private void Move()
    {
        if (p == point.posA)
        {
            moveGround.transform.DOMove(posB.position, timeMove).OnComplete(() => {
                p = point.posB;
                Move();
            }).SetEase(Ease.Linear);
        }
        else
        {
            moveGround.transform.DOMove(posA.position, timeMove).OnComplete(() => {
                p = point.posA;
                Move();
            }).SetEase(Ease.Linear);
        }
        
    }
    private void Update()
    {
        if (!isPlayerOn) return;
        //tính tốc độ di chuyển chuyển ground
        CalculateSpeedGround();
        //tính hướng di chuyển của ground
        CalculateDirectionMove();
        //áp dụng tốc độ và hướng di chuyển đó của ground lên player
        ApplyToPlayer(player.transform);
    }
    

    private void ApplyToPlayer(Transform transformPl)
    {
        transformPl.position += direction * spdMove * Time.deltaTime;
    }

    private void CalculateDirectionMove()
    {
        if (p == point.posA)
        {
            direction = (posB.position - posA.position).normalized;
        }
        else
        {
            direction = (posA.position - posB.position).normalized;
        }
    }

    private void CalculateSpeedGround()
    {
        //công thức : v = quãng đường / thời gian
        spdMove = Vector3.Distance(posA.position, posB.position) / timeMove;
    }
}
