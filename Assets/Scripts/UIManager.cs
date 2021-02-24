using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public Button RoadButton;
    public Button WoodButton;
    public Button SteelButton;
    public BarCreator BarCreator;

    public Image Help;

    public Slider BudgetSlider;
    public Text BudgetText;
    public Gradient myGradient;

    private void Start()
    {
        ToggleHelp();
        RoadButton.onClick.Invoke();
    }


    public void Play()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneBuildIndex:0);
    }

    public void ChangeBar(int myBarType)
    {
        if(myBarType == 0)
        {
            WoodButton.GetComponent<Outline>().enabled = false;
            RoadButton.GetComponent<Outline>().enabled = true;
            SteelButton.GetComponent<Outline>().enabled = false;
            BarCreator.BarToInstantiate = BarCreator.RoadBar;
        }
        else if(myBarType == 1)
        {
            WoodButton.GetComponent<Outline>().enabled = true;
            RoadButton.GetComponent<Outline>().enabled = false;
            SteelButton.GetComponent<Outline>().enabled = false;
            BarCreator.BarToInstantiate = BarCreator.WoodBar;
        }
        else if(myBarType == 2)
        {
            WoodButton.GetComponent<Outline>().enabled = false;
            RoadButton.GetComponent<Outline>().enabled = false;
            SteelButton.GetComponent<Outline>().enabled = true;
            BarCreator.BarToInstantiate = BarCreator.SteelBar;
        }
    }

    public void UpdateBudgetUI(float currentBudget, float levelBudget)
    {
        BudgetText.text = "$" + Mathf.RoundToInt(currentBudget).ToString();
        BudgetSlider.value = currentBudget / levelBudget;
        BudgetSlider.fillRect.GetComponent<Image>().color = myGradient.Evaluate(BudgetSlider.value);
    }

    public void ToggleHelp()
    {
        if (Help.IsActive())
            Help.enabled = false;
        else
            Help.enabled = true;
    }
}
