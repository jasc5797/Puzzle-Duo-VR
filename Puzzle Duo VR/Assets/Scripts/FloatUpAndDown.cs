﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpAndDown : MonoBehaviour
{
	

	public float Strength = 0.5f;
    public float HeightOffset = 0.5f;

    private float OriginalY;

    private bool isFloating = true;


    // Start is called before the first frame update
    void Start()
    {
		this.OriginalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFloating)
        {
            transform.position = new Vector3(transform.position.x, OriginalY + HeightOffset + ((float)Mathf.Sin(Time.time) * Strength), transform.position.z);
        }
    }

    public void Stop()
    {
        isFloating = false;
    }
}
