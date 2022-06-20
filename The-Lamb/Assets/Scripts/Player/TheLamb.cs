using UnityEngine;

public class TheLamb : MonoBehaviour
{
   [Header("Lambs Stats")]
    public LambStats Lamb;
        
    private float movementH, movementV;
    private Rigidbody2D rb2d;
    private Collider2D col2d;

    [SerializeField]
    private float distance, checkRaid, JimeCounter;

    public float Jime;

    public bool climbable,isGrounded,Jumpped;

    private bool Face = true;

    public Transform gound;

#region Flame leeps Bools&Floats
     
    [Header("Goats Leap")]
    [SerializeField] 
    private float Raduis;  // how far they need to be to Candles
    [SerializeField]
    private float Firespower; // how much kick is in the kick off
    [SerializeField]
    private float FlamesTime; // How much time is in the Abuilty.
    [SerializeField]
    GameObject Candleflame; //The Objects in Question for this abuilty
    [SerializeField]
    private GameObject Arrow; // The pointy thing.
    private bool Nearflame; //Near Targets
    private bool ChosingDir; //Chosing the way to go.
    private bool isFlame; // is using there abuilty
    Vector3 FlamesLeap; //Force?
    private float FlamesTimeRest; //Just resets the clock.

#endregion


    private void Awake() 
    {
       rb2d = GetComponent<Rigidbody2D>();
       col2d = GetComponent<Collider2D>();      
    }

    // Start is called before the first frame update
    void Start()
    {
        FlamesTimeRest = FlamesTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(gound.position, checkRaid, Lamb.ground);
        if(isFlame == false)
        
        Climbing();
        Flip();
    }

    void Update()
    {
         if(Input.GetButtonDown("Jump") && isGrounded){
            rb2d.velocity = Vector2.up * Lamb.jumpSpeed;
            JimeCounter = Jime;
            Jumpped = true;
        }

       if(Input.GetButton("Jump")&& Jumpped == true){
           if(JimeCounter > 0){
               rb2d.velocity = Vector2.up * Lamb.jumpSpeed;
               JimeCounter -= Time.deltaTime;
           }else{
               Jumpped = false;
           }
       }

       if(Input.GetButtonUp("Jump")){
           Jumpped = false;
       }
       //CandleJack();
       Movement();
    }
    

#region Movement
    void Movement()
    {    
        movementH = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(movementH * Lamb.speed, rb2d.velocity.y);
    }
#endregion

#region Flip
    void Flip()
    {
    if (Face == false && movementH > 0){
        AboutFace();
        }else if(Face == true && movementH <0){
            AboutFace();
        }       
    }

    void AboutFace()
    {
        Face = !Face;
        transform.Rotate (0f, 180f, 0);
    }
    
#endregion

#region Climbing
    public void Climbing()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, Lamb.climb);
        Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.black);

        if(hitInfo.collider!=null)
        {
            if(Input.GetButtonDown("Vertical")){
                climbable = true;    
            }else{
                if (Input.GetButtonDown("Horizontal")){
                    climbable = false;
                }
            }               
        }

        if(climbable == true && hitInfo.collider != null )
        {
           movementV = Input.GetAxisRaw("Vertical");
           rb2d.velocity = new Vector2(rb2d.velocity.x, movementV * Lamb.speed);
           rb2d.gravityScale = 0;
         }else{
            rb2d.gravityScale = 1;
        }

        if (climbable == true && hitInfo.collider != null && Jumpped == false ){
            rb2d.gravityScale = 0;
            
        }
        
 
    }
 #endregion

#region Mothers Flame
//    void CandleJack()//"scream"
//    {
//        RaycastHit2D[] Rays = Physics2D.CircleCastAll(transform.position, Raduis,Vector3.forward);
//        foreach(RaycastHit2D ray in Rays)
//        {
//            Nearflame = false;

//            if(ray.collider.tag == "Candles")
//            {
//                Nearflame = true;
//                Candleflame = ray.collider.transform.gameObject;
//                break;
//            }
//        }
//        if (Nearflame)
//         {
//            Candleflame.GetComponent<SpriteRenderer>().color = Color.yellow;
//            if(Input.GetKeyDown(KeyCode.Mouse0))
//             {
//                Time.timeScale = 0f;
//                Candleflame.transform.localScale = new Vector2(1.4f,1.4f);
//                Arrow.SetActive(true);
//                Arrow.transform.position = Candleflame.transform.transform.position;
//                ChosingDir = true;
//             }
//             else if (ChosingDir && Input.GetKeyUp(KeyCode.Mouse0))
//             {
//            Time.timeScale = 1f;
//            Candleflame.transform.localScale = new Vector2(1,1);
//            ChosingDir = false;
//            isFlame = true;
//            rb2d.velocity = Vector2.zero;
//            transform.position = Candleflame.transform.position;
//            FlamesLeap = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
//            FlamesLeap.z = 0;
//            if(FlamesLeap.x > 0)
//            {
//                transform.eulerAngles = new Vector3(0,0,0);
//            }
//            else
//            {
//                transform.eulerAngles = new Vector3(0,180,0);
//            }
//            FlamesLeap = FlamesLeap.normalized;
//            Candleflame.GetComponent<Rigidbody2D>().AddForce(-FlamesLeap * 50, ForceMode2D.Impulse);
//            Arrow.SetActive(false);
//             }
//          else if (Candleflame != null)
//          {
//            Candleflame.GetComponent<SpriteRenderer>().color = Color.white;
//          }

//          //The Real pain in the ass.

//        if (isFlame)
//        {
//            if(FlamesTime > 0)
//            {
//                FlamesTime -= Time.deltaTime;
//                rb2d.velocity = FlamesLeap * Firespower * Time.deltaTime;
//            }
//            else
//            {
//                isFlame = false;
//                FlamesTime = FlamesTimeRest;
//                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
//            }
//         }              
//     }
// }   
    #endregion

#region Interaction
    private void Interact(float val)
    {
         if (val == 1 && Interacting()){
             Debug.Log("Hey a " + gameObject.name);
            }
    }

   private bool Interacting(){
       Vector2 Lambbod = transform.position;
       Lambbod.x += col2d.bounds.extents.x;
       return Physics2D.OverlapCircle(Lambbod,.1f,Lamb.interact);

   }
#endregion

    //#region Damage
    //public void Damage()
    //{
    //    Lamb.Light--;    

    //    if(Lamb.Light < 1){
    //    Destroy(this.gameObject);
     //   }else if(Hole()){
    //    Destroy(this.gameObject);
    //}   

    //private bool Hole()
    //{
    //Vector2 Lambbod = transform.position;
    //return Physics2D.OverlapCircle(Lambbod,Lamb.pit);
    //}
    //#endregion
}