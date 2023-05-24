using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator animator;
    private string enemyType;
    private struct EnemyAnimation
    {
        public string type;
        public string runAnimation;
    }

    private EnemyAnimation[] enemyAnimations = new EnemyAnimation[]
    {
        new EnemyAnimation {type = "Ranged", runAnimation = "EnemyRangedRunning"},
        new EnemyAnimation {type = "Close", runAnimation = "EnemyCloseRunning"}
    };

    void Start()
    {

        animator = GetComponent<Animator>();

        enemyType = gameObject.tag;
    }

    void Update()
    {
        string runAnimation = null;
        for (int i = 0; i < enemyAnimations.Length; i++)
        {
            if (enemyAnimations[i].type == enemyType)
            {
                runAnimation = enemyAnimations[i].runAnimation;
                break;
            }
        }


        if (runAnimation != null)
        {
            animator.Play(runAnimation);
        }
    }

    public void PlayRunAnimation(string enemyType)
    {
        string animationName = $"{enemyType}Run"; // Generieren des Animationsnamens

        if (animator != null)
        {
            animator.Play(animationName);
        }
    }
}
