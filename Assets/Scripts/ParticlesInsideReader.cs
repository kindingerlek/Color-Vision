using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ParticlesInsideReader : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    public IPhotonProcessor photonProcessor;

    // particles
    List<ParticleSystem.Particle> buffer = new List<ParticleSystem.Particle>();

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        
        // Set-Up the particle system to callback when particle Inside in filter
        var trigger = particleSystem.trigger;
        trigger.enabled = true;
        trigger.inside = ParticleSystemOverlapAction.Callback;
    }

    void OnParticleTrigger()
    {
        if (photonProcessor == null)
            return;
        
        particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, buffer);
        photonProcessor.Process(buffer);        
        particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, buffer);
        
    }
}