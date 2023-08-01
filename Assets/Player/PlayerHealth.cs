using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Tooltip("玩家难绷值")] public float Tough = 0;
    [SerializeField, Tooltip("最大难绷值")] public float MaxTough = 100f;
    public Image NanBeng;
    public Image NaiLi;
   
    private void Start()
    {
        Amount();
    }
    private void Update()
    {
        //销毁
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
        //Debug.Log("碰撞");
        if (collision.tag == "Enemy") {
            Harm(collision.GetComponentInParent<EnemyAction>().damage);
        }
    }
}
