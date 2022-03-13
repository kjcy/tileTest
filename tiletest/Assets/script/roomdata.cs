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
    /// 입력을 하는 함수
    /// </summary>
    /// <param name="around">호출된 room</param>
    /// <param name="count">호출된 room 이 이 room 의 어느방향인지,wellcount 를 사용 </param>
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

   
    //room 의 주변에 다른 room 이 없다면 그 모서리를 벽으로 생성
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
        //roomdata 의 welldata 을 비교했을때 none 이 하나라면 none well 방향을 , 아니라면 wellcount.NONE 을 반환한다.
        wellcount temp = CheckOneWellroom();

        //NONE 이 아니라면 none 이 하나라는 뜻이니 그 방향을 문으로 변경하고 ture 를 반환한다.
        if(temp != wellcount.NONE)
        {
            welldatalist[(int)temp] = welldata.door;
            return true;
        }
        else
        {
            return false;//없다면 false 를 반환
        }



    }



    public wellcount CheckOneWellroom()
    {
        int checkW = 0;

        wellcount r_wellcount = wellcount.NONE;

        for(int i = 0; i < 4; i++)
        {
            //4벽면중 1면만 none 일경우 checkW의 값은 1이 되고 1이될때 방향이 r_wellcount에 들어간다.
            if(welldatalist[i] == welldata.none)
            {
                checkW += 1;
                r_wellcount = (wellcount)i;
            }
        }


        if(checkW == 1)//checkW 값이 1이라면 저장항 방향을 반환
        {
            return r_wellcount;
        }
        else
        {
            return wellcount.NONE;//아닐경우는 none을 반환한다.
        }
            

        
    }



}
