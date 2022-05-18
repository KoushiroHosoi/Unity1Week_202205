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

    //ゲームスタート時の会話
    IEnumerator StartTalk()
    {
        soundManager.PlayBgm(0);
        //会話を始める
        GameData.MyFlowchart.SendFungusMessage("StartTalk");
        //ゲームモードを中断中にする
        GameData.ChangeIsGamePlaying(false);

        yield break;
    }

    //会話が終わった後の処理
    public void StartGame()
    {
        //マップを生成する
        createMap = this.gameObject.GetComponent<CreateMap>();
        createMap.StartCreating();

        //プレイヤーを生成する
        Instantiate(playerPrefab, playerStartPos, Quaternion.identity);

        //UIを表示させる
        uIManager = this.gameObject.GetComponent<UIManager>();
        uIManager.SetUpUI();
        
        gameData = this.gameObject.GetComponent<GameData>();

        //カメラの初期化をおこなう
        cameraController.SetUpCamera();

        ExitTalk();
    }

    public void ExitTalk()
    {
        GameData.ChangeIsGamePlaying(true);
    }
}
