using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public enum enemyrank { NULLrank, nomal, named, boss};

    public enemyrank e_rank;

    public int id;

    //���� ä��
    public float hp;

    public float power;

    public float defanse;

    [SerializeField]
    private GameObject[] itemtable = new GameObject[4];//���⼭ �ѹ� �ʱ�ȭ ������� ã�� �� �ִ�.

    private List<Itemdata> droplist = new List<Itemdata>();//���⵵

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
    /// �⺻ ai, ��ҿ��� 8�������� ray �� �߻��� ������ �Ÿ��� ã�� ����ġ�� ���� �������� �ٴϴٰ� player �� �Ÿ��� ���´ٸ� �÷��̾� �������� ����(�� �Ѿ� �߰�x)
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
            Debug.Log("�÷��̾� ã��");
            this.transform.Translate((Player.player.playerObject.transform.position - this.transform.position).normalized * speed * Time.deltaTime);
        }
        else { 

           this.transform.Translate(navdir * speed * Time.deltaTime);
        }
    }

    public Vector2 Ray2d()
    {
        Debug.Log("Ray2d�۵���");
        Vector2 navdir = Vector3.zero;

        int layerMask = LayerMask.GetMask("well") | LayerMask.GetMask("enemy");

        Debug.LogFormat("{0}", layerMask);
        RaycastHit2D hit2d;
        for (int i = 0; i < 8; i++)
        {
            float rand = Random.Range(0.5f, 2.5f);
            hit2d = Physics2D.Raycast(transform.position + (Vector3)dir[i], dir[i], 100f, layerMask);//�ڽ���ġ + ���� �Ÿ��� �༭ �ڽ��� �浹���� �ʵ��� ����
       
            navdir += dir[i].normalized * hit2d.distance * rand;
        }


        return navdir.normalized;
    }


    public void Downhp(float power,float brokearmer)
    {
        //����� ������ ���� ���� ����
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

   


    //������ ���̺� ��ȯ
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
