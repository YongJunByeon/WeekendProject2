using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KEYS : MonoBehaviour {
    [SerializeField]
    AudioClip playSound;
    public bool test = false;
    public float time = 0.2f;
    public float lasttime;
    //   AudioSource playSound;
    public Color press = Color.green;
    public Color originalColor;
    IEnumerator PressEffect()
    {
        GetComponent<UISprite>().color = press;
        yield return new WaitForSeconds(0.2f);
        GetComponent<UISprite>().color = originalColor;
    }
    public void AddSound(string soundName)
    {
       playSound =  Resources.Load(soundName) as AudioClip;
            GetComponent<AudioSource>().clip = playSound;
    }
    public void AddSound(AudioClip audio)
    {
        playSound = audio;
        GetComponent<AudioSource>().clip = playSound;
    }
    // Use this for initialization
    public void PlayOctave()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(PressEffect());

    }
    public void Pressed(bool bo)
    {
    }
    void Start()
    {
        originalColor = GetComponent<UISprite>().color;
    }
}
