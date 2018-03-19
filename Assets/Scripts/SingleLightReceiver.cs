using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLightReceiver : MonoBehaviour {

    public SpriteRenderer colorDisplay;
    public FilterParticles filter;

    // Flashlighter
    public FlashLight redFlashlight;

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
        Color c1 = new Color();
        Color c2 = new Color();

        ColorHSV c = new ColorHSV(filter.filterColor);
        c.S = 1 - filter.fallOff;
        c.V = Mathf.Clamp01(filter.fallOff / 0.1f);

        c.H = c.H - filter.fallOff / 2;
        c1 = c.ToRGB();

        c.H = c.H + filter.fallOff;
        c2 = c.ToRGB();

        return (c1+c2)/2f;
    }
}
