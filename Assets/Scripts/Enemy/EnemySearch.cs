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

    //�v���C���[�𔭌������Ƃ��̏���
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player(Clone)")
        {
            //�v���C���[�Ƃ̑��������Ȃ�~�߂�
            if(GameData.IsMeetPlayer == false)
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
            }
            onFindPlayer();
        }
    }

    //�v���C���[�����������Ƃ��̏���
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            onMissPlayer();
        } 
    }

    //�v���C���[���͈͓��ɂ���Ƃ��̏���
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            onChasePlayer();
        }
    }
}
