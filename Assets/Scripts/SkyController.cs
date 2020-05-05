using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Imphenzia;

public class SkyController : MonoBehaviour
{
    private GradientSkyObject skyScript;

    private Color darkerColor;
    private Color lighterColor;

    void Awake()
    {
        skyScript = GetComponent<GradientSkyObject>();
        darkerColor = Color.black;
        lighterColor = Color.gray;
    }

    void SetSkyByColors (Color topColor, Color bottomColor)
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        colorKey = new GradientColorKey[2];
        colorKey[0].color = topColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = bottomColor;
        colorKey[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
        skyScript.gradient = gradient;
    }

    public void SetSkyByColor (Color color)
    {
        float h, s, i;
        Color.RGBToHSV(color, out h, out s, out i);
        Color newLighterColor = Color.HSVToRGB(h, s-0.3f, i + 0.1f);
        Color newDarkerColor = Color.HSVToRGB(h, s-0.3f, i/3 + 0.1f);

        StopCoroutine("ChangeColors");
        StartCoroutine (ChangeColors(0.5f, darkerColor, lighterColor, newDarkerColor, newLighterColor));
        darkerColor = newDarkerColor;
        lighterColor = newLighterColor;
    }

    public void SetSkyByColorInstant (Color color)
    {
        float h, s, i;
        Color.RGBToHSV(color, out h, out s, out i);
        Color newLighterColor = Color.HSVToRGB(h, s - 0.3f, i + 0.1f);
        Color newDarkerColor = Color.HSVToRGB(h, s - 0.3f, i / 3 + 0.1f);

        SetSkyByColors(newLighterColor, newDarkerColor);
        darkerColor = newDarkerColor;
        lighterColor = newLighterColor;
    }

    IEnumerator ChangeColors (float time, Color darkerColor1, Color lighterColor1, Color darkerColor2, Color lighterColor2)
    {
        float t = 0;
        Color lerpedColorDarker = darkerColor1;
        Color lerpedColorLighter = lighterColor1;
        
        while (t < time)
        {
            lerpedColorDarker = Color.Lerp(darkerColor1, darkerColor2, t / time);
            lerpedColorLighter = Color.Lerp(lighterColor1, lighterColor2, t / time);
            SetSkyByColors(lerpedColorLighter, lerpedColorDarker);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetSkyByColors(lighterColor2, darkerColor2);
        yield return null;
    }
}
