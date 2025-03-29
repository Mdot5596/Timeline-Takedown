using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Ensure NavMeshAgent is accessible


// This script ensures that only active, alive enemies on a valid NavMesh attempt to follow the player.
// Without these checks, you'd get annoying errors in Unity's console when enemies die or leave the NavMesh.

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;
    private EnemyHealth enemyHealth;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        // Check if the player exists, enemy is alive, and the agent is active & on NavMesh
        if (player == null || enemyHealth == null || enemyHealth.IsDead) return;

        if (nav != null && nav.enabled && nav.isOnNavMesh)
        {
            nav.SetDestination(player.position);
        }
    }
}
