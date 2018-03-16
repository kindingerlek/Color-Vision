using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesReceiver : MonoBehaviour {
    // Enable print in cosole some useful information
    [SerializeField] private bool debug;

    // Flashlighter
    [SerializeField] private ParticleSystem   redEmmitter;
    [SerializeField] private ParticleSystem greenEmmitter;
    [SerializeField] private ParticleSystem  blueEmmitter;
    
    
    [SerializeField, Range(0.1f, 2f)]
    // This is the speed to change the current color to a new color generated
    private float colorChangePerception = 1.5f; 

    [SerializeField]
    // Set the 'Max Photons' per channel to use when generate a new color
    private int channelMax = 15;

    // Store the photons collision
    private List<ParticleCollisionEvent>   redPhotonCollisions = new List<ParticleCollisionEvent>();
    private List<ParticleCollisionEvent> greenPhotonCollisions = new List<ParticleCollisionEvent>();
    private List<ParticleCollisionEvent>  bluePhotonCollisions = new List<ParticleCollisionEvent>();

    // Current color
    private Color color;
    
	// Update is called once per frame
	void Update () {
        // Read the collision of photons to this receiver
        ParticlePhysicsExtensions.GetCollisionEvents(   redEmmitter, gameObject,   redPhotonCollisions);
        ParticlePhysicsExtensions.GetCollisionEvents( greenEmmitter, gameObject, greenPhotonCollisions);
        ParticlePhysicsExtensions.GetCollisionEvents(  blueEmmitter, gameObject,  bluePhotonCollisions);
        
        // Smooth change the color to new color
        color = Color.Lerp(color, GetColor(), colorChangePerception * Time.deltaTime);

        // Enable the debug
        if (debug)
            DebugThis();
    }

    // Print in Console some useful informations
    void DebugThis()
    {
        Debug.Log(string.Format(
            "<color=#{6}>[■]</color> R[Cl:{0}; Er:{1}] G[Cl:{2}; Er:{3} ] B[Cl:{4}; Er:{5}]",
              redPhotonCollisions.Count, redEmmitter.emission.rateOverTime.constant,
            greenPhotonCollisions.Count, greenEmmitter.emission.rateOverTime.constant,
             bluePhotonCollisions.Count, blueEmmitter.emission.rateOverTime.constant,
             ColorUtility.ToHtmlStringRGB(color)));
    }

    // Generate a new color based in how much photons were absorbed by this receiver
    private Color GetColor()
    {
        Color c = new Color();

        // Clamp the values between the limits [0;channelMax] and them normalize it (value between 0 and 1)
        c.r = Mathf.Clamp(   redPhotonCollisions.Count, 0, channelMax) / (float) channelMax;
        c.g = Mathf.Clamp( greenPhotonCollisions.Count, 0, channelMax) / (float) channelMax;
        c.b = Mathf.Clamp(  bluePhotonCollisions.Count, 0, channelMax) / (float) channelMax;
        c.a = 1f;

        return c;
    }    
}
