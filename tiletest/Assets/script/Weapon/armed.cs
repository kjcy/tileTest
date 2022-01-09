using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armed : MonoBehaviour
{
    //장비 이름
    public string armedName;
    //장비 카테고리
    public enum armedcategory{NULLcategory,Weapon, Armor, Ornaments};
    public armedcategory category;
    //장비 공격력
    public int power;
    //장비 방어력
    public int defense;


   public virtual void Init()
    {
        armedName = "nullarmed";
        category = armedcategory.NULLcategory;
        power = 0;
        defense = 0;
    }
    

    public virtual void Init(string i_armedName, armedcategory i_category, int i_power, int i_defense)
    {
        armedName = i_armedName;
        category = i_category;
        power = i_power;
        defense = i_defense;
    }


}
