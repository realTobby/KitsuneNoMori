using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBehaviour : MonoBehaviour
{
    private int maxHits = 5;
    public GameObject woodItemPrefab;

    public void Start()
    {
        this.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        if(maxHits <= 0)
        {
            Instantiate(woodItemPrefab, this.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    public void SendHit()
    {
        maxHits--;
    }
}
