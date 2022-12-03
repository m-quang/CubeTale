using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : MonoBehaviour, IEnviroment
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            GetComponentInChildren<Animator>().Play("Up");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetComponentInChildren<Animator>().Play("Down");
        }
    }

    public void Init()
    {

    }

    public void OnDestroy()
    {
        if (Game.Instance)
        {
            if (Game.Instance.particleClone)
            {
                Game.Instance.particleClone.startColor = Color.red;
                Game.Instance.particleClone.gameObject.SetActive(true);
            }
        }
    }

    public void localPosition(int x, int y, int z)
    {
        this.transform.localPosition = new Vector3(x, y, z);
    }
}
