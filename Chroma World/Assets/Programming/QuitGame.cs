using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    void Start()
    {
        // Quits the game when the "Quit Game" button is clicked
        Button button = GetComponent<Button>();
        button.onClick.AddListener(QuitTheGame);
    }

    // Quits the game
    private void QuitTheGame()
    {
        Application.Quit();
    }
}
