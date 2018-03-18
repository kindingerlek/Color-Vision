using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorHSV
{
    private float h;
    private float s;
    private float v;

    public float H
    {
        get { return h; }
        set { h = Mathf.Clamp01(value); }
    }

    public float S
    {
        get { return s; }
        set { s = Mathf.Clamp01(value); }
    }

    public float V
    {
        get { return v; }
        set { v = Mathf.Clamp01(value); }
    }

    public ColorHSV()
    {
        H = 0;
        S = 0;
        V = 0;
    }

    public ColorHSV(float h, float s, float v)
    {
        this.H = h;
        this.S = s;
        this.V = v;
    }

    public ColorHSV(Color rgbColor)
    {
        float h, s, v;
        Color.RGBToHSV(rgbColor, out h, out s, out v);

        H = h;
        S = s;
        V = v;
    }

    public ColorHSV(float r, float g, float b, float a)
    {
        float h, s, v;
        Color.RGBToHSV(new Color(r, g, b, a), out h, out s, out v);

        H = h;
        S = s;
        V = v;
    }

    public Color ToRGB()
    {
        return Color.HSVToRGB(H, S, V);
    }
}