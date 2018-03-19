using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesReceiver : MonoBehaviour, IPhotonProcessor {
    // Enable print in cosole some useful information
    [SerializeField] private bool debug;

    [SerializeField]
    private int maxColorAbsorb = 60;

    // Flashlighter
    public FlashLight[] flashlightsList;
        
    [SerializeField, Range(0.1f, 5f)]
    // This is the speed to change the current color to a new color generated
    private float colorChangePerception = 1.5f; 

    // Current color
    public Color color;

    private Queue<Color> colorBuffer = new Queue<Color>();

    private void Start()
    {
        foreach (var flashlight in flashlightsList)
            flashlight.GetComponentInChildren<ParticlesTriggerEventReader>().enterProcessor = this ;
    }


    // Update is called once per frame
    void Update () {        
        // Smooth change the color to new color
        color = Color.Lerp(color, ReadColor(),  colorChangePerception * Time.deltaTime);        
    }


    void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        flashlightsList[0].emitter.GetCollisionEvents(other, collisionEvents);
        
        Debug.Log("collisions: " + collisionEvents.Count);
    }

    void IPhotonProcessor.Process(List<ParticleSystem.Particle> particlesList, ParticleSystem particleSystem)
    {
        
        for(int i = colorBuffer.Count-1 ; i >= 0; i++)
        {
            if (colorBuffer.Count >= maxColorAbsorb)
            {
                colorBuffer.Dequeue();
                colorBuffer.Enqueue(particlesList[i].GetCurrentColor(particleSystem));
            }
            
        }
    }    

    public Color GetColor()
    {
        return color;
    }

    // Generate a new color based in how much photons were absorbed by this receiver
    public Color ReadColor()
    {
        if (colorBuffer == null || colorBuffer.Count == 0)
            return Color.white;
        
        Color c = new Color();

        foreach(var particleColor in colorBuffer)
        {
            c += particleColor;
        }

        return c / colorBuffer.Count;
    }
}
