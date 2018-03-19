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
    Slider timeSlider;
    
    [SerializeField]
    Slider redSlider;

    [SerializeField]
    Slider greenSlider;

    [SerializeField]
    Slider blueSlider;

    private void Start()
    {
        if(!colorBallon)
            colorBallon = GameObject.Find("Color Balloon").GetComponent<SpriteRenderer>();

        if (!receiver)
            receiver = GameObject.FindObjectOfType<ParticlesReceiver>();

        if (!timeSlider)
            timeSlider = this.transform.Find("TimeControl/Slider").GetComponent<Slider>();

        if(!redSlider)
            timeSlider = this.transform.Find("LightSliders/Red/RedSlider").GetComponent<Slider>();

        if (!greenSlider)
            timeSlider = this.transform.Find("LightSliders/Green/GreenSlider").GetComponent<Slider>();

        if (!blueSlider)
            timeSlider = this.transform.Find("LightSliders/Blue/BlueSlider").GetComponent<Slider>();


        timeSlider.onValueChanged.AddListener(delegate { UpdateTime(); });

        redSlider.onValueChanged.AddListener(delegate { UpdateLights(); });
        greenSlider.onValueChanged.AddListener(delegate { UpdateLights(); });
        blueSlider.onValueChanged.AddListener(delegate { UpdateLights(); });
        
    }

    void Update()
    {
        colorBallon.color = receiver.GetColor();
    }

    void UpdateTime()
    {
        Time.timeScale = timeSlider.value;
    }

    void UpdateLights()
    {
        receiver.flashlightsList[0].SetIntensity(redSlider.value);
        receiver.flashlightsList[1].SetIntensity(greenSlider.value);
        receiver.flashlightsList[2].SetIntensity(blueSlider.value);
    }
}
