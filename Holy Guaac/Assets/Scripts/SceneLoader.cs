using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string Scenename;
    public string Scenename2;
    public string Scenename3;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Scenename);
    }
    public void Gallery()
    {
        SceneManager.LoadScene(Scenename2);
    }

    public void StartStory()
    {
        SceneManager.LoadScene(Scenename3);
    }
}
