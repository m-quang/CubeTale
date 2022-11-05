using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    public TextAsset jsonfile;
    
    [SerializeField]
    public GameObject whiteObject;
    [SerializeField]
    public GameObject redObject;
    [SerializeField]
    public GameObject blueObject;
    [SerializeField]
    public GameObject blackObject;
    [SerializeField]
    public GameObject yellowObject;
    [SerializeField]
    public GameObject barrier;

    [System.Serializable]
    public class LevelMap
    {
        public int Width;
        public int Height;
        public int Level;
        public string[] Map;
    }

    [System.Serializable]
    public class Map
    {
        public LevelMap[] LevelMap;
    }

    public Map map = new Map();

    private void Awake()
    {
        map = JsonUtility.FromJson<Map>(jsonfile.text);
        //Debug.Log(map.LevelMap[0].Width);
        //Debug.Log(map.LevelMap[0].Height);
        int height = map.LevelMap[0].Height;
        int width = map.LevelMap[0].Width;
        for (int i = height*width - 1, y = 0; y < map.LevelMap[0].Height; y++)
        {
            for (int x = 0; x < map.LevelMap[0].Width; x++, i--)
            {
                CreateVoxel(i, x, y);
            }
        }
    }

    private void CreateVoxel(int i, int x, int y)
    {
        //GameObject o = Instantiate(voxelPrefab) as GameObject;
        //o.transform.localPosition = new Vector3((x + 0.5f) , 0, (y + 0.5f));
        //Debug.Log(map.LevelMap[0].Map[i]);
        GameObject o = new GameObject();

        switch (map.LevelMap[0].Map[i])
        {
            case "x":
                o = Instantiate(whiteObject) as GameObject;
                o.transform.localPosition = new Vector3(y, 0, x);
                break;
            case "a":
                o = Instantiate(yellowObject) as GameObject;
                o.transform.localPosition = new Vector3(y, 0, x);
                break;
            case "b":
                o = Instantiate(whiteObject) as GameObject;
                o.transform.localPosition = new Vector3(y, 0, x);
                GameObject b = Instantiate(barrier) as GameObject;
                b.transform.localPosition = new Vector3(y, 1, x);
                break;
            case ".":
                o = Instantiate(blackObject) as GameObject;
                o.transform.localPosition = new Vector3(y, 0, x);
                break;
            case "j":
                o = Instantiate(blueObject) as GameObject;
                o.transform.localPosition = new Vector3(y, 0, x);
                break;
            case "u":
                o = Instantiate(redObject) as GameObject;
                o.transform.localPosition = new Vector3(y, 0, x);
                break;
            case "s":
                o = Instantiate(whiteObject) as GameObject;
                o.transform.localPosition = new Vector3(y, 0, x);
                GameObject s = Instantiate(blackObject) as GameObject;
                s.transform.localPosition = new Vector3(y, 1, x);
                break;
            default:
                break;
        }
    }
}