using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

//check when the collider of enemies's weapons hits the player (check for trigger events)

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ParticleSystem))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int startingHealth = 100;
    [SerializeField]
    float timeSinceLastHit = 2f;
    [SerializeField]
    Slider healthSlider;

    //keep trck of time between hits
    private float timer = 0f;
    //once we die we don't want to be ble to move our hero
    private CharacterController characterController;
    //play death nimation
    private Animator anim;
    private int currentHealth;
    private AudioSource audio;
    private ParticleSystem blood;

    private void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource> ();
        blood = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //running update on the timer
        timer += Time.deltaTime;
        
    }
    void OnTriggerEnter(Collider other)
    {
        //only care if a weapon strickes and not any collider
        //need a tg for that ^
        //check if it's a weapon
        //register a hit only after timeSinceLastHit

        if (timer >= timeSinceLastHit && !GameManager.Instance.GameOver)
        {
            if (other.tag == "Weapon")
            {
                takeHit();
                timer = 0;
            }
        }
    }

    void takeHit()
    {
        if (currentHealth > 0)
        {
            GameManager.Instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
            blood.Play();
        }

        if (currentHealth <= 0)
        {
            killPlayer();
        }
    }

    void killPlayer()
    {
        GameManager.Instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
        audio.PlayOneShot(audio.clip);
        blood.Play();
    }
}
