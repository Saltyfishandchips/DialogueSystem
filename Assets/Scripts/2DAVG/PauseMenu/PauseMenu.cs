using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UIGassuion uiGassuion;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private Button returnGame;
    [SerializeField] private Button returnTitle;
    [SerializeField] private Button quitGame;

    private bool isPauseMenuShow = false;

    private void Awake() {
        returnGame.onClick.AddListener(() => {
            ReturnGame();
        });  

        // returnTitle.onClick.AddListener(() => {
        //     SceneManager.LoadScene("GameStart");
        // });

        quitGame.onClick.AddListener(() => {
            Application.Quit();
        });
    
    }

    private void Start() {
        EnhancedInput.Instance.OnExitEvent += OnEscEvent;
        gameObject.SetActive(false);
        uiGassuion.enabled = false;
    }

    private void OnEscEvent(object sender, EventArgs eventArgs) {
        isPauseMenuShow = !isPauseMenuShow;
        if (isPauseMenuShow) {
            uiGassuion.enabled = true;
            gameObject.SetActive(true);
            dialogueCanvas.SetActive(false);
            Time.timeScale = 0;
        }
        else {
            gameObject.SetActive(false);
            uiGassuion.enabled = false;
            dialogueCanvas.SetActive(true);
            Time.timeScale = 1;
        }
    }

    private void ReturnGame() {
        isPauseMenuShow = false;
        gameObject.SetActive(false);
        uiGassuion.enabled = false;
        dialogueCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    private void OnDestroy() {
        EnhancedInput.Instance.OnExitEvent -= OnEscEvent;
    }

}
