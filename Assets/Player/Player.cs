using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public event UnityAction<float,float> onStaminaChanged;

    new Rigidbody2D rigidbody;
    private Vector2 moveVector2 = Vector2.zero;
    private bool playerIsBounceBack = false;

    [SerializeField, Tooltip("�������")] private PlayerInput input;
    [SerializeField,Tooltip("����ٶ�")] private float playerSpeed;
    [SerializeField,Tooltip("��Ҽ���ʱ��")] private float playerAccelerationTime;
    [SerializeField,Tooltip("��Ҽ���ʱ��")] private float playerDecelerationTime;
    [SerializeField, Tooltip("�������ֵ")] private float playerStamina;
    [SerializeField, Tooltip("����������ֵ")] private float playerMaxStamina; 
    [SerializeField, Tooltip("�������ֵ�ָ��ٶ�")] private float playerStaminaRecoverSpeed;
    [SerializeField, Tooltip("��ҵ�������")] private float playerBounceBackCost;
    [SerializeField, Tooltip("��ҵ���CD")] private float playerBounceBackCD;
    [SerializeField, Tooltip("�������CDImage")] private Image staminaCDImage;
    [SerializeField, Tooltip("�������CDText")] private Text staminaCDText;

    public bool playerIsInvicible = false;
    //[SerializeField, Tooltip("����ѱ�ֵ")] private float playerTough = 0;
    //[SerializeField, Tooltip("����ѱ�ֵ")] private float playerMaxTough = 100f;
    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onBounceBack += BounceBack;
    }

    private void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
        input.onBounceBack -= BounceBack;
    }

    void Start()
    {
        rigidbody.gravityScale = 0f;
        input.EnablePlayerInputs();
        playerStamina = playerMaxStamina;
        staminaCDText.text = playerBounceBackCD.ToString("0.0" + "s");
    }

    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerIsBounceBack);
    }

    void Move(Vector2 moveInput)
    {
        StopCoroutine("MoveCoroutine");
        moveVector2.x = moveInput.x; //���������ƶ�
        StartCoroutine(MoveCoroutine(playerAccelerationTime ,moveVector2.normalized * playerSpeed));
    }

    void StopMove()
    {
        StopCoroutine("MoveCoroutine");
        StartCoroutine(MoveCoroutine(playerDecelerationTime, Vector2.zero));
    }

    void BounceBack()
    {
        if(playerBounceBackCost <= playerStamina && !playerIsBounceBack)
        {
            StopCoroutine(nameof(BounceBackCoroutine));
            playerStamina -= playerBounceBackCost;
            onStaminaChanged.Invoke(playerStamina, playerMaxStamina);
            StartCoroutine(nameof(BounceBackCoroutine));
        }
    }

    void StaminaRecover()
    {
        StopCoroutine(nameof(StaminaRecoverCoroutine));
        if (playerStamina < playerMaxStamina)
        {
            StartCoroutine(nameof(StaminaRecoverCoroutine));
        }
    }

    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity)
    {
        float t = 0f;
        while(t < time)
        {
            t += Time.fixedDeltaTime;
            rigidbody.velocity = Vector2.Lerp(rigidbody.velocity, moveVelocity, t / time);
            yield return null;
        }
    }

    //����
    IEnumerator BounceBackCoroutine()
    {
        float t = 0f;
        Debug.Log(playerStamina);
        playerIsBounceBack = true;
        playerIsInvicible = true;
        while (t < playerBounceBackCD)
        {
            t += Time.deltaTime;
            staminaCDImage.fillAmount = t / playerBounceBackCD;
            staminaCDText.text = (playerBounceBackCD - t).ToString("0.0" + "s");
            yield return null;
        }
        playerIsBounceBack = false;
        playerIsInvicible = false;
        staminaCDText.text = playerBounceBackCD.ToString("0.0"+"s");
        StaminaRecover();
    }

    //�����ָ�
    IEnumerator StaminaRecoverCoroutine()
    {
        while(playerStamina <= playerMaxStamina)
        {
            if (playerIsBounceBack)
            {
                StopCoroutine(nameof(StaminaRecoverCoroutine));
            }
            playerStamina += Time.deltaTime* playerStaminaRecoverSpeed;
            onStaminaChanged.Invoke(playerStamina,playerMaxStamina);
            yield return null;
        }
    }
}
