using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Movement
{
    public class Movement
    {

        static Vector2 clampedVelocity;

        /// <summary>
        /// This function returns a Vector2 Axis of Horixontal and Vertical Inputs.
        /// </summary>
        public static Vector2 Axis
        {
            get => new Vector2(Input.GetAxis("Horizontal"), 
                    Input.GetAxis("Vertical"));
        }

        /// <summary>
        /// This function returns a Vector2 Axis of Horizontal and Vertical Inputs multiplied by Delta Time.
        /// </summary>
        public static Vector2 AxisDelta
        {
            get => new Vector2(Input.GetAxis("Horizontal"),
                    Input.GetAxis("Vertical")) * Time.deltaTime;
        }

        /// <summary>
        /// Moves the player in Horizontal multiplied by speed variable.
        /// </summary>
        /// <param name="t">Transform component of the player</param>
        /// <param name="speed">The move speed of the player</param>
        public static void SimpleMovement(Transform t, float speed)
        {
            t.Translate(Vector2.right * Axis.x * speed);
        }

        /// <summary>
        /// Moves the player in Horizontal multiplied by speed variable
        /// and Delta Time.
        /// </summary>
        /// <param name="t">Transform component of the player</param>
        /// <param name="speed">The move speed of the player</param>
        public static void DeltaMovement(Transform t, float speed)
        {
            t.Translate(Vector2.right * AxisDelta.x * speed);
        }

        /// <summary>
        /// Returns if player is touching jump button
        /// </summary>
        public static bool Btn_Jump
        {
            get => Input.GetButtonDown("Jump");
        }

        /// <summary>
        /// Makes player jumps with an impulse in rigidbody.
        /// </summary>
        /// <param name="rb2d">Rigidbody2D component of player.</param>
        /// <param name="jumpForce">The magnitude of the jump.</param>
        public static void PhysicJumpUp(Rigidbody2D rb2d, float jumpForce)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


        /// <summary>
        /// Moves the player with physics on the x Axis
        /// </summary>
        /// <param name="rb2d"></param>
        /// <param name="moveSpeed"> The amount of force for movement in X</param>
        /// <param name="maxSpeed">Max amount of player movement</param>
        public static void PhysicMovement(Rigidbody2D rb2d, float moveSpeed, float maxSpeed)
        {
            rb2d.AddForce(Vector2.right * moveSpeed * Axis.x, ForceMode2D.Impulse);

            clampedVelocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

            rb2d.velocity = new Vector2(clampedVelocity.x, rb2d.velocity.y);
        }
    }
}
