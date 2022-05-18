using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject groundPrefab;

    [SerializeField] private GameObject bookPrefab;

    [SerializeField] private GameObject soPrefab;

    [SerializeField] private GameObject enemyFactory;

    [SerializeField] private GameObject wallPrefab;

    [SerializeField] private GameObject[,] groundObjects;
    
    private int[,] bookObjects;
    private int[,] soObjects;

    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartCreating()
    {
        gameData = this.gameObject.GetComponent<GameData>();
        groundObjects = new GameObject[gameData.MapSize, gameData.MapSize];
        bookObjects = new int[gameData.MapSize, gameData.MapSize];
        soObjects = new int[gameData.MapSize, gameData.MapSize];
        CreateGround();
        CreateBooks();
        CreateSos();
        CreateEnemyFactory();
    }

    private void CreateGround()
    {
        for (int i = 0; i < gameData.MapSize; i++)
        {
            for (int n = 0; n < gameData.MapSize; n++)
            {
                Vector3 transform = new Vector3(i, 0, n);
                GameObject ground = Instantiate(groundPrefab, transform, Quaternion.identity);
                groundObjects[i, n] = ground;
                bookObjects[i, n] = 0;
                soObjects[i, n] = 0;

                //�ǂ����
                if (i == gameData.MapSize -1 || i == 0 || n == gameData.MapSize -1 || n == 0)
                {
                    for(int m = 1; m < 4; m++)
                    {
                        Vector3 wallPos = new Vector3(i, m, n);
                        if(m == 3)
                        {
                            Instantiate(wallPrefab, wallPos, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(groundPrefab, wallPos, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }

    private void CreateBooks()
    {
        List<Vector3> bookPos = new List<Vector3>();

        for (int i = 0; i < gameData.BookAmount; i++)
        {
            A:

            //�����ňʒu�����߂�
            int xPos = Random.Range(12, gameData.MapSize - 12);
            int zPos = Random.Range(12, gameData.MapSize - 12);
            Vector3 pos = new Vector3(xPos, 1, zPos);
            
            float? distance = null;

            //���łɒu���ꂽ�{�ƈ��ȏ�̋���������Ă��邩�`�F�b�N
            foreach(var n in bookPos)
            {
                distance = Vector3.Distance(pos, n);

                //������40m�����Ȃ痐�����Đ�������
                if(distance <= 40)
                {
                    goto A;
                }
            }
            //����Ă����炻�̈ʒu�ɐ���
            bookPos.Add(pos);
            bookObjects[xPos, zPos] = 1;
            Instantiate(bookPrefab, pos, Quaternion.Euler(0, 0, 40));
        }
    }

    private void CreateSos()
    {
        List<Vector3> soPos = new List<Vector3>();

        for(int i = 0; i < gameData.SoAmount; i++)
        {
            A:
            int xPos;
            int zPos;
            Vector3 pos;
            //�����ňʒu�����߂�
            if (i == 0)
            {
                xPos = 8;
                zPos = 8;
            }
            else
            {
                xPos = Random.Range(5, gameData.MapSize - 5);
                zPos = Random.Range(5, gameData.MapSize - 5);
            }

            pos = new Vector3(xPos, 0, zPos);

            float? distance = null;

            //���łɒu���ꂽ�ŏ��ƈ��ȏ�̋���������Ă��邩�`�F�b�N
            foreach (var n in soPos)
            {
                distance = Vector3.Distance(pos, n);

                //������20m�����܂��͓��L�Əd�Ȃ��Ă���Ȃ痐�����Đ�������
                if (distance <= 20 || bookObjects[xPos, zPos] == 1)
                {
                    goto A;
                }
            }

            soPos.Add(pos);
            //Ground���폜���ă\�𐶐�
            for(int m =-1; m < 2; m++)
            {
                for (int l = -1; l < 2; l++)
                {
                    Destroy(groundObjects[xPos + m, zPos + l]);
                    soObjects[xPos, zPos] = 1;
                    Instantiate(soPrefab, pos + new Vector3(m, 0, l), Quaternion.identity);
                }
            }
            
        }
    }

    private void CreateEnemyFactory()
    {
        List<Vector3> enemyFactoryPos = new List<Vector3>();

        for (int i = 0; i < gameData.EnemyFactoryCount; i++)
        {
            A:

            //�����ňʒu�����߂�
            int xPos = Random.Range(12, gameData.MapSize - 12);
            int zPos = Random.Range(12, gameData.MapSize - 12);
            Vector3 pos = new Vector3(xPos, 1, zPos);

            float? distance = null;

            //���łɒu���ꂽ�{�ƈ��ȏ�̋���������Ă��邩�`�F�b�N
            foreach (var n in enemyFactoryPos)
            {
                distance = Vector3.Distance(pos, n);

                //������40m�����Ȃ痐�����Đ�������
                if (distance <= 30 || bookObjects[xPos, zPos] == 1 || soObjects[xPos, zPos] == 1)
                {
                    goto A;
                }
            }
            //����Ă����炻�̈ʒu�ɐ���
            enemyFactoryPos.Add(pos);
            Instantiate(enemyFactory, pos, Quaternion.identity);
        }
    }

    //�e�X�g�p���\�b�h
    private void DistanceTest(List<Vector3> posList,Vector3 pos)
    {
        float min = 10000;
        foreach(var n in posList)
        {
            float distance = Vector3.Distance(pos, n);
            if (distance < min)
            {
                min = distance;
            }
        }
        Debug.Log(min);
    }
}
