using UnityEngine;

namespace Model
{
    public class Unit : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        public TileDefinitionData TileDefinition { get; private set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Unit")) animator.SetBool("isVisited", true);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Unit")) animator.SetBool("isVisited", false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Unit")) animator.SetBool("isVisited", true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Unit")) animator.SetBool("isVisited", false);
        }

        public void Place(TileDefinitionData targetTile)
        {
            // Make sure old tile location is not still pointing to this unit
            if (TileDefinition != null && TileDefinition.content.Equals(gameObject)) TileDefinition.content = null;

            // Link unit and tile references
            TileDefinition = targetTile;

            if (targetTile != null) TileDefinition.content = gameObject;
        }

        public void Match()
        {
            transform.localPosition = TileDefinition.position;
        }
    }
}