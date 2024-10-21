using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _game.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitAI : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float searchRadius = 10f;
        [SerializeField] private float searchInterval = 1f;
        
        [SerializeField] private UnitState unitState;
        
        
        [SerializeField] private float _searchTimer;

        private enum UnitState
        {
            Idle,
            Moving,
            Working,
        }
        
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            unitState = UnitState.Idle;
        }
        
        void Update()
        {
            _searchTimer += Time.deltaTime;
            if (_searchTimer >= searchInterval)
            {
                _searchTimer = 0;
                SearchForTarget();
            }
            
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // The agent has reached its destination
                    unitState = UnitState.Working;
                }
            }
        }
        
        private void SearchForTarget()
        {
            var colliders = Physics.OverlapSphere(transform.position, searchRadius);
            foreach (var collider in colliders)
            {
                collider.TryGetComponent(out ITree tree);

                if(tree == null) continue;
                
                var targetPosition = collider.transform.position;
                agent.SetDestination(targetPosition);
                Debug.Log($"Moving to {targetPosition}");
                unitState = UnitState.Moving;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }

        private void OnValidate()
        {
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }
        }
    }
}
