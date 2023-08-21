using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToPaint : MonoBehaviour
{
    List<Color> colors = new List<Color>();
    List<Sprite> sprites= new List<Sprite>();
    [SerializeField] LevelSO levelSO;
    [SerializeField] Image image, visualFindColor;
    bool ready;
    [SerializeField] Button buttonToChange;
    [SerializeField] GameObject buttonReset, buttonImage, cameraParent,pictureParent;
    int progress;

    private void Start()
    {
        UpdateSO();
        SetVisual();
    }

    private void UpdateSO()
    {
        for (int i = 0; i < levelSO.colors.Count; i++)
        {
            colors.Add(levelSO.colors[i]);
            sprites.Add(levelSO.sprites[i]);
        }
        progress = PlayerPrefs.GetInt("Level" + levelSO.level);
    }

    private void SetVisual()
    {
        if (progress <= 0)
        {
            image.sprite = levelSO.defaultSprite;
            buttonToChange.image.sprite = image.sprite;
        }
        else
        {
            image.sprite = sprites[progress - 1];
            buttonToChange.image.sprite = sprites[progress - 1];
            for (int i = 0; i <= progress; i++)
            {
                colors.RemoveAt(0);
                sprites.RemoveAt(0);
            }
        }
        if (colors.Count > 0)
        {
            visualFindColor.color = colors[0];
            buttonReset.SetActive(false);
            buttonImage.gameObject.SetActive(true);
        }
        else
        {
            buttonReset.SetActive(true);
            buttonImage.gameObject.SetActive(false);
        }
    }

    public void ChangeImage()
    {
        if (!ready)
        {
            image.sprite = sprites[0];
            buttonToChange.image.sprite = sprites[0];
            colors.RemoveAt(0);
            sprites.RemoveAt(0);
            if (colors.Count <= 0)
            {
                ready = true;
                buttonReset.SetActive(true);
                pictureParent.SetActive(true);
                buttonImage.SetActive(false);
                cameraParent.SetActive(false);
            }
            else
            {
                visualFindColor.color = colors[0];
                progress++;
                PlayerPrefs.SetInt("Level" + levelSO.level, progress);
            }

        }
    }

    public void FindColor()
    {
        if(colors.Count>0)
        if (CameraDetected.instance.detectedColor(colors[0])) ChangeImage();
        else print("Nooo");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) PlayerPrefs.DeleteAll();
    }

    public void ResetImage()
    {
        progress= 0;
        PlayerPrefs.SetInt("Level" + levelSO.level, 0);
        buttonReset.SetActive(false);
        UpdateSO();
        SetVisual();
    }
}
