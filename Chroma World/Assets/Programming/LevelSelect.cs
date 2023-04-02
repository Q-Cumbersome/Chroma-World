using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Loads the "LevelSelect" scene
    public void LoadScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
