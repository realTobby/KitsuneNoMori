using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBehaviour : MonoBehaviour
{
    private int health = 5;
    public GameObject woodItemPrefab;
    void Update()
    {
        if(health <= 0)
        {
            Instantiate(woodItemPrefab, this.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    public void SendHit()
    {
        health--;
    }
}
