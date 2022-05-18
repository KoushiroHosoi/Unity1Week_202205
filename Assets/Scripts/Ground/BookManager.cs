using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<IGetBook>();

        if(player != null && GameData.IsGamePlaying == true)
        {
            player.GetBook();
            Destroy(this.gameObject);
        }
    }
}
