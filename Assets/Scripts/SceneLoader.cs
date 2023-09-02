using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Loading Scene Properties:")]
    [SerializeField] private float LoadingDelay;
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject Panel;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        StartCoroutine(PlayLoad());
    }

    public IEnumerator PlayLoad()
    {
        LoadingPanel.SetActive(true);

        Panel.SetActive(false);

        yield return new WaitForSeconds(LoadingDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Levels()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Levels
    public void Level_1()
    {
        StartCoroutine(Level_1Load());
    }

    public void Level_2()
    {
        StartCoroutine(Level_2Load());
    }

    public void Level_3()
    {
        StartCoroutine(Level_3Load());
    }

    public void Level_4()
    {
        StartCoroutine(Level_4Load());
    }

    public void Level_5()
    {
        StartCoroutine(Level_5Load());
    }

    public IEnumerator Level_1Load()
    {
        LoadingPanel.SetActive(true);

        Panel.SetActive(false);

        yield return new WaitForSeconds(LoadingDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator Level_2Load()
    {
        LoadingPanel.SetActive(true);

        yield return new WaitForSeconds(LoadingDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public IEnumerator Level_3Load()
    {
        LoadingPanel.SetActive(true);

        yield return new WaitForSeconds(LoadingDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public IEnumerator Level_4Load()
    {
        LoadingPanel.SetActive(true);

        yield return new WaitForSeconds(LoadingDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public IEnumerator Level_5Load()
    {
        LoadingPanel.SetActive(true);

        yield return new WaitForSeconds(LoadingDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }
}
