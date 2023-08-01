using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    public int EnemyType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(transform.GetComponent<CircleCollider2D>());

            gameObject.GetComponentInParent<EnemyAction>().Event(EnemyType);
        }
    }
}
