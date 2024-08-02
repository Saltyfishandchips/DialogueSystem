using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayPassUI : MonoBehaviour
{
    
    private Button closeButton;

    public static event EventHandler OnDayPassUIStart;
    public static event EventHandler OnDayPassUIEnd;

    private void Awake() {
        
        closeButton = GetComponentInChildren<Button>();
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        

        if (closeButton) {
            closeButton.onClick.AddListener(() => {
                gameObject.SetActive(false);
                OnDayPassUIEnd?.Invoke(this, EventArgs.Empty);
            });
        }
    }

    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        OnDayPassUIStart?.Invoke(this, EventArgs.Empty);
    }

    
}
