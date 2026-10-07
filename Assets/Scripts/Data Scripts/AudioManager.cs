using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public int currentBGM;
    
    public AudioSource menuMusic, levelSelectMusic;

    public AudioSource[] bgMusic;
    public AudioSource[] sFX;

    private bool playingBGM;

    private void Awake()
    {
        if (instance == null)
        {
           instance = this;

           DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playingBGM)
        {
            if (bgMusic[currentBGM].isPlaying == false)
            {
                currentBGM++;
                if(currentBGM >= bgMusic.Length)
                {
                    currentBGM = 0;
                }

                bgMusic[currentBGM].Play();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                bgMusic[currentBGM].Stop();
            }
        }
    }

    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlayLevelSelectMusic()
    {
        StopMusic();
        levelSelectMusic.Play();
    }

    public void PlayBGM()
    {
        StopMusic();

        currentBGM = Random.Range(0, bgMusic.Length);

        bgMusic[currentBGM].Play();

        playingBGM = true;
    }

    public void StopMusic()
    {
        menuMusic.Stop();
        levelSelectMusic.Stop();
        foreach(AudioSource track in bgMusic)
        {
            track.Stop();
        }
        playingBGM = false;
    }

    public void PlaySFX(int sfxToPlay)
    {
        sFX[sfxToPlay].Stop();
        sFX[sfxToPlay].Play();
    }
}
