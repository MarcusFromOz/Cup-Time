using UnityEngine;
using TMPro;
using RPG.Saving;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;
using RPG.Control;
using RPG.Combat;

namespace RPG.Attributes
{
    public class Trophy : MonoBehaviour, IRaycastable
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
            CollectTrophy(other);
        }

        private void CollectTrophy(Collider other)
        {
            //ToDo
            // ..Check the win condition
            // ..throw Update some info to the canvas

            player = other.GetComponent<Health>();

            if (other.tag == "Player")
            {
                player.HealDamage(healAmount);

                if (gameObject.tag != null)
                {
                    SetTrophyAsCollected();
                }

                //ToDo **fix this
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

                    // ToDo - this is to do with storing some text with each trophy - telling a bit of a story about it
                    // - Get the text from the Dictionary
                    // - This is a dodgey way to do it - removed for now

                    //if (textMeshProUGUI.enabled == false)
                    //{
                    //textMeshProUGUI.color = Color.black;
                    //textMeshProUGUI.enabled = true;
                    //}
                }

            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Trophy;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player = callingController.GetComponent<Health>();

                if (callingController.tag == "Player")
                {
                    player.HealDamage(healAmount);

                    if (gameObject.tag != null)
                    {
                        SetTrophyAsCollected();
                    }

                    Destroy(gameObject, 0.5f);
                }
            }
            return true;
        }


        //public object CaptureState()
        //{
        //    ToDo - Get Trophy state saving between levels and restarts
        //      - Not sure why I couldn't debug this with a breakpoint - it didn't trigger
        //    return years;
        //}

        //public void RestoreState(object state)
        //{
        //  GameObject[] storedTrophies = (GameObject[])state;
        //  years = storedTrophies;

        //  foreach (GameObject year in years)
        //  {
        //    textMeshProUGUI = year.GetComponent<TextMeshProUGUI>();
        //    if (textMeshProUGUI != null)
        //    {
        //        textMeshProUGUI.color = Color.white;
        //    }
        //  }
        
    }
}