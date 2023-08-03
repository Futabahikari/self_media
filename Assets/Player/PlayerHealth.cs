using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("���")] public Player player;
    //private SpriteRenderer sr;
    //private Color originColor;
    private AudioSource music;
    public AudioClip clip;

    [Tooltip("�޵�ʱ��")] public float invicibleCD = 0.5f;
    //public Image NanBeng;
    public Image NaiLi;
    [Header("�ѱ���")]
    [Tooltip("����ѱ�ֵ")] public float Tough = 0;
    [Tooltip("����ѱ�ֵ")] public float MaxTough = 100f;
    public Text maxNanBeng;
    public Text curNanBeng;
    [Header("�ѱ������л�")]   
    public Sprite NanBengUp;
    public Sprite NanBengDown;
    private SpriteRenderer sr;
    private Sprite originS;

    private void Awake()
    {
        player = GetComponent<Player>();
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;

        player.onStaminaChanged += StaminaChanged;//�����¼���
    }

    private void Start()
    {
        Amount();
        sr = GetComponent<SpriteRenderer>();
        originS = sr.sprite;
        //originColor = sr.color;
    }
    private void Update()
    {
        //����
        if (Tough >= MaxTough) { } ;
           
    }
     void Harm(float damage) { // ����
        if(!player.playerIsInvicible && damage != 0)//�޵��ж�
        {
            Tough += damage;

            music.clip = clip;//������Ч
            music.Play();

            Amount();
            Switchexpression(true);

            player.playerIsInvicible = true;
            StartCoroutine("InvicibleCD");
        }
    }

    void Switchexpression(bool ifUp) { //�л�����
        if (ifUp)
            sr.sprite = NanBengUp;
        else
            sr.sprite = NanBengDown;
        Invoke("Reset", invicibleCD + 0.3f);
    }
    void Reset() {
        sr.sprite = originS;
    }
    void recover(float gain) { // �ָ�
        Tough -= gain;
        if (Tough < 0) Tough = 0;
        Amount();
        Switchexpression(false);
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
        else if (collision.tag == "SimpleEnemy")
            Harm(collision.GetComponentInParent<SimpleEnemyAction>().toughDamage);
        else if (collision.tag == "Item") {
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
