using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; 

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }
    
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }
    [field: SerializeField] public EventReference Menumusic { get; private set; }
    
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: SerializeField] public EventReference Reload { get; private set; }
    [field: SerializeField] public EventReference Hurt { get; private set; }
    
    [field: Header("Shoot SFX")]
    [field: SerializeField] public EventReference Shoot { get; private set; }    
    
    [field: Header("Zombie SFX")]
    [field: SerializeField] public EventReference Zombie { get; private set; }
    [field: SerializeField] public EventReference ZombieDeath { get; private set; }
    [field: SerializeField] public EventReference ZombieExplosion { get; private set; }
    
    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference UIButtonHover { get; private set; }
    [field: SerializeField] public EventReference UIButtonClick { get; private set; }
    [field: SerializeField] public EventReference ScriptNPC { get; private set; }
    [field: SerializeField] public EventReference CallOverNPC { get; private set; }
    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in scene.");
        }
        instance = this;
    }
}
