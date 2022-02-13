using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum itemType
{
    one,two,three,four
}
public class item : MonoBehaviour
{
    public itemType type;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //cộng vào manager + hiển thị lên UI
            //gọi hiệu ứng visual chúc mừng something
            //destroy bản thân đi
            Destroy(this.gameObject);
            foreach(itemUI i in itemsUI.instance.items)
            {
                if (i.type == type)
                {
                    i.isOwn = true;
                    itemsUI.instance.itemPos = this.transform.position;
                    break;
                }
            }
            itemsUI.instance.UpdateItemUI();
        }
    }
}
