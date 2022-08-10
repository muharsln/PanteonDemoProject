using UnityEngine;
using UnityEngine.SceneManagement;

namespace PanteonDemo.Scene
{
    public class SceneManagement : MonoBehaviour
    {
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}