using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToPaint : MonoBehaviour
{
    [SerializeField] List<Color> colors;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] Image image, visualFindColor;
    bool ready;
    [SerializeField] Button buttonToChange;

    private void Start()
    {
        buttonToChange.image.sprite = image.sprite;
        visualFindColor.color = colors[0];
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
            else visualFindColor.color = colors[0];
        }
    }

    public void FindColor()
    {
        if (CameraDetected.instance.detectedColor(colors[0])) ChangeImage();
        else print("Nooo");
    }
}
