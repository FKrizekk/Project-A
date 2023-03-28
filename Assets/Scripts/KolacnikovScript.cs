using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolacnikovScript : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject player;
    public bool shoot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Bullet = GameObject.Find("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot){
            //Debug.Log("Rotating");
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (player.transform.position - transform.position).normalized, 100 * Time.deltaTime, 0.0f),transform.up);
            //Debug.DrawRay(transform.TransformPoint(new Vector3(transform.position.x-0.4f,transform.position.y+0.38f,transform.position.z)),(player.transform.position - transform.position).normalized * 10,Color.green,1f);
        }
    }

    void Shoot(){
        shoot = true;
        Spawn();
    }

    void StopShoot(){
        shoot = false;
    }

    void Spawn(){
        if(shoot){
            Instantiate(Bullet,new Vector3(transform.position.x,transform.position.y+0.4f,transform.position.z),Quaternion.LookRotation((player.transform.position - transform.position).normalized));
            Invoke("Spawn",0.8f);
        }
    }
}
