using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource _source;

    public static AudioSource Source { get; private set; }

    public AudioSource boom;

    private void Awake()
    {
        Source = _source;
        Source.enabled = true;
    }


    public static void playBoom()
    {
        Source.Play();
    }

}
