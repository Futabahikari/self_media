using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Tooltip("����ѱ�ֵ")] public float Tough = 0;
    [SerializeField, Tooltip("����ѱ�ֵ")] public float MaxTough = 100f;
    public Image NanBeng;
    public Image NaiLi;
   
    private void Start()
    {
        Amount();
    }
    private void Update()
    {
        //����
        if (Tough >= MaxTough) { } ;
           
    }
    public void Harm(float damage) {
        Tough += damage;
        Amount();
    }

    void Amount() {
        NanBeng.fillAmount = Tough / MaxTough; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("��ײ");
        if (collision.tag == "Enemy") {
            Harm(collision.GetComponentInParent<EnemyAction>().damage);
        }
    }
}
