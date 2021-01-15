using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private bool forceStress;

    private void Start()
    {
        bool stress = IsStressTesting();
        
        if (forceStress || stress)
            SceneManager.LoadScene("StressTest");
        else
            SceneManager.LoadScene("OneIntegrationExample");
    }

    private static bool IsStressTesting()
    {
        var args = Environment.GetCommandLineArgs();
        return Array.FindIndex(args, s => string.Equals(s, "-stress", StringComparison.Ordinal)) != -1;
    }
}