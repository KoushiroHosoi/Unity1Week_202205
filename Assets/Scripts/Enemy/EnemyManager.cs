using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //プレイヤーを探すクラス
    [SerializeField] private EnemySearch searchCollider;
    //向きを変える時間と移動スピード
    [SerializeField] private float directionChangetime;
    [SerializeField] private float speed;

    //Playerを追っているかどうか
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
        //追跡中ならプレイヤーを追うようにしてそれ以外なら一定間隔で向きを変える
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
        //生成中なら前方向に速度を生成
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

    //向きを適当に変える
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
        //初めて会った時のみ演出を再生
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
            //反対方向に動かす
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
