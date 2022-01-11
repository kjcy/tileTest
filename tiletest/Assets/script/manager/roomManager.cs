using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    public roomdata[,] room = new roomdata[5,5];

    public int startpointX;
    public int startpointY;
    public GameObject roomPri;

   
    // Start is called before the first frame update
    void Start()
    {
        Setstartpoint();
    }

    public void Setstartpoint()
    {
        startpointX = Random.Range(0, 4);
        startpointY = Random.Range(0, 4);

        GameObject temp = Instantiate(roomPri, new Vector2(startpointX * 15,startpointY*15), Quaternion.identity, this.transform);
        room[startpointX, startpointY] = temp.GetComponent<roomdata>();
        room[startpointX, startpointY].Reset();
        for(int i=0;i<4;i++) room[startpointX, startpointY].welldatalist[i] = roomdata.welldata.none;
        room[startpointX, startpointY].Updatewell();


    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
