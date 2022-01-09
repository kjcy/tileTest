using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdata
{
    
    private int hpvalue;
    public int HP
    {
        get { return hpvalue; }
        set {
            hpvalue = value;
            CheckDead();
        }
    }

    private int coinvalue;
    public int COIN
    {
        get { return coinvalue; }
        set { coinvalue = value; }
    }

    private float speed;
    public float SPEED
    {
        get { return speed; }
        set { speed = value; }
    }

    private bool dead;

    

    public bool DEAD
    {
        get { return dead; }
    }

    /// <summary>
    /// ������ ����Ʈ, key = Itemdata.id , data= id�� �ش� ��
    /// </summary>
    public  Dictionary<int, int> Itemlist = new Dictionary<int, int>();


    public Playerdata() : this(0, 0,10)
    {

    }

    public Playerdata(int i_hp,int i_coin, float i_speed)
    {
        Init(i_hp, i_coin, i_speed);
    }

    public void Init(int i_hp,int i_coin, float i_speed)
    {
        dead = false;
        hpvalue = i_hp;
        coinvalue = i_coin;
        speed = i_speed;
    
        
    }

    public void GetItem(int index,int inputCount)
    {
        Debug.LogFormat("index : {0}", index);
        if (!Itemlist.ContainsKey(index))//���� Ű�� ã���� ���ٸ� 
        {
            Debug.Log("����Ʈ ����");
            Itemlist.Add(index, 0);//������ id �� ���� �����͸� �߰�
        }

        Itemlist[index] += inputCount;//���� ������ �߰�
    }

    private void CheckDead()
    {
        if (hpvalue <= 0)
        {
            dead = true;
        }
    }


}
