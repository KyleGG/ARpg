using UnityEngine;
using System.Collections;

public class SkeletonMob : MonoBehaviour {

   public float speed;
   public float range;
   public Transform player;

   public CharacterController controller;

   public AnimationClip run;
   public AnimationClip idle;
   public AnimationClip die;
   public AnimationClip attackClip;

   private PlayerCombat opponent;
   public int health;
   public int damage;
   public float hitTime;
   public bool hit;

	// Use this for initialization
	void Start ()
   {
	   health = 100;
      opponent = player.GetComponent<PlayerCombat>();
	}
	
	// Update is called once per frame
	void Update() 
   {
      if(!isDead())
      {
         if(!inRange())
            chase();
         else if(!opponent.isDead())
         {
            attack();            
         }
      }
      else
         dieMethod();
	}
   void dieMethod()
   {
      GetComponent<Animation>().CrossFade(die.name,0.15f);

      if(GetComponent<Animation>()[die.name].time > (float)GetComponent<Animation>()[die.name].length*0.9)
         Destroy(gameObject);
   }
   bool isDead()
   {
      return health<= 0;
   }

   bool inRange()
   {
      return Vector3.Distance(transform.position, player.position) <= range;
   }

   public void getHit(int damage)
   {
      health = health - damage;
      if(health <= 0)
      {
         health = 0;
      }
   }

   void chase()
   {
      if(!isDead())
      {
         transform.LookAt(player.position);
         controller.SimpleMove(transform.forward * speed);

         GetComponent<Animation>().CrossFade(run.name, 0.15f);
      }
   }

   void attack()
   {

      if(!GetComponent<Animation>().IsPlaying(attackClip.name))
      {
         hit = false;  
         GetComponent<Animation>().CrossFade(attackClip.name,0.15f);
      }
      else
      {
         float animationTime = GetComponent<Animation>()[attackClip.name].time;
         if(animationTime > hitTime && !hit)
         { 
            opponent.getHit(damage);   
            hit = true;
         }
      }      
   }
   void OnMouseOver()
   {
      player.GetComponent<PlayerCombat>().opponent = gameObject;
   }


}
