using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesReceiver : MonoBehaviour {
    [SerializeField] private bool debug;

    [SerializeField] private ParticleSystem   redEmmitter;
    [SerializeField] private ParticleSystem greenEmmitter;
    [SerializeField] private ParticleSystem  blueEmmitter;
    
    [SerializeField, Range(0.1f, 2f)]
    private float colorChangePerception = 1.5f;

    [SerializeField, Range(1, 30)]
    private int channelMax = 15;

    private List<ParticleCollisionEvent>   redPhotonCollisions = new List<ParticleCollisionEvent>();
    private List<ParticleCollisionEvent> greenPhotonCollisions = new List<ParticleCollisionEvent>();
    private List<ParticleCollisionEvent>  bluePhotonCollisions = new List<ParticleCollisionEvent>();


    private Color color;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        ParticlePhysicsExtensions.GetCollisionEvents(   redEmmitter, gameObject,   redPhotonCollisions);
        ParticlePhysicsExtensions.GetCollisionEvents( greenEmmitter, gameObject, greenPhotonCollisions);
        ParticlePhysicsExtensions.GetCollisionEvents(  blueEmmitter, gameObject,  bluePhotonCollisions);
        
        color = Color.Lerp(color, GetColor(), colorChangePerception * Time.deltaTime);

        if (debug)
            DebugThis();
    }

    void DebugThis()
    {
        Debug.Log(string.Format(
            "<color=#{6}>[■]</color> R[Cl:{0}; Er:{1}] G[Cl:{2}; Er:{3} ] B[Cl:{4}; Er:{5}]",
              redPhotonCollisions.Count, redEmmitter.emission.rateOverTime.constant,
            greenPhotonCollisions.Count, greenEmmitter.emission.rateOverTime.constant,
             bluePhotonCollisions.Count, blueEmmitter.emission.rateOverTime.constant,
             ColorUtility.ToHtmlStringRGB(color)));
    }

    private Color GetColor()
    {
        Color c = new Color();

        c.r = Mathf.Clamp(   redPhotonCollisions.Count, 0, channelMax) / (float) channelMax;
        c.g = Mathf.Clamp( greenPhotonCollisions.Count, 0, channelMax) / (float) channelMax;
        c.b = Mathf.Clamp(  bluePhotonCollisions.Count, 0, channelMax) / (float) channelMax;
        c.a = 1f;

        return c;
    }    
}
