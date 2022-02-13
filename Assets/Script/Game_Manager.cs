using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager ins;
    public Slider bossHealthBar;
    public BossControl Boss;
    public DestroyableObject boss_des;
    private void Awake()
    {
        ins = this;
    }
    public void EndGame()
    {
        //show UI endgame
        DialogueUI.instance.ShowDialog("the sorcerer has been destroyed, the curse has been breaked forever !!! :)");
        DialogueUI.instance.action = () =>
        {
            UIManager.instance.panelCredit.SetActive(true);
        };
    }
    public void InitBossFight()
    {
        DialogueUI.instance.ShowDialog("kill the sorcerer, pay attention to the magic he uses, when he runs out of energy is the best time to counterattack.");
        DialogueUI.instance.action = () => {
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.maxValue = boss_des.heathTrap;
            bossHealthBar.value = boss_des.heathTrap;
            bossHealthBar.gameObject.SetActive(true);
            Boss.isfight = true;
        };
        
    }
    public void updateBossHealthBar(int healthBoss)
    {
        bossHealthBar.value = healthBoss;
    }
}
