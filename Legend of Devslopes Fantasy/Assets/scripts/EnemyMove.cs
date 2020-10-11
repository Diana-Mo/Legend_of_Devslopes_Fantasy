using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    Transform player;
    private NavMeshAgent nav;
    private Animator anim;

    private void Awake()
    {
        Assert.IsNotNull(player);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
    }
}
