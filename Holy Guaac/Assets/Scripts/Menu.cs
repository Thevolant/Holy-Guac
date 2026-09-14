using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public string Levelname;


    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Levelname);
    }
}
