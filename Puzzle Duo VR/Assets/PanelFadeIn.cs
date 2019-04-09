using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PanelFadeIn : MonoBehaviour
{
    public Image panel;
    public Text text;

    public float FadeInSpeed = 0.01f;

    private bool isFadeIn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeIn)
        {
            float alpha = text.color.a + (FadeInSpeed * Time.deltaTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, alpha);
        }
    }

    public void FadeIn()
    {
        // isFadeIn = true;
        panel.canvasRenderer.SetAlpha(0.0f);
        panel.CrossFadeAlpha(1.0f, 3.0f, false);
        text.CrossFadeAlpha(0.0f, 0.0f, false);
        text.CrossFadeAlpha(1.0f, 3.0f, false);
    }
}
