using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float range = 3f;
    [SerializeField]
    private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;


    // Start is called before the first frame update
    void Start()
    {
        weaponColliders = GetComponentsInChildren<BoxCollider> ();
        player = GameManager.Instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
        //print(playerInRange);
    }

    //coRoutine is a function that is able to work in code blocks.
    //start a block of code, then wait for a few seconds, then continue on
    IEnumerator attack()
    {
        if (playerInRange && !GameManager.Instance.GameOver)
        {
            anim.Play("Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(attack());
    }
}
