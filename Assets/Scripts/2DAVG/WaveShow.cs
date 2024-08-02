using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveShow : MonoBehaviour
{
    public GameObject player;
    public CameraFollow cameraFollow;
    public GameObject wave;
    public GameObject darkImage;
    public TextAsset inkJosn;

    [SerializeField] private AudioClip waterSound;
    private AudioSource audioSource;

    bool canJumpScene = false;

    private void Awake() {
        darkImage.SetActive(false);
        wave.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        DialogueManager.Instance.OnStoryEnd += JumpScene;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Destroy(player);
            wave.SetActive(true);
            cameraFollow.target = transform;     
        }

        darkImage.SetActive(true);
        canJumpScene = true;
        
        if (audioSource && waterSound) {
            audioSource.clip = waterSound;
            audioSource.Play();
        }

        DialogueManager.Instance.InitializedStroy(inkJosn, 0);
        
    }

    private void JumpScene(object sender, EventArgs eventArgs) {
        //TODO: 增加场景跳转，到一殿之前的路
        if (canJumpScene) {
            canJumpScene = false;
            
            SceneManager.LoadScene("WorkRoadScene");

            
            // wave.SetActive(false);
        }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        DialogueManager.Instance.OnStoryEnd -= JumpScene;
    }

}
