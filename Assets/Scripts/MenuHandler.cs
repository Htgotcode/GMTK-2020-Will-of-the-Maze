using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    [SerializeField] private AudioSource aS;
    [SerializeField] private Text text;

    private void Start() {
        aS = GetComponent<AudioSource>();
        text.text = PlayerController.stepCount.ToString();
    }

    public void PlayButton() {
        
        SceneManager.LoadScene(1);
    }
    public void AgainButton() {
        SceneManager.LoadScene(1);
    }
    public void ExitGame() {
        Application.Quit();
    }
}
