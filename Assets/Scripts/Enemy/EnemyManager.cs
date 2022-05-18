using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //�v���C���[��T���N���X
    [SerializeField] private EnemySearch searchCollider;
    //������ς��鎞�Ԃƈړ��X�s�[�h
    [SerializeField] private float directionChangetime;
    [SerializeField] private float speed;

    //Player��ǂ��Ă��邩�ǂ���
    private bool isChasing;

    private float timer;

    private GameObject playerObject;
    private PlayerDataManager playerData;

    private Vector3 vec;
    private Rigidbody rb;

    private SoundManager soundManager;

    public delegate void DestoyMine();
    public DestoyMine onDestoroyMine;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        isChasing = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
        
        playerObject= GameObject.Find("Player(Clone)");
        playerData = playerObject.GetComponent<PlayerDataManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        searchCollider.onFindPlayer += FindPlayer;
        searchCollider.onMissPlayer += MissPlayer;
        searchCollider.onChasePlayer += ChasePlayer;

        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //�ǐՒ��Ȃ�v���C���[��ǂ��悤�ɂ��Ă���ȊO�Ȃ���Ԋu�Ō�����ς���
        if (!isChasing)
        {
            timer += Time.deltaTime;
            if(timer >= directionChangetime)
            {
                ChangeDirection();
            }
        }
        else
        {
            vec = playerObject.transform.position - this.gameObject.transform.position;
        }
    }

    void FixedUpdate()
    {
        //�������Ȃ�O�����ɑ��x�𐶐�
        if (GameData.IsGamePlaying)
        {
            transform.rotation = Quaternion.LookRotation(vec);
            rb.velocity = transform.forward * speed;
        }
        else
        {
            rb.velocity = transform.forward * 0;
        }
    }

    //������K���ɕς���
    private void ChangeDirection()
    {
        timer = 0;
        float x = Random.Range(-1.0f, 1.0f);
        float z = Random.Range(-1.0f, 1.0f);
        vec = new Vector3(x, 0, z).normalized;
    }

    private void FindPlayer()
    {
        isChasing = true;
        soundManager.PlayBgm(1);
        //���߂ĉ�������̂݉��o���Đ�
        if (GameData.IsMeetPlayer == false)
        {
            GameData.ChangeIsGamePlaying(false);
            GameData.FirstMeetPlayer();
        }
    }

    private void MissPlayer()
    {
        isChasing = false;
        soundManager.PlayBgm(0);
        ChangeDirection();
    }

    private void ChasePlayer()
    {
        if(playerData.IsInPoison == true)
        {
            isChasing = false;
            //���Ε����ɓ�����
            timer = 0;
            vec = vec + Quaternion.Euler(0, 0, 180).eulerAngles;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<ITakeDamaged>();

        if (player != null)
        {
            soundManager.PlayBgm(0);
            player.TakeDamaged();
            onDestoroyMine();
            Destroy(gameObject);
        }
    }
}
