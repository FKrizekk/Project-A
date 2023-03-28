using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public SpawnScriptManager SSM;
    public GameObject EnemyTestPrefab;
    private bool EnemySpawned = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            SpawnEnemys();
        }
        if (SpawnScriptManager.SpawnEnemyCheck == true && EnemySpawned == false)
        {
            SpawnEnemys();
            EnemySpawned = true;
            
        }
    }
    public void SpawnEnemys()
    {
        Instantiate(EnemyTestPrefab, this.transform.position, transform.rotation);
    }
}
