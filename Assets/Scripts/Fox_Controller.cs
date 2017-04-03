using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Fox_Controller: MonoBehaviour {

	public static Fox_Controller instance;

    public float speed,jumpForce,cooldownHit;
	public bool running,up,down,jumping,crouching,dead,attacking,special;
    private Rigidbody2D rb;
    private Animator anim;
	private SpriteRenderer sp;
	private float rateOfHit;
	private GameObject[] life;
	private int qtdLife = 3;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		sp=GetComponent<SpriteRenderer>();
		running=false;
		up=false;
		down=false;
		jumping=false;
		rateOfHit=Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(dead==false){

			if(attacking==false){												
				if(crouching==false){
					Movement();
					//Attack();
					Special();
				}
				Jump();
				//Crouch();
			}
			Dead();
		}

	}

	public void lifeLost(){
		qtdLife = qtdLife - 1;
		Destroy (GameObject.FindGameObjectWithTag ("Life"));
	}

	void Movement(){
		//Character Move
		float move = Input.GetAxisRaw("Horizontal");
		if(Input.GetKey(KeyCode.Z)){
			//Run
			rb.velocity = new Vector2(move*speed*Time.deltaTime*3,rb.velocity.y);
			running=true;
		}else{
			//Walk
			rb.velocity = new Vector2(move*speed*Time.deltaTime,rb.velocity.y);
			running=false;
		}

		//Turn
		if(rb.velocity.x<0){
			sp.flipX=true;
		}else if(rb.velocity.x>0){
			sp.flipX=false;
		}
		//Movement Animation
		if(rb.velocity.x!=0&&running==false){
			anim.SetBool("Walking",true);
		}else{
			anim.SetBool("Walking",false);
		}
		if(rb.velocity.x!=0&&running==true){
			anim.SetBool("Running",true);
		}else{
			anim.SetBool("Running",false);
		}
	}

	void Jump(){
		//Jump
		if(Input.GetKeyDown(KeyCode.UpArrow)&&rb.velocity.y==0){
			rb.AddForce(new Vector2(0,jumpForce));
		}
		//Jump Animation
		if(rb.velocity.y>0&&up==false){
			up=true;
			jumping=true;
			anim.SetTrigger("Up");
		}else if(rb.velocity.y<0&&down==false){
			down=true;
			jumping=true;
			anim.SetTrigger("Down");
		}else if(rb.velocity.y==0&&(up==true||down==true)){
			up=false;
			down=false;
			jumping=false;
			anim.SetTrigger("Ground");
		}
	}

	/*void Attack(){																//I activated the attack animation and when the 
		//Atacking																//animation finish the event calls the AttackEnd()
		if(Input.GetKeyDown(KeyCode.X)){
			rb.velocity=new Vector2(0,0);
			anim.SetTrigger("Attack");
			attacking=true;
		}
	}*/

	void AttackEnd(){
		attacking=false;
	}

	void Special(){
		if(Input.GetKey(KeyCode.X)){
			anim.SetBool("Special",true);
		}else{
			anim.SetBool("Special",false);
		}
	}

	void OnTriggerEnter2D(Collider2D other){							//Case of Bullet
		if(other.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
	}								

	void OnCollisionEnter2D(Collision2D other) {						//Case of Touch
		if(other.gameObject.tag=="Enemy"){
			anim.SetTrigger("Damage");
			Hurt();
		}
	}

	void Hurt(){
		if(rateOfHit<Time.time){
			rateOfHit=Time.time+cooldownHit;
		}
	}

	void Dead(){
		if(qtdLife<=0){
			anim.SetTrigger("Dead");
			dead=true;
			Debug.Log("Player Dead! Show Game Over UI");
		}
	}
}
