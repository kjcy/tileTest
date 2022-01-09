using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Player : MonoBehaviour
{
    static public Player player;

    public GameObject playerObject;

    public Playerdata playerdata;

    public GameObject hand;

    //�̵��ӵ�
    public float speed;

    public float attacktime = 0.44f;
    //�ð��� üũ�ؾ� �ϴ� ���õ� �����͵�
    public float[] timecount = new float[2];
    //�ڽ� rigidbody
    Rigidbody2D rigi2D;

    public bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = this;
        }

        if (playerdata == null) playerdata = new Playerdata();
       

        hand = this.transform.GetChild(0).gameObject;

        rigi2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Timecount();
        InputMove();
        InputAttack();
        LookWeapon();
    }

    public void Timecount()
    {
        for(int i=0;i < timecount.Length; i++)
        {
            if (timecount[i] > 0)
            {
                timecount[i] -= Time.deltaTime;
            }
        }
    }
    public void InputAttack()
    {
       /* if (Input.GetKeyDown(KeyCode.Z)&&!attack)
        {
            Debug.Log("z �Է�");
            Playattack();
        }*/

        if(Input.GetMouseButton(0) && timecount[0]<=0)
        {
            Playattack();
        }

    }

    public void Playattack()
    {
        timecount[0] = attacktime;
        GameObject weapon = hand.transform.GetChild(0).gameObject;

        GameObject attackObject = Instantiate(weapon);

        attackObject.transform.position = weapon.transform.position;
        attackObject.transform.rotation = hand.transform.rotation;
        attackObject.GetComponent<weapon>().spped = 3;
        attackObject.GetComponent<weapon>().move = true;
      /*  StartCoroutine(Playattackcor(weapon.GetComponent<weapon>().startRota, weapon.GetComponent<weapon>().endRota,weapon.GetComponent<weapon>().attackrank));*/
    }


    public IEnumerator Playattackcor(Vector3 startRota, Vector3 endRota, float rank)
    {
        float attackframe = timecount[0] * rank;
        attack = true;
        for(int i = 0; i < attackframe; i++)
        {
            Debug.Log("�� ����������");
         
            hand.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(startRota.z, endRota.z, i / attackframe));
           
            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < attackframe; i++)
        {
            Debug.Log("�� �ö󰡴���");
           
            hand.transform.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(endRota.z, startRota.z, i / attackframe));
            yield return new WaitForFixedUpdate();

        }
        attack = false;
    }


    /// <summary>
    /// Ű �Է°��� Ȯ���� �÷��̾ �̵���Ű�� �Լ�
    /// </summary>
    public void InputMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x > 0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }else if(x < -0.01)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 pos = new Vector3((playerdata.SPEED*x),(playerdata.SPEED * y),0);

        rigi2D.MovePosition(transform.position + pos * Time.deltaTime);
    }

   
    public void LookWeapon()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - this.transform.position.y, mouse.x - this.transform.position.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
  


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Item"))
        {
            int id = collision.gameObject.GetComponent<Itemdata>().id;
            switch (id)
            {
                //����
                case 1:
                    playerdata.COIN += 1;
                    
                    break;
                //����
                case 2:
                    playerdata.GetItem(id, 1);
                    
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
