using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
   public GameObject opponent;
   public AnimationClip attack;
   public AnimationClip die;
   public int damage;

   public float hitTime;
   public bool hit;
   public float range;
   public int health;

	// Use this for initialization
	void Start ()
   {
	
	}
	
	// Update is called once per frame
	void Update ()
   {
      if(Input.GetKey(KeyCode.Space) && inRange() && !isDead())
      {
         GetComponent<Animation>().Play(attack.name);
         ClickToMove.attack = true;
         if(opponent != null && opponent.name != "Player")
         {         
            transform.LookAt(opponent.transform);
         }
      }

      float animationTime = GetComponent<Animation>()[attack.name].time;
      float animationLength = GetComponent<Animation>()[attack.name].length;
      if(animationTime > 0.9*animationLength)

      {
         ClickToMove.attack = false;
         hit = false;
      }

      impact();
      if(isDead())
         dieMethod();
   }

   void impact()
   {

      if(opponent != null && GetComponent<Animation>().IsPlaying(attack.name) && !hit)
      {
         float animationTime = GetComponent<Animation>()[attack.name].time;
         float animationLength = GetComponent<Animation>()[attack.name].length;

         if(animationTime > hitTime && (animationTime < 0.9*animationLength))
         {            
            opponent.GetComponent<SkeletonMob>().getHit(damage);
            hit = true;
         }
      }
   }

   public void getHit(int damage)
   {
      health = health - damage;
      if(health <= 0)
      {
         health = 0;
      }
   }

   bool inRange()
   {
         return (Vector3.Distance(opponent.transform.position, transform.position) <= range);
   }

   public bool isDead()
   {
      return (health <= 0);
   }
   void dieMethod()
   {

      GetComponent<Animation>().CrossFade(die.name);

      if(GetComponent<Animation>()[die.name].time > (float)GetComponent<Animation>()[die.name].length*0.9)
      {
         Destroy(gameObject);
      }
   }
}
