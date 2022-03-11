using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  public enum MovementDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
public class SenseWithRays : MonoBehaviour
{

   // serialize field **REsearch

    [Range(2, 10)]
    public int accuracyLevel = 3;
    public BoxCollider2D selfBox;
    public float rayDistance = 1f;
    public LayerMask affectedLayer; // allows us to choose the layers that are affected by the script
    [Range(0f, 1f)]
    public float skinWidth = 0.02f; // use to put the rays outside the collider
    [Range(0f, 1f)]
    public float extraDistanceRatioFromCorners = 0.01f;
    public MovementDirection direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   void Update()
    {
        ThrowRays(direction, rayDistance);
      // ThrowRays(MovementDirection.Up, rayDistance);
      // ThrowRays(MovementDirection.Right, rayDistance);
     //  ThrowRays(MovementDirection.Down, rayDistance);
     //  ThrowRays(MovementDirection.Left, rayDistance);
    }

    public bool ThrowRays(MovementDirection whichWay, float rayDistance)
    {

        bool result = false;

        Vector2 rayDirection = Vector2.zero;
        if (whichWay == MovementDirection.Up) rayDirection = Vector2.up;
        if (whichWay == MovementDirection.Right) rayDirection = Vector2.right;
        if (whichWay == MovementDirection.Down) rayDirection = Vector2.down;
        if (whichWay == MovementDirection.Left) rayDirection = Vector2.left;

        for (int i = 0; i < accuracyLevel; i++)
        // 13. now we need to make MULTIPLE rays to come out in the direction of movement. so for each number there is a whole loop
        // 16. (we need to use the i variable to find the origin)    
        {
            Vector2 rayOrigin = CalculateRayOrigin(whichWay, i);       // 18. now we make a homemade function to calculate the ray origin (from the four sides of the character)
                                                                       // 24. we input moevemntdirection and the #raynumber(from the number of rays)

            if (whichWay == MovementDirection.Up) rayOrigin.y += skinWidth; // 29. here we apply the skinwidth to the ray orighin in the direction that we move in
            if (whichWay == MovementDirection.Right) rayOrigin.x += skinWidth;
            if (whichWay == MovementDirection.Down) rayOrigin.y -= skinWidth;
            if (whichWay == MovementDirection.Left) rayOrigin.x -= skinWidth;

            RaycastHit2D rayResult = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, affectedLayer); // 4. send out a ray and store it a raycasthit variable. we get the information from the input for the function 
                                                                                              // this code will later be put into a FOR loop (#13)

           // Debug.DrawRay(rayOrigin, rayDirection, Color.black, 0.3f);

            if (rayResult.collider != null)                   // 9. ##original code [result = (rayResult.collider != null)]here the result will be filled as true always if the ray DOES NOT RETURN NOTHING. 
                                                              // it will later be moved into the FOR loop then changed into an IF statement (as it cannot stay in the for loop or it will only return the LAST ray) (#14) - move into for loop
            {
                Debug.Log(rayResult.collider.name);
                result = true;                                  // 14. move #9 into the for loop. //?// 
                                                                //15. then create an if statement that says if one returns back as true then it is true.
                break;
            }
        }

        return result;
        // 3. setting return as whatever is in the variable **same as above
    }
    public Vector2 CalculateRayOrigin(MovementDirection dir, int rayIndex)    // 19. create the function with a vector2 output. //?//
    {                                                               //?// what are the parameters for the origin? we can get the centre of our character
                                                                    // 22. we use the enumeration to fill the brackets and give a new variable for it (dir) 
                                                                    // 23. we input the ray index to put a number on each ray.
                                                                    // now we need to target the four corners// we have two options.

        Vector2 result = transform.position;                // 20. we set an empty variable for the result

        Vector2 firstCorner = Vector2.zero;
        Vector2 lastCorner = Vector2.zero;

        if (dir == MovementDirection.Up)
        {
            firstCorner = GetSpecificCorner(true, false); // upper left
            firstCorner.x += 0.5f * selfBox.size.x * transform.lossyScale.x * extraDistanceRatioFromCorners;


            lastCorner = GetSpecificCorner(true, true); // upper right
            lastCorner.x -= 0.5f * selfBox.size.x * transform.lossyScale.x * extraDistanceRatioFromCorners;

        }
        else if (dir == MovementDirection.Right)
        {
            firstCorner = GetSpecificCorner(true, true); // upper right
            firstCorner.y -= 0.5f * selfBox.size.y * transform.lossyScale.y * extraDistanceRatioFromCorners;

            lastCorner = GetSpecificCorner(false, true); //lower right
            lastCorner.y += 0.5f * selfBox.size.y * transform.lossyScale.y * extraDistanceRatioFromCorners;

        }
        else if (dir == MovementDirection.Down)
        {
            firstCorner = GetSpecificCorner(false, false); // lower left
            firstCorner.x += 0.5f * selfBox.size.x * transform.lossyScale.x * extraDistanceRatioFromCorners;

            lastCorner = GetSpecificCorner(false, true); // lower right
            lastCorner.x -= 0.5f * selfBox.size.x * transform.lossyScale.x * extraDistanceRatioFromCorners;

        }
        else if (dir == MovementDirection.Left)
        {
            firstCorner = GetSpecificCorner(false, false); //lower left
            firstCorner.y += 0.5f * selfBox.size.y * transform.lossyScale.y * extraDistanceRatioFromCorners;

            lastCorner = GetSpecificCorner(true, false);   // upper left
            lastCorner.y -= 0.5f * selfBox.size.y * transform.lossyScale.y * extraDistanceRatioFromCorners;

        }

        float positionRatio = (float)rayIndex / ((float)accuracyLevel - 1f);                          // 27. find the position ratio to find the i value (1:01:35)

        result = firstCorner * positionRatio + lastCorner * (1 - positionRatio); // replaced by the line below
        result = Vector2.Lerp(firstCorner, lastCorner, positionRatio); // started with mathf.lerp
        return result;                                      // 21. we declare the result variable as the return valuable 
                                                            // now we must fll the ()
    }
    Vector2 GetSpecificCorner(bool up, bool right)
    {
        Vector2 result = transform.position;

        float invertX = 1;
        float invertY = 1;

        if (!right) invertX = -1;
        if (!up) invertY = -1;

        result.x += selfBox.offset.x; // apply offset
        result.x += selfBox.size.x * 0.5f * invertX;// add or remove half the size (1:50)
        result.x *= transform.lossyScale.x; // apply scale to everything
       // result.x += skinWidth;
        //Same thing above + below   // LossyScale refers to world/global
        
        result.y += (selfBox.size.y * 0.5f * invertY + selfBox.offset.y) * transform.lossyScale.y;

        return result;
    }
}

