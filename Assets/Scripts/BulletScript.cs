using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody>();
        this.tag = "EnemyBullet";
        transform.localScale = new Vector3(0.02f,0.02f,0.02f);
        //transform.position = new Vector3(0,2.75900006f,2.56200004f);
        transform.rotation = Quaternion.LookRotation((new Vector3(player.transform.position.x,player.transform.position.y+0.6f,player.transform.position.z) - transform.position).normalized,transform.up);
        rb.AddForce(transform.forward,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}