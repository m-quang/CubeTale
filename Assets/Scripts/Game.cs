using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    [SerializeField]
    public TextAsset jsonfile;

    [SerializeField]
    public int m_Level = 4;

    [SerializeField]
    public GameConfig config;

    public bool isWin = false;

    [SerializeField]
    public WhiteCube whiteObject;
    [SerializeField]
    public RedCube redObject;
    [SerializeField]
    public BlueCube blueObject;
    [SerializeField]
    public BlackCube blackObject;
    [SerializeField]
    public YellowCube yellowObject;
    [SerializeField]
    public Barrier barrier;
    [SerializeField]
    public Player playerController;
    [SerializeField]
    public LayerMask gameLayers;

    List<IEnviroment> enviroments = new List<IEnviroment>();

    public Player player { set; get; }

    [SerializeField]
    public ParticleSystem particle;
    
    public ParticleSystem particleClone { set; get; }

    public GameObject frontEnd;

    #region map
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
    #endregion

    #region ray
    private bool isCollider = false;

    private bool isLasing = false;
    private bool destroyLasedObject = false;

    [SerializeField]
    private GameObject laser;

    GameObject ls;
    #endregion

    [SerializeField]
    GameButton gameButton;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(frontEnd);
        }

        gameButton.InitScene();

        map = JsonUtility.FromJson<Map>(jsonfile.text);
        int height = map.LevelMap[config.level].Height;
        int width = map.LevelMap[config.level].Width;
        for (int i = height * width - 1, y = 0; y < map.LevelMap[config.level].Height; y++)
        {
            for (int x = 0; x < map.LevelMap[config.level].Width; x++, i--)
            {
                CreateMap(i, x, y);
            }
        }

        ls = Instantiate(laser) as GameObject;
        ls.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
        ls.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
    }

    private void Start()
    {
        particle.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        updateLazerRay();
    }

    private void Update()
    {
        if (isWin == true)
        {

        }

    }

    void LateUpdate()
    {
        if (Input.anyKeyDown)
        {
            createRay();

            // rollback if stand in nothing
            if (player.currentStandingBlock == default || isCollider == true)
            {
                isCollider = false;
                player.mAnimator.Play("Animation");
                player.currentPosition = player.prevPosition;
                player.nextPosition = default;
                createRay();
            }

            if (player.currentStandingBlock != default)
            {
                if (player.currentStandingBlock.name == "RedCube")
                {
                    if (player.nextBlock != default)
                    {
                        Destroy(player.nextBlock.transform.parent.gameObject);
                        particleClone = Instantiate(particle, player.nextBlock.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)), GameObject.Find("Enviroment").transform);
                        particleClone.gameObject.SetActive(false);
                        player.nextBlock = default;
                    }
                }

            }

            if (player.prevousStandBlock)
            {
                if (player.prevousStandBlock.name == "RedCube" && player.currentPosition != player.transform.position)
                {
                    player.prevousStandBlock.transform.parent.GetComponentInChildren<Animator>().Play("Up");
                }
                if (player.prevousStandBlock.name == "YellowCube" && player.currentPosition != player.transform.position)
                {
                    player.prevousStandBlock.transform.parent.GetComponentInChildren<Animator>().Play("Up");
                }
            }

            /*            Debug.Log("currentStandingBlock: " + player.currentStandingBlock);
                        Debug.Log("prevousStandBlock: " + player.prevousStandBlock);
                        Debug.Log("getNextBlock: " + player.nextBlock);*/
        }
        
        if (player.currentStandingBlock)
        {

            if (player.currentStandingBlock.name == "BlackObject" && player.currentPosition == player.transform.position)
            {
                if(isWin == false)
                {
                    StartCoroutine(Winning());
                }
                isWin = true;
            }
        }
    }

    IEnumerator Winning()
    {
        player.mAnimator.Play("Celebration");
        yield return new WaitForSeconds(1);
        gameButton.ShowPanelScore();
    }

    private void CreateMap(int i, int x, int y)
    {
        if (map.LevelMap[config.level].Map[i] != "0")
        {
            switch (map.LevelMap[config.level].Map[i])
            {
                case "x":
                    WhiteCube whiteCube = Instantiate(whiteObject);
                    whiteCube.localPosition(y, 0, x);
                    enviroments.Add(whiteCube);
                    break;
                case "a":
                    YellowCube yellowCube = Instantiate(yellowObject);
                    yellowCube.localPosition(y, 0, x);
                    enviroments.Add(yellowCube);
                    break;
                case "b":
                    WhiteCube whiteCube1 = Instantiate(whiteObject);
                    whiteCube1.localPosition(y, 0, x);
                    enviroments.Add(whiteCube1);
                    Barrier barrier1 = Instantiate(barrier);
                    barrier1.localPosition(y, 1, x);
                    enviroments.Add(barrier1);
                    break;
                case ".":
                    BlackCube blackCube = Instantiate(blackObject);
                    blackCube.localPosition(y, 0, x);
                    enviroments.Add(blackCube);
                    break;
                case "j":
                    BlueCube blueCube = Instantiate(blueObject);
                    blueCube.localPosition(y, 0, x);
                    enviroments.Add(blueCube);
                    break;
                case "u":
                    RedCube redCube = Instantiate(redObject);
                    redCube.localPosition(y, 0, x);
                    enviroments.Add(redCube);
                    break;
                case "s":
                    WhiteCube whiteCube2 = Instantiate(whiteObject);
                    whiteCube2.localPosition(y, 0, x);
                    enviroments.Add(whiteCube2);
                    player = Instantiate(playerController);
                    player.transform.localPosition = new Vector3(y, 0.9f, x);
                    break;
                default:
                    break;
            }
        }
    }

    public void ClearMap()
    {

    }

    public void RestartLevel()
    {
        ClearMap();
        SceneManager.LoadScene("Game");
    }

    public void createRay()
    {
        // check standing ray
        Ray ray = new Ray(player.currentPosition, new Vector3(0, -1, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1f, gameLayers))
        {
            player.currentStandingBlock = hit.collider.gameObject;
        }
        else
        {
            player.currentStandingBlock = default;
        }

        Ray prevoursBlockRay = new Ray(player.prevPosition, new Vector3(0, -1, 0));
        RaycastHit prevoursBlockHit;
        if (Physics.Raycast(prevoursBlockRay, out prevoursBlockHit, 1f, gameLayers))
        {
            player.prevousStandBlock = prevoursBlockHit.collider.gameObject;
        }
        else
        {
            player.prevousStandBlock = default;
        }

        Ray nextBlockRay = new Ray(player.nextPosition, new Vector3(0, -1, 0));
        RaycastHit nextBlockHit;
        if (Physics.Raycast(nextBlockRay, out nextBlockHit, 1f, gameLayers))
        {
            player.nextBlock = nextBlockHit.collider.gameObject;
        }
        else
        {
            player.nextBlock = default;
        }

        // collision ray
        Ray collisionRay = new Ray(player.mTransform.position, player.currentPosition - player.prevPosition);
        RaycastHit collisionHit;
        if (Physics.Raycast(collisionRay, out collisionHit, 0.70f, gameLayers))
        {
            isCollider = true;
        }
    }

    public void updateLazerRay()
    {
        // lazer ray
        if (player.currentStandingBlock != default)
        {
            if (player.currentStandingBlock.name == "YellowCube" && player.currentPosition == player.transform.position)
            {
                //Debug.Log("lasing");
                Ray laserRay = new Ray(player.mTransform.position, player.currentPosition - player.prevPosition);
                RaycastHit laserHit;
                if (Physics.Raycast(laserRay, out laserHit, 10f, gameLayers))
                {
                    //Debug.Log("lasing");
                    isLasing = true;
                    Vector3 targetPos = laserHit.collider.gameObject.GetComponent<Transform>().position;
                    ls.GetComponent<LineRenderer>().SetPosition(0, player.currentPosition);
                    ls.GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, player.mTransform.position.y, targetPos.z));
                    StartCoroutine("lasing");

                    if (destroyLasedObject)
                    {
                        Destroy(laserHit.collider.gameObject);
                        particleClone = Instantiate(particle, laserHit.collider.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)), GameObject.Find("Enviroment").transform);
                        particleClone.startColor = Color.yellow;
                        particleClone.gameObject.SetActive(true);
                        destroyLasedObject = false;
                        StopCoroutine("lasing");

                    }
                }
                else
                {
                    ls.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
                    ls.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
                    StopCoroutine("lasing");
                }
            }
            else
            {
                ls.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
                ls.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
                StopCoroutine("lasing");
            }
        }
    }

    IEnumerator lasing()
    {
        if (isLasing)
        {
            yield return new WaitForSeconds(1f);
        } 
        else
        {
            yield break;
        }
        destroyLasedObject = true;
        ls.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
        ls.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
        isLasing = false;
    }
}
