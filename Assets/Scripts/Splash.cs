using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] private GameObject titleCanvas;
    [SerializeField] private GameObject buttonCanvas;
    [SerializeField] private GameObject player;

    private int tiks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tiks = 0;
        Invoke("ActiveMenu", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ActiveMenu()
    {
        player.gameObject.GetComponent<Animator>().Play("Hablando");

        buttonCanvas.transform.GetChild(0).gameObject.SetActive(true);
        buttonCanvas.transform.GetChild(1).gameObject.SetActive(true);
        buttonCanvas.transform.GetChild(2).gameObject.SetActive(false);
        buttonCanvas.SetActive(true);
    }

    public void PlayButton()
    {
        player.GetComponent<Animator>().SetTrigger("Victory");

        InvokeRepeating("ChangeToGame", 0.0f, 1.0f);
    }

    private void ChangeToGame()
    {
        if (tiks < 3)
        {
            buttonCanvas.transform.GetChild(0).gameObject.SetActive(false);
            buttonCanvas.transform.GetChild(1).gameObject.SetActive(false);
            buttonCanvas.transform.GetChild(2).gameObject.SetActive(true);
            buttonCanvas.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text += ".";
            tiks++;
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void QuitButton()
    {
        buttonCanvas.SetActive(false);

        player.GetComponent<Animator>().SetTrigger("Defeat");

        Application.Quit();
    }
}
