using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement 
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        //** variables
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6.0f;

        NavMeshAgent navMeshAgent;
        Health health;

        //** start and update

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            //prevent movement if dead
            navMeshAgent.enabled = !health.IsDead();
            
            UpdateAnimator();
        }

        //** public methods
        
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
                
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }
        
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        //** private methods
        private void UpdateAnimator()
        {
            //give the animator the forward speed so it knows which animation to use

            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
