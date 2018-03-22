using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectorController : MonoBehaviour {

    public GameObject singleLight;
    public GameObject multiLight;

    public void SingleLight()
    {
        singleLight.SetActive(true);
        multiLight.SetActive(false);
    }

    public void MultiLight()
    {
        singleLight.SetActive(false);
        multiLight.SetActive(true);
    }

}
