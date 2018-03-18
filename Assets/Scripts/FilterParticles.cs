using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterParticles : MonoBehaviour {
    
    public Color filterColor;
    public FlashLight flashlight;

    [Range(0.001f, 1f)]
    public float fallOff = .1f;


    private ParticleSystem ps;
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    private ParticleSystem.TriggerModule trigger;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();


        // Set-Up the particle system to callback when particle Inside in filter
        var trigger = ps.trigger;
        trigger.enabled = true;
        trigger.SetCollider(0,ps.GetComponentInChildren<Collider>());
        trigger.inside = ParticleSystemOverlapAction.Callback;

        ColorHSV c = new ColorHSV(filterColor);
        c.S = 1;
        c.V = 1;
        filterColor = c.ToRGB();
    }
    
    void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enterList = new List<ParticleSystem.Particle>();
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, enterList);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enterList[i];

            float f = Filter(p.startColor, filterColor, fallOff);

            if (f == 0)
                p.remainingLifetime = 0;

            enterList[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, enterList);
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
