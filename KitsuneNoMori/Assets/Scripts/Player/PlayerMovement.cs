using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public float PLAYER_SPEED = 10;
    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.position = new Vector3(playerRigidbody.position.x + Input.GetAxis("Horizontal") * PLAYER_SPEED * Time.deltaTime, playerRigidbody.position.y, playerRigidbody.position.z + Input.GetAxis("Vertical") * PLAYER_SPEED * Time.deltaTime);
    }
}
