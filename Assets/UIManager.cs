using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject PanelStartGame;
    public GameObject PanelMain;
    public GameObject panelCredit;
    public GameObject Guide;
    public GameObject PanelendGame;
    public void StartGame()
    {
        PanelStartGame.SetActive(false);
        PanelMain.SetActive(true) ;
        DialogueUI.instance.ShowDialog("you have been cursed by an ancient witch . you will never see the light of day again !");
        DialogueUI.instance.action = () =>
        {
            DialogueUI.instance.ShowDialog("Go down to the castle cellar to find enough items and break the curse");
            Guide.SetActive(true);
        };
    }
    public void Quit()
    {
        
    }
    public void buttonReplay()
    {
        SceneManager.LoadScene(0);
    }
}
