using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform mTransform;

    private Animator mAnimator;

    [SerializeField]
    private float speed = 1.2f;
    // Start is called before the first frame update
    public Vector3 targetPosition = new Vector3(0, 1, 0);

    public LayerMask layersToHit;

    void Start()
    {
        mTransform =  GetComponent<Transform>();
        mAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        Ray ray = new Ray(mTransform.position, new Vector3(0, -1, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1f, layersToHit))
        {
            // Debug.Log(hit.collider.gameObject.name + " something was hit");
            if (hit.collider.gameObject.name == "YellowCube")
            {
                RaycastHit hit1;
                Ray ray1 = new Ray(mTransform.position, new Vector3(1, 0, 0));
                Ray ray2 = new Ray(mTransform.position, new Vector3(-1, 0, 0));
                Ray ray3 = new Ray(mTransform.position, new Vector3(0, 0, 1));
                Ray ray4 = new Ray(mTransform.position, new Vector3(0, 0, -1));
                if (Physics.Raycast(ray1, out hit1, 10f, layersToHit))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    GetComponent<LineRenderer>().SetPosition(0, mTransform.position);
                    GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, mTransform.position.y, targetPos.z));
                    
                }
                if (Physics.Raycast(ray2, out hit1, 10f, layersToHit))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    GetComponent<LineRenderer>().SetPosition(0, mTransform.position);
                    GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, mTransform.position.y, targetPos.z));
                }
                if (Physics.Raycast(ray3, out hit1, 10f, layersToHit))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    GetComponent<LineRenderer>().SetPosition(0, mTransform.position);
                    GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, mTransform.position.y, targetPos.z));
                }
                if (Physics.Raycast(ray4, out hit1, 10f, layersToHit))
                {
                    Vector3 targetPos = hit1.collider.gameObject.GetComponent<Transform>().position;
                    GetComponent<LineRenderer>().SetPosition(0, mTransform.position);
                    GetComponent<LineRenderer>().SetPosition(1, new Vector3(targetPos.x, mTransform.position.y, targetPos.z));
                }
            }
        }
        Debug.DrawRay(mTransform.position, new Vector3(0, -1, 0), Color.red);

        mTransform.position = Vector3.MoveTowards(mTransform.position, targetPosition, speed * Time.deltaTime);
        AnimatorClipInfo[] animatorinfo = mAnimator.GetCurrentAnimatorClipInfo(0);
        string current_animation = animatorinfo[0].clip.name;
        if(current_animation == "Idle")
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                speed = 1.2f;
                targetPosition = mTransform.position + new Vector3(0, 0, 1);
                targetPosition = new Vector3(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y), Mathf.Round(targetPosition.z));
                mAnimator.Play("Move");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                speed = 1.2f;
                targetPosition = mTransform.position + new Vector3(0, 0, -1);
                targetPosition = new Vector3(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y), Mathf.Round(targetPosition.z));
                mAnimator.Play("Animation");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                speed = 1.2f;
                mAnimator.Play("Celebration");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                speed = 2.4f;
                targetPosition = mTransform.position + new Vector3(0, 0, -2);
                targetPosition = new Vector3(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y), Mathf.Round(targetPosition.z));
                mAnimator.Play("Jump");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                speed = 1.2f;
                mAnimator.Play("Fall");
            }

        }
    }
}
