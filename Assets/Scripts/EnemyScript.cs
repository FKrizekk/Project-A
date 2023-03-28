using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public Animator _animator;
    NavMeshAgent agent;
    private bool Ready;
    private bool Stopped = true;
    private bool bc = false;
    private int hp = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        PositionSetup();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ready && Vector3.Distance(player.transform.position,transform.position) > 15){
            Stopped = false;
            agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;
            NavMeshHit hit;
            bool obstacle = agent.Raycast(player.transform.position, out hit);
            if (!agent.Raycast(player.transform.position, out hit))
            {   
                NavigateToPlayer();
                BroadcastMessage("StopShoot");
                bc = false;
                //Debug.Log("I SEE");
                
            }else{
                _animator.SetBool("PlayerSpotted", false);
                //Debug.Log("PLAYER SIGHT LOST");
            }
        }
        NavMeshHit Hit;
        if(Vector3.Distance(player.transform.position,transform.position) < 15 && !agent.Raycast(player.transform.position, out Hit)){
            agent.isStopped = true;
            _animator.SetBool("navigating", false);
            _animator.SetBool("PlayerSpotted", true);
            Stopped = true;
            if(!bc){
                BroadcastMessage("Shoot");
                bc = true;
            }
        }

        if(Stopped){
            Quaternion rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, (player.transform.position - transform.position).normalized, 1 * Time.deltaTime, 0.0f),transform.up);
            transform.rotation = rotation;
        }
        if(hp < 1){
            BroadcastMessage("StopShoot");
            _animator.SetTrigger("IsDying");
            Invoke("Death",1f);
        }
    }

    void PositionSetup(){
        RaycastHit spawnHit;
        Debug.DrawRay(transform.position, -transform.up * 20, Color.yellow,20f);
        if (Physics.Raycast(transform.position, -transform.up, out spawnHit, 20f))
        {
            Debug.Log("RAYCAST HIT");
            transform.position = new Vector3(transform.position.x,spawnHit.transform.position.y+5.27f,transform.position.z);
        }
        Debug.Log("READY");
        Ready = true;
    }

    void NavigateToPlayer(){
        //Debug.Log("NAVIGATING");
        if(Vector3.Distance(player.transform.position,transform.position) > 15){
            agent.destination = player.transform.position;
            _animator.SetBool("PlayerSpotted", false);
            _animator.SetBool("navigating", true);
        }
    }

    public void GotHit(int dmg){
        hp -= dmg;
    }

    void Death(){
        //Debug.Log("I DIED");
        Destroy(this.gameObject);
    }


}