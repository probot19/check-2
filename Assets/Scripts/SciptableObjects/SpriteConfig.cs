using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Sprite Config", fileName = "SpriteConfig")]
public class SpriteConfig : ScriptableObject
{
    public List<Sprite> sprites = new List<Sprite>();
}
