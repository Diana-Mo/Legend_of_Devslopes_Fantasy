using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(Animator))]
public class RangerAttack : MonoBehaviour
{
    [SerializeField]
    private float range = 3f;
    [SerializeField]
    private float timeBetweenAttacks = 1f;
    [SerializeField]
    Transform fireLocation;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private EnemyHealth enemyHealth;
    private GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        arrow = GameManager.Instance.Arrow;
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameManager.Instance.Player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsALive)
        {
            playerInRange = true;
            anim.SetBool("PlayerInRange", true);
            RotateTowards(player.transform);
            //float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            //playerInRange = distanceToPlayer < range && enemyHealth.IsALive;
            //print(playerInRange);
        }
        else
        {
            playerInRange = false;
            anim.SetBool("PlayerInRange", false);
        }
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

    private void RotateTowards(Transform player)
    {
        //create a direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        //creating the rotation
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //performing the rotation, passing in the enemy's current rotation, where we want him to look and how fast it will rotate
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    public void FireArrow()
    {
        //create arrow
        //set position to a specific location
        //set rotation
        //set velocity

        GameObject newArrow = Instantiate(arrow);
        newArrow.transform.position = fireLocation.position;
        newArrow.transform.rotation = transform.rotation;
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25f;
    }

}

