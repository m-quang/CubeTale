using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class WhiteCube : MonoBehaviour, IEnviroment
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        
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
                Game.Instance.particleClone.startColor = Color.white;
                Game.Instance.particleClone.gameObject.SetActive(true);
            }
        }

    }

    public void localPosition(int x, int y, int z)
    {
        this.transform.localPosition = new Vector3(x, y, z);
    }
}
