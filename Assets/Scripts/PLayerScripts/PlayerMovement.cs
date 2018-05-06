using System;
using UnityEngine;

namespace PLayerScripts {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour {
        public int Player;

        public float AnimSpeed = 1.5f;
        public bool UseCurves = true;
        public float ForwardSpeed = 7.0f;

        private CapsuleCollider _col;
        private Vector3 _velocity;
        private float _orgColHight;
        private Vector3 _orgVectColCenter;

        private Animator _anim;
        private AnimatorStateInfo _currentBaseState;

        private static readonly int IdleState = Animator.StringToHash("Base Layer.Idle");
        private static readonly int LocoState = Animator.StringToHash("Base Layer.Locomotion");
        private static readonly int RestState = Animator.StringToHash("Base Layer.Rest");


        private void Start() {
            _anim = GetComponent<Animator>();
            _col = GetComponent<CapsuleCollider>();
            _orgColHight = _col.height;
            _orgVectColCenter = _col.center;
        }

        private void Update() {
            
            var h = Player == 1 ? Input.GetAxis("Horizontal1") : Input.GetAxis("HorizontalJoystick");
            var v = Player == 1 ? Input.GetAxis("Vertical1") : Input.GetAxis("VerticalJoystick");
            
            var axisInput = (new Vector3(h, 0, v));

            if (axisInput != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(axisInput);
                _anim.SetFloat("Speed", 1);
            }
            else {
                _anim.SetFloat("Speed", 0);
            }
            _anim.speed = AnimSpeed;
            _currentBaseState = _anim.GetCurrentAnimatorStateInfo(0);
            
            transform.Translate (axisInput * ForwardSpeed * Time.deltaTime, Space.World);
            
            if (_currentBaseState.fullPathHash == LocoState) {
                if (UseCurves) {
                    ResetCollider();
                }
            }

            else if (_currentBaseState.fullPathHash == IdleState) {
                if (UseCurves) {
                    ResetCollider();
                }
            }

            else if (_currentBaseState.fullPathHash == RestState) {
                if (!_anim.IsInTransition(0)) {
                    _anim.SetBool("Rest", false);
                }
            }
        }

        private void ResetCollider() {
            _col.height = _orgColHight;
            _col.center = _orgVectColCenter;
        }
    }
}