using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private const float TIME_FOR_ATTACK_DESTROY = 0.5f;

    private PlayerBehaviour myPlayerBehaviour;


    // Start is called before the first frame update
    void Start()
    {
        myPlayerBehaviour = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerBehaviour>();
        Destroy(gameObject, TIME_FOR_ATTACK_DESTROY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("A collision");
        if (collision.gameObject.tag == "destroyable")
        {
            myPlayerBehaviour.AttackTrigger(collision);
            Destroy(gameObject, TIME_FOR_ATTACK_DESTROY);
        }

        if (collision.gameObject.tag == "enemy")
        {
            // todo
        }

    }

}
