using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public string scene_name;
    public static Lose Instance { get; set; }

    private void Start()
    {
        Instance = this;
    }

    public void Defeat()
    {
        Player.Instance.isPaused = true;
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Player.Instance.isPaused = false;
        SceneManager.LoadScene(scene_name);
    }
}
