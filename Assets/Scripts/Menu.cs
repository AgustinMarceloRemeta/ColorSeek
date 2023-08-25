using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    LevelSO[] levels;
    Button[] buttons;
    [SerializeField] GameObject buttonsParent, cameraOn;

    private void Start()
    {
        FindCamera();
        SetLevels();
    }

    private void SetLevels()
    {
        buttons = buttonsParent.GetComponentsInChildren<Button>();
        levels = Resources.LoadAll<LevelSO>("LevelS");
        for (int i = 0; i < levels.Length; i++)
        {
            int levelActual = levels[i].level;
            int progress = PlayerPrefs.GetInt("Level" + levelActual);
            if (progress != 0) buttons[i].image.sprite = levels[i].sprites[progress - 1];
            else buttons[i].image.sprite = levels[i].defaultSprite;
            buttons[i].onClick.AddListener(() => SceneManager.LoadScene("Level " + levelActual));
        }
    }

    public void FindCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0 && devices != null) cameraOn.SetActive(false);
        else cameraOn.SetActive(true);
    }
}
