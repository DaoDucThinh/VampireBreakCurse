using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManaegr : MonoBehaviour
{
    public static MapManaegr instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject DoorToBoss;
}
