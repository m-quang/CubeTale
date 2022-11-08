using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCube : MonoBehaviour, IEnviroment
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

    }

    public void localPosition(int x, int y, int z)
    {
        this.transform.localPosition = new Vector3(x, y, z);
    }
}
