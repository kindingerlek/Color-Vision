using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleFlashlightController : MonoBehaviour {
    [SerializeField]
    SingleLightReceiver receiver;

    [SerializeField]
    FlashLight flashlight;

    [SerializeField]
    FilterParticles filter;

    [SerializeField]
    Slider flashColorSlider;

    [SerializeField]
    Slider flashSaturationSlider;

    [SerializeField]
    Slider filterColorSlider;

    [SerializeField]
    Slider filterOpacitySlider;


    // Use this for initialization
    void Start () {       

        if (!flashColorSlider)
            flashColorSlider = this.transform.Find("Panel/Panel Flashlight Color Hue/Slider").GetComponent<Slider>();

        if (!flashSaturationSlider)
            flashSaturationSlider = this.transform.Find("Panel/Panel Flashlight Color Saturation/Slider").GetComponent<Slider>();

        if (!filterColorSlider)
            filterColorSlider = this.transform.Find("Panel/Panel Filter Color Hue/Slider").GetComponent<Slider>();
        
        if (!filterOpacitySlider)
            filterOpacitySlider = this.transform.Find("Panel/Panel Filter Opacity/Slider").GetComponent<Slider>();


             flashColorSlider.onValueChanged.AddListener(delegate { UpdateFlashlight(); });
        flashSaturationSlider.onValueChanged.AddListener(delegate { UpdateFlashlight(); });
            filterColorSlider.onValueChanged.AddListener(delegate { UpdateFilter(); });
          filterOpacitySlider.onValueChanged.AddListener(delegate { UpdateFilter(); });

        filterOpacitySlider.value = filter.fallOff;

        UpdateFlashlight();
        UpdateFilter();
    }
	

    void UpdateFlashlight()
    {
        ColorHSV c = new ColorHSV(flashlight.color);

        c.H = flashColorSlider.value;
        c.S = flashSaturationSlider.value;

        flashColorSlider.transform.Find("Handle Slide Area/Handle").GetComponent<Image>().color = c.ToRGB();

        flashlight.color = c.ToRGB();
    }

    void UpdateFilter()
    {
        ColorHSV c = new ColorHSV(filter.filterColor);

        c.H = filterColorSlider.value;
        filter.fallOff = filterOpacitySlider.value;

        filterColorSlider.transform.Find("Handle Slide Area/Handle").GetComponent<Image>().color = c.ToRGB();

        filter.filterColor = c.ToRGB();
    }
}
