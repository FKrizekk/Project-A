using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperScript : MonoBehaviour
{
    private int counter = 100;
    public GameObject textik;
    private float rechargeTime = 20f;
    // Start is called before the first frame update
    void Start()
    {
        textik = GameObject.Find("SniperAmmoCount");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController2.cWeapon == 2){
            if(counter > 100){
                counter = 100;
            }
            textik.GetComponent<Text>().text = counter.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                if(counter == 100){
                    counter = 0;
                    StartCoroutine(Recharge());
                }else{

                }
            }
        }
    }

    IEnumerator Recharge(){
        while(counter < 100){
            yield return new WaitForSeconds(rechargeTime/100f);
            counter++;
        }
    }
}
