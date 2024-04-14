using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public GameObject ?fadeInTransition;
    public GameObject ?fadeOutTransition;
    // Start is called before the first frame update
    public static SceneTransitions instance;
    private void Start()
    {
        instance = this;
        if (fadeInTransition != null)
            fadeInTransition.SetActive(false);
        if (fadeOutTransition != null)
            StartCoroutine(fadeOut());
    }
    public static IEnumerator fadeIn(int scene)
    {
        if (instance.fadeInTransition == null) yield break;
        Player.Instance.isPaused = true;
        instance.fadeInTransition.SetActive(true);
        yield return new WaitForSeconds(1);
        instance.fadeInTransition.SetActive(false);
        SceneManager.LoadScene(scene); // перемещение на сцену победы
    }
    public static IEnumerator fadeOut()
    {
        if (instance.fadeOutTransition == null) yield break;
        Player.Instance.isPaused = true;
        float player_speed = Player.Instance.speed;
        Player.Instance.speed=0;
        instance.fadeOutTransition.SetActive(true);
        yield return new WaitForSeconds(1);
        Player.Instance.isPaused = false;
        Player.Instance.speed = player_speed;
        instance.fadeOutTransition.SetActive(false);
    }
}
