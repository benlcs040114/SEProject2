using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance; //singleton
    public float speed = 0;//speed

    Vector3 movement;//velocity vector
    Animator anim;//Animation Controller Component
    Rigidbody playerRigidbody;//Rigid body component
    int floorMask;//Set the floor level
    float camRayLength = 100f;//camera ray length 


    float jumpHigh = 1.2f;//long jump height
    bool isJump = false;//whether to jump

    bool isGround = true;//whether on the ground

    public Vector3 GetMousePoint { get; set; }//mouse point
    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");//get hierarchy

        anim = GetComponent<Animator>();//Get the animation controller component

        playerRigidbody = GetComponent<Rigidbody>();//Get the rigid body component

        instance = this;//singleton
    }

    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");//Horizontal direction
        float v = Input.GetAxis("Vertical");//vertical movement direction

        float w = Input.GetAxis("Mouse X");//mouse axis

        if (Input.GetKeyDown(KeyCode.Space))//press space
        {
            if (!isJump)//jump state
            {
                isJump = true;//change jump state

                isGround = false;//is on the ground
            }
        }
        
        if (!isGround)//is on the ground
        {
            if (isJump)//whether to jump
            {

                transform.position += new Vector3(0, 0.04f, 0);//Increase 0.04 in y direction
                if (transform.position.y >= 1.1999f)
                {
                    transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);//set location
                    isJump = false;//control jump state
                }
            }
            else
            {
                transform.position -= new Vector3(0, 0.04f, 0);//set location
                if (transform.position.y <= 0.05)
                {
                    isGround = true;//whether on the ground
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);//set location

                }   
             
            }
        }

      // transform.GetComponent<Rigidbody>().AddForce(transform.up * 100);

        Move(h, v);//control movement
        Turning(w);//control steering
        Animating(h, v);//control animation
    }

    void Move(float h,float v)
    {
    
        transform.Translate(new Vector3(h,0,v) * 0.1f, Space.Self);//control movement

   
    }
    /// <summary>
    /// camera emits rays
    /// </summary>
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);//rays
        RaycastHit floorHit;//ray return information
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))//Whether to hit the level
        {
            Vector3 playerToMouse = floorHit.point - transform.position;//mouse direction
            GetMousePoint = playerToMouse;//mouse point
            playerToMouse.y = 0f;//y direction
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);//to rotate
            playerRigidbody.MoveRotation(newRotation);//Control task orientation
        }
        Debug.DrawRay(Camera.main.transform.position, floorHit.point, Color.red);//draw rays
        Debug.DrawLine(Camera.main.transform.position, floorHit.point, Color.red);//draw rays
    }
    /// <summary>
    /// control task steering
    /// </summary>
    /// <param name="h"></param>
    void Turning(float h)
    {
       
        if(Mathf.Abs(h)>0.3f)
           transform.localEulerAngles += new Vector3(0,  5*h, 0);

//void Animating(float h,float v)
    {
   //     bool walking = h != 0f || v != 0f;
   //     anim.SetBool("IsWalking", walking);
    }
}


     

    /// <summary>
    /// animate
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    void Animating(float h,float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
