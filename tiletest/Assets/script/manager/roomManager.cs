using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    public roomdata[,] room = new roomdata[5,5];

    public int pointX;
    public int pointY;
    public GameObject roomPri;

   
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("{0}", room[0, 0] == null);
        Setstartpoint();
        for(int i = 0; i < 10; i++) { 
          CreateRoom();
        }
    }

    public void Setstartpoint()
    {
        pointX = Random.Range(0, 4);
        pointY = Random.Range(0, 4);

        GameObject temp = Instantiate(roomPri,new Vector2(pointX*15,pointY*15),Quaternion.identity, this.transform);
        room[pointX, pointY] = temp.GetComponent<roomdata>();
        room[pointX, pointY].Reset();
        room[pointX, pointY].pointX = pointX;
        room[pointX, pointY].pointY = pointY;
        for (int i=0;i<4;i++) room[pointX, pointY].welldatalist[i] = roomdata.welldata.none;
        room[pointX, pointY].Updatewell();


    }

  
    public void CreateRoom()
    {
        GameObject temp = Instantiate(roomPri, this.transform);
        temp.GetComponent<roomdata>().Setwell();
        for(int x = 0; x< 5; x++)
        {
            for(int y = 0; y < 5; y++)
            {
                if (room[x, y] != null)
                {
                  int check = room[x, y].Checksame(temp.GetComponent<roomdata>());

                    if (check == 999) continue;

                    //0:오른쪽, 1:왼쪽, 2:위, 3:아래
                    switch (check)
                    {
                        case 0:
                            temp.transform.position = new Vector2((x+1) * 15, y * 15);
                            room[x + 1, y] = temp.GetComponent<roomdata>();
                            room[x+1, y].pointX = x+1;
                            room[x+1, y].pointY = y;
                            break;
                        case 1:
                            temp.transform.position = new Vector2((x - 1) * 15, y * 15);
                            room[x - 1, y] = temp.GetComponent<roomdata>();
                           
                            room[x - 1, y].pointX = x - 1;
                            room[x - 1, y].pointY = y;
                            break;
                        case 2:
                            temp.transform.position = new Vector2(x * 15, (y + 1) * 15);
                            room[x, y + 1] = temp.GetComponent<roomdata>();
                            room[x, y+1].pointX = x;
                            room[x, y+1].pointY = y + 1;
                            break;
                        case 3:
                            temp.transform.position = new Vector2(x * 15, (y - 1) * 15);
                            room[x, y - 1] = temp.GetComponent<roomdata>();
                            room[x, y - 1].pointX = x;
                            room[x, y - 1].pointY = y - 1;
                            break;
                    }
                }
            }
        }
        temp.GetComponent<roomdata>().Updatewell();


    }

  

    // Update is called once per frame
    void Update()
    {
        
    }



}
