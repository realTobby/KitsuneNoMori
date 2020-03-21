using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private string lastVisitedChunk;
    private GameObject chunkHandler;

    // Start is called before the first frame update
    void Start()
    {
        chunkHandler = GameObject.FindGameObjectsWithTag("ChunkHandler")[0];
    }

    void Update()
    {

        if(Input.anyKeyDown)
        {
            int horizontalIn = (int)Input.GetAxisRaw("Horizontal");
            int verticalIn = (int)Input.GetAxisRaw("Vertical");

            if(horizontalIn != 0 || verticalIn != 0)
            {
                switch (horizontalIn)
                {
                    case 1:
                        this.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case -1:
                        this.transform.rotation = Quaternion.Euler(0, -90, 0);
                        break;
                }
                switch (verticalIn)
                {
                    case 1:
                        this.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case -1:
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                }
            }
        }
        

    }

    void OnCollisionEnter(Collision collision)
    {
        if(lastVisitedChunk != collision.gameObject.name)
        {
            chunkHandler.GetComponent<ChunkHandling>().GetChunk(collision.gameObject.name);
            lastVisitedChunk = collision.gameObject.name;
        }
        
    }
}
