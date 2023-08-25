using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ToMenu() =>  SceneManager.LoadScene("Menu");
}
