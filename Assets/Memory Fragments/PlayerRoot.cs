using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public FragmentUI fragmentUI;

    Fragment currentFragment;
    bool uiOpen = false;

    void Update()
    {
        if (currentFragment != null && !uiOpen && Input.GetKeyDown(KeyCode.E))
        {
            fragmentUI.Open(currentFragment.description);
            uiOpen = true;
            currentFragment.gameObject.SetActive(false);
        }
        else if (uiOpen && Input.GetKeyDown(KeyCode.E))
        {
            fragmentUI.Close();
            uiOpen = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Fragment frag = other.GetComponent<Fragment>();
        if (frag)
            currentFragment = frag;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Fragment>())
            currentFragment = null;
    }
}