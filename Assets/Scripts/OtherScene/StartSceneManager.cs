using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager.PlayBgm(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("GameMainScene");
    }
}
