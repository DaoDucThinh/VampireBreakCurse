using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameObject doorToBoss;
    public GameObject napHam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //nhay man hinh den. lam` sau
            PlayerControl.instance.transform.position = new Vector3(7.25f, -8.23999977f, 0);
            transform.position = new Vector3(7.25f, -8.23999977f, 0);
            GetComponent<Collider2D>().enabled = false;
            doorToBoss = GameObject.FindGameObjectWithTag("DoorToBoss");
            StartCoroutine(I_GoToBossFight());
            //dong nap ham
            GameObject.FindGameObjectWithTag("napHam").GetComponent<SpriteRenderer>().enabled = true ;
            GameObject.FindGameObjectWithTag("napHam").GetComponent<BoxCollider2D>().enabled = true;
            GameObject.FindGameObjectWithTag("BigHealth").GetComponent<SpriteRenderer>().enabled = true ;
            GameObject.FindGameObjectWithTag("BigHealth").GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    IEnumerator I_GoToBossFight()
    {
        yield return new WaitForSeconds(1f);
        DialogueUI.instance.ShowDialog("The witch is in the next room. Go ahead and destroy him");
        //unlockbossfight
        doorToBoss.SetActive(false);
    }
}
