using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 playerStartPos;

    [SerializeField] private CameraController cameraController;

    private CreateMap createMap;
    private UIManager uIManager;
    private GameData gameData;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        StartCoroutine(StartTalk());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�Q�[���X�^�[�g���̉�b
    IEnumerator StartTalk()
    {
        soundManager.PlayBgm(0);
        //��b���n�߂�
        GameData.MyFlowchart.SendFungusMessage("StartTalk");
        //�Q�[�����[�h�𒆒f���ɂ���
        GameData.ChangeIsGamePlaying(false);

        yield break;
    }

    //��b���I�������̏���
    public void StartGame()
    {
        //�}�b�v�𐶐�����
        createMap = this.gameObject.GetComponent<CreateMap>();
        createMap.StartCreating();

        //�v���C���[�𐶐�����
        Instantiate(playerPrefab, playerStartPos, Quaternion.identity);

        //UI��\��������
        uIManager = this.gameObject.GetComponent<UIManager>();
        uIManager.SetUpUI();
        
        gameData = this.gameObject.GetComponent<GameData>();

        //�J�����̏������������Ȃ�
        cameraController.SetUpCamera();

        ExitTalk();
    }

    public void ExitTalk()
    {
        GameData.ChangeIsGamePlaying(true);
    }
}
