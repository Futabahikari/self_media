using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Tooltip("玩家")] public Player player;
    //private SpriteRenderer sr;
    //private Color originColor;
    private AudioSource music;
    public AudioClip clip;
    public AudioClip bounckBackClip;
    public AudioClip bounckBackSucceedClip;

    [Tooltip("无敌时间")] public float invicibleCD = 0.5f;
    //public Image NanBeng;
    public Image NaiLi;
    [Header("难绷条")]
    [Tooltip("玩家难绷值")] public float Tough = 0;
    [Tooltip("最大难绷值")] public float MaxTough = 100f;
    public Text maxNanBeng;
    public Text curNanBeng;
    [Header("难绷表情切换")]  
    public Sprite NanBengUp;
    public Sprite NanBengDown;
    private SpriteRenderer sr;
    private Sprite originS;

    public Sprite BounceBackS;
    private bool BounceBackIsSucceed = false;

    public GameObject recoverVFX;

    private void Awake()
    {
        player = GetComponent<Player>();
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;

        player.onStaminaChanged += StaminaChanged;//耐力事件绑定
        player.onBounceBacking += BounceBacking;
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
        //销毁
        if (Tough >= MaxTough) {
            Destroy(gameObject);
        } 
           
    }
     void Harm(float damage) { // 受伤
        if(damage != 0)//无敌判定
        {
            Tough += damage;
            if (Tough > MaxTough) Tough = MaxTough;

            music.clip = clip;//播放音效
            music.Play();

            Amount();
            Switchexpression(true);

            player.SetPlayerInvicibleAtt(true);
            StartCoroutine("InvicibleCD");
        }
    }

    void Switchexpression(bool ifUp) { //切换表情
        if (ifUp)
            sr.sprite = NanBengUp;
        else
            sr.sprite = NanBengDown;
        Invoke("Reset", invicibleCD + 0.3f);
    }

    void ChangeBounceSprite()
    {
        StartCoroutine(nameof(ChangeBounceSpriteCoroutine));
    }

    IEnumerator ChangeBounceSpriteCoroutine()
    {
        float t = 0f;
        sr.sprite = BounceBackS;
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= 1.2f;
        localScale.y *= 1.2f;
        gameObject.transform.localScale = localScale;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        sr.sprite = originS;
        localScale.x /= 1.2f;
        localScale.y /= 1.2f;
        gameObject.transform.localScale = localScale;
        StopCoroutine(nameof(ChangeBounceSpriteCoroutine));
    }
    void Reset() {
        sr.sprite = originS;
    }
    void recover(float gain) { // 恢复
        Tough -= gain;
        if (Tough < 0) Tough = 0;
        Amount();
        Switchexpression(false);
        Instantiate(recoverVFX, player.transform.position, player.transform.rotation);
    }

    IEnumerator InvicibleCD()
    {
        yield return new WaitForSeconds(invicibleCD);
        player.SetPlayerInvicibleAtt(false);
    }

    void Amount() {
        //NanBeng.fillAmount = Tough / MaxTough;
        curNanBeng.text = Tough.ToString();
        maxNanBeng.text = MaxTough.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision) //碰撞掉血
    {
        {
            if (collision.tag == "Enemy")
            {

                if (player.GetPlayerInvicibleAtt())
                {
                    //StartCoroutine(nameof(BounceBackIsSucceedCoroutine));
                    player.PlayBounceBackPS();
                    music.clip = bounckBackSucceedClip;//播放音效
                    music.Play();
                }
                else Harm(collision.GetComponentInParent<EnemyAction>().toughDamage);

            }
            else if (collision.tag == "SimpleEnemy")
                Harm(collision.GetComponentInParent<SimpleEnemyAction>().toughDamage);
            else if (collision.tag == "Item")
            {
                recover(collision.GetComponent<Item>().toughAdd);
            }
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


    public void BounceBacking()
    {
        music.clip = bounckBackClip;//播放音效
        music.Play();
        //if(!BounceBackIsSucceed)
        //{
            ChangeBounceSprite();
        //}    
    }

    /*IEnumerator BounceBackIsSucceedCoroutine()
    {
        BounceBackIsSucceed = true;
        yield return new WaitForSeconds(0.2f);
        BounceBackIsSucceed = false;
        StopCoroutine(nameof(BounceBackIsSucceedCoroutine));
    }*/

}
