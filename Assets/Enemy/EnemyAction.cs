using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public float speed;
    public float toughDamage;
    [Tooltip("需要实例化的")] public GameObject prefab;
    [Tooltip("个体还是区域")] public bool ifArea = false;

    [Header("销毁")]
    [Tooltip("是否自动销毁")] public bool ifSelfdestroy;
    [Tooltip("销毁时间")] public float destroyTime = 3f;
    private float timer = 0;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Run(); //移动
        if (ifSelfdestroy)
        { //销毁
            timer += Time.deltaTime;
            if (timer >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }

    void Run()
    {
        float yPos = transform.position.y - speed * Time.deltaTime;
        float xPos = transform.position.x;
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

    public void Event(int EnemyType)
    {
        if (EnemyType == 0) //变大
            transform.localScale *= 1.5f;
        if (EnemyType == 1) //暑假集训（不能变道）
        {
            Vector3 pos1 = GameObject.Find("Player").transform.position;
            Vector3 pos2 = pos1;
            pos1.x -= 0.8f;
            pos2.x += 0.8f;
            Instantiate(prefab, pos1, transform.rotation);
            Instantiate(prefab, pos2, transform.rotation);
        }
        if (EnemyType == 2) //周围出现六辆车
        {
            Vector3 pos1 = transform.position;
            Vector3 pos2 = pos1;
            Vector3 pos3 = pos1;
            Vector3 pos4 = pos1;
            Vector3 pos5 = pos1;
            Vector3 pos6 = pos1;

            GameObject g1 = Instantiate(prefab, pos1, transform.rotation);
            g1.GetComponent<SimpleEnemyAction>().moveVector2 = new Vector2(1f, 0.5f);
            //g1.GetComponent<SimpleEnemyAction>().enabled
            GameObject g2 = Instantiate(prefab, pos2, transform.rotation);
            g2.GetComponent<SimpleEnemyAction>().moveVector2 = new Vector2(-1f, 0.5f);       
            GameObject g3 = Instantiate(prefab, pos3, transform.rotation);
            g3.GetComponent<SimpleEnemyAction>().moveVector2 = Vector2.up;
            
            GameObject g4 = Instantiate(prefab, pos4, transform.rotation);
            g4.GetComponent<SimpleEnemyAction>().moveVector2 = new Vector2(1f, -0.5f);
            GameObject g5 = Instantiate(prefab, pos5, transform.rotation);
            g5.GetComponent<SimpleEnemyAction>().moveVector2 = new Vector2(-1f, -0.5f);
            GameObject g6 = Instantiate(prefab, pos6, transform.rotation);
            g6.GetComponent<SimpleEnemyAction>().moveVector2 = Vector2.down;
            
            audio.Play();

            Destroy(gameObject);
        }
        if (EnemyType == 3)
        {  //外资区域
            //ifSelfdestroy = true;

            GameObject gameObject = GameObject.Find("AreaGenerate");
            Vector3 pos = gameObject.transform.position;
            pos.x += Random.Range(-2f, 2f);
            pos.y += 10f;
            Instantiate(prefab, pos, gameObject.transform.rotation);
        }
        if (EnemyType == 4)
        { // 封校
            GameObject gameObject = GameObject.Find("AreaGenerate");
            Vector3 pos = gameObject.transform.position;

            pos.x += Random.Range(-2.5f, 2.5f);
            Instantiate(prefab, pos, gameObject.transform.rotation);
        }
    }
}