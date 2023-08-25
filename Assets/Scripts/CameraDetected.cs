using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraDetected : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] GameObject target;
    [SerializeField] GameObject findCamera;
    bool cameraActive;
    WebCamTexture webCam;
    private WebCamTexture webCamTexture;

    [Header("Ui")]
    [SerializeField] RawImage rawImage;
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] Image visualActualColor;
    [SerializeField] Sprite cameraNoFound;
    [SerializeField] Button buttonToChange;

    [Header("Color")]
    [SerializeField] float distanceColor;
    Color actualColor;

    public static CameraDetected instance;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    void Start()
    {
        AsignCamTexture();
    }

    public void AsignCamTexture()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0 && devices != null)
        {
            cameraActive = true;
            target.SetActive(true);
            findCamera.SetActive(false);
            webCamTexture = new WebCamTexture(devices[0].name);
            webCamTexture.Play();

            rawImage.texture = webCamTexture;
            RawImage rawImageCamera = buttonToChange.GetComponent<RawImage>();
            rawImageCamera.texture = webCamTexture;
            if (webCamTexture.videoRotationAngle != 0)
            {
                rawImage.transform.Rotate(0, 0, -webCamTexture.videoRotationAngle);
                rawImageCamera.transform.Rotate(0, 0, -webCamTexture.videoRotationAngle);
            }
        }
        else
        {
            cameraActive = false;
            rawImage.texture = cameraNoFound.texture;
            target.SetActive(false);
            findCamera.SetActive(true);
        }
    }

    void Update()
    {
       if(cameraActive) actualColor = GetCameraPixelColor(webCamTexture, .5f, .5f);
        visualActualColor.color = actualColor;
    }
    void OnDestroy()
    {
        if (webCamTexture != null && webCamTexture.isPlaying)
        {
            webCamTexture.Stop();
        }
    }
    private Color GetCameraPixelColor(WebCamTexture texture, float u, float v)
    {
        int x = Mathf.FloorToInt(u * texture.width);
        int y = Mathf.FloorToInt(v * texture.height);

        if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
        {
            Color[] pixels = texture.GetPixels();
            int index = y * texture.width + x;
            return pixels[index];
        }
        else
        {
            Debug.LogError("Coordenadas fuera de rango.");
            return Color.black;
        }
    }
    private float ColorDistance(Color a, Color b)
    {
        float distance = Mathf.Sqrt(
            Mathf.Pow(a.r - b.r, 2) +
            Mathf.Pow(a.g - b.g, 2) +
            Mathf.Pow(a.b - b.b, 2)
        );

        return distance;
    }

    public bool detectedColor(Color colorFind)
    {
        return ColorDistance(colorFind, actualColor) <= distanceColor;
    }
}
