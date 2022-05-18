using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ÉJÉÅÉâé¸ÇËÇÃêßå‰ÇÇ®Ç±Ç»Ç§

public class CameraController : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private float normalDistance;
    [SerializeField] private float farDistance;

    private float distance;
    private float zPos;
    private Vector3 pos;

    private PlayerDataManager playerData;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetUpCamera()
    {
        player = GameObject.Find("Player(Clone)");
        this.gameObject.transform.Rotate(75, 0, 0);
        playerData = player.GetComponent<PlayerDataManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GameData.IsGamePlaying == true)
        {
            if (playerData.NowPoiososAmount <= 40)
            {
                distance = farDistance;
                zPos = -2.4f;
            }
            else
            {
                distance = normalDistance;
                zPos = -2.15f;
            }

            pos = new Vector3(0, distance, zPos);
            this.gameObject.transform.position = player.transform.position + pos;
            this.gameObject.transform.rotation = Quaternion.Euler(75, 0, 0);
        }
    }
}
