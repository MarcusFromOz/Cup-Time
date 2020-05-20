using UnityEngine;
using TMPro;
using RPG.Saving;

namespace RPG.Resources
{
    public class Trophy : MonoBehaviour, ISaveable
    {
        Health player;
        [SerializeField] float healAmount = 50f;
        int numberOfTrophies;
        public GameObject[] years;
        private TextMeshProUGUI textMeshProUGUI;
        private TextMeshPro textMeshPro;

        // Start is called before the first frame update
        void Start()
        {
            //ToDo
            //Get Trophies.ChildCount and populate this variable for the level
            // use for the win condition
        }


        private void OnTriggerEnter(Collider other)
        {
            player = other.GetComponent<Health>();

            if (other.tag == "Player")
            {
                // Add 50ish to players health
                player.HealDamage(healAmount);

                Debug.Log(gameObject.tag + " was collided with");

                //ToDo - remove hardcoded 1977 
                if (gameObject.tag != null)
                {
                    SetTrophyAsCollected();
                }

                Destroy(gameObject);

            }

            //ToDo
            //Add to UI
            // Load up list of trophies in the UI - 
            // Make the trophy disappear (with effects - over 2 seconds)
            // Check the win condition
            // throw Update some info to the canvas
        }

        private void SetTrophyAsCollected()
        {
            years = GameObject.FindGameObjectsWithTag(gameObject.tag);

            foreach (GameObject year in years)
            {
                textMeshProUGUI = year.GetComponent<TextMeshProUGUI>();
                if (textMeshProUGUI != null)
                {
                    textMeshProUGUI.color = Color.white;

                    //if (textMeshProUGUI.enabled == false)
                    //{
                    //ToDo - this is to do with storing some text with each trophy - telling a bit of a story about it
                    // ToDo - Get the text from the Dictionary
                    // ToDo - This is a very dodgey way to do it - be more specific or remove
                    //textMeshProUGUI.color = Color.black;
                    //textMeshProUGUI.enabled = true;
                    //}
                }

                //if inactive by default then activate it
                //textMeshPro = year.GetComponent<TextMeshPro>();
                //if (textMeshPro != null)
                //{
                //    textMeshPro.gameObject.SetActive(true);
                //}
            }
        }

        public object CaptureState()
        {
            //ToDo - Get Tropy state saving between levels and restarts
            // not sure why I couldn't debug this with a breakpoint - it didn't trigger
            return years;
        }

        public void RestoreState(object state)
        {
            GameObject[] storedTrophies = (GameObject[])state;
            years = storedTrophies;

            foreach (GameObject year in years)
            {
                textMeshProUGUI = year.GetComponent<TextMeshProUGUI>();
                if (textMeshProUGUI != null)
                {
                    textMeshProUGUI.color = Color.white;
                }
            }
        }
    }
}