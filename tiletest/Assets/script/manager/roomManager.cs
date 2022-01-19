using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    public roomdata[,] room;

    public int maxpointX;
    public int maxpointY;
    public int pointX;
    public int pointY;
    public GameObject roomPri;

    bool[] checkpoint = { false, false, false, false };
    // Start is called before the first frame update
    void Start()
    {
      
        maxpointX = 5;
        maxpointY = 5;

        room = new roomdata[maxpointX, maxpointY];
        Setstartpoint();
        for(int i = 0; i < 10; i++) { 
          CreateRoom();
        }
        Outerwall();
    }

    public void Setstartpoint()
    {
        pointX = maxpointX / 2;
        pointY = maxpointY / 2;

        GameObject temp = Instantiate(roomPri,new Vector2(pointX*15,pointY*15),Quaternion.identity, this.transform);
        room[pointX, pointY] = temp.GetComponent<roomdata>();
        room[pointX, pointY].Reset();
        room[pointX, pointY].pointX = pointX;
        room[pointX, pointY].pointY = pointY;
        for (int i=0;i<4;i++) room[pointX, pointY].welldatalist[i] = roomdata.welldata.none;
        room[pointX, pointY].Updatewell();


    }

  //���� �����ϰ� �ִ���� �����Ŀ�ִ� ��� => ��, �ܺ��� �Ű澲�� �ʴ´�.
    public void CreateRoom()
    {
        int outcount = 0;
        
        GameObject temp = Instantiate(roomPri, this.transform);
       
        temp.GetComponent<roomdata>().Reset();
        temp.GetComponent<roomdata>().Setwell();
        do
        {
            for (int x = 0; x < maxpointX; x++)
            {
                for (int y = 0; y < maxpointY; y++)
                {
                    if (room[x, y] != null) continue;//ĭ�� room �� �ִٸ� �н���

                    if (room[x + 1 >= maxpointX ? x : x + 1, y] != null ||
                        room[x - 1 < 0 ? x : x - 1, y] != null ||
                        room[x, y + 1 >= maxpointY ? y : y + 1] != null ||
                        room[x, y - 1 < 0 ? y : y - 1] != null
                    )
                    {

                        for (int i = 0; i < 4; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (x + 1 >= maxpointX)
                                    {
                                        checkpoint[i] = true;
                                        continue;
                                    }


                                    checkpoint[i] = temp.GetComponent<roomdata>().Checkwelldata(i, room[x + 1, y]);

                                    break;
                                case 1:
                                    if (x - 1 < 0)
                                    {
                                        checkpoint[i] = true;
                                        continue;
                                    }
                                    checkpoint[i] = temp.GetComponent<roomdata>().Checkwelldata(i, room[x - 1, y]);

                                    break;
                                case 2:
                                    if (y + 1 >= maxpointY)
                                    {
                                        checkpoint[i] = true;
                                        continue;
                                    }
                                    checkpoint[i] = temp.GetComponent<roomdata>().Checkwelldata(i, room[x, y + 1]);

                                    break;
                                case 3:
                                    if (y - 1 < 0)
                                    {
                                        checkpoint[i] = true;
                                        continue;
                                    }
                                    checkpoint[i] = temp.GetComponent<roomdata>().Checkwelldata(i, room[x, y - 1]);

                                    break;
                            }
                        }

                        if (checkpoint[0] && checkpoint[1] && checkpoint[2] && checkpoint[3])
                        {
                            room[x, y] = temp.GetComponent<roomdata>();
                            for (int j = 0; j < 4; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        if (x + 1 >= maxpointX) continue;
                                        if (room[x + 1, y] != null) room[x, y].Inputaround(room[x + 1, y], room[x + 1, y].gameObject.transform, j);
                                        break;
                                    case 1:
                                        if (x - 1 < 0) continue;
                                        if (room[x - 1, y] != null) room[x, y].Inputaround(room[x - 1, y], room[x - 1, y].gameObject.transform, j);
                                        break;
                                    case 2:
                                        if (y + 1 >= maxpointY) continue;
                                        if (room[x, y + 1] != null) room[x, y].Inputaround(room[x, y + 1], room[x, y + 1].gameObject.transform, j);
                                        break;
                                    case 3:
                                        if (y - 1 < 0) continue;
                                        if (room[x, y - 1] != null) room[x, y].Inputaround(room[x, y - 1], room[x, y - 1].gameObject.transform, j);
                                        break;
                                }
                            }
                            room[x, y].pointX = x;
                            room[x, y].pointY = y;
                            temp.transform.position = new Vector2(room[x, y].pointX * 15, room[x, y].pointY * 15);
                            return;
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                checkpoint[i] = false;

                            }

                        }
                    }
                }
            }

            //�ڸ��� ã�� �� ������ ���� �缳�� �� �ٽ� ã�ƺ���.
            temp.GetComponent<roomdata>().Setwell();
            outcount++;
        } while (outcount<500);
       


    }

    public void Outerwall()
    {
        for(int x=0;x<maxpointX; x++)
        {
            for(int y = 0; y < maxpointY; y++)
            {
                if(room[x,y] != null)
                room[x, y].Setaroundwell();
            }
        }
    }





    // Update is called once per frame
    void Update()
    {
        
    }



}
