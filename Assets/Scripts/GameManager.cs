using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float LevelBudget = 9999;
    public float CurrentBudget = 0;

    public UIManager UIManager;

    public static Dictionary<Vector2, Point> AllPoints = new Dictionary<Vector2, Point>();
    private void Awake()
    {
        AllPoints.Clear();
        Time.timeScale = 0;
        CurrentBudget = LevelBudget;
        UIManager.UpdateBudgetUI(CurrentBudget, LevelBudget);
    }

    public bool CanPlaceItem(float itemCost)
    {
        if (itemCost > CurrentBudget)
            return false;
        else
            return true;
    }
    public void UpdateBudget(float itemCost)
    {
        CurrentBudget -= itemCost;
        UIManager.UpdateBudgetUI(CurrentBudget, LevelBudget);
    }

}
