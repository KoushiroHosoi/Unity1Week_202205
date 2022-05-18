using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class GameData : MonoBehaviour
{
    private static Flowchart flowchart;
    [SerializeField] private Flowchart inspecterChart;

    private static bool isGamePlaying;
    private static int endNumber;
    private static bool isMeetPlayer;

    [SerializeField] private int mapSize;
    [SerializeField] private int bookAmount;
    [SerializeField] private int soAmount;
    [SerializeField] private int enemyFactoryCount;

    public delegate void OnChangeBookAmount();
    public OnChangeBookAmount onChangeBookAmount;

    public int MapSize { get => mapSize; }
    public int BookAmount { get => bookAmount; }
    public int SoAmount { get => soAmount; }
    public static bool IsGamePlaying { get => isGamePlaying; }
    public static int EndNumber { get => endNumber; }
    public int EnemyFactoryCount { get => enemyFactoryCount; }
    public static bool IsMeetPlayer { get => isMeetPlayer; }
    public static Flowchart MyFlowchart { get => flowchart; }

    private void Awake()
    {
        flowchart = inspecterChart;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGamePlaying = false;
        isMeetPlayer = false;
        endNumber = 0;
        Time.timeScale = 1;
    }

    public static void ChangeIsGamePlaying(bool b)
    {
        isGamePlaying = b;
    }

    public static void GameEnd(int e)
    {
        endNumber = e;
        SceneManager.LoadScene("EndScene");
    }

    public static void FirstMeetPlayer()
    {
        isMeetPlayer = true;
        MyFlowchart.SendFungusMessage("FirstMeetSoldier");
    }

    public void GetBook()
    {
        if(bookAmount > 0)
        {
            bookAmount--;
            onChangeBookAmount();
            if(bookAmount == 0)
            {
                if(PlayerDataManager.AccumulatedPoisonAmount > 1000)
                {
                    GameEnd(2);
                }
                else if(PlayerDataManager.AccumulatedPoisonAmount > 750)
                {
                    GameEnd(3);
                }
                else if(PlayerDataManager.AccumulatedPoisonAmount > 400 && PlayerDataManager.IsKill)
                {
                    GameEnd(4);
                }
                else if(PlayerDataManager.AccumulatedPoisonAmount > 400 && !PlayerDataManager.IsKill)
                {
                    GameEnd(5);
                }
            }
        }
    }
}
