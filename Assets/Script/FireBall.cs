using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum dir
{
    left,right,top,down
}
public class FireBall : MonoBehaviour
{
    public float moveSpd;
    public dir direction;
    public GameObject graphic;
    public Vector3 direcFire; //cái này nếu null thì k lgi còn nếu có thì sẽ bắn theo hướng đó
    Vector3 direc;
    
    private void Update()
    {
        switch (direction)
        {
            case dir.down:
                direc = Vector3.down;
                graphic.transform.eulerAngles = new Vector3(0,0,90);
                break;
            case dir.top:
                direc = Vector3.up;
                break;
            case dir.left:
                direc = Vector3.left;
                break;
            case dir.right:
                direc = Vector3.right;
                break;
        }
        if(direcFire != Vector3.zero)
        {
            direc = -direcFire.normalized;
        }
        transform.position += direc * moveSpd * Time.deltaTime;
    }
    
}
