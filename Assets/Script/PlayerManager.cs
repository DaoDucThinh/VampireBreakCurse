using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }
    public int health = 5;
    public PlayerControl playerHuman;
    public PlayerBatControl playerBat;

    public GameObject graphicHuman;
    public GameObject graphicBat;

    public Rigidbody2D rb;
    public void ChangeMode(bool isHuman)
    {
        playerHuman.enabled = isHuman;
        graphicHuman.SetActive(isHuman);
        

        playerBat.enabled = !isHuman;
        graphicBat.SetActive(!isHuman);

        if (isHuman)
        {
            rb.gravityScale = 1f;
        }
        else
        {
            rb.gravityScale = 0.1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ChangeMode"))
        {
            ChangeMode(true);
            UIManager.instance.Guide.SetActive(false);
            DialogueUI.instance.ShowDialog("Press C to Attack & press V to jump");
        }
    }
    bool oneTime;
    private void Update()
    {
        if(health <= 0 && !oneTime)
        {
            oneTime = true;
            Debug.Log("die");
            PlayerControl.instance.ChangeState(playerState.Death);
            PlayerControl.instance.GetComponent<BoxCollider2D>().enabled = false;
            PlayerControl.instance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            UIManager.instance.PanelendGame.SetActive(true);
        }
    }
}
