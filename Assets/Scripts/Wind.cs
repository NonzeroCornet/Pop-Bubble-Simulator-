    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Wind : MonoBehaviour
    {
        public float speed = 5f;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            if (!IsMouseOver())
            {
                rb.AddForce(movement * speed);
            }
        }

        bool IsMouseOver()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform == transform;
            }

            return false;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bubble"))
            {
                Rigidbody bubbleRb = collision.gameObject.GetComponent<Rigidbody>();
                bubbleRb.AddForce(collision.contacts[0].normal * 50f, ForceMode.Impulse);
            }
        }
    }

