using DG.Tweening;
using UnityEngine;

public static class Bootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void PerformBootstrap()
    {
        Debug.Log("Performing bootstrap...");
        Application.targetFrameRate = 60;
        DOTween.Init(logBehaviour: LogBehaviour.Verbose);
    }
}