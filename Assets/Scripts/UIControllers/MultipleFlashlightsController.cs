using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleFlashlightsController : MonoBehaviour {
    [SerializeField]
    SpriteRenderer thinkingBalloonSprite;

    [SerializeField]
    ParticlesReceiver receiver;

    void Update()
    {
        thinkingBalloonSprite.color = receiver.GetColor();
    }
}
