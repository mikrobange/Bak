using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuUI : MonoBehaviour
{
    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("start");
        Button settingsButton = root.Q<Button>("settings");
        Button exitButton = root.Q<Button>("exit");

        startButton.clicked += () => SceneManager.LoadScene(0);

    }
}
