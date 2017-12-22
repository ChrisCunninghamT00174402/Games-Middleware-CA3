using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class AI : MonoBehaviour {


   
    public NavMeshAgent navMeshAgent;
    public State state;
    private bool alive;
    public GameObject[] waypoints;
    private int waypointInd = 0;
    public float chaseSpeed = 1f;
    public float patrolSpeed = 0.5f;
    public GameObject target;
    public ThirdPersonCharacter character;



    public enum State
    {

        PATROL, 
        CHASE

    }

    // Use this for initialization
    void Start () {

        navMeshAgent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();

        navMeshAgent.updatePosition = true;
        navMeshAgent.updateRotation = false;

        state = AI.State.PATROL;

        alive = true;

        StartCoroutine("StateMachine");
    }
	


    IEnumerator StateMachine()
    {
        while (alive)
        {

            switch (state)
            {

                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;

            }
            yield return null;

        }


    }

    void Patrol()
    {
        navMeshAgent.speed = patrolSpeed;
        NewMethod();

    }

    private void NewMethod()
    {
        if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {

            navMeshAgent.SetDestination(waypoints[waypointInd].transform.position);
            character.Move(navMeshAgent.desiredVelocity, false, false);


        }
        else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
        {

            waypointInd += 1;

            if (waypointInd >= waypoints.Length)
            {
                waypointInd = 0;
            }
        }
        else
        {

            character.Move(Vector3.zero, false, false);

        }
    }

    void Chase()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(target.transform.position);
        character.Move(navMeshAgent.desiredVelocity, false, false);


    }

    private void OnTriggerEnter(Collider player)
    {

        if (player.tag == "Player")
        {
            Debug.Log("hit");
            state = AI.State.CHASE;
            target = player.gameObject;

        }

    }

}
