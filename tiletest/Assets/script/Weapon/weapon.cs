using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : armed
{
    public float attackrank;

    public float spped;

    public GameObject hand;

    public Vector3 startRota;
    public Vector3 endRota;

    public bool move = false;

    public void Update()
    {
        if (move)
        {
            MoveWeapon();
        }
    }

    public void MoveWeapon() {

        this.transform.Translate(Vector3.up* spped * Time.deltaTime);
    }

    public override void Init()
    {
        base.Init();
        attackrank = 0;
    
    }

    public new void Init(string i_armedName, armedcategory i_category, int i_power, int i_defense, float attackTime)
    {
        base.Init(i_armedName, i_category, i_power, i_defense);
        this.attackrank = attackTime;
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (move) { 
            if (collision.tag.Contains("Enemy"))
            {
                Destroy(this.gameObject);
                collision.GetComponent<enemy>().Downhp(power, 1);
                Destroy(this.gameObject);
            }

            if (collision.tag.Contains("well"))
            {
                Destroy(this.gameObject);

            }
        }
    }

   
}
