using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public float speed; 
    public float damage;

    public GameObject prefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        float yPos = transform.position.y - speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void Event(int EnemyType) {
        if (EnemyType == 0) //变大
            transform.localScale *= 1.5f;
        if (EnemyType == 1) //车变多
        {
            Vector3 pos1 = transform.position;
            Vector3 pos2 = transform.position;
            pos1.x += 1f;
            pos2.x -= 1f;
            Instantiate(prefab, pos1, transform.rotation);
            Instantiate(prefab, pos2, transform.rotation);
        }
    }
}
