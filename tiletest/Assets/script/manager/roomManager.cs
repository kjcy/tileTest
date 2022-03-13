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
        //외부 벽 생성
        Outerwall();
        //박스방 문벽 생성
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

  //room 들이 연결될 수 있도록 설정해주는 함수
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
            if (room[x, y] != null) continue;//칸에 room 이 있다면 패스함

                    if (room[x + 1 >= maxpointX ? x : x + 1, y] != null ||
                        room[x - 1 < 0 ? x : x - 1, y] != null ||
                        room[x, y + 1 >= maxpointY ? y : y + 1] != null ||
                        room[x, y - 1 < 0 ? y : y - 1] != null
                    )//4방향중에 한곳이라도 room 이 있을때
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
                

            //자리를 찾을 수 없을때 벽을 재설정 후 다시 찾아본다.
          //  temp.GetComponent<roomdata>().Setwell();
            outcount++;
        } while (outcount<500);
    }

    //room데이터에서 벽을 확인하는 프로그램
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
        int savecount = 0;//안전장치
        do
        {
            if (++savecount > 100) break;


            do {
                x = Random.Range(0, maxpointX);
                y = Random.Range(0, maxpointY);
            } while (room[x,y] == null);//random 으로 생성된 곳에 room 이 없다면 다시 


        } while (!room[x, y].CreateBoxRoom());//Boxroom 을 생성에 성공하면 true 를 반환한다. 성공 할때까지 반복문을 돌리기 위해  do while을 돌린다.
        room[x, y].Updatewell();//벽 생성한것을 업데이트
    }


    // Update is called once per frame
    void Update()
    {
        
    }



}
