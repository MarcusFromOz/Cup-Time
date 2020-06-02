using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier { A, B, C, D, E }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] int currentScene = -1;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

        //ToDo - **** Remove this field and logic post gameJam :)
        [SerializeField] bool isGameJam = true;

        //To go from Opening Scene to Scene 1 
        public void LoadFirstScene()
        {
            sceneToLoad = 1;
            StartCoroutine(Transition()); 
        }

        public void LoadClosingScene()
        {
            sceneToLoad = 5;

            SceneManager.LoadScene(sceneToLoad);
            //StartCoroutine(Transition());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !isGameJam)
                {
                StartCoroutine(Transition());
                }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            if (fader != null)
            {
                yield return fader.FadeOut(fadeOutTime);
            }

            //ToDo uncomment and use after gameJam

            //Save current level
            //SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            //if (wrapper != null)
            //{
            //    wrapper.Save();
            //}

            if (currentScene > 0 && currentScene < 5)
            {
                PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
                playerController.enabled = false;
            }

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            PlayerController newPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayerController.enabled = false;

            //Load current level
            //if (wrapper != null)
            //{
            //    wrapper.Load();
            //}

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            //wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);

            if (fader != null)
            {
                fader.FadeIn(fadeInTime);
            }

            newPlayerController.enabled = true;
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");

            //prevent clash with NavMesh agent
            //alternate method to manage this .. player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        
        //Fix after GameJam
        //    player.GetComponent<NavMeshAgent>().enabled = false;
        //    player.transform.position = otherPortal.spawnPoint.position;
        //    player.transform.rotation = otherPortal.spawnPoint.rotation;
        //    player.GetComponent<NavMeshAgent>().enabled = true;
        
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }

    }
}
