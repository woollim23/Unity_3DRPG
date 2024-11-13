using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] public int StageLevel { get; private set; }
    public int DeathEnemyCount { get; private set; }

    [SerializeField] private GameObject mazePrefab;

    void Awake()
    {
        StageLevel = 1;
        NewStage();
    }

    public void StageClear()
    {
        StageLevelUp();

        Invoke("NewStage", 6);
    }

    private void StageLevelUp()
    {
        StageLevel++;
    }

    public void NewStage()
    {
        DeathEnemyCount = 0;

        GameObject currentMaze = GameObject.FindGameObjectWithTag("Maze");
        if (currentMaze != null)
        {
            Debug.Log("ÆÄ±«");
            Destroy(currentMaze);
        }

        CreateMaze();
    }

    private void CreateMaze()
    {
        Instantiate(mazePrefab, Vector3.zero, Quaternion.identity);
    }

    public void DeathEnemy()
    {
        DeathEnemyCount++;

        if (DeathEnemyCount == StageLevel)
            StageClear();
    }

}
