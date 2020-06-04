using RPG.Control;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;
        [SerializeField] float respawnTime = 5f;
        [SerializeField] float distanceToPickup = 5.0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.GetComponent<Fighter>());
            }
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            //Hide or Show the collider
            GetComponent<Collider>().enabled = shouldShow;
            
            //Hide or show the child objects eg the renders, while leaving the root object enabled,
            // otherwise the Coroutine won't work
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            //Do a distance check
            if (Vector3.Distance(callingController.transform.position, transform.position) < distanceToPickup)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Pickup(callingController.GetComponent<Fighter>());
                }
                return true;
            }
            return false;            
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}

