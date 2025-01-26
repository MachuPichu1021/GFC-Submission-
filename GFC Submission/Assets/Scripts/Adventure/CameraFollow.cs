using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        if (player == null)
            Debug.LogError("Player not found!");
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
