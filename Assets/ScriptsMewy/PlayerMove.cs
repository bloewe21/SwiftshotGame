using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Shooting sh;
    private ShootingMagnet shm;
    private Animator anim;
    private SpriteRenderer sr;
    private PlayerMelee pmelee;
    private enum MovementState {idle, walk, jump, fall, roll, wallslide, turn, meleeidle, crouch, dance, meleewalk, meleejump};
    private enum BowState {yes, no};

    [SerializeField] public float moveSpeed = 9.5f;
    [SerializeField] public float jumpSpeed = 2.0f;
    [SerializeField] public float doubleJumpSpeed = 1.0f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck2;
    public float dirX;
    private float distToGround;

    private float fHorizontalVelocity;
    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingBasic = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenStopping = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float fHorizontalDampingWhenTurning = 0.5f;

    public bool facingRight;
    private bool canFlip = true;

    public float chargeShot = 0.0f;
    public Transform firepoint;

    private bool canRoll = true;
    private bool isRolling = false;
    private bool isRollingForce = false;
    private bool hasJumped = false;
    public bool hasDoubled = false;

    public bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    //private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.05f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 14f);

    private Vector3 screenMousePos;
    private Vector2 mousePos;
    private Vector2 mouseDirection;
    private float prevMouseDirectionX;

    private string lastCollision;

    public int currWeapon = 0;
    public bool canSwitch = true;
    public RawImage highlight1;
    public RawImage highlight2;
    public RawImage cursor1;
    public RawImage cursor2;

    public GameObject ammoText;

    public ParticleSystem dust;

    public bool canMove = true;



    [SerializeField] private AudioSource ammo_sound;
    [SerializeField] private AudioSource jump_sound;
    [SerializeField] private AudioSource djump_sound;
    [SerializeField] private AudioSource slide_sound;
    [SerializeField] private AudioSource bowdraw_sound;
    [SerializeField] private AudioSource turn_sound;
    [SerializeField] private AudioSource walk_sound;
    [SerializeField] private AudioSource walljump_sound;
    //[SerializeField] private AudioSource melee_sound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sh = GetComponent<Shooting>();
        shm = GetComponent<ShootingMagnet>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pmelee = GetComponent<PlayerMelee>();

        //dust = GetComponent<ParticleSystem>();

        distToGround = coll.bounds.extents.y;
        facingRight = true;

        highlight1.enabled = true;
        highlight2.enabled = false;
        cursor1.enabled = true;
        cursor2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //print(rb.velocity.x);

        if (!canMove) {
            Fire();
            SwitchWeapon();
            Animate();
            return;
        }
        
        Flip();
        Jump();
        DoubleJump();
        Fire();
        FireMagnet();
        Roll();
        WallJump();
        WallSlide();
        SwitchWeapon();

        Animate();
        


        fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.deltaTime * 10f);
            //fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.deltaTime * 10f);

        //rb.velocity = new Vector2(fHorizontalVelocity * moveSpeed, rb.velocity.y);

        dirX = Input.GetAxisRaw("Horizontal");

        screenMousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        mouseDirection = mousePos - (Vector2)rb.position;

        if (IsGrounded()) {
            hasJumped = false;
            hasDoubled = false;
        }
        else {
            hasJumped = true;
        }
    }

    void FixedUpdate() {
        if (canMove) {

            rb.velocity = new Vector2(fHorizontalVelocity * moveSpeed, rb.velocity.y);

            if (isRollingForce) {
                float rollForce = 25.0f - Mathf.Abs(rb.velocity.x);
                //float rollForce = 25.0f;
                if (!facingRight) {
                    rollForce = rollForce * -1;
                }
                //rb.velocity = new Vector2(rollForce, rb.velocity.y);
                rb.AddForce(new Vector2(rollForce, 0), ForceMode2D.Impulse);
            }
        }

        //FOR FIRE()
        if (Input.GetMouseButton(0)) {
            canFlip = false;
            //increase speed of incoming shot
            if (chargeShot < 6.0f) {
                chargeShot += 5.0f * Time.deltaTime;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        lastCollision = collision.gameObject.tag;
    }

    private void Flip()
    {
        if (canFlip && (facingRight && dirX < 0f || !facingRight && dirX > 0f))
        {
            FlipLogic();
        }

        if (Input.GetMouseButton(0) && currWeapon == 0)
        {
            if ((facingRight && mouseDirection.x < 0) || (!facingRight && mouseDirection.x > 0))
            {
                // if (canFlip) {
                //     FlipLogic();
                // }
                FlipLogic();
            }
        }

        prevMouseDirectionX = mouseDirection.x;
    }

    private void FlipLogic() {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

        if (IsGrounded()) {
            CreateDust();
        }
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rb.velocity = new Vector2(fHorizontalVelocity * moveSpeed, jumpSpeed);
            hasJumped = true;
            CreateDust();

            jump_sound.Play();
        }
    }

    private void DoubleJump() {
        if (hasJumped && Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && !hasDoubled && !IsWalled()) {
            if (Mathf.Sign(dirX) != Mathf.Sign(rb.velocity.x)) {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            rb.velocity = new Vector2(fHorizontalVelocity * moveSpeed, doubleJumpSpeed);
            hasDoubled = true;
            CreateDust();

            djump_sound.Play();
        }
    }

    private void Fire() {
        if (Input.GetMouseButtonDown(0) && currWeapon == 0) {
            chargeShot = 2.0f;

            bowdraw_sound.Play();
        }
        // if (Input.GetMouseButton(0)) {
        //     if (chargeShot < 6.0f) {
        //         chargeShot += 5.0f * Time.deltaTime;
        //     }
        // }
        if (Input.GetMouseButtonUp(0)) {
            sh.fire(chargeShot);
            chargeShot = 0.0f;
            canFlip = true;

            bowdraw_sound.Stop();
        }
    }

    //Can only fire if no other magnets are found
    private void FireMagnet() {
        if (Input.GetMouseButtonDown(1) && GameObject.FindGameObjectsWithTag("Magnet").Length == 0) {
            shm.fire();
        }
    }

    private void Roll() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll) {
        //if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (IsGrounded()) {
                //ROLL FORCE ADDED IN FIXEDUPDATE
                // float rollForce = 25.0f - Mathf.Abs(rb.velocity.x);
                // //float rollForce = 25.0f;
                // if (!facingRight) {
                //     rollForce = rollForce * -1;
                // }
                // //rb.velocity = new Vector2(rollForce, rb.velocity.y);
                // rb.AddForce(new Vector2(rollForce, 0), ForceMode2D.Impulse);
                CreateDust();
                slide_sound.Play();
                StartCoroutine(rollTimer());
            }
        }
    }

    private IEnumerator rollTimer()
    {
        canRoll = false;
        isRolling = true;
        isRollingForce = true;
        yield return new WaitForSeconds(.05f);
        isRollingForce = false;
        yield return new WaitForSeconds(.20f);
        isRolling = false;
        yield return new WaitForSeconds(.25f);
        canRoll = true;
    }

    private bool IsWalled()
    {
        bool playerWalled = false;
        if (lastCollision != "Ground") {
            return playerWalled;
        }
        playerWalled = Physics2D.OverlapCircle(wallCheck.position, 0.1f, jumpableGround);
        if (!playerWalled) {
            playerWalled = Physics2D.OverlapCircle(wallCheck2.position, 0.01f, jumpableGround);
        }
        return playerWalled;
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            //canFlip = false;

            //isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingDirection = -dirX;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            //canFlip = true;

            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            //isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            CreateDust();

            walljump_sound.Play();

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingRight = !facingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        //isWallJumping = false;
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && dirX != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }


    private void SwitchWeapon() {
        if (ammoText.GetComponent<AmmoScript>().ammoCount <= 0) {
            return;
        }
        if (currWeapon == 0 && Input.GetMouseButton(0)) {
            canSwitch = false;
        }
        else {
            canSwitch = true;
        }
        if (Input.GetKeyDown(KeyCode.Q) && canSwitch) {
            if (currWeapon == 0) {
                currWeapon = 1;
                highlight1.enabled = false;
                highlight2.enabled = true;
                cursor1.enabled = false;
                cursor2.enabled = true;
            }
            else if (currWeapon == 1) {
                currWeapon = 0;
                highlight1.enabled = true;
                highlight2.enabled = false;
                cursor1.enabled = true;
                cursor2.enabled = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Goop") {
            moveSpeed = moveSpeed / 1.5f;
            jumpSpeed = jumpSpeed / 1.6f;
            wallJumpingPower = new Vector2(6f, 10f);
            wallSlidingSpeed = .5f;
        }

        if (collision.gameObject.tag == "Ammo") {
            ammoText.GetComponent<AmmoScript>().ammoCount = ammoText.GetComponent<AmmoScript>().refreshAmmoCount;
            ammoText.GetComponent<AmmoScript>().refresh = true;
            ammo_sound.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Goop") {
            moveSpeed = moveSpeed * 1.5f;
            jumpSpeed = jumpSpeed * 1.6f;
            wallJumpingPower = new Vector2(8f, 14f);
            wallSlidingSpeed = 2f;
        }
    }



    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void CreateDust() {
        dust.Play();
    }







    private void Animate() {
        //idle, walk, jump, fall, roll, wallslide, turn, meleeidle, crouch, dance
        //BowState: yes, no
        MovementState state;
        BowState bstate;

        state = MovementState.idle;

        //Dancing
        if (state == MovementState.idle && Input.GetKeyDown(KeyCode.Alpha1)) {
            state = MovementState.dance;
        }


        //Walking
        if (dirX != 0 && IsGrounded()) {
            state = MovementState.walk;

            if (!walk_sound.isPlaying) {
                walk_sound.Play();
            }
        }
        else {
            walk_sound.Stop();
        }
        //Rolling
        if (isRolling == true) {
            state = MovementState.roll;
        }

        //Default Falling
        if (!IsGrounded()) {
            state = MovementState.fall;
        }

        //Jumping
        if (rb.velocity.y > 0 && !IsGrounded()) {
            state = MovementState.jump;
        }
        //Falling
        if (rb.velocity.y < 0 && !IsGrounded()) {
            state = MovementState.fall;
        }

        //WallSliding
        if (isWallSliding == true) {
            state = MovementState.wallslide;
        }

        //Turning
        if (Mathf.Sign(dirX) != Mathf.Sign(rb.velocity.x) && dirX != 0 && IsGrounded() && Mathf.Abs(rb.velocity.x) > 0.1f) {
            state = MovementState.turn;
            // if (!turn_sound.isPlaying) {
            //     turn_sound.Play();
            // }
        }

        //Melee?
        //if (Input.GetMouseButtonDown(0) && pmelee.canMelee && currWeapon == 1 && !isWallSliding) {
        if (Input.GetMouseButtonDown(0) && currWeapon == 1 && !isWallSliding && state == MovementState.idle) {
            state = MovementState.meleeidle;
        }
        if (Input.GetMouseButtonDown(0) && currWeapon == 1 && !isWallSliding && state == MovementState.walk) {
            state = MovementState.meleewalk;
        }
        if (Input.GetMouseButtonDown(0) && currWeapon == 1 && !isWallSliding && (state == MovementState.jump || state == MovementState.fall)) {
            state = MovementState.meleejump;
        }

        //Crouching
        if (state == MovementState.idle && Input.GetKey(KeyCode.S)) {
            state = MovementState.crouch;
        }


        //Hitbox change
        if (state == MovementState.crouch || state == MovementState.roll) {
            coll.size = new Vector2(coll.size.x, .6f);
            coll.offset = new Vector2(coll.offset.x, -.18f);
        }
        else {
            coll.size = new Vector2(coll.size.x, .86f);
            coll.offset = new Vector2(coll.offset.x, -.05f);
        }



        //Holding bow
        if (Input.GetMouseButton(0) && state == MovementState.idle) {
            bstate = BowState.yes;
            sr.flipX = false;
        }
        else if (Input.GetMouseButton(0) && state == MovementState.walk) {
            bstate = BowState.yes;
            sr.flipX = false;
        }
        else if (Input.GetMouseButton(0) && state == MovementState.roll) {
            bstate = BowState.yes;
            sr.flipX = false;
        }
        else if (Input.GetMouseButton(0) && state == MovementState.jump) {
            bstate = BowState.yes;
            sr.flipX = false;
        }
        else if (Input.GetMouseButton(0) && state == MovementState.fall) {
            bstate = BowState.yes;
            sr.flipX = false;
        }
        else if (Input.GetMouseButton(0) && state == MovementState.wallslide) {
            bstate = BowState.yes;
            sr.flipX = true;
        }
        else if (Input.GetMouseButton(0) && state == MovementState.crouch) {
            bstate = BowState.yes;
            sr.flipX = false;
        }
        else {
            bstate = BowState.no;
            sr.flipX = false;
        }

        if (currWeapon != 0) {
            bstate = BowState.no;
            sr.flipX = false;
        }

        anim.SetInteger("state", (int)state);
        anim.SetInteger("bstate", (int)bstate);
    }
}
