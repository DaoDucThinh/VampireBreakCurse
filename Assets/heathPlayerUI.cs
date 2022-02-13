using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heathPlayerUI : MonoBehaviour
{
    public static heathPlayerUI instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] healths;
    public void updateHeathUI()
    {
        for(int i = 0; i < healths.Length; i++)
        {
            if (i <= PlayerManager.instance.health - 1)
            {
                healths[i].SetActive(true);
            }
            else healths[i].SetActive(false);
        }
    }
}
