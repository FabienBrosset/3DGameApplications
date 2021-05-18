using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{

    public float fadeValue = 1f;

    void Start()
    {
        this.transform.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, fadeValue);

        ToFadeMode(this.transform.GetComponent<Renderer>().material);

        this.transform.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, fadeValue);
    }

    
    void Update()
    {
        fadeValue -= 0.3f * Time.deltaTime;

        this.transform.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, fadeValue);

        if (fadeValue < 0)
            Destroy(this.gameObject);
    }

    public void ToFadeMode(Material material)
    {
        material.SetOverrideTag("RenderType", "Transparent");
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

}
