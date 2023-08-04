using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public Transform generatePos;
    [Header("���ˡ�����")]
    public GameObject[] prefabs;
    [Tooltip("��������")] public int num = 0;
    [Tooltip("����������")] public int maxNum = 5;
    [Space()]
    public float generateTime1;
    public float generateTime2;
    private float time;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        time = generateTime2;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        num = enemies.Length;
        if (num <= maxNum)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                int length = prefabs.Length;

                int i = Random.Range(0, length);
                if (i > 4) //����ǵ��ߣ������һ��
                {
                    int j = Random.Range(0, 4);
                    if (j != 0) i = Random.Range(0, 4); //����ɵ���
                }

                Vector3 pos = generatePos.position;
                pos.x += Random.Range(-1.5f, 1.5f);
                Instantiate(prefabs[i], pos, transform.rotation);
                time = generateTime2;
            }        
        }
    }
}
