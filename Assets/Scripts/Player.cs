using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform mTransform { set; get; }

    public Animator mAnimator { set; get; }

    public GameObject currentStandingBlock { set; get; }
    public GameObject prevousStandBlock { set; get; }
    public GameObject nextBlock { set; get; }

    [SerializeField]
    public Vector3 currentPosition;
    public Vector3 nextPosition;
    public Vector3 prevPosition;

    [SerializeField]
    private float speed = 1.2f;
    // Start is called before the first frame update

    [SerializeField]
    public LayerMask layersToHit;

    void Start()
    {
        mTransform =  GetComponent<Transform>();
        mAnimator = GetComponentInChildren<Animator>();
        currentPosition = mTransform.position;

        currentStandingBlock = default;
        prevousStandBlock = default;
        nextBlock = default;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(mTransform.position, new Vector3(0, -1, 0), Color.red);
        if (currentStandingBlock)
        {
            Debug.DrawRay(currentStandingBlock.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), Color.red);
        }        
        if (prevousStandBlock)
        {
            Debug.DrawRay(prevousStandBlock.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), Color.green);
        }        
        if (nextBlock)
        {
            Debug.DrawRay(nextBlock.transform.position + new Vector3(0, 1, 0), new Vector3(0, -1, 0), Color.blue);
        }


        //
        mTransform.position = Vector3.MoveTowards(mTransform.position, currentPosition, speed * Time.deltaTime);
        AnimatorClipInfo[] animatorinfo = mAnimator.GetCurrentAnimatorClipInfo(0);
        string current_animation = animatorinfo[0].clip.name;

        //
        if(!Game.Instance.isWin)
        {
            if (current_animation == "Idle")
            {
                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        speed = 1.2f;
                        prevPosition = currentPosition;
                        currentPosition = mTransform.position + new Vector3(-1, 0, 0);
                        nextPosition = currentPosition + (currentPosition - mTransform.position);
                        mTransform.rotation = new Quaternion(0, 0, 0, 0);
                        mTransform.Rotate(0, -90, 0);
                        currentPosition = new Vector3(Mathf.Round(currentPosition.x), (currentPosition.y), Mathf.Round(currentPosition.z));
                        mAnimator.Play("Move");
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        speed = 1.2f;
                        prevPosition = currentPosition;
                        currentPosition = mTransform.position + new Vector3(1, 0, 0);
                        nextPosition = currentPosition + (currentPosition - mTransform.position);
                        mTransform.rotation = new Quaternion(0, 0, 0, 0);
                        mTransform.Rotate(0, 90, 0);
                        currentPosition = new Vector3(Mathf.Round(currentPosition.x), (currentPosition.y), Mathf.Round(currentPosition.z));
                        mAnimator.Play("Move");
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        speed = 1.2f;
                        prevPosition = currentPosition;
                        currentPosition = mTransform.position + new Vector3(0, 0, 1);
                        nextPosition = currentPosition + (currentPosition - mTransform.position);
                        mTransform.rotation = new Quaternion(0, 0, 0, 0);
                        mTransform.Rotate(0, 0, 0);
                        currentPosition = new Vector3(Mathf.Round(currentPosition.x), (currentPosition.y), Mathf.Round(currentPosition.z));
                        mAnimator.Play("Move");
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        speed = 1.2f;
                        prevPosition = currentPosition;
                        currentPosition = mTransform.position + new Vector3(0, 0, -1);
                        nextPosition = currentPosition + (currentPosition - mTransform.position);
                        mTransform.rotation = new Quaternion(0, 0, 0, 0);
                        mTransform.Rotate(0, 180, 0);
                        currentPosition = new Vector3(Mathf.Round(currentPosition.x), (currentPosition.y), Mathf.Round(currentPosition.z));
                        mAnimator.Play("Move");
                    }
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        speed = 1.2f;
                        mAnimator.Play("Fall");
                    }
                }
            }
        }
    }

}
