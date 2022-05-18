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

    //���ڂɓŏ��ɓ��������̂ݏ���
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

    //�\�ɐڐG���A�񕜂�����
    public void GetPoison(float amount)
    {
        playerData.AccumulatePoison(amount);
        playerData.Animator.SetBool("isResting", true);  
    }

    //�ŏ�����o�����̏���
    public void LostPoison()
    {
        playerData.Animator.SetBool("isResting", false);
        playerData.ExitPoison();
    }

    //�{���擾����
    public void GetBook()
    {
        gameData.GetBook();
        material.color = normalColor;
    }

    //�G����_���[�W���󂯂��Ƃ��̏���
    public void TakeDamaged()
    {
        playerData.ChangePlayerMode();
    }

    //�߂��ɖ{����������F��ς���
    private void StartSearchBook()
    {
        material.color = searchingColor;
    }

    //�߂��ɖ{���Ȃ��Ȃ�����F��ς���
    private void ExitSearchBook()
    {
        material.color = normalColor;
    }
}
