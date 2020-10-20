using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player.transform;
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.GameOver && enemyHealth.IsALive)
        {

            nav.SetDestination(player.position);
        }
        else if ((!GameManager.Instance.GameOver || GameManager.Instance.GameOver) && !enemyHealth.IsALive)
        {
            nav.enabled = false;
        }
        else
        {
            nav.enabled = false;
            anim.Play("Idle");
        }
    }
}
