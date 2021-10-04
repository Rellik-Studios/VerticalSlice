using UnityEngine;

namespace Himanshu
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationHandler : MonoBehaviour
    {
        private Animator m_animator;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }
    }
}