using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace i3D.Example
{
    public class OneIntegrationExample : MonoBehaviour
    {
        private OneServer _server;
        
        private IEnumerator Start()
        {
            _server = FindObjectOfType<OneServer>();
            Debug.Log(_server.Status);

            yield return new WaitForSeconds(5f);

            Debug.Log(_server.Status);

            _server.SetLiveState(1,
                                5,
                                "Example",
                                "Example map",
                                "Example mode",
                                "v0.1",
                                null);
            
            if (SceneManager.GetActiveScene().buildIndex != 2)
                SceneManager.LoadScene(2);
        }
    }
}