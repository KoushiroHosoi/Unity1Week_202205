using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    public delegate void FindPlayer();
    public FindPlayer onFindPlayer;

    public delegate void MissPlayer();
    public MissPlayer onMissPlayer;

    public delegate void ChasePlayer();
    public ChasePlayer onChasePlayer;

    //プレイヤーを発見したときの処理
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player(Clone)")
        {
            //プレイヤーとの遭遇が初なら止める
            if(GameData.IsMeetPlayer == false)
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
            }
            onFindPlayer();
        }
    }

    //プレイヤーを見失ったときの処理
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            onMissPlayer();
        } 
    }

    //プレイヤーが範囲内にいるときの処理
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            onChasePlayer();
        }
    }
}
