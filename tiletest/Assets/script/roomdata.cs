using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomdata : MonoBehaviour
{
    public enum welldata { none, well, door };

    public int pointX;
    public int pointY;

    public Transform wellParent;
    public enum wellcount { right, left, up, down ,NONE = 999};
    public welldata[] welldatalist = new welldata[4];

    public Transform[] aroundwell = new Transform[4];

    public Vector2 maxsize = new Vector2(15,15);

    static int seed = 100;
    public void Reset()
    {
        Random.InitState(System.DateTime.Now.Second);
        wellParent = this.transform.GetChild(1);
        for (int i = 0; i < 4; i++)
        {
            welldatalist[i] = welldata.none;
        }
    }

    public int Conversewellcount(wellcount count)
    {

        wellcount returncount = wellcount.NONE;
        switch (count)
        {
            case wellcount.left:
                returncount=wellcount.right;
                break;
            case wellcount.right:
                returncount = wellcount.left;
                break;
            case wellcount.up:
                returncount = wellcount.down;
                break;
            case wellcount.down:
                returncount = wellcount.up;
                break;
        }
        return (int)returncount;
    }

  

   public bool Checkwelldata(int dirwell , roomdata aroundwell)
    {
      /*  if(welldatalist[dirwell] == welldata.door)
        {
            if (welldatalist[dirwell] == aroundwell.welldatalist[Conversewellcount((wellcount)dirwell)]) return true;
        }*/

        if (aroundwell == null)
        {
            return true;
        }else if (welldatalist[dirwell] == aroundwell.welldatalist[Conversewellcount((wellcount)dirwell)]) return true;

        return false;
    }

    /// <summary>
    /// �Է��� �ϴ� �Լ�
    /// </summary>
    /// <param name="around">ȣ��� room</param>
    /// <param name="count">ȣ��� room �� �� room �� �����������,wellcount �� ��� </param>
    public void Inputaround(roomdata roomdata,Transform around,int count)
    {
        aroundwell[count] = around;
        roomdata.Inputaround(this.transform, (int)Conversewellcount((wellcount)count));
    }

    public void Inputaround(Transform around, int count)
    {
        aroundwell[count] = around;

    }


    /*public void Setwell()
    {

        int rand;
        int rand1;
        Random.InitState(seed += Mathf.RoundToInt(Random.Range(-5,5)));
        rand = Random.Range(0, 10000) % 4;
        do
        {
            rand1 = Random.Range(0, 10000) % 4;
        } while (rand == rand1);
        welldatalist[rand] = welldata.none;
        welldatalist[rand1] = welldata.none;

      *//*  for (int i = 0; i < 4; i++)
        {
            if (welldatalist[i] == welldata.none) continue;

            rand = Random.Range(0,100)%10;
            if (rand == 0)
            {
                welldatalist[i] = welldata.door;
            }
        }*//*
        Updatewell();
    }*/


    public void Updatewell()
    {
        for(int i = 0; i < 4; i++)
        {
            switch (welldatalist[i])
            {
                case welldata.none:

                    break;
                case welldata.well:
                    wellParent.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    break;
                case welldata.door:
                    wellParent.GetChild(i).GetChild(1).gameObject.SetActive(true);
                    break;
            }
        }
    }

   
    //room �� �ֺ��� �ٸ� room �� ���ٸ� �� �𼭸��� ������ ����
    public void Setaroundwell()
    {
        for(int i = 0; i < 4; i++)
        {
            if(aroundwell[i] == null)
            {
                welldatalist[i] = welldata.well;
            }

        }

        Updatewell();
    }


    public bool CreateBoxRoom()
    {
        //roomdata �� welldata �� �������� none �� �ϳ���� none well ������ , �ƴ϶�� wellcount.NONE �� ��ȯ�Ѵ�.
        wellcount temp = CheckOneWellroom();

        //NONE �� �ƴ϶�� none �� �ϳ���� ���̴� �� ������ ������ �����ϰ� ture �� ��ȯ�Ѵ�.
        if(temp != wellcount.NONE)
        {
            welldatalist[(int)temp] = welldata.door;
            return true;
        }
        else
        {
            return false;//���ٸ� false �� ��ȯ
        }



    }



    public wellcount CheckOneWellroom()
    {
        int checkW = 0;

        wellcount r_wellcount = wellcount.NONE;

        for(int i = 0; i < 4; i++)
        {
            //4������ 1�鸸 none �ϰ�� checkW�� ���� 1�� �ǰ� 1�̵ɶ� ������ r_wellcount�� ����.
            if(welldatalist[i] == welldata.none)
            {
                checkW += 1;
                r_wellcount = (wellcount)i;
            }
        }


        if(checkW == 1)//checkW ���� 1�̶�� ������ ������ ��ȯ
        {
            return r_wellcount;
        }
        else
        {
            return wellcount.NONE;//�ƴҰ��� none�� ��ȯ�Ѵ�.
        }
            

        
    }



}
