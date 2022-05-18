using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody rb;
    float moveX;
    float moveZ;

    private Vector3 playerPos;

    private PlayerDataManager playerData;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

        playerPos = this.gameObject.GetComponent<Transform>().position;
        playerData = this.gameObject.GetComponent<PlayerDataManager>();

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //���͂��擾
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (GameData.IsGamePlaying && !playerData.IsTakeDamaged)
        {
            //���ۂɑ��x�𐶐����Ĉړ�������
            rb.velocity = new Vector3(moveX, 0, moveZ).normalized * playerData.MoveSpeed;
            if (rb.velocity.magnitude > 0)
            {
                soundManager.PlaySe(0);
                playerData.Animator.SetBool("isMoving", true);
            }
            else
            {
                playerData.Animator.SetBool("isMoving", false);
            }

            //�����̕ύX
            Vector3 diff = transform.position - playerPos;
            if (diff.magnitude > 0.01)
            {
                Quaternion rot = Quaternion.LookRotation(diff);
                rot = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * 5);
                transform.rotation = rot;
            }
            playerPos = transform.position;
        }
    }
}
