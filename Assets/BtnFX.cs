using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFX : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void PlayHoverFX() {
        myFx.PlayOneShot(hoverFx);
    }

    public void PlayClickFX()
    {
        myFx.PlayOneShot(clickFx);
    }

}
