using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhotonProcessor {
    void Process(List<ParticleSystem.Particle> particles);
}
