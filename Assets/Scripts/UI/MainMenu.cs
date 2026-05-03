using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _authorsPanel;

    [SerializeField] private Button _playButton;

    private bool _isLoading = false;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(PlayGame);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(PlayGame);
    }

    public void PlayGame()
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ToggleAuthors()
    {
        if (_authorsPanel.activeSelf == true)
        {
            _authorsPanel.SetActive(false);
        }
        else
        {
            _authorsPanel.SetActive(true);
        }
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}