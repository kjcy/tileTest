using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public enum enemyrank { NULLrank, nomal, named, boss};

    public enemyrank e_rank;

    public int id;

    //몬스터 채력
    public float hp;

    public float power;

    public float defanse;

    [SerializeField]
    private GameObject[] itemtable = new GameObject[4];//여기서 한번 초기화 시켜줘야 찾을 수 있다.

    private List<Itemdata> droplist = new List<Itemdata>();//여기도

    static Vector2[] dir = {
        new Vector2(0, 1).normalized,
        new Vector2(0, -1).normalized ,
        new Vector2(1, 0).normalized ,
        new Vector2(-1, 0).normalized ,
        new Vector2(1, 1).normalized ,
        new Vector2(-1, 1).normalized ,
        new Vector2(1, -1).normalized ,
        new Vector2(-1, -1).normalized };

    public Vector2 navdir = Vector2.zero;

    public float speed = 1.5f;

    private float navTime;

    public GameObject deadbox;

    public void Start()
    {
        for(int i = 0; i < itemtable.Length; i++)
        {
            Itemdata data = itemtable[i].GetComponent<Itemdata>();
            if (data != null) { 
                if (data.Dropitem())
                {
                    droplist.Add(data);
                }
            }
        }
    }

    public void FixedUpdate()
    {
        AIenemy();
    }

    /// <summary>
    /// 기본 ai, 평소에는 8방향으로 ray 를 발사해 벽과의 거리를 찾아 가중치에 따라 방향으로 다니다가 player 가 거리에 들어온다면 플레이어 방향으로 추적(벽 넘어 추격x)
    /// </summary>
    public void AIenemy()
    {
        navTime += Time.deltaTime;
        if(navTime > 1f)
        {
            navTime -= 1f;
            navdir = Ray2d();
        }

        RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position, (Player.player.playerObject.transform.position - this.transform.position).normalized,10f);

       
        if (raycastHit2D.transform.CompareTag("Player"))
        {
            Debug.Log("플레이어 찾음");
            this.transform.Translate((Player.player.playerObject.transform.position - this.transform.position).normalized * speed * Time.deltaTime);
        }
        else { 

           this.transform.Translate(navdir * speed * Time.deltaTime);
        }
    }

    public Vector2 Ray2d()
    {
        Debug.Log("Ray2d작동중");
        Vector2 navdir = Vector3.zero;

        int layerMask = LayerMask.GetMask("well") | LayerMask.GetMask("enemy");

        Debug.LogFormat("{0}", layerMask);
        RaycastHit2D hit2d;
        for (int i = 0; i < 8; i++)
        {
            float rand = Random.Range(0.5f, 2.5f);
            hit2d = Physics2D.Raycast(transform.position + (Vector3)dir[i], dir[i], 100f, layerMask);//자신위치 + 일정 거리를 줘서 자신이 충돌되지 않도록 설정
       
            navdir += dir[i].normalized * hit2d.distance * rand;
        }


        return navdir.normalized;
    }


    public void Downhp(float power,float brokearmer)
    {
        //빙어력 비율에 따라 방어력 감소
        hp -= power * (brokearmer *(1 /1+defanse));
        if (hp <= 0)
            DeadEnemy();
    }


    public void DeadEnemy()
    {
        if(droplist.Count > 0) { 
        GameObject gameObject = Instantiate(deadbox, this.transform.position, Quaternion.identity);

        gameObject.GetComponent<chestItem>().inputItemlist = droplist;
        }
        GetComponent<Animation>().play = false;
        GetComponent<Animation>().dead = true;
    }

   


    //죽을때 태이블 소환
    public void OnDestroy()
    {
        //for(int i = 0; i < itemtable.Length; i++) { 
        //    if (itemtable[i].Getitem())
        //    {
        //        droplist.Add(itemtable[i].DropItemdata);
        //    }
        //}
    }
}
