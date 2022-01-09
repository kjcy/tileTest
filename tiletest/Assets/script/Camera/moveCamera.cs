using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class moveCamera : MonoBehaviour
{
    public GameObject Player;

    public Tilemap Background;

    public Vector2 maxsize;

    public Vector2 minsize;

    public float xCameraSize;

    public float yCameraSize;
    // Start is called before the first frame update
    void Start()
    {
        yCameraSize = Camera.main.orthographicSize;
        xCameraSize = yCameraSize * Camera.main.aspect;
        maxsize = new Vector2(Background.size.x / 2, Background.size.y / 2);
        minsize = new Vector2(-Background.size.x / 2, -Background.size.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        MovetoPlay();
    }


    public void MovetoPlay()
    {
        float x = Mathf.Clamp(Player.transform.position.x,minsize.x+xCameraSize,maxsize.x-xCameraSize);
        float y = Mathf.Clamp(Player.transform.position.y, minsize.y+yCameraSize, maxsize.y-yCameraSize);

        Debug.LogFormat("world :  {0}", Background.size);

        this.transform.position = new Vector3(x, y, -10);
    }
}
