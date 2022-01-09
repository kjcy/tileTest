using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class chestItem : MonoBehaviour
{
    public List<Itemdata> inputItemlist;

    public GameObject itemlistUI;

    public Sprite closeFrame;

    public Sprite openframe;

    public bool open = false;

    public void Update()
    {
        Checkmaouse();
        OpenFrame();

    }

    public void OpenFrame()
    {
        if (open)
        {
            this.transform.GetComponent<SpriteRenderer>().sprite = openframe;
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().sprite = closeFrame;
        }
    }

    public void Destroybox()//UI를 닫을때 발생
    {
        if (CheckItemlist()) Destroy(this.gameObject);
    }

    private bool CheckItemlist()
    {
        bool temp = true;
      for(int i = 0; i < inputItemlist.Count; i++)
        {
            if (inputItemlist[i] != null) temp = false;//null 이 아닌것이 있다면 flase를 반환
        }

        return temp;
    }

    public void Checkmaouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
          
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit,10f)) {
               
                if (raycastHit.transform.gameObject == this.transform.gameObject && open == false)
                {
                    
                    OpenlistUI();
                }
            }
        }
    }


    /// <summary>
    /// 상자 초기화
    /// </summary>
    /// <param name="itemdatas"></param>
    public void CreateInit(List<Itemdata> itemdatas)
    {
        inputItemlist = itemdatas;
    }

    public void OpenlistUI()
    {
        open = true;

        

        RectTransform canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        Vector2 viewpoint = Camera.main.WorldToViewportPoint(this.transform.position + new Vector3(0,1.5f,0));
        Vector2 point = new Vector2(
         ((viewpoint.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
         ((viewpoint.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

        GameObject listUI = Instantiate(itemlistUI,Vector3.zero, Quaternion.identity,GameObject.Find("Canvas").transform);

        listUI.GetComponent<RectTransform>().anchoredPosition = point;
       // listUI.GetComponent<itemlistUI>().itemdatas = inputItemlist;
        listUI.GetComponent<itemlistUI>().openbox = this;
    }




}
