using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class itemUI
{
    public GameObject item;
    public bool isOwn;
    public itemType type;
}
public class itemsUI : MonoBehaviour
{
    public static itemsUI instance;
    public GameObject teleport;

    public Vector3 itemPos;

    GameObject teleportCurrent;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public itemUI[] items;
    public void UpdateItemUI()
    {
        int allItems = 0;
        foreach (itemUI i in items)
        {
            if (i.isOwn) {
                i.item.GetComponent<Image>().color = Color.white;
                allItems += 1;

            }
            
        }
        if (allItems == 4)
        {
            OpenTeleport();
        }
    }

    private void OpenTeleport()
    {
        teleportCurrent = Instantiate(teleport, itemPos + Vector3.left , Quaternion.identity);
    }
}
