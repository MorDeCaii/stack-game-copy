using UnityEngine;

public class ColorPicker
{
    public Color color;

    private float hue;
    private float saturation;
    private float intensity;

    private float intensityStep = 4;
    private float hueStepMin = 20;
    private float hueStepMax = 50;
    private readonly float minI = 30;
    private readonly float maxI = 90;

    public ColorPicker()
    {
        hue = Random.Range(0, 360);
        saturation = 40;
        intensity = Random.Range(minI, maxI);
    }

    public Color GetNextColor()
    {
        intensity += intensityStep;
        if (intensity > maxI || intensity < minI)
        {
            if (intensity > maxI) intensity = maxI;
            else intensity = minI;

            intensityStep *= -1;
            hue += Random.Range(hueStepMin, hueStepMax);
            if (hue > 360) hue -= 360;
        }

        color = Color.HSVToRGB(hue / 360, saturation / 100, intensity / 100);
        return color;
    }
}
