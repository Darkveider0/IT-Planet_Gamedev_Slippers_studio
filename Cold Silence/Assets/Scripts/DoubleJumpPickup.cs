using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoubleJumpPickup : MonoBehaviour
{
    public Text textbox;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.Instance.has_double_jump = true;
            StartCoroutine(popup(textbox));
            //StartCoroutine(popup_dissapear(textbox));
            //textbox.text = "Вы получили двойной прыжок!";
            //WaitForSeconds waitForSeconds = new WaitForSeconds(2f);
            //textbox.text = "";
            
        }
    }
    IEnumerator popup(Text textbox)
    {
        textbox.text = "Вы получили двойной прыжок!";
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        textbox.text = "";
        //yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    /*IEnumerator popup_dissapear(Text textbox)
    {
        textbox.text = "";
        yield return new WaitForSeconds(2);
        //yield return new WaitForSeconds(2);
    }*/
}