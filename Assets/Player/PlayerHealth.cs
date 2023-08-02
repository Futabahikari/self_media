using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("���")] public Player player;
    private SpriteRenderer sr;
    private Color originColor;
    [Tooltip("����ѱ�ֵ")] public float Tough = 0;
    [Tooltip("����ѱ�ֵ")] public float MaxTough = 100f;
    [Tooltip("�޵�ʱ��")] public float invicibleCD = 0.5f;
    //public Image NanBeng;
    public Image NaiLi;
    public Text maxNanBeng;
    public Text curNanBeng;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.onStaminaChanged += StaminaChanged;//�����¼���
    }

    private void Start()
    {
        Amount();
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }
    private void Update()
    {
        //����
        if (Tough >= MaxTough) { } ;
           
    }
     void Harm(float damage) { // ����
        if(!player.playerIsInvicible)//�޵��ж�
        {
            Tough += damage;
            Amount();
            FlashColor();

            player.playerIsInvicible = true;
            StartCoroutine("InvicibleCD");
        }
    }

    void FlashColor() { //������˸
        sr.color = Color.red;
        Invoke("ResetColor", invicibleCD);
    }
    void ResetColor() {
        sr.color = originColor;
    }
    void recover(float gain) { // �ָ�
        Tough -= gain;
        if (Tough < 0) Tough = 0;
        Amount();
    }

    IEnumerator InvicibleCD()
    {
        yield return new WaitForSeconds(invicibleCD);
        player.playerIsInvicible = false;
    }

    void Amount() {
        //NanBeng.fillAmount = Tough / MaxTough;
        curNanBeng.text = Tough.ToString();
        maxNanBeng.text = MaxTough.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision) //��ײ��Ѫ
    {
        if (collision.tag == "Enemy") {
            Harm(collision.GetComponentInParent<EnemyAction>().toughDamage);
        }
        if (collision.tag == "Item") {
            recover(collision.GetComponent<Item>().toughAdd);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)  //������Ѫ
    {
        if (collision.tag == "EnemyArea")
        {
            Harm(collision.GetComponentInParent<EnemyAction>().toughDamage);
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
