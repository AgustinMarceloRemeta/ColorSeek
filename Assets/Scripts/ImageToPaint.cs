using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToPaint : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public List<Sprite> sprites= new List<Sprite>();
    [SerializeField] LevelSO levelSO;
    [SerializeField] Image image, visualFindColor;
    bool ready;
    [SerializeField] Button buttonToChange;
    public int progress;

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
        if (colors.Count > 0) visualFindColor.color = colors[0];
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
                visualFindColor.color = Color.black;
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
}
