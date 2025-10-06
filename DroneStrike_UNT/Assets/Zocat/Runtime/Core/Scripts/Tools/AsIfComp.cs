using System.Collections;
using UnityEngine;

namespace Zocat
{
    public static class AsIfComp
    {
        public static void SetAnimation(Animator animator, int index)
        {
            animator.SetInteger("Index", index);
        }
    }
}