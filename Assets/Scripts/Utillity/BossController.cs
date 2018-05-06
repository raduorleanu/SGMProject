using UnityEngine;

namespace Utillity {
    public class BossController : MonoBehaviour {
        private Animator _animator;

        private void Start() {
            _animator = GetComponent<Animator>();
            _animator.SetInteger("moving", 1);
        }
    }
}