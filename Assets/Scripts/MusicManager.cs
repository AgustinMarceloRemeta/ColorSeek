using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] AudioSource source;
    [SerializeField] Slider slider;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeMusic() => source.volume = slider.value;
    
}
