using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayScript : MonoBehaviour
{
    public GameObject player;
	private float SwayAmount = 0.1f;
    public GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        MainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, MainCamera.transform.position - player.GetComponent<Rigidbody>().velocity.normalized * SwayAmount, 0.1f);
    }
}