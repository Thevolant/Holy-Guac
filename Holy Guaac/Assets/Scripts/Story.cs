using UnityEngine;
using UnityEngine.SceneManagement;
public class Story : MonoBehaviour
{
    public string storyname;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StartStory()
    {
        SceneManager.LoadScene(storyname);

    }

}
