using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Transition : MonoBehaviour
{
    public int scene_number;    
    public GameObject message;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerData.SavePlayerData();
            //StartCoroutine(SceneTransitions.fadeIn(scene_number));
            //SceneManager.LoadScene(scene_number);
            message.SetActive(true);
            Player.Instance.isPaused = true;
            StartCoroutine(Wait());
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        Player.Instance.isPaused = false;
        StartCoroutine(SceneTransitions.fadeIn(scene_number));
    }
}
