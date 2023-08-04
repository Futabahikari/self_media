using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEvent : MonoBehaviour
{
    public int EnemyType;
    //public event UnityAction onPlayerBounceBackSucceed = delegate { };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collision.gameObject.GetComponent<Player>().GetPlayerInvicibleAtt())
        {
            Destroy(transform.GetComponent<Collider2D>());
            gameObject.GetComponentInParent<EnemyAction>().Event(EnemyType);
        }
    }
}
