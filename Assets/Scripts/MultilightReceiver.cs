using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultilightReceiver : MonoBehaviour {

    public SpriteRenderer colorDisplay;

    // Flashlighter
    public FlashLight redFlashlight;
    public FlashLight greenFlashlight;
    public FlashLight blueFlashlight;

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
        Color c = new Color();

        // Clamp the values between the limits [0;channelMax] and them normalize it (value between 0 and 1)
        c.r =   redFlashlight.GetIntensity();
        c.g = greenFlashlight.GetIntensity();
        c.b =  blueFlashlight.GetIntensity();
        c.a = 1f;

        return c;
    }
}
