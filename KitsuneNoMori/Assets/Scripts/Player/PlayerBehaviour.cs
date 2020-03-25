using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal enum  PlayerDirection
{
    Left,
    Right,
    Up,
    Down
}

public class PlayerBehaviour : MonoBehaviour
{
    private string lastVisitedChunk;
    private GameObject chunkHandler;
    private PlayerDirection playerDirection;
    public GameObject attackHitboxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        chunkHandler = GameObject.FindGameObjectsWithTag("ChunkHandler")[0];
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            #region Moving/Rotating

            int horizontalIn = (int)Input.GetAxisRaw("Horizontal");
            int verticalIn = (int)Input.GetAxisRaw("Vertical");

            if(horizontalIn != 0 || verticalIn != 0)
            {
                switch (horizontalIn)
                {
                    case 1:
                        this.transform.rotation = Quaternion.Euler(0, 90, 0);
                        playerDirection = PlayerDirection.Left;
                        break;
                    case -1:
                        this.transform.rotation = Quaternion.Euler(0, -90, 0);
                        playerDirection = PlayerDirection.Right;
                        break;
                }
                switch (verticalIn)
                {
                    case 1:
                        this.transform.rotation = Quaternion.Euler(0, 0, 0);
                        playerDirection = PlayerDirection.Up;
                        break;
                    case -1:
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                        playerDirection = PlayerDirection.Down;
                        break;
                }
            }
            #endregion

            #region Attacking

            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameObject newAttack = Instantiate(attackHitboxPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.x), Quaternion.identity);
                //newAttack.transform.parent = GameObject.FindGameObjectWithTag("attackBuffer").transform;
                Vector3 attackPos = new Vector3(0,0,0);
                switch (playerDirection)
                {
                    case PlayerDirection.Left:
                        attackPos = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
                        newAttack.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case PlayerDirection.Right:
                        attackPos = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
                        newAttack.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case PlayerDirection.Up:
                        attackPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z + 0.5f);
                        newAttack.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case PlayerDirection.Down:
                        attackPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z - 0.5f);
                        newAttack.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                }
                newAttack.transform.position = attackPos;
            }

            #endregion

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("PLAYER COLLISION");
        if(collision.gameObject.tag == "chunk")
        {
            if (lastVisitedChunk != collision.gameObject.name)
            {
                chunkHandler.GetComponent<ChunkHandling>().GetChunk(collision.gameObject.name);
                lastVisitedChunk = collision.gameObject.name;
            }
        }
    }

    public void AttackTrigger(Collision hit)
    {
        hit.gameObject.GetComponent<DestroyableBehaviour>().SendHit();
    }
}
