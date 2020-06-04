using UnityEngine;
using TMPro;
using RPG.Saving;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;
using RPG.Control;
using RPG.Combat;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class Trophy : MonoBehaviour, IRaycastable
    {
        Health player;
        [SerializeField] float healAmount = 50f;
        public GameObject[] years;
        private TextMeshProUGUI textMeshProUGUI;
        private TextMeshPro textMeshPro;
        [SerializeField] Text trophyText = null;
        [SerializeField] Canvas trophyInfo = null;
        [SerializeField] float distanceToPickup = 5.0f;

        // Start is called before the first frame update
        void Start()
        {
            trophyText.text = gameObject.name;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Trophy;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            //proximity check
            if (Vector3.Distance(callingController.transform.position, transform.position) < distanceToPickup)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (callingController.tag == "Player")
                    {
                        player = callingController.GetComponent<Health>();
                        player.HealDamage(healAmount);

                        if (gameObject.tag != null)
                        {
                            UpdateUI();
                        }

                        player.IncrementTrophyCount();

                        trophyInfo.GetComponent<Animation>().Play();

                        Destroy(gameObject, 2f);
                    }
                }
                return true;
            }
            return false;
        }

        private void UpdateUI()
        {
            years = GameObject.FindGameObjectsWithTag(gameObject.tag);

            foreach (GameObject year in years)
            {
                textMeshProUGUI = year.GetComponent<TextMeshProUGUI>();
                if (textMeshProUGUI != null)
                {
                    textMeshProUGUI.color = Color.yellow;
                    textMeshProUGUI.fontStyle = FontStyles.Bold;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // CollectTrophy(other); - had 2 ways of collecting - this caused issues
        }

        //private void CollectTrophy(Collider other)
        //{
        
        //    player = other.GetComponent<Health>();

        //    if (other.tag == "Player")
        //    {
        //        player.HealDamage(healAmount);

        //        if (gameObject.tag != null)
        //        {
        //            UpdateUI();
        //        }

        //        player.IncrementTrophyCount();

        //        Destroy(gameObject, 0.5f);
        //    }
        //}
                

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