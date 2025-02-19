using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    Transform player;
    Animator animator;
    bool isWalking = false;

    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only update the X and Z position, keep Y fixed to the current Y position
        Vector3 targetPosition = player.position;
        targetPosition.y = transform.position.y; // Keep the current Y position

        // Update the NavMeshAgent's destination with the adjusted targetPosition
        nav.SetDestination(targetPosition);

        // Check if the agent is moving and trigger walking animation
        if (nav.velocity.magnitude > 0.1f)
        {
            if (!isWalking)
            {
                animator.SetBool("IsWalking", true);
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                animator.SetBool("IsWalking", false);
                isWalking = false;
            }
        }
    }
}
