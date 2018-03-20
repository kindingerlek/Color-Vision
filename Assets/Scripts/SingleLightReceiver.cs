using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLightReceiver : MonoBehaviour {

    public SpriteRenderer colorDisplay;
    public FilterParticles filter;

    // Flashlighter
    public FlashLight flashlight;

    [SerializeField, Range(0.1f, 5f)]
    // This is the speed to change the current color to a new color generated
    private float colorChangePerception = 1.5f;

    // Current color
    private Color color;

    private void Start()
    {
        if (!colorDisplay)
            colorDisplay = this.transform.Find("Color Display").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    
    void Update()
    {
        // Smooth change the color to new color
        color = Color.Lerp(color, ReadColor(), colorChangePerception * Time.deltaTime);

        colorDisplay.color = color;
    }

    public Color GetColor()
    {
        return color;
    }

    // Generate a new color based in how much photons were absorbed by this receiver
    public Color ReadColor()
    {
        // If filter is not enabled, then return flashlight color
        if (!filter.gameObject.activeSelf)
            return flashlight.color;

        Color Lcolor = new Color();
        Color Rcolor = new Color();
        Color Fcolor = Color.white;


        ColorHSV filterHSV = new ColorHSV(filter.filterColor);
        ColorHSV flashLHSV = new ColorHSV(flashlight.color);
        
        // Saturation get lower when more of other particles pass through filter
        filterHSV.S = 1 - filter.fallOff + flashLHSV.S;

        // Brightness get lower when very few particles pass through filter
        filterHSV.V = Mathf.Clamp01(1f - filter.absorptionRate);

        // Move hue 50% of falloff from original hue to the left
        filterHSV.H = filterHSV.H - filter.fallOff / 2;
        Lcolor = filterHSV.ToRGB();

        // Move hue 50% of falloff from original hue to the right
        filterHSV.H = filterHSV.H + filter.fallOff;
        Rcolor = filterHSV.ToRGB();

        Fcolor = flashlight.color;

        // Linear interpolation between pivot colors
        return (Lcolor+Rcolor+Fcolor)/3f;
    }
}
