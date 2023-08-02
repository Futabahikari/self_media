using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float toughAdd = 10;
    public float speed;

    void Update()
    {
        Run(); //移动
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
        if (collision.tag == "Finish")
        { //出屏幕外销毁
            Destroy(gameObject);
        }
    }
}
