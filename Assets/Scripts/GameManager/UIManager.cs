using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider poisonSlider;

    [SerializeField] private Image bookImage;
    [SerializeField] private Text bookCount;

    [SerializeField] private GameObject soundPanel;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    private PlayerDataManager playerData;
    private GameData gameData;
    private SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        bgmSlider.value = soundManager.BgmVolume;
        seSlider.value = soundManager.SeVolume;

        bgmSlider.onValueChanged.AddListener(value => soundManager.BgmVolume = value);
        seSlider.onValueChanged.AddListener(value => soundManager.SeVolume = value);
    }

    public void SetUpUI()
    {
        poisonSlider.gameObject.SetActive(true);
        bookImage.gameObject.SetActive(true);

        playerData = GameObject.Find("Player(Clone)").GetComponent<PlayerDataManager>();
        playerData.onChangePoison += UpdatePoisonUI;
        UpdatePoisonUI();

        gameData = this.gameObject.GetComponent<GameData>();
        gameData.onChangeBookAmount += UpdateBookUI;
        UpdateBookUI();
    }

    private void Update()
    {
        if(GameData.IsGamePlaying == false)
        {
            poisonSlider.gameObject.SetActive(false);
            bookImage.gameObject.SetActive(false);
        }
        else
        {
            poisonSlider.gameObject.SetActive(true);
            bookImage.gameObject.SetActive(true);
        }

        PauseUI();
    }

    public void UpdatePoisonUI()
    {
        poisonSlider.value = playerData.NowPoiososAmount / playerData.MaxPoisonAmount;
    }

    public void UpdateBookUI()
    {
        bookCount.text = ("X " + gameData.BookAmount.ToString());
    }

    public void PauseUI()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
        {
            if (soundPanel.activeSelf == true)
            {
                soundPanel.SetActive(false);
                GameData.ChangeIsGamePlaying(true);
            }
            else
            {
                if (GameData.IsGamePlaying == true)
                {
                    soundPanel.SetActive(true);
                    GameData.ChangeIsGamePlaying(false);
                }
            }
        }
    }
}
