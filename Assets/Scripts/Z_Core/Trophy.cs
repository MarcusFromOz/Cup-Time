using UnityEngine;
using TMPro;
using RPG.Saving;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;

namespace RPG.Attributes
{
    public class Trophy : MonoBehaviour
    {
        Health player;
        [SerializeField] float healAmount = 50f;
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
            //ToDo
            // Check the win condition
            // throw Update some info to the canvas

            player = other.GetComponent<Health>();

            if (other.tag == "Player")
            {
                // Add 50ish to players health
                player.HealDamage(healAmount);

                if (gameObject.tag != null)
                {
                    SetTrophyAsCollected();
                }

                //ToDo fix this
                //Portal portal = GetComponent<Portal>();
                //portal.IncrementTrophyCount();

                Destroy(gameObject, 0.5f);
                }
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
                    // ToDo - this is to do with storing some text with each trophy - telling a bit of a story about it
                    // ToDo - Get the text from the Dictionary
                    // ToDo - This is a very dodgey way to do it - be more specific or remove
                    //textMeshProUGUI.color = Color.black;
                    //textMeshProUGUI.enabled = true;
                    //}
                }

                // if inactive by default then activate it
                // textMeshPro = year.GetComponent<TextMeshPro>();
                // if (textMeshPro != null)
                // {
                //    textMeshPro.gameObject.SetActive(true);
                // }
            }
        }

        //public object CaptureState()
        //{
            // ToDo - Get Trophy state saving between levels and restarts
            // not sure why I couldn't debug this with a breakpoint - it didn't trigger
            
        //    return years;
        //}

        //public void RestoreState(object state)
        //{
            //GameObject[] storedTrophies = (GameObject[])state;
            //years = storedTrophies;

            //foreach (GameObject year in years)
            //{
            //    textMeshProUGUI = year.GetComponent<TextMeshProUGUI>();
            //    if (textMeshProUGUI != null)
            //    {
            //        textMeshProUGUI.color = Color.white;
            //    }
            //}
        //}
    }
}