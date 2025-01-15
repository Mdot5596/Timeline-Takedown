using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        player = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination (player.position);
    }
}
