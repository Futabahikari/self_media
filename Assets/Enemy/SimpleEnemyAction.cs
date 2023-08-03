using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAction : MonoBehaviour
{
    public float speed;
    public Vector2 moveVector2;
    public float toughDamage;
    [Tooltip("���廹������")] public bool ifArea = false;

    [Header("����")]
    [Tooltip("�Ƿ��Զ�����")] public bool ifSelfdestroy;
    [Tooltip("����ʱ��")] public float destroyTime = 3f;
    private float timer = 0;

    private void Start()
    {
        //enabled = false;
    }


    void Update()
    {
        Run(moveVector2); //�ƶ�
        if (ifSelfdestroy)
        { //����
            timer += Time.deltaTime;
            if (timer >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    void Run(Vector2 direction)
    {
        float xPos = transform.position.x + direction.x * speed * Time.deltaTime;
        float yPos = transform.position.y + direction.y * speed * Time.deltaTime;
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && ifArea == false) //���������
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Finish" && ifArea == false) //����Ļ������(����)
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Finish2" && ifArea == true) //����Ļ������(����)
        {
            Destroy(gameObject);
        }
    }
}
