using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterParticles : MonoBehaviour, IPhotonProcessor {
    
    public Color filterColor;
    public FlashLight flashlight;

    [Range(0.001f, 1f)]
    public float fallOff = .1f;


    private ParticleSystem ps;
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();

        ColorHSV c = new ColorHSV(filterColor);
        c.S = 1;
        c.V = 1;
        filterColor = c.ToRGB();

        flashlight.GetComponentInChildren<ParticlesTriggerEventReader>().AddPhotonProcessor(this, GetComponentInChildren<Collider>(), ParticleSystemTriggerEventType.Inside);
    }

    public void Process(List<ParticleSystem.Particle> list, ParticleSystem particleSystem)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ParticleSystem.Particle p = list[i];
            
            float f = Filter(p.startColor, filterColor, fallOff);

            if (f == 0)
                p.remainingLifetime = 0;

            list[i] = p;
        }
    }
    
    public float Filter(Color raw, Color filter, float falloff)
    {
        ColorHSV rawHSV = new ColorHSV(raw);
        ColorHSV filterHSV = new ColorHSV(filter);

        /*
         * Linear Expression:  y = -A * x + B;
         *      A = 1;
         *      x = Absolute value of Hue Difference. This value is normalized, between [0:1]
         *      B = 1;
         */

        return -1f * Mathf.Clamp01(Mathf.Abs(rawHSV.H - filterHSV.H) / falloff) + 1f;
    }
}
