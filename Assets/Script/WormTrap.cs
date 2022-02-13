using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormTrap : MonoBehaviour
{
    public GameObject[] worms;
    private void Awake()
    {
        StartCoroutine(I_ActiveTrap());
    }
    IEnumerator I_ActiveTrap()
    {
        foreach (GameObject worm in worms)
        {
            worm.SetActive(true);
            yield return new WaitForSeconds(0.8f);
        }
    }
}
