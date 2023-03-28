using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    GameObject player;
    GameObject tppoint;
    Vector3 tppos;
    public SpawnScriptManager spawnscriptmanager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (this.gameObject.name.Equals("PitTrigger1"))
        {
            tppoint = GameObject.Find("PitTrigger1Point");
            tppos = tppoint.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            if (this.gameObject.name.Equals("PitTrigger1"))
            {
                player.transform.position = tppos;
            }
            if (this.gameObject.name.Equals("EnemyTrigger1"))
            {
                spawnscriptmanager.SpawnEnemyGroup();
            }
        }
    }
}
