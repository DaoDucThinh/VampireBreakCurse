using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform player;
    public bool isFollow = true;
    private void Awake()
    {
        instance = this;
        GetComponent<Camera>().fieldOfView = 28f;
    }
    private void Update()
    {
        if (!isFollow)
        {
            transform.position = new Vector3(19.7099991f, -4.73000002f, -13.3800001f);
        }
        else
        {
            transform.DOMove(new Vector3(player.position.x, player.position.y, transform.position.z), 1f).SetEase(Ease.Linear);
        }
        
    }
}
