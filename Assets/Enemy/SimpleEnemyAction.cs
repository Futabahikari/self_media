using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAction : MonoBehaviour
{
    public float speed;
    public Vector2 moveVector2;
    public float toughDamage;
    [Tooltip("个体还是区域")] public bool ifArea = false;

    [Header("销毁")]
    [Tooltip("是否自动销毁")] public bool ifSelfdestroy;
    [Tooltip("销毁时间")] public float destroyTime = 3f;
    private float timer = 0;

    private void Start()
    {
        //enabled = false;
    }


    void Update()
    {
        Run(moveVector2); //移动
        if (ifSelfdestroy)
        { //销毁
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
        if (collision.tag == "Player" && ifArea == false) //个体才销毁
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Finish" && ifArea == false) //出屏幕外销毁(个体)
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Finish2" && ifArea == true) //出屏幕外销毁(区域)
        {
            Destroy(gameObject);
        }
    }
}
