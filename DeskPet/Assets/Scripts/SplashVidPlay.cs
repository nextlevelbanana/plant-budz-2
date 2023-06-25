using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashVidPlay : MonoBehaviour
{
    public float sceneTime = 4f;

    private void Start()
    {
        StartCoroutine(SceneHold());
    }

    private IEnumerator SceneHold()
    {
        yield return new WaitForSeconds(sceneTime);

        SceneManager.LoadScene(1);
    }
}
