using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SpeedPowerUp : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
        playerController = player.GetComponent<PlayerController>();
        GameManager.Instance.RegisterPowerUp();
    }

    void OnTriggerEvent(Collider other)
    {
        if (other.gameObject == player)
        {
            playerController.SpeedPowerUp();
            Destroy(gameObject);
        }
    }
}
