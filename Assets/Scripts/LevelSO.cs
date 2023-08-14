using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ScriptableLevel", menuName = "Levels")]
public class LevelSO : ScriptableObject
{
    public int level;
    public List<Color> colors;
    public List<Sprite> sprites;
    public Sprite defaultSprite;
}
