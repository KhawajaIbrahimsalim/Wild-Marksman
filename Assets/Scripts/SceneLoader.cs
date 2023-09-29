using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Loading Scene Properties:")]
    [SerializeField] private float LoadingDelay;
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject Panel;

    private GameObject Audio_Source;

    private void Start()
    {
        Audio_Source = GameObject.Find("Audio Source");

        Time.timeScale = 1f;
    }

    public void Play()
    {
        // Play Audio
        PlayUIAudio();

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
        // Play Audio
        PlayUIAudio();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        // Play Audio
        PlayUIAudio();

        Application.Quit();
    }

    public void ToMenu()
    {
        // Play Audio
        PlayUIAudio();

        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        // Play Audio
        PlayUIAudio();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        // Play Audio
        PlayUIAudio();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Back()
    {
        // Play Audio
        PlayUIAudio();

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

    private void PlayUIAudio()
    {
        // Play Audio
        Audio_Source.GetComponent<AudioController>().PlayAudio(Audio_Source.GetComponent<AudioController>().UI_Button_Audio);
    }
}
