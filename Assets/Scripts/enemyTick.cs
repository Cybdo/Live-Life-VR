using Mono.Unix;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class enemyTick : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject destination;
    NavMeshAgent agent;
    //public float stoppingDistance = 0.9f;
    public float destroyDistance = 70.0f;
    void Start()
    {
        //navigation start
        destination = GameObject.FindGameObjectWithTag("Player");

        //Debug.LogError(destination.name);
        //Debug.LogError(destination.transform.position);
        agent = GetComponent<NavMeshAgent>();
        /*
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.SetDestination(destination.transform.position);
        }
        else { Destroy(gameObject); }*/

        //agent.stoppingDistance = stoppingDistance; // Set the stopping distance
        agent.SetDestination(destination.transform.position);
        //navigation end

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destination.transform.position); //navigation update playerpos
        if (Vector3.Distance(transform.position, destination.transform.position) > destroyDistance)
        {
            Destroy(gameObject);
            Debug.LogError("killed cus 70 radius");
        }

    }
}
