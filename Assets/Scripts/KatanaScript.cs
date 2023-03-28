using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaScript : MonoBehaviour
{
    public static bool IsDamaging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attacked(){
        IsDamaging = true;
    }
    public void StoppedAttacking(){
        IsDamaging = false;
    }
}