using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button coninueButton;
    [SerializeField] private Button gameSettingButton;
    [SerializeField] private Button quitGameButton;

    private void Awake() {
        
    }

    private void Start() {
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene("HuanquanRoadScene");
        });

        coninueButton.onClick.AddListener(() => {});

        gameSettingButton.onClick.AddListener(() => {});

        quitGameButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
    

}
