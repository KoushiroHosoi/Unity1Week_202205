using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    private int endPattern;
    [SerializeField] private int debugNumber;
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private SoundManager soundManager;

    public bool isDebug;

    private void Awake()
    {
        endPattern = GameData.EndNumber;
        if (isDebug)
        {
            endPattern = debugNumber;
        }
        Debug.Log("í~êœì≈ó ÅF" + PlayerDataManager.AccumulatedPoisonAmount);
        Debug.Log(endPattern);
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (endPattern)
        {
            case 1:
                soundManager.PlayBgm(0);
                flowchart.SendFungusMessage("ED1");
                break;
            case 2:
                soundManager.PlayBgm(1);
                flowchart.SendFungusMessage("ED2");
                break;
            case 3:
                soundManager.PlayBgm(2);
                flowchart.SendFungusMessage("ED3");
                break;
            case 4:
                soundManager.PlayBgm(3);
                flowchart.SendFungusMessage("ED4");
                break;
            case 5:
                soundManager.PlayBgm(4);
                flowchart.SendFungusMessage("ED5");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
