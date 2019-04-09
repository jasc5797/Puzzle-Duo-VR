using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float fadeSpeed = 0.01f;

    private Material material;
    private Color color;

    private bool shrinking = false;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        color = material.color;
    }

    void Update()
    {
        if (shrinking)
        {
            gameObject.transform.localScale -= Vector3.one * Time.deltaTime * fadeSpeed;
            Vector3 scale = gameObject.transform.localScale;
            if (scale.x < 0 || scale.y < 0 || scale.z < 0)
            {
                shrinking = false;
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered the trigger");
        if (other.tag == "Player")
        {
            Debug.Log("Player entered the trigger");
            gameObject.GetComponentInParent<CollectableInteract>().HandlePlayerInteraction();
        }
    }

    /*public IEnumerator AlphaFade()
    {
        float alpha = color.a;

        while(alpha > 0.0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;

            material.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }*/

    public void ScaleFade()
    {
        shrinking = true;
        gameObject.GetComponent<FloatUpAndDown>().Stop();
    }
}
