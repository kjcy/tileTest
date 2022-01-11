using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomdata : MonoBehaviour
{
    public enum welldata { none, well, door };

    public Transform wellParent;
    public enum wellcount { right, left, up, down };
    public welldata[] welldatalist = new welldata[4];

    public Transform[] aroundwell = new Transform[4];

    public Vector2 maxsize = new Vector2(15,15);
    public void Reset()
    {
        Random.InitState(System.DateTime.Now.Second);
        wellParent = this.transform.GetChild(1);
        for (int i = 0; i < 4; i++)
        {
            welldatalist[i] = welldata.well;
        }
        

    
    }

    /// <summary>
    /// 입력을 하는 함수
    /// </summary>
    /// <param name="around">호출된 room</param>
    /// <param name="count">호출된 room 이 이 room 의 어느방향인지,wellcount 를 사용 </param>
    public void Inputaround(Transform around,int count)
    {
        aroundwell[count] = around;
    }


    public void Setwell()
    {

        int rand;

        rand = Random.Range(1, 4);

        welldatalist[rand] = welldata.none;

        for (int i = 0; i < 4; i++)
        {
            if (welldatalist[i] == welldata.none) continue;

            rand = Mathf.FloorToInt(Random.value);
            if (rand != 0)
            {
                welldatalist[i] = welldata.door;
            }
        }
        Updatewell();
    }


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

}
