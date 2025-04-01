using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0,1)]
    
    public float masterVolume = 1;
    [Range(0,1)]
    
    public float musicVolume = 1;
    [Range(0,1)]
    
    public float ambienceVolume = 1;
    [Range(0,1)]
    
    public float SFXVolume = 1;
    
    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;
    private Bus uiBus;
    
    private List<EventInstance> eventInstances; 
    private List<StudioEventEmitter> eventEmitters;
    private EventInstance mainMenuMusicInstance;
    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;
    public static AudioManager instance { get; private set; }
    
    private bool isInMainMenu = true;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
        
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        uiBus = RuntimeManager.GetBus("bus:/UI");
    }

    private void Start()
    {
        PlayMainMenuMusic();
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(SFXVolume);
    }
    public void PlayMainMenuMusic()
    {
        mainMenuMusicInstance = RuntimeManager.CreateInstance(FMODEvents.instance.Menumusic); // Replace this with your actual main menu music event
        mainMenuMusicInstance.start();
    }
    
    public void StopMainMenuMusic()
    {
        mainMenuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        mainMenuMusicInstance.release();
    }
    
    public void PlayAmbienceAndMusic()
    {
        isInMainMenu = false;

        ambienceEventInstance = CreateInstance(FMODEvents.instance.ambience);
        ambienceEventInstance.start();

        //musicEventInstance = CreateInstance(FMODEvents.instance.music);
        //musicEventInstance.start();
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }
    
    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetAmbienceParameter(string ParameterName, float ParameterValue)
    {
        ambienceEventInstance.setParameterByName(ParameterName, ParameterValue);
    }

    public void SetMusicArea(MusicArea area)
    {
        musicEventInstance.setParameterByName("area", (float) area);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference); 
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }
    
    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
    
    public void MuteAll(bool mute)
    {
        float targetVolume = mute ? 0f : 1f;

        masterVolume = targetVolume;
        musicVolume = targetVolume;
        ambienceVolume = targetVolume;
        SFXVolume = targetVolume;

        // Immediately apply the volume changes
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(SFXVolume);

        Debug.Log($"AudioManager: All audio {(mute ? "muted" : "unmuted")}.");
    }
    
    public void PauseAllSounds(bool pause)
    {
        musicBus.setPaused(pause);
        ambienceBus.setPaused(pause);
        sfxBus.setPaused(pause);
        uiBus.setPaused(false);
        Debug.Log($"AudioManager: All sounds {(pause ? "paused" : "resumed")}.");
    }
}
