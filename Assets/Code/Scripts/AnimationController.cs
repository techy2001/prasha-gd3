using System;
using UnityEngine;

namespace Code.Scripts {
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour {
        private Animator animator;
        private Vector3 velocity;
        private bool grounded;
        private bool jumpHeld;
        private bool wasJumpHeld;

        private void Awake() {
            this.animator = this.GetComponent<Animator>();
        }
        
       public void SetVelocity(Vector3 velocity) {
            this.velocity = velocity;
        }

        public void SetGrounded(bool grounded) {
            this.grounded = grounded;
        }

        public void SetJumpHeld(bool jumpHeld) {
            this.jumpHeld = jumpHeld;
        }

        public void SetWasJumpHeld(bool wasJumpHeld) {
            this.wasJumpHeld = wasJumpHeld;
        }

        private void FixedUpdate() {
            
            var speed = Mathf.Sqrt(Mathf.Pow(this.velocity.x, 2) + Mathf.Pow(this.velocity.z, 2));
            
            this.animator.SetFloat("isMoving", speed);
            this.animator.SetBool("grounded", this.grounded);
            this.animator.SetBool("jumpHeld", this.jumpHeld);
            this.animator.SetBool("wasJumpHeld", this.wasJumpHeld);
        }
    }
}