using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesReceiver : MonoBehaviour {

    public ParticleSystem emmitter;

    public List<ParticleCollisionEvent> particleCollisions;

    public int maxGreenParticles = 0;

	// Use this for initialization
	void Start () {
        particleCollisions = new List<ParticleCollisionEvent>();
	}
	
	// Update is called once per frame
	void Update () {
        ParticlePhysicsExtensions.GetCollisionEvents(emmitter, gameObject, particleCollisions);

        Debug.Log(string.Format("G: [{0} collisions | Max: {1:0} | emmission rate: {2} ]",
            particleCollisions.Count,
            maxGreenParticles,
            emmitter.emission.rateOverTime.constant));
        
            maxGreenParticles += Mathf.FloorToInt( 0.25f * (particleCollisions.Count - maxGreenParticles));
	}
    /*
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision");
    }*/
    
}
