using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("玩家")] public Player player;
    private SpriteRenderer sr;
    private Color originColor;
    [Tooltip("玩家难绷值")] public float Tough = 0;
    [Tooltip("最大难绷值")] public float MaxTough = 100f;
    [Tooltip("无敌时间")] public float invicibleCD = 0.5f;
    //public Image NanBeng;
    public Image NaiLi;
    public Text maxNanBeng;
    public Text curNanBeng;

    private void Awake()
    {
        player = GetComponent<Player>();
        player.onStaminaChanged += StaminaChanged;//耐力事件绑定
    }

    private void Start()
    {
        Amount();
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
    }
    private void Update()
    {
        //销毁
        if (Tough >= MaxTough) { } ;
           
    }
     void Harm(float damage) { // 受伤
        if(!player.playerIsInvicible)//无敌判定
        {
            Tough += damage;
            Amount();
            FlashColor();

            player.playerIsInvicible = true;
            StartCoroutine("InvicibleCD");
        }
    }

    void FlashColor() { //受伤闪烁
        sr.color = Color.red;
        Invoke("ResetColor", invicibleCD);
    }
    void ResetColor() {
        sr.color = originColor;
    }
    void recover(float gain) { // 恢复
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

    private void OnTriggerEnter2D(Collider2D collision) //碰撞掉血
    {
        if (collision.tag == "Enemy") {
            Harm(collision.GetComponentInParent<EnemyAction>().toughDamage);
        }
        if (collision.tag == "Item") {
            recover(collision.GetComponent<Item>().toughAdd);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)  //持续掉血
    {
        if (collision.tag == "EnemyArea")
        {
            Harm(collision.GetComponentInParent<EnemyAction>().toughDamage);
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
