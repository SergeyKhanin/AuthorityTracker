using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsView : MonoBehaviour
{
    private VisualElement _root;
    private CustomButton _backButton;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _backButton = _root.Q<CustomButton>("back-button");
    }

    private void OnEnable() => _backButton.clicked += OnBackButtonClicked;
    private void OnDisable() => _backButton.clicked -= OnBackButtonClicked;
    private void OnBackButtonClicked() => SceneManager.LoadScene(CommonScenesList.SettingsScene);
}