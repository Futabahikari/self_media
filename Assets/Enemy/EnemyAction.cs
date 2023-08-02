using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public float speed; 
    public float toughDamage;
    [Tooltip("��Ҫʵ������")] public GameObject prefab;
    [Tooltip("���廹������")] public bool ifArea = false;

    [Header("����")]
    [Tooltip("�Ƿ��Զ�����")] public bool ifSelfdestroy;
    [Tooltip("����ʱ��")] public float destroyTime = 3f;
    private float timer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Run(); //�ƶ�
        if (ifSelfdestroy) { //����
            timer += Time.deltaTime;
            if (timer >= destroyTime) {
                Destroy(gameObject);
            }
        }
    }

    void Run()
    {
        float yPos = transform.position.y - speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.tag == "Player" && ifArea == false) //���������
            {
                Destroy(gameObject);
            }
            if (collision.tag == "Finish")
            { //����Ļ������
                Destroy(gameObject);
            }
    }

    public void Event(int EnemyType) {
        if (EnemyType == 0) //���
            transform.localScale *= 1.5f;
        if (EnemyType == 1) //�����
        {
            Vector3 pos1 = transform.position;
            Vector3 pos2 = transform.position;
            pos1.x += 1f;
            pos2.x -= 1f;
            Instantiate(prefab, pos1, transform.rotation);
            Instantiate(prefab, pos2, transform.rotation);
        }
        if (EnemyType == 2) //�����Χ���ֳ�
        {
            Vector3 pos1 = GameObject.Find("Player").transform.position;
            Vector3 pos2 = GameObject.Find("Player").transform.position;
            pos1.x += 1.8f;
            pos2.x -= 1.8f;
            Instantiate(prefab, pos1, transform.rotation);
            Instantiate(prefab, pos2, transform.rotation);
        }
        if (EnemyType == 3) {  //��������
            //ifSelfdestroy = true;

            GameObject gameObject = GameObject.Find("AreaGenerate");
            Vector3 pos = gameObject.transform.position;
            pos.x += Random.Range(-2, 2);
            Instantiate(prefab, pos, gameObject.transform.rotation);
        }
    }
}
