﻿using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
   public GameObject opponent;
   public AnimationClip attackClip;
   public AnimationClip dieClip;
   public int damage;

   public float hitTime;
   public bool hit;
   public float range;
   public int health;
   public float reviveModifier;
   private int startHealth;
   private bool hasDied;

	// Use this for initialization
	void Start ()
   {
      ClickToMove.hasDied = false;
      startHealth = health;
	}
	
	// Update is called once per frame
	void Update ()
   {
       if(!isDead())
      {
         attack();
      }
      else
         dieMethod();
   }

     void attack()
   {
      if(Input.GetKey(KeyCode.Space) && !isDead() && inRange() && !ClickToMove.hasDied)
      {
         if(opponent != null && opponent.name != "Player")
         {         
            transform.LookAt(opponent.transform);
         }

         GetComponent<Animation>().Play(attackClip.name);
         ClickToMove.attack = true;
      }     

      float animationTime = GetComponent<Animation>()[attackClip.name].time;
      float animationLength = GetComponent<Animation>()[attackClip.name].length;
      if(animationTime > 0.9*animationLength)

      {
         ClickToMove.attack = false;
         hit = false;
      }

      impact();
   }

   void impact()
   {

      if(opponent != null && GetComponent<Animation>().IsPlaying(attackClip.name) && !hit  )
      {
         float animationTime = GetComponent<Animation>()[attackClip.name].time;
         float animationLength = GetComponent<Animation>()[attackClip.name].length;

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
      if(health<=0)
         ClickToMove.hasDied = true;
      return health <= 0;
   }

   void dieMethod()
   {
      if(isDead())
         GetComponent<Animation>().Play(dieClip.name);         

      if(GetComponent<Animation>()[dieClip.name].time > (float)GetComponent<Animation>()[dieClip.name].length*0.9)
      {
         ClickToMove.hasDied = false;
         health = (int)(startHealth*reviveModifier);

      }
   }
}
