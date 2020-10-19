using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ParticleSystem))]

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    private int startingHealth = 20;
    [SerializeField]
    private float timeSinceLastHit = 0.5f;
    [SerializeField]
    private float dissapearSpeed = 2f;

    private AudioSource audio;
    private float timer = 0f; //how long it has been since enmy has been struck
    private Animator anim;
    private NavMeshAgent nav;
    private Rigidbody rigidBody;
    private CapsuleCollider capsuleCollider;
    private bool isAlive;
    private bool dissapearEnemy = false;
    private int currentHealth;
    private ParticleSystem blood;

    public bool IsALive
    {
        get
        {
            return isAlive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        isAlive = true;
        currentHealth = startingHealth;
        blood = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.Instance.GameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                takeHit();
                blood.Play();
                timer = 0f;
            }
        }   
    }

    void takeHit()
    {
        if (currentHealth > 0)
        {
            audio.PlayOneShot(audio.clip);
            anim.Play("hurt");
            currentHealth -= 10;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        capsuleCollider.enabled = false;
        nav.enabled = false;
        anim.SetTrigger("EnemyDie");
        rigidBody.isKinematic = true;

        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        //wait for seconds after enemy dies
        yield return new WaitForSeconds(4f);
        //start to sink the enemy
        dissapearEnemy = true;
        //wait for 2 seconds
        yield return new WaitForSeconds(2f);
        //destroy the game object
        Destroy(gameObject);
    }

}
