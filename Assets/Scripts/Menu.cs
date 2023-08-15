using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject buttonsParent;
    public LevelSO[] levels;
    public Button[] buttons;

    private void Start()
    {
        levels = Resources.LoadAll<LevelSO>("LevelS");
        buttons = buttonsParent.GetComponentsInChildren<Button>();
        for (int i = 0; i < levels.Length; i++)
        {
            int levelActual = levels[i].level;
            int progress = PlayerPrefs.GetInt("Level" + levelActual);
            if (progress != 0) buttons[i].image.sprite = levels[i].sprites[progress-1];
            else buttons[i].image.sprite = levels[i].defaultSprite;
            buttons[i].onClick.AddListener(() => SceneManager.LoadScene("Level"+levelActual));
        }
    }
}
