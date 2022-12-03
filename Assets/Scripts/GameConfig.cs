using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Range(0, 11)]
    public int level;
    [SerializeField]
    public Config.Graphic graphic;
    [SerializeField]
    public Config.FPS fps;
    [SerializeField]
    public Config.Music music;
    [SerializeField]
    public Config.SFX sfx;
}

namespace Config
{
    public enum Music
    {
        On,
        Off,
    }

    public enum SFX
    {
        On,
        Off,
    }

    public enum Graphic
    {
        Low,
        Medium,
        High,
    }

    public enum FPS
    {
        Low = 30,
        Medium = 60,
        High = 90,
        VeryHigh = 120,
    }
}
