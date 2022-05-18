using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class PlayerCollisionController : MonoBehaviour, IGetPoison,IGetBook,ITakeDamaged
{
    private Rigidbody rb;
    private Flowchart flowchart;
    private PlayerDataManager playerData;
    private GameData gameData;
    private bool isFirstPoison;

    [SerializeField] private PlayerSearch playerSearch;
    private Material material;
    private Color normalColor;
    [SerializeField] private Color searchingColor;
    [SerializeField] private Color damagedColor;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        playerData = this.gameObject.GetComponent<PlayerDataManager>();
        gameData = GameObject.Find("GameManager").GetComponent<GameData>();

        playerSearch.onSearchingBook = StartSearchBook;
        playerSearch.onExitSearching = ExitSearchBook;
        material = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        normalColor = material.color;

        playerData.onEndTakeDamaged = ExitSearchBook;

        isFirstPoison = true;
    }

    private void Update()
    {
        if (playerData.IsTakeDamaged)
        {
            material.color = damagedColor;
        }
    }

    //一回目に毒沼に入った時のみ処理
    public void EnterPoison()
    {
        if (isFirstPoison)
        {
            rb.velocity = new Vector3(0, 0, 0);
            GameData.ChangeIsGamePlaying(false);
            flowchart.SendFungusMessage("FirstEnterPoison");
            isFirstPoison = false;
        }
    }

    //ソに接触中、回復させる
    public void GetPoison(float amount)
    {
        playerData.AccumulatePoison(amount);
        playerData.Animator.SetBool("isResting", true);  
    }

    //毒沼から出た時の処理
    public void LostPoison()
    {
        playerData.Animator.SetBool("isResting", false);
        playerData.ExitPoison();
    }

    //本を取得する
    public void GetBook()
    {
        gameData.GetBook();
        material.color = normalColor;
    }

    //敵からダメージを受けたときの処理
    public void TakeDamaged()
    {
        playerData.ChangePlayerMode();
    }

    //近くに本があったら色を変える
    private void StartSearchBook()
    {
        material.color = searchingColor;
    }

    //近くに本がなくなったら色を変える
    private void ExitSearchBook()
    {
        material.color = normalColor;
    }
}
