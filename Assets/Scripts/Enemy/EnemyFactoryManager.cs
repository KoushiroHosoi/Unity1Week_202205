using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyPrefab;
    [SerializeField] private float intervalTime;

    private bool nowInEnemy;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        nowInEnemy = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.IsGamePlaying)
        {
            if(timer > intervalTime && !nowInEnemy)
            {
                timer = 0;
                nowInEnemy = true;
                EnemyManager enemy = Instantiate(enemyPrefab, this.gameObject.transform);
                enemy.onDestoroyMine += DestroyMine;
            }
        }

        if (!nowInEnemy)
        {
            timer += Time.deltaTime;
        }
    }

    private void DestroyMine()
    {
        nowInEnemy = false;
    }
}
