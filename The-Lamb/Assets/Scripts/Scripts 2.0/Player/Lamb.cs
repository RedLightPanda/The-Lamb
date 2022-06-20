using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamb : MonoBehaviour
{
    
    [Header("Lambs Lists")]
    [SerializeField] private LayerList LList;
    [SerializeField] private Stats LStats;

    [Header("Refs")]
    public Transform ground;
    private Rigidbody2D rb2d;
    private CapsuleCollider2D cold2d;
    [SerializeField]private Animator TestLamb;

    [Header("Props")]
    [SerializeField] private float dist; //Distance
    [SerializeField] private float Caid; //Check Raides
    [SerializeField] private float Jimecounter; //Jump Time Counter
    [SerializeField] private float Jime; //Jump Time
    private float moveH, moveV;
    public bool isGround,Jumpped;
    private bool Face = true;


    void Awake() 
    {
        rb2d = GetComponent<Rigidbody2D>();
        cold2d = GetComponent<CapsuleCollider2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate() 
    {
        isGround = Physics2D.OverlapCircle(ground.position, Caid, LList.Ground);
        Flip();
    }

    // Update is called once per frame
    void Update()
    {
        #region Jump
         if(Input.GetButtonDown("Jump") && isGround){
            rb2d.velocity = Vector2.up * LStats.jumpSpeed;
            Jimecounter = Jime;
            Jumpped = true;
        }

       if(Input.GetButton("Jump")&& Jumpped == true){
           if(Jimecounter > 0){
               rb2d.velocity = Vector2.up * LStats.jumpSpeed;
               Jimecounter -= Time.deltaTime;
           }else{
               Jumpped = false;
           }
       }

       if(Input.GetButtonUp("Jump")){
           Jumpped = false;
       }
       #endregion 
        Movement();   
    }

    #region Movement & Flip
    void Movement()
    {
        moveH = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(moveH * LStats.speed, rb2d.velocity.y);
    }

    void Flip()
    {
    if (Face == false && moveH > 0){
            AboutFace();
        }else if(Face == true &&moveH < 0){
            AboutFace();
        }       
    }

    void AboutFace()
    {
        Face = !Face;
        transform.Rotate(0f, 180f, 0f);
    }

    #endregion

}
