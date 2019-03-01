using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUpAndDown : MonoBehaviour
{
	float originalY;

	public float strength = 0.5f;
    public float heightOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
		this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(transform.position.x, originalY + heightOffset + ((float)Mathf.Sin(Time.time) * strength), transform.position.z);
    }
}
