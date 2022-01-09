using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemlistUI : MonoBehaviour
{
   // public List<Itemdata> itemdatas;

    public chestItem openbox;


    public void Update()
    {
        updateUI();
    }

    public void updateUI()
    {
        for(int i = 0; i < openbox.inputItemlist.Count; i++)
        {
            if (openbox.inputItemlist[i] != null)
            {//박스안쪽의 물건을 들고간후 그 위치를 다시 그리지 않을려고
                transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = openbox.inputItemlist[i].itemsprite;
            }
            else
            {
                transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = null;
            }
        }
    }

    public void ButtonItem(int index)
    {
       
        if (index >= openbox.inputItemlist.Count) return;
       
        if (openbox.inputItemlist[index].id == 0) return;
      
        Player.player.playerdata.GetItem(openbox.inputItemlist[index].id, 1);

        openbox.inputItemlist[index] = null;
        transform.GetChild(0).GetChild(index).GetComponent<Image>().sprite = null;
    }

    public void CloseUI()
    {
        openbox.open = false;
        
        openbox.Destroybox();
        Destroy(this.transform.gameObject);
    }

}
