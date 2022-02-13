using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserBeam : MonoBehaviour
{
    public GameObject laserBeam;
    public GameObject lightning;
    [ContextMenu("Active")]
    public void Active()
    {
        laserBeam.SetActive(true);
        laserBeam.transform.localScale = new Vector3(0.64f, 1, 1);
        laserBeam.transform.DOScaleX(1,0.8f).OnComplete(()=> {
            laserBeam.transform.DOScaleX(0, 0.5f).OnComplete(() => {
                StartCoroutine(ActiveLightning());
            });
        });
    }
    IEnumerator ActiveLightning()
    {
        lightning.SetActive(true);
        lightning.GetComponent<Animator>().Play("lightning");
        yield return new WaitForSeconds(1f);
        lightning.SetActive(false);
        laserBeam.SetActive(false);
        //sau 1s thi` setactivelightining false && setobject laserbeam false
    }
}
