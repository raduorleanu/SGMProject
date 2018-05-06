using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

namespace Utillity {
    public class DistanceBasedTarget : MonoBehaviour {

        private static GameObject[] _targets;
        private NavMeshAgent _agent;
        private float _defaultAgentSpeed;

        public GameObject ProjectileIfRanged;
        public float PollingInterval; // time in seconds
        public float AggroDistance = 10f;
        public float AttackDistance = 5;
        public float ChargeSpeed = 10;

        private bool _gettingIntoRange;
        private GameObject _lockedTarget;
        private float _timeStamp;

        private float _attackSpeed;
        private Animation _animation;
        private AnimationState _attackState;

        private Vector3 _house = new Vector3(39, 0, 23);

        private void Start() {
            if (PollingInterval <= 0) {
                PollingInterval = 1;
            }
            _animation = GetComponent<Animation>();
            
            if (_animation) {
                foreach (AnimationState o in _animation) {
                    if (o.name.ToLower().Contains("attack")) {
                        _attackState = o;
                        break;
                    }
                }
            }

            _timeStamp = Time.time;
            _targets = ProximityProvider.GetPlayersPosition();
            _agent = GetComponent<NavMeshAgent>();
            _defaultAgentSpeed = _agent.speed;
            _attackSpeed = GetComponent<Enemy>().AttackSpeed;
            InvokeRepeating("UpdateTarget", 1, PollingInterval);
        }

        private void Update() {
            if (_gettingIntoRange) {
                _agent.speed = ChargeSpeed;
                if (Vector3.Distance(transform.position, _lockedTarget.transform.position) <= AttackDistance) {
                    _gettingIntoRange = false;
                    _agent.speed = 0;
                    //InvokeRepeating("AttackTarget", 0, _attackSpeed);
                    AttackTarget();
                }
            }
        }

        private void UpdateTarget() {
            _targets = ProximityProvider.GetPlayersPosition();
            foreach (var target in _targets) {
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= AggroDistance) {
                    //Debug.Log("Target aquired");
                    _agent.SetDestination(target.transform.position);
                    _lockedTarget = target;
                    _gettingIntoRange = true;
                    return;
                }
            }
            
            // todo: change to castle;
            if (Vector3.Distance(_agent.destination, _house) >= 4) {
                //Debug.Log("Changig destination from " + _agent.destination);
                if (!_agent.isOnNavMesh) return;
                _agent.SetDestination(_house);
                _agent.speed = _defaultAgentSpeed;
                _gettingIntoRange = false;

//                CancelInvoke("AttackTarget");
            }
        }

        private void AttackTarget() {
            if (_timeStamp <= Time.time) {
                if (_attackState) {
                    _animation.Play(_attackState.name);
                }
                transform.LookAt(_lockedTarget.transform);
                var x = Instantiate(ProjectileIfRanged);

//                if (x.GetComponent<MeshRenderer>() && x.GetComponent<MeshFilter>()) {
//                    x.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
//                    x.GetComponent<MeshRenderer>().materials = GetComponent<MeshRenderer>().materials;
//                }
                
                x.transform.localScale = transform.localScale / 4;
                x.transform.position = transform.position;
                x.GetComponent<EnemyProjectileMovement>().ProjectileDamage = GetComponent<Enemy>().HitDamage;
                x.GetComponent<EnemyProjectileMovement>().Target = _lockedTarget;
                _timeStamp = Time.time + _attackSpeed;
            }
        }
    }
}