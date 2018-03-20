using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterParticles : MonoBehaviour, IPhotonProcessor {
    
    public Color filterColor;
    public FlashLight flashlight;

    [Range(0.001f, 1f)]
    public float fallOff = .1f;
    [Range(0f, 3f)]
    public float absorptionSpeed = 1.5f;

    [System.NonSerialized]
    public float absorptionRate;

    private ParticleSystem ps;
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();

        ColorHSV c = new ColorHSV(filterColor);
        c.S = 1;
        c.V = 1;
        filterColor = c.ToRGB();

        flashlight.GetComponentInChildren<ParticlesTriggerEventReader>().AddPhotonProcessor(this, GetComponentInChildren<Collider>(), ParticleSystemTriggerEventType.Enter);
    }

    public void Process(List<ParticleSystem.Particle> list, ParticleSystem particleSystem)
    {
        int absorbed = 0;

        for (int i = 0; i < list.Count; i++)
        {
            ParticleSystem.Particle p = list[i];
            
            float f = Filter(p.startColor, filterColor, fallOff);

            p.remainingLifetime *= Random.Range(0, 1f) < f ? 1f : 0f;
            if (p.remainingLifetime == 0)
            {
                absorbed++;
            }
                        

            list[i] = p;
        }

        absorptionRate = list.Count > 0 ? Mathf.Lerp ( absorptionRate,absorbed / list.Count, absorptionSpeed * Time.deltaTime) : 1f;
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
         
        float d1 = Mathf.Abs(rawHSV.H - filterHSV.H);
        float d2 = Mathf.Abs(1 + rawHSV.H - filterHSV.H);
        float df = d1 < d2 ? d1 : d2;

        return -1f * Mathf.Clamp01(df / falloff) + 1f;
    }
}
