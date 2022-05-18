using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGroundManager : MonoBehaviour
{
    [SerializeField] private float recoverAmount;
    private MeshRenderer meshRenderer;
    private float value;

    void Start()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, Time.time));
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<IGetPoison>();

        if (player != null && GameData.IsGamePlaying == true)
        {
            player.EnterPoison();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        var player = collision.gameObject.GetComponent<IGetPoison>();

        if(player != null && GameData.IsGamePlaying == true)
        {
            player.GetPoison(recoverAmount);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var player = collision.gameObject.GetComponent<IGetPoison>();
        if (player != null && GameData.IsGamePlaying == true)
        {
            player.LostPoison();
        }
    }
}
