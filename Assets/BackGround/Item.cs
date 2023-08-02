using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float toughAdd = 10;
    public float speed;

    void Update()
    {
        Run(); //�ƶ�
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
        { //����Ļ������
            Destroy(gameObject);
        }
    }
}
