using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemdata : MonoBehaviour
{
    public int id;

    public string Itemname;

    public int maxhaveItem;

    public Sprite itemsprite;

    public float dropcount;//0.1~ 1.1;

   public void datainit(int i_id, string i_name,int i_maxhaveItem)
    {
        id = i_id;
        Itemname = i_name;
        maxhaveItem = i_maxhaveItem; 
    }

    public bool Dropitem()
    {
        float incount = Random.value;
        if(dropcount > incount)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}
