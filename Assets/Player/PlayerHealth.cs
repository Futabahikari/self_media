using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Tooltip("玩家")] public Player player;
    [SerializeField, Tooltip("玩家难绷值")] public float Tough = 0;
    [SerializeField, Tooltip("最大难绷值")] public float MaxTough = 100f;
    public Image NanBeng;
    public Image NaiLi;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.onStaminaChanged += StaminaChanged;//耐力事件绑定
    }

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
        if(!player.playerIsInvicible)//无敌判定
        {
            Tough += damage;
            Amount();
        }
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

    //耐力变化回调
    public void StaminaChanged(float playerStamina, float playerMaxStamina)
    {
        if(playerStamina > playerMaxStamina)
        {
            playerStamina = playerMaxStamina;
        }
        NaiLi.fillAmount = playerStamina / playerMaxStamina;
    }


}
