using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlider : MonoBehaviour
{
    [SerializeField] Animator anim;

    public bool isMenuOpen;
    public float timer;

    private bool canUse;

    private void Start()
    {
        anim.GetComponent<Animator>();
    }

    private void Update()
    {
        if (canUse)
        {
            anim.SetBool("OpenUI", true);
            isMenuOpen = true;
            Debug.Log("Opened UI");

            if (isMenuOpen)
            {
                timer += Time.deltaTime;
            }
        }

        if (isMenuOpen && timer > 0.5f && !canUse)
        {
            anim.SetBool("OpenUI", false);
            timer = 0;
            isMenuOpen = false;
        }
    }

    public void OpenBuildMenu()
    {
        canUse = !canUse;
    }
}
