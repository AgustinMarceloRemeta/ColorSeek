using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject buttonsParent, selectDifficulty, principal, easy, medium, hard;
    [SerializeField] float easyValue, mediumValue, hardValue;
    public LevelSO[] levels;
    public Button[] buttons;


    private void Start()
    {
        float distance = PlayerPrefs.GetFloat("DistanceColor");
        if (distance != 0)
        {
            selectDifficulty.SetActive(false);
            principal.SetActive(true);
            if (easyValue == distance) easy.SetActive(true);
            else if (mediumValue == distance) medium.SetActive(true);
            else if (hardValue == distance) hard.SetActive(true);
        }
        levels = Resources.LoadAll<LevelSO>("LevelS");
        buttons = buttonsParent.GetComponentsInChildren<Button>();
        for (int i = 0; i < levels.Length; i++)
        {
            int levelActual = levels[i].level;
            int progress = PlayerPrefs.GetInt("Level" + levelActual);
            if (progress != 0) buttons[i].image.sprite = levels[i].sprites[progress-1];
            else buttons[i].image.sprite = levels[i].defaultSprite;
            buttons[i].onClick.AddListener(() => SceneManager.LoadScene("Level "+levelActual));
        }
    }

    public void SetDistance(float detected)
    {
        PlayerPrefs.SetFloat("DistanceColor", detected);
    }
}
