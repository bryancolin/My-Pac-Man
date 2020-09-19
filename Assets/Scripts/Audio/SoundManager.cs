using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static AudioClip eating, moving, colliding;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        eating = Resources.Load<AudioClip>("Audio/Sound Effect (Eating Pellet)");
        moving = Resources.Load<AudioClip>("Audio/Sound Effect (Pac man Moving)");
        colliding = Resources.Load<AudioClip>("Audio/Sound Effect/Collision");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch(clip) {
            case "eat":
                audioSource.PlayOneShot(eating);
                break;
            case "move":
                audioSource.PlayOneShot(moving);
                break;
            case "collide":
                audioSource.PlayOneShot(colliding);
                break;
        }
    }
}
