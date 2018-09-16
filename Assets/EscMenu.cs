using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public bool showing = false;
    public bool shown = false;
    float showCt = 0.0f;
    public Player player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            toggle();

        showHide();
    }

    public void toggle()
    {
        showing = !showing;
        if (!shown)
        {
            player.stunEquipment(true);
            player.stunned = true;
            Time.timeScale = 0;
        }
        else
        {
            player.stunEquipment(false);
            player.stunned = false;
            Time.timeScale = 1;
        }
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        toggle();
    }

    public void showHide()
    {
        if (showing)
        {

            if (shown)
            {
                showCt -= 0.1f;
                this.transform.localScale = new Vector3(showCt, showCt, showCt);
                if (showCt <= 0.0f)
                {
                    this.transform.localScale = new Vector3(0, 0, 0);

                    shown = !shown;
                    showCt = 0.0f;
                    showing = false;
                }
            }
            else
            {
                showCt += 0.1f;
                this.transform.localScale = new Vector3(showCt, showCt, showCt);
                if (showCt >= 1.0f)
                {
                    shown = !shown;
                    showCt = 1.0f;
                    showing = false;
                }
            }
        }
    }
}
