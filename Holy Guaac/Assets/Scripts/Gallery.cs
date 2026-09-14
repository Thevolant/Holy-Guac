using UnityEngine;
using UnityEngine.SceneManagement; 
public class Gallery : MonoBehaviour
{
    public string GalleryName;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void StartGallery()
    {
        SceneManager.LoadScene(GalleryName);
    }

}
