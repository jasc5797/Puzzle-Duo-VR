using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOutline : MonoBehaviour
{
    //public float OutlineWidth = 2f;
    //public Color OutlineColor;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Add", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Add()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineHidden;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 2f;
    }
}
