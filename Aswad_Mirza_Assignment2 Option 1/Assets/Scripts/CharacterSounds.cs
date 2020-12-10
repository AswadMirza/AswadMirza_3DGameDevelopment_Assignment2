using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aswad Mirza 991445135

public class CharacterSounds : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource _audioSource;
    Animator _anim;
    public AudioClip walkingSound;
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_anim.GetFloat("Forward")>0.2 && _anim.GetBool("OnGround")){
           // _audioSource.clip = walkingSound;
            if (!_audioSource.isPlaying) {
                _audioSource.PlayOneShot(walkingSound);
            }
        }
    }
}
