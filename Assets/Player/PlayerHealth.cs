using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Tooltip("���")] public Player player;
    [SerializeField, Tooltip("����ѱ�ֵ")] public float Tough = 0;
    [SerializeField, Tooltip("����ѱ�ֵ")] public float MaxTough = 100f;
    public Image NanBeng;
    public Image NaiLi;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.onStaminaChanged += StaminaChanged;//�����¼���
    }

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
        if(!player.playerIsInvicible)//�޵��ж�
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
        //Debug.Log("��ײ");
        if (collision.tag == "Enemy") {
            Harm(collision.GetComponentInParent<EnemyAction>().damage);
        }
    }

    //�����仯�ص�
    public void StaminaChanged(float playerStamina, float playerMaxStamina)
    {
        if(playerStamina > playerMaxStamina)
        {
            playerStamina = playerMaxStamina;
        }
        NaiLi.fillAmount = playerStamina / playerMaxStamina;
    }


}
