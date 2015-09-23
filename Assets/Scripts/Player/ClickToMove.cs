using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
   //External
   public float moveSpeed;
   public float rotateSpeed;
   public CharacterController controller;
   //Internal
   private Vector3 position;
   //Animation
   public AnimationClip run;
   public AnimationClip idle;
   public static bool attack;
   public static bool hasDied;

   //Called on object creation
   void Start()
   {
      position = transform.position;
  
   }

   //Called every frame
   void Update()
   {
      if(!attack && !hasDied)
      {
         // Check for left click
         if(Input.GetMouseButton(0))
         {
            // Locate terrain click location
            locatePosition();
         }

         // Move player to position
         moveToPosition();
         }
      else
      {

      }
   }

   void locatePosition()
   {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if(Physics.Raycast(ray, out hit, 1000))
      {
         if(hit.collider.tag!="Player" && hit.collider.tag!="Enemy")
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
      }
   }

   void moveToPosition()
   {
      //Game object moving
      if(Vector3.Distance(transform.position, position) > 1)
      {
         Quaternion newRotation = Quaternion.LookRotation(position - transform.position);

         newRotation.x = 0f;
         newRotation.z = 0f;
         transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
         controller.SimpleMove(transform.forward * moveSpeed);

         GetComponent<Animation>().CrossFade(run.name, 0.15f);
      }
      //Game object not moving
      else
      {
         GetComponent<Animation>().CrossFade(idle.name, 0.15f);
      }
   }
}