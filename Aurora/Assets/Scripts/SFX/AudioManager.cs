using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Sound[] sfx;
    public Sound[] music;

    public static AudioManager Instance;

    private AudioSource music1;
    private AudioSource music2;
    private Boolean isMusic1Source;

    [Range (0f, 1f)]
    public float sfxVolume;
    [Range (0f, 1f)]
    public float musicVolume;

    void Awake () {

        if (Instance != null) {
            Destroy (gameObject);
            return;
        } else {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        }

        foreach (Sound s in sfx) {
            s.source = gameObject.AddComponent<AudioSource> ();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        this.music1 = gameObject.AddComponent<AudioSource> ();
        this.music1.loop = true;
        this.music2 = gameObject.AddComponent<AudioSource> ();
        this.music2.loop = true;
        isMusic1Source = true;
    }

    public void PlayMusic (string name) {
        AudioSource activeSource = isMusic1Source ? music1 : music2;
        Sound s = Array.Find (music, sound => sound.name == name);
        activeSource.clip = s.clip;
        activeSource.volume = s.volume * musicVolume;
        activeSource.Play ();
    }

    public void PlayMusicWithFade (string name, float transitionTime = 1.0f) {
        AudioSource activeSource = isMusic1Source ? music1 : music2;
        Sound s = Array.Find (music, sound => sound.name == name);

        if (s == null)
            return;

        StartCoroutine (UpdateMusicWithFade (activeSource, s, transitionTime));
    }

    public void PlayMusicWithCrossFade (string name, float transitionTime = 1.0f) {
        AudioSource activeSource = isMusic1Source ? music1 : music2;
        AudioSource newSource = isMusic1Source ? music2 : music1;

        Sound s = Array.Find (music, sound => sound.name == name);

        if (s == null)
            return;

        isMusic1Source = !isMusic1Source;
        newSource.clip = s.clip;
        newSource.volume = s.volume * musicVolume;
        newSource.Play ();
        StartCoroutine (UpdateMusicWithCrossFade (activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade (AudioSource source, Sound sound, float transitionTime) {
        if (!source.isPlaying)
            source.Play ();

        // Fade-out music
        float time = 0f;
        for (; time < transitionTime; time += Time.deltaTime) {
            source.volume = (musicVolume * sound.volume - (time / transitionTime) * musicVolume * sound.volume);
            yield return null;
        }

        source.Stop ();
        source.clip = sound.clip;
        source.Play ();

        // Fade-in
        for (time = 0f; time < transitionTime; time += Time.deltaTime) {
            source.volume = (time / transitionTime) * musicVolume * sound.volume;
            yield return null;
        }

    }

    private IEnumerator UpdateMusicWithCrossFade (AudioSource originalSource, AudioSource newSource, float transitionTime) {

        float time = 0f;
        float originalSourceVol = originalSource.volume;
        float newSourceVol = newSource.volume;

        for (; time < transitionTime; time += Time.deltaTime) {
            originalSource.volume = originalSourceVol - (time / transitionTime) * originalSourceVol;
            newSource.volume = (time / transitionTime) * newSourceVol;
            yield return null;
        }

        originalSource.Stop ();

    }

    public void PlaySFX (string name) {
        Sound s = Array.Find (sfx, sound => sound.name == name);
        if (s != null)
            s.source.PlayOneShot (s.clip, s.volume * sfxVolume);
    }

    public void PlaySFX (string name, Transform transform) {
        Sound s = Array.Find (sfx, sound => sound.name == name);
        if (s != null) {
            AudioSource.PlayClipAtPoint (s.clip, transform.position, s.volume * sfxVolume);
        }
    }

    public void updateVolumeSFX(float newVolume) {
        sfxVolume = newVolume;
    }

    public void updateVolumeMusic(float newVolume) {
        musicVolume = newVolume;
    }
}