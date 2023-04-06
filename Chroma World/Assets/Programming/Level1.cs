using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    // Loads the "Level1" scene
    public void LoadScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
