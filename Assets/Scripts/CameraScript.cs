using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController2.menuOpened == false){
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -PlayerController2.MSensitivity, 0, 0));
        }
    }
}
