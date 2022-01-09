using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileManager : MonoBehaviour
{
    public Tilemap well;

   

    public Tile[] bluedoor = new Tile[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mousedown();
    }

    public void Mousedown()
    {
        if (Input.GetMouseButtonDown(1)) { 
        Findtile();
        }
    }
    
    public TileBase Findtile()
    {
        TileBase tileBase = null;
        try { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit2D = Physics2D.Raycast(ray.origin,Vector3.zero);

            if(this.well == hit2D.transform.GetComponent<Tilemap>())
            {
                this.well.RefreshAllTiles();
                int x, y;
                //ray 의 위치에 해당하는 타일맵의 좌표를 들고오는 코드
                x = this.well.WorldToCell(ray.origin).x;
                y = this.well.WorldToCell(ray.origin).y;

                Vector3Int v3Int = new Vector3Int(x, y, 0);

                //Debug.LogFormat("{0}, {1}", x, y);

                tileBase = well.GetTile(v3Int);

                well.SetTileFlags(v3Int, TileFlags.None);
                for (int i = 0; i < bluedoor.Length; i++)
                {
                    if (tileBase.name == bluedoor[i].name && Player.player.playerdata.Itemlist[2] > 0)
                    {
                        Player.player.playerdata.Itemlist[2] -= 1;
                        well.SetTile(v3Int, null);

                    }
                }
                //if (tileBase.name == bluedoor[0].name && Player.playerdata.Itemlist[2]>0)
                //{
                //    Matrix4x4 mat = Matrix4x4.Scale(new Vector3(1, 1, 1));
                //    Player.playerdata.Itemlist[2] -= 1;//열쇠 소비
                    
                //    well.SetTile(v3Int, bluedoor[1]);
                //    well.SetTransformMatrix(v3Int, mat);
                //}
                //else if(tileBase.name == bluedoor[1].name)
                //{
                //    Matrix4x4 mat = Matrix4x4.Scale(new Vector3(1, 2, 1));
                //    well.SetTile(v3Int, bluedoor[0]);
                //    well.SetTransformMatrix(v3Int, mat);
                //}
            }

        }
        catch (NullReferenceException)
        {
            return null;
        }
        return tileBase;
    }

    

}
