﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class ColorShift : MonoBehaviour,IFocusable, ISourceStateHandler
{
    private Material myMat;
    private List<MeshRenderer> myMatAll;
    private SpriteRenderer sp;

    private float shiftValue = 204f;
    private bool shiftComplete;
    private bool isIncreasing;

	// Use this for initialization
	void Start () {
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            myMat = gameObject.GetComponent<MeshRenderer>().material;
        }
        else {
            myMat = null;
            myMatAll = new List<MeshRenderer>();
            foreach (MeshRenderer mesh in transform.GetComponentsInChildren<MeshRenderer>()) {
                myMatAll.Add(mesh);
            }

        }
        
        if (transform.childCount > 0)
        {
            sp = transform.GetComponentInChildren<SpriteRenderer>();
            Debug.Log(sp);
        }
        else
        {
            sp = null;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (isIncreasing) {
            shiftValue -= 32f;
            if (shiftValue <= 0) {
                shiftValue = 0;
            }
        }
        else
        {
            shiftValue += 32f;
            if(shiftValue >= 204)
            {
                shiftValue = 204;
            }
        }
        if (myMat != null)
        {
            myMat.color = new Color(shiftValue / 255.0f, 204 / 255.0f, 204 / 255.0f);
        }
        else
        {
            foreach (MeshRenderer mesh in myMatAll)
            {
                mesh.material.color = new Color(shiftValue / 255.0f, 204 / 255.0f, 204 / 255.0f);
            }
        }

        if (sp != null)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, (204.0f - shiftValue) / 204.0f);
        }
	}


    public void OnFocusEnter()
    {
        if (isIncreasing) {
            return;
        }

        isIncreasing = true;

    }

    public void OnFocusExit()
    {
        if (!isIncreasing)
        {
            return;
        }

        isIncreasing = false;
    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
        // Nothing to do
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        isIncreasing = false;
    }
}
