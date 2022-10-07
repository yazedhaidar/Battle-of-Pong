using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string namePowerUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if(namePowerUp=="BonusGoal")
            {
                Debug.Log("Dapat Bonus Goal");
                collision.GetComponent<Ball>().bonusGoal = true;
            }


            if (namePowerUp == "SpeedUp")
            {
                Debug.Log("Dapat Speed UP");
                Ball ball = collision.GetComponent<Ball>();
               
                ball.speed *= 2f;
                


            }

            if (namePowerUp == "ChangeDir")
            {
                Debug.Log("Dapat Change Direction");
                Ball ball = collision.GetComponent<Ball>();
                if (ball.isLastHit1) 
                {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, Random.Range(-1, 1)) * ball.speed;
                }
                else
                {
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(1, Random.Range(-1, 1)) * ball.speed;
                }

            }

            Destroy(gameObject);
        }

        
    }

    
}

