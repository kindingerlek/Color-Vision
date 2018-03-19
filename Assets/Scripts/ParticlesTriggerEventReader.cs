using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ParticlesTriggerEventReader : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private bool onEnter;
    private bool onInside;


    private IPhotonProcessor insideProcessor;
    private IPhotonProcessor enterProcessor;

    // particles
    List<ParticleSystem.Particle> enterParticleList = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> insideParticleList = new List<ParticleSystem.Particle>();


    private void AddPhotonProcessor(IPhotonProcessor processor, Collider collider, ParticleSystemTriggerEventType eventType)
    {
        particleSystem = GetComponent<ParticleSystem>();

        // Set-Up the particle system to callback when particle Inside in filter
        var trigger = particleSystem.trigger;
        trigger.enabled = true;

        switch(eventType)
        {
            case ParticleSystemTriggerEventType.Enter:
                onEnter = true;
                break:
        }


        trigger.inside = onInside ? ParticleSystemOverlapAction.Callback : ParticleSystemOverlapAction.Ignore;
        trigger.enter = onEnter ? ParticleSystemOverlapAction.Callback : ParticleSystemOverlapAction.Ignore;
    }

    void OnParticleTrigger()
    {
        if (particleSystem == null)
            return;

        if(onEnter && enterProcessor != null)
        {
            particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticleList);
            enterProcessor.Process(enterParticleList, particleSystem);
            particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticleList);
        }

        if (onInside && insideProcessor != null)
        {
            particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, insideParticleList);
            insideProcessor.Process(insideParticleList, particleSystem);
            particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, insideParticleList);
        }
    }
}