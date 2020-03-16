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

    // Update is called once per frame
    void Update()
    {
        // check if inside a chunk

        



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
