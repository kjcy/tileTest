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
        //�ܺ� �� ����
        Outerwall();
        //�ڽ��� ���� ����
        setBoxroom();
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
        
        room[pointX, pointY].Updatewell();


    }

  //room ���� ����� �� �ֵ��� �������ִ� �Լ�
    public void CreateRoom()
    {
        int x, y;

        int outcount = 0;
        
        GameObject temp = Instantiate(roomPri, this.transform);
       
        temp.GetComponent<roomdata>().Reset();
        //temp.GetComponent<roomdata>().Setwell();
        do
        {
            x = Random.Range(0, maxpointX);
            y = Random.Range(0, maxpointY);
            if (room[x, y] != null) continue;//ĭ�� room �� �ִٸ� �н���

                    if (room[x + 1 >= maxpointX ? x : x + 1, y] != null ||
                        room[x - 1 < 0 ? x : x - 1, y] != null ||
                        room[x, y + 1 >= maxpointY ? y : y + 1] != null ||
                        room[x, y - 1 < 0 ? y : y - 1] != null
                    )//4�����߿� �Ѱ��̶� room �� ������
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
                

            //�ڸ��� ã�� �� ������ ���� �缳�� �� �ٽ� ã�ƺ���.
          //  temp.GetComponent<roomdata>().Setwell();
            outcount++;
        } while (outcount<500);
    }

    //room�����Ϳ��� ���� Ȯ���ϴ� ���α׷�
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


    public void setBoxroom()
    {
        int x =0, y =0;
        int savecount = 0;//������ġ
        do
        {
            if (++savecount > 100) break;


            do {
                x = Random.Range(0, maxpointX);
                y = Random.Range(0, maxpointY);
            } while (room[x,y] == null);//random ���� ������ ���� room �� ���ٸ� �ٽ� 


        } while (!room[x, y].CreateBoxRoom());//Boxroom �� ������ �����ϸ� true �� ��ȯ�Ѵ�. ���� �Ҷ����� �ݺ����� ������ ����  do while�� ������.
        room[x, y].Updatewell();//�� �����Ѱ��� ������Ʈ
    }


    // Update is called once per frame
    void Update()
    {
        
    }



}
