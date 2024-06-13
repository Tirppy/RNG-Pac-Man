using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject settingsUI;

    [SerializeField] public Slider xSlider;
    [SerializeField] public Slider ySlider;

    [SerializeField] private TextMeshProUGUI sizeText;

    private int xSIZE = 10;
    private int ySIZE = 10;

    void Start()
    {
        settingsUI.SetActive(false);
    }

    public void EnableMain()
    {
        mainUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void EnableSettings()
    {
        mainUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void UpdateX()
    {
        xSIZE = (int) xSlider.value;
        UpdateText();
    }

    public void UpdateY()
    {
        ySIZE = (int) ySlider.value;
        UpdateText();
    }

    public void UpdateText()
    {
        sizeText.text = xSIZE + " X " + ySIZE;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void ExiGame()
    {
        Application.Quit();
    }
}
