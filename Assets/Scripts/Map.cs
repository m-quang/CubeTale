using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class Map : MonoBehaviour
{
    [SerializeField]
    public TextAsset jsonfile;

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
}

