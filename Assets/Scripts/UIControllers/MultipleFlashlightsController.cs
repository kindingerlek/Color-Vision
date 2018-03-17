using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleFlashlightsController : MonoBehaviour {
    [SerializeField]
    SpriteRenderer colorBallon;

    [SerializeField]
    ParticlesReceiver receiver;

    [SerializeField]
    Slider timeSpeed;

    private void Start()
    {
        if(!colorBallon)
            colorBallon = GameObject.Find("Color Balloon").GetComponent<SpriteRenderer>();

        if (!receiver)
            receiver = GameObject.FindObjectOfType<ParticlesReceiver>();

        if (!timeSpeed)
            timeSpeed = this.transform.Find("TimeControl/Slider").GetComponent<Slider>();

        
    }

    void Update()
    {
        colorBallon.color = receiver.GetColor();
        Time.timeScale = timeSpeed.value;
    }
}
