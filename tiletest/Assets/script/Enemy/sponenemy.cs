using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sponenemy : MonoBehaviour
{
    public GameObject enemyparent;

    public GameObject sponenemydata;

    public float sponMaxtime;
    public float spontime;

    public int sponcount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        Sponenemy();
    }

   

    public void Sponenemy()
    {
        spontime -= Time.deltaTime;
        if(spontime < 0)
        {
            sponcount++;
            spontime = sponMaxtime;
            Instantiate(sponenemydata, this.transform.position, Quaternion.identity, enemyparent.transform);
            if (sponcount > 10)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
