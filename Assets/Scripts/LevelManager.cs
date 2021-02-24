using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public BarCreator BarCreator;

    public static int CurrentLevel;
    public int TotalLevels;

    public GameObject LoadedLevel;
    public GameObject[] Levels;

    private void Awake()
    {
        TotalLevels = Levels.Length;
        LoadNext();
    }

    [ContextMenu("LoadLevel")]
    public void LoadLevel()
    {
        if (CurrentLevel == TotalLevels - 1)
            CurrentLevel = -1;
        CurrentLevel++;
        SceneManager.LoadScene(0);
    }
    public void LoadNext()
    {
        LoadedLevel = Instantiate(Levels[CurrentLevel]);
        GameManager.LevelBudget = Levels[CurrentLevel].GetComponent<Level>().LevelBudget;
    }

    private void OnEnable()
    {
        Events.LevelComplete += LoadLevel;
    }
    private void OnDisable()
    {
        Events.LevelComplete -= LoadLevel;
    }
}
/*
public void LoadNextLevel()
{
    if (CurrentLevel == TotalLevels)
        CurrentLevel = 0;
    CurrentLevel++;

    Transform[] allChildren = BarCreator.PointParent.GetComponentsInChildren<Transform>();
    foreach (Transform child in allChildren)
    {
        Destroy(child.gameObject);
    }
    allChildren = EnvironmentParent.GetComponentsInChildren<Transform>();
    foreach (Transform child in allChildren)
    {
        Destroy(child.gameObject);
    }

    Point[] Points = Levels[CurrentLevel].GetComponentsInChildren<Point>();
    foreach (Point child in Points)
    {
        child.gameObject.transform.parent = BarCreator.PointParent.transform;
    }
    BoxCollider2D[] BoxCollider2Ds = Levels[CurrentLevel].GetComponentsInChildren<BoxCollider2D>();
    foreach (BoxCollider2D child in BoxCollider2Ds)
    {
        child.gameObject.transform.parent = EnvironmentParent.transform;
    }

    GameManager.LevelBudget = Levels[CurrentLevel].GetComponent<Budget>().budget;
}*/