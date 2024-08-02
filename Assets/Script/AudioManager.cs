using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [SerializeField] public List<AudioClip> musicClips;
    [SerializeField] public List<AudioClip> sfxClips;

    private void Awake()
    {
        // 确保只有一个 AudioManager 实例
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // 切换场景时不销毁
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // 播放背景音乐
    public void PlayMusic(string clipName)
    {
        AudioClip clip = musicClips.Find(c => c.name == clipName);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("Music clip not found: " + clipName);
        }
    }

    // 播放音效
    public void PlaySFX(string clipName)
    {
        AudioClip clip = sfxClips.Find(c => c.name == clipName);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("SFX clip not found: " + clipName);
        }
    }

    // 停止背景音乐
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // 停止所有音效
    public void StopAllSFX()
    {
        sfxSource.Stop();
    }

    public void PlayWoodMusic()
    {
        AudioManager.Instance.StopMusic();
        //
        AudioManager.Instance.PlaySFX("SceneTransition");
        AudioManager.Instance.PlaySFX("Wood");

        AudioManager.Instance.PlayMusic("LyCheckBackground");
    }
}
