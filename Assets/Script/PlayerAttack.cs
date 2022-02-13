using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour
{
    public PlayerControl playerControl;
    public float distance;
    public int damage;
    public GameObject effect;
    public GameObject effectSlash;
    public void Attack()
    {
        
        if(playerControl.faceTo == dir.left)
        {
            GameObject slash = Instantiate(effectSlash, transform.position, Quaternion.identity);
            slash.GetComponent<SpriteRenderer>().flipX = true;
            slash.transform.DOMove(transform.position + Vector3.left * distance,0.33f);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left,distance,LayerMask.GetMask("destroyable"));
            Debug.DrawLine(transform.position, transform.position + Vector3.left * distance, Color.red,1f);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.tag);
                //triển khai VFX tại điểm giao nhau
                Instantiate(effect, hit.point, Quaternion.identity);
                //gọi hàm - máu
                hit.collider.GetComponent<DestroyableObject>().Hit(damage,null);
            }
        }
        else if(playerControl.faceTo == dir.right)
        {
            GameObject slash = Instantiate(effectSlash, transform.position, Quaternion.identity);
            
            slash.transform.DOMove(transform.position + Vector3.right * distance, 0.33f);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, distance, LayerMask.GetMask("destroyable"));
            Debug.DrawLine(transform.position, transform.position + Vector3.right * distance, Color.red,1f);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.tag);
                //triển khai VFX tại điểm giao nhau
                //gọi hàm - máu
                hit.collider.GetComponent<DestroyableObject>().Hit(damage,null);
            }
        }
    }
}
