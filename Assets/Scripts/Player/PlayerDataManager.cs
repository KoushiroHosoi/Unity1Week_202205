using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    //�Łi�\�j����̃f�[�^
    [SerializeField] private float maxPoisonAmount;
    private float nowPoiososAmount;
    private static float accumulatedPoisonAmount;
    [SerializeField] private float dicreasePoisonSpeed;
    private bool isInPoison;

    //�X�s�[�h����
    [SerializeField] private float normalMoveSpeed;
    [SerializeField] private float highMoveSpeed;
    private float moveSpeed;

    //�A�j���[�V����
    private Animator animator;

    //�_���[�W���󂯂Ă��邩�ǂ���
    private bool isTakeDamaged;

    //�G���E�������Ƃ����邩
    private static bool isKill;

    private SoundManager soundManager;

    //�f���Q�[�g
    public delegate void OnChangePoison();
    public OnChangePoison onChangePoison;

    public delegate void EndTakeDamaged();
    public EndTakeDamaged onEndTakeDamaged;

    public float MaxPoisonAmount { get => maxPoisonAmount; }
    public float NowPoiososAmount { get => nowPoiososAmount; }
    public float MoveSpeed { get => moveSpeed; }
    public bool IsInPoison { get => isInPoison; }
    public Animator Animator { get => animator; }
    public bool IsTakeDamaged { get => isTakeDamaged; }
    public static float AccumulatedPoisonAmount { get => accumulatedPoisonAmount; }
    public static bool IsKill { get => isKill; }

    // Start is called before the first frame update
    void Start()
    {
        //�f�[�^�̏�����
        moveSpeed = normalMoveSpeed;
        nowPoiososAmount = MaxPoisonAmount;
        accumulatedPoisonAmount = 0;
        isInPoison = false;
        isTakeDamaged = false;
        isKill = false;

        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameData.IsGamePlaying == true)
        {
            if (nowPoiososAmount < 40)
            {
                moveSpeed = highMoveSpeed;
            }
            else
            {
                moveSpeed = normalMoveSpeed;
            }

            nowPoiososAmount = nowPoiososAmount - dicreasePoisonSpeed;

            if (nowPoiososAmount <= 0)
            {
                nowPoiososAmount = 0;
                GameData.GameEnd(1);
            }

            onChangePoison();
        }
    }

    public void AccumulatePoison(float amount)
    {
        isInPoison = true;
        soundManager.PlaySe(1);
        if(nowPoiososAmount <= maxPoisonAmount)
        {
            nowPoiososAmount += amount;
            accumulatedPoisonAmount += amount;
        }
        else
        {
            nowPoiososAmount = maxPoisonAmount;
        }

        onChangePoison();
    }
    public void ExitPoison()
    {
        isInPoison = false;
    }

    public void ChangePlayerMode()
    {
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        soundManager.PlaySe(2);
        isKill = true;
        isTakeDamaged = true;
        yield return new WaitForSeconds(2);
        isTakeDamaged = false;
        onEndTakeDamaged();
        yield break;
    }
}
