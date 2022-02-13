using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum fireType
{
    banToa,BanTarget
}
public enum BossState
{
    CastSkill,OutEnergy
}
public class BossControl : DestroyableObject
{
    public SpriteRenderer BossGraphic;
    public Animator animBoss;
    public DestroyableObject bossHealth;
    public Vector3 maxZoneMove;
    public Vector3 minZoneMove;
    public float spdMove;

    public GameObject skeletons;
    public bool isfight;
    public float healthBoss = 1000;
    public bool isCastSkill;
    public bool isBossHurt;
    PlayerControl playerControl;

    List<Action> Skills;
    public int numSkillFall;
    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        
    }
    private void Update()
    {
        TurnFaceToPlayer();
        if (!isfight) return;
        if (!isCastSkill && !isBossHurt)
        {
            Move(CastSkillRandom);
            numSkillFall += 1;
            isCastSkill = true;
            StartCoroutine(I_CastSkill(4f));
        }


        if (isBossHurt) return; 
        if(numSkillFall >= 3)
        {
            //boss roi vao` trang thai danh' dc.
            StartCoroutine(I_BossHurt());
        }
    }
    IEnumerator I_CastSkill(float time)
    {
        yield return new WaitForSeconds(time);
        isCastSkill = false;
    }
    IEnumerator I_BossHurt()
    {
        transform.DOMoveY(-7f,2f);
        Debug.Log("Boss Hurt");
        isBossHurt = true;
        yield return new WaitForSeconds(10f);
        isBossHurt = false;
        numSkillFall = 0;
        
    }
    /*
     * cho con boss tung chiêu  nối tiếp chiêu
     * tung 3 chiêu liên tiếp rồi gục xuống
     */
    private void TurnFaceToPlayer()
    {
        if(playerControl.transform.position.x < transform.position.x)
        {//left
            BossGraphic.flipX = true;
        }
        else if(playerControl.transform.position.x > transform.position.x)
        {//right
            BossGraphic.flipX = false;
        }
    }
    public void Move(Action onComplete)
    {
        //di chuyển ngẫu nhiên trong zone move xong dừng lại
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(minZoneMove.x, maxZoneMove.x), UnityEngine.Random.Range(minZoneMove.y, maxZoneMove.y), 0);
        float timeMove = Vector2.Distance(transform.position, randomPos) / spdMove;
        
        transform.DOMove(randomPos,timeMove).OnComplete(()=>
        {
            onComplete();
        }).SetEase(Ease.InOutBack);
    }
    #region skill
    [ContextMenu("RainFireBall")]
    public void RainFireBall()
    {
        StartCoroutine(I_FireBall(1f));
    }
    IEnumerator I_FireBall(float timeWait)
    {
        
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(timeWait);
            int number = 4;
            while (number > 0)
            {
                number -= 1;
                Vector3 randomPos = new Vector3(UnityEngine.Random.Range(minZoneMove.x, maxZoneMove.x), UnityEngine.Random.Range(minZoneMove.y, maxZoneMove.y), 0);
                GameObject obj = ObjectPooler.instance.SpawnFromPool("FireBall", randomPos + Vector3.up * 20, Quaternion.identity);
                obj.GetComponent<FireBall>().moveSpd = UnityEngine.Random.Range(25, 30);
                obj.GetComponent<FireBall>().direction = dir.down;
                obj.GetComponent<FireBall>().direcFire = Vector3.zero;
            }
        }
    }
    [ContextMenu("Fire")]
    public void FireFireBalls()
    {
        //if(type == fireType.banToa)
        StartCoroutine(I_FireToa(1f,2));
    }
    public void FireFireBalls1()
    {

        //else if (type == fireType.BanTarget)
        StartCoroutine(I_FireTarget(0.5f));
    }
    IEnumerator I_FireTarget(float time)
    {
        int turn = 5;
        for(int i = 0; i < turn; i++)
        {
            yield return new WaitForSeconds(time);
            Vector3 dir = transform.position - playerControl.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            GameObject obj = ObjectPooler.instance.SpawnFromPool("FireBall", transform.position, Quaternion.AngleAxis(angle,Vector3.forward));
            obj.GetComponent<FireBall>().direcFire = dir;
            obj.GetComponent<FireBall>().moveSpd = 8;
        }
    }
    IEnumerator I_FireToa(float time, int turn)
    {
        for(int i = 0; i < turn; i++)
        {
            yield return new WaitForSeconds(time);
            for (int y = -2; y < 3; y++)
            {
                Vector3 dir = transform.position - (playerControl.transform.position + Vector3.up * y * 2 );
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                GameObject obj = ObjectPooler.instance.SpawnFromPool("FireBall", transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                obj.GetComponent<FireBall>().direcFire = dir;
            }
            
        }
    }
    [ContextMenu("Thunder Bolt")]
    public void ThunderBolt()
    {
        float diffThunder = 2;
        Vector3 startPos = new Vector3(UnityEngine.Random.Range(minZoneMove.x -2, minZoneMove.x),0,0);
        //hiện 1 tia lade thông báo
        //sau khi hiện tia lade thì tia sét đánh thẳng từ trên trời xuống vị trí hiện lade
        for(int i = 0; i < 10; i++)
        {
            Vector3 spawnPos = new Vector3(startPos.x + diffThunder * i, -6f);
            GameObject obj = ObjectPooler.instance.SpawnFromPool("LaserBeam", spawnPos, Quaternion.identity);
            obj.GetComponent<LaserBeam>().Active();
        }
        
    }
    [ContextMenu("Skeletons")]
    public void SummonSkeletons()
    {
        int numberSpawn = 3;
        Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(minZoneMove.x, maxZoneMove.x), -5f, 0);
        for(int i = 0; i < numberSpawn; i++)
        {
            Instantiate(skeletons, spawnPos, Quaternion.identity);
        }
    }
    [ContextMenu("Bats")]
    public void SummonBats()
    {

    }
    public void CastSkillRandom()
    {
        int i = UnityEngine.Random.Range(0, 3);
        switch (i)
        {
            case 0:
                RainFireBall();
                break;
            case 1:
                ThunderBolt();
                break;
            case 2:
                FireFireBalls1();
                break;
        }
            
    }
    #endregion
    public override void Hit(int damage,Action action)
    {
        animBoss.Play("TakeHit", LayerMask.GetMask("default"), -1);
        base.Hit(damage,()=> {
            if(heathTrap <= 0)
            {
                Game_Manager.ins.EndGame();
            }
        });
        Game_Manager.ins.updateBossHealthBar(heathTrap);
    }
}
