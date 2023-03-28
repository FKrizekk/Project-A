using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScriptManager : MonoBehaviour
{
    public static bool SpawnEnemyCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEnemyGroup()
    {
            SpawnEnemyCheck = true;
    }
}
