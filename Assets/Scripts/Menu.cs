using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]List<LevelSO> levels;
    [SerializeField] List<Button> buttons;

    private void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            buttons[i].image.sprite = levels[i].sprites[PlayerPrefs.GetInt("Level" + levels[i].level)];
            buttons[i].onClick.AddListener(() => SceneManager.LoadScene("Level"+ i)) ;
        }
    }
}
