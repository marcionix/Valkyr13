using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportadorScript : MonoBehaviour {

    public Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerProjectile") {
            player.position = GameObject.FindGameObjectWithTag("Lapide").transform.position;
        }
    }
}
