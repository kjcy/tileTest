using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

    public Sprite[] frames;
    public Sprite[] deadframe;
    private int index;
    private int deadindex;

    private float currentTime = 0;
    private float deadcurrentTime = 0;
    public float playTime;


    private SpriteRenderer randerer;


    public bool dead;
    public bool play;

    // Start is called before the first frame update
    void Awake()
    {
        randerer = this.transform.GetComponent<SpriteRenderer>();
        deadindex = 0;
        index = 0;
        play = true;
        dead = false;
    }



    private void FixedUpdate()
    {
        if (play)
        {
            Playani();
        }

        if (dead)
        {
            Deadanimation();
        }
    }


    private void Playani()
    {
        currentTime += Time.deltaTime;
        if(currentTime > playTime)
        {
            currentTime -= playTime;
            randerer.sprite = frames[index++];
            if (index >= frames.Length) index = 0;
        }       
    }

   private void Deadanimation()
    {
        deadcurrentTime += Time.deltaTime;
        if(deadcurrentTime> deadframe.Length/60)
        {
            deadcurrentTime -= deadframe.Length / 60;
            randerer.sprite = deadframe[deadindex++];
            if(deadindex >= deadframe.Length)
            {
                Destroy(this.gameObject);
            }
        }
    }


}
