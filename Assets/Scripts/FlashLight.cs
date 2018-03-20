
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {
    
    public Color color = Color.white;

    [SerializeField]
    private int maxPhotonsRate = 800;
    
    [SerializeField, Range(0,1)]
    private float intensity = 1;
    
    [SerializeField]
    private AnimationCurve intensityCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private ParticleSystem m_emitter;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule emission;
    
    public ParticleSystem emitter
    {
        get
        {
            if (m_emitter == null)
            {
                m_emitter = this.GetComponentInChildren<ParticleSystem>();

                if (m_emitter == null)
                    Debug.LogError("There is no particle system as child of this object", this);
            }

            return m_emitter;
        }
    }

    // Use this for initialization
    void Start() {
        // Get the ParticleSystem Modules
        main = emitter.main;
        emission = emitter.emission;

        // Add 100% to maxParticles to avoid drop down the flow rate
        main.maxParticles = Mathf.RoundToInt(maxPhotonsRate * 2f);
        emission.rateOverTime = maxPhotonsRate;
    }

    private void Update()
    {
        var startColor = main.startColor;

        // Set the startColor
        startColor.colorMin = SetStartColor(color);
        startColor.colorMax = SetStartColor(color);

        startColor.mode = ParticleSystemGradientMode.TwoColors;

        main.startColor = startColor;

	}

    internal float GetIntensity()
    {
        return intensity;
    }

    public void SetIntensity(float value)
    {
        intensity = value;
        emission.rateOverTime = intensityCurve.Evaluate(intensity) * maxPhotonsRate;
    }

    private Color SetStartColor( Color rgb)
    {
        ColorHSV hsv = new ColorHSV(rgb);

        hsv.H += Random.Range(0, 1f - hsv.S);

        hsv.V = 1f;

        hsv.S = 1f;


        return hsv.ToRGB();
    }
}
