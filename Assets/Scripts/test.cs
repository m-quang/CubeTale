using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    public GameObject playerController;
    [SerializeField]
    public GameObject laser;
    [SerializeField]
    public LayerMask gameLayers;

    GameObject player;

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
        for (int i = height * width - 1, y = 0; y < map.LevelMap[0].Height; y++)
        {
            for (int x = 0; x < map.LevelMap[0].Width; x++, i--)
            {
                CreateVoxel(i, x, y);
            }
        }

        player = new GameObject();
    }

    private void Start()
    {
        GameObject ls = Instantiate(laser) as GameObject;
    }

    void Update()
    {
        laser.GetComponent<LineRenderer>().enabled = true;
        //Debug.Log(player.GetComponent<Transform>().position);
        Ray ray = new Ray(player.GetComponent<Transform>().position, new Vector3(0, -1, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f, gameLayers))
        {
            // Debug.Log(hit.collider.gameObject.name + " something was hit");
            if (hit.collider.gameObject.name == "YellowCube")
            {
                RaycastHit hit1;
                Ray ray1 = new Ray(player.GetComponent<Transform>().position, new Vector3(1, 0, 0));
                Ray ray2 = new Ray(player.GetComponent<Transform>().position, new Vector3(-1, 0, 0));
                Ray ray3 = new Ray(player.GetComponent<Transform>().position, new Vector3(0, 0, 1));
                Ray ray4 = new Ray(player.GetComponent<Transform>().position, new Vector3(0, 0, -1));
                if (Physics.Raycast(ray1, out hit1, 10f, gameLayers))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    laser.GetComponent<LineRenderer>().SetPosition(0, player.GetComponent<Transform>().position);
                    laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, player.GetComponent<Transform>().position.y, targetPos.z));
                }
                if (Physics.Raycast(ray2, out hit1, 10f, gameLayers))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    laser.GetComponent<LineRenderer>().SetPosition(0, player.GetComponent<Transform>().position);
                    laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, player.GetComponent<Transform>().position.y, targetPos.z));
                }
                if (Physics.Raycast(ray3, out hit1, 10f, gameLayers))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    laser.GetComponent<LineRenderer>().SetPosition(0, player.GetComponent<Transform>().position);
                    laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, player.GetComponent<Transform>().position.y, targetPos.z));
                }
                if (Physics.Raycast(ray4, out hit1, 10f, gameLayers))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    laser.GetComponent<LineRenderer>().SetPosition(0, player.GetComponent<Transform>().position);
                    laser.GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, player.GetComponent<Transform>().position.y, targetPos.z));
                }
            }
        }
        Debug.DrawRay(player.GetComponent<Transform>().position, new Vector3(0, -1, 0), Color.red);
    }

    private void CreateVoxel(int i, int x, int y)
    {
        //GameObject o = Instantiate(voxelPrefab) as GameObject;
        //o.transform.localPosition = new Vector3((x + 0.5f) , 0, (y + 0.5f));
        //Debug.Log(map.LevelMap[0].Map[i]);
        if (map.LevelMap[0].Map[i] != "0")
        {
            GameObject o;
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
                    GameObject player = Instantiate(playerController) as GameObject;
                    player.transform.localPosition = new Vector3(y, 1, x);
                    break;
                default:
                    break;
            }
        }
    }
}
