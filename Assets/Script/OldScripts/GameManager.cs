using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public float moveSpd;
    public GameObject[] Background;
    private void Start()
    {
        StartCoroutine(I_SpawnFireBall(SpawnPosRandom(),5f));
        StartCoroutine(I_SpawnFireBall(SpawnPosRandom(), 2f));
        StartCoroutine(I_SpawnFireBall(SpawnPosPlayer(), 3.5f));
    }

    private Vector3 SpawnPosPlayer()
    {
        return player.transform.position + Vector3.right * 30;
    }

    private Vector3 SpawnPosRandom()
    {
        return new Vector3(30,UnityEngine.Random.Range(9f,-9f),0);
    }

    private void Update()
    {
        BackGroundMove();
    }

    private void BackGroundMove()
    {
        foreach (GameObject bg in Background)
        {
            if (bg.transform.position.x <= -44.7f)
            {
                bg.transform.position = new Vector3(44.7f,0,0);
            }
        }
        //x đến vị trí -36 là quay lại vị trí trên đầu
        Background[0].transform.position += Vector3.left * moveSpd * Time.deltaTime;
        Background[1].transform.position += Vector3.left * moveSpd * Time.deltaTime;
    }
    /*
     * cách 5s sẽ spawn 1 quả cầu lửa random vị trí
     * cách 3.5s sẽ spawn 1 quả cầu lửa thẳng vào vị trí player
     */
    IEnumerator I_SpawnFireBall(Vector3 spawnPos,float timeSpawn)
    {
        bool a = true;
        while (a)
        {
            yield return new WaitForSeconds(timeSpawn);
            ObjectPooler.instance.SpawnFromPool("FireBall", spawnPos, Quaternion.identity);
        }
    }
}
