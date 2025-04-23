using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public string characterName;
    public int maxHealth = 100;
    public Transform spawnPoint;
    public Image health;
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpHeight = 5;
    public bool onGround = true;
    private bool isDead = false;

    public Image ability;
    public float abilityCooldown = 10f;
    private float currentCooldown = 0f;
    private bool abilityReady = true;

    private bool isMovable = true;
    private bool isInvincible = false;


    private float movement;
    public float moveSpeed = 5f;
    private bool facingRight = true;

    public Transform punchPoint;
    public float punchRadius = 1f;
    public LayerMask punchLayer;

    public Transform kickPoint;
    public float kickRadius = 0.2f;
    public LayerMask kickLayer;

    public Transform gunPoint;
    public float gunRadius = 0.2f;
    public LayerMask gunLayer;

    public GameObject opponent;
    private string opponentName;

    public int punchDamage = 3;

    private float powerUpTimer = 0f;
    private bool isPoweredUp = false;

    private float damageTakenWindow = 0f;
    private int damageAccumulated = 0;
    public int damageThreshold = 30;
    public float timeThreshold = 5f;
    private bool damageMonitoring = false;

    public int roundsWon = 0;
    public Image[] roundIcons;
    public Sprite roundWonSprite;
    public Sprite roundEmptySprite;
    public Image characterPortrait;

    public AudioSource src;
    public AudioClip romanianSound1, romanianSound2, romanianSound3, romanianSound4, kick, punch, missedKick, missedPunch;
    public AudioClip[] voiceLines;
    public Sprite romanianFace, irishFace, americanFace, frenchFace;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (opponent != null)
        {
            CharacterInfo info = opponent.GetComponent<CharacterInfo>();
            if(info != null)
            {
                opponentName = info.characterName; // figure out what opponent he is fighting << currently manual (hopefully can automate)
            }
        }
        if (opponent != null) //opponent layer different to player layer
        {
            int opponentLayer = opponent.layer;
            punchLayer = 1 << opponentLayer;
            kickLayer = 1 << opponentLayer;
            gunLayer = 1 << opponentLayer;
        }
        switch (characterName.ToLower())
        {
            case "romanian": characterPortrait.sprite = romanianFace; break;
            case "irish": characterPortrait.sprite = irishFace; break;
            case "american": characterPortrait.sprite = americanFace; break;
            case "french": characterPortrait.sprite = frenchFace; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (maxHealth <= 0) // if health is <0 player is dead
        {
            Die();
        }

        if (damageMonitoring) //if tracking damage
        {
            damageTakenWindow += Time.deltaTime; //track how long since tracking began

            if(damageTakenWindow >= timeThreshold)
            {
                damageMonitoring = false; //stop tracking  
                damageTakenWindow = 0f; //reset timer
                damageAccumulated = 0; //reset damage
            }
        }
        if (!abilityReady) // if ability not ready
        {
            currentCooldown += Time.deltaTime; //track the cooldown
            ability.fillAmount = currentCooldown / abilityCooldown; //fill the ability bar accordingly  
            
            if(currentCooldown >= abilityCooldown) // if no more cooldown exists
            {
                abilityReady = true; //u can use ability
                currentCooldown = 0f; //cooldown reset
            }
        }
        if (health != null)
            health.fillAmount = (float)maxHealth / 100; //fill health bar accordingly

            movement = Input.GetAxis("Horizontal"); // move horizontally

        if (movement < 0f && facingRight) // detects if player is moving left
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f); // flips the character horizontally to face left
            facingRight = false;
        }
        else if (movement > 0f && facingRight == false) // tests if player is moving right
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f); //flips player horizontally to face right
            facingRight = true;
        }

        if (Input.GetKey(KeyCode.Space) && onGround==true) // detects if space is inputed and if the player is on the ground
        {
            Jump(); // calls jump 
            onGround = false; // states player is no longer on the ground
            animator.SetBool("Jump", true); //plays the jump animation when you jump

        }

        if (Mathf.Abs(movement) > 0f)
        {
            animator.SetFloat("Walk", 1f); //if moving then play animation of walking
        } else if(movement < 0.1f)
        {
            animator.SetFloat("Walk", 0f); //if not moving play animation of idle
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Punch"); // when you press "e" you punch
            LockMovementFor(0.2f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Kick"); // kick when you press "f"
            LockMovementFor(0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Z) && abilityReady && characterName == "Romanian") // romanian ability
        {
            useAbilityAgainst(opponentName); //calls special method depending on enemy

            abilityReady = false; // reset ability
            currentCooldown = 0f; // reset cooldown
            ability.fillAmount = 0f; //reset ability bar
        }
        
        if(Input.GetKeyDown(KeyCode.Z) && abilityReady && characterName == "Irish") // irish ability
        {
            irishPowerUp();
            animator.SetTrigger("PowerUp");
            LockMovementFor(0.5f);

            abilityReady = false;
            currentCooldown = 0f;
            ability.fillAmount = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Z) && abilityReady && characterName == "American") // american ability
        {
            AmericanPowerUp();
            animator.SetTrigger("PowerUp");
            LockMovementFor(1f);

            abilityReady = false;
            currentCooldown = 0f;
            ability.fillAmount = 0f;

        }

        if (Input.GetKeyDown(KeyCode.Z) && abilityReady && characterName == "French") //french ability
        {
            FrenchPowerUp();
            animator.SetBool("PowerUp", true);

            abilityReady = false;
            currentCooldown = 0f;
            ability.fillAmount = 0f;
        }
        if (isPoweredUp) // if character is powered up (english,irish or romanian)
        {
            powerUpTimer -= Time.deltaTime; //count how long it's been powered up for

            if (powerUpTimer <= 0f) //if powerup timer is 0
            {
                isPoweredUp = false; //not powered up anymore
                punchDamage = 3; //damage reset to normal
                animator.SetBool("PowerUp", false); //normal animations
            }
        }
    }

    void useAbilityAgainst(string opponentName) //romanian ability continued 
    {
        switch (opponentName) //depending on which character do the following
        {
            case "Irish": // irish
                animator.SetTrigger("IrishPowerUp");
                irishPowerUp();
                LockMovementFor(0.5f);
                src.clip = romanianSound1; 
                src.Play();
                break;

            case "American": //american
                animator.SetTrigger("AmericanPowerUp");
                AmericanPowerUp();
                LockMovementFor(1f);
                src.clip = romanianSound2;
                src.Play();
                break;

            case "French": //french
                animator.SetBool("FrenchPowerUp", true);
                FrenchPowerUp();
                src.clip = romanianSound3;
                src.Play();
                break;

            case "English": //english
                animator.SetBool("EnglishPowerUp", true);
                EnglishPowerUp();
                src.clip = romanianSound3;
                src.Play();
                break;
        }
    }
    public void Punch() //punch physics
    {
        Collider2D collInfo = Physics2D.OverlapCircle(punchPoint.position, punchRadius, punchLayer); //create a collider at the fist

        if (collInfo)
        {
            if (collInfo.gameObject.GetComponent<Movement>() != null)
            {
                if (characterName != "American")
                {
                    collInfo.gameObject.GetComponent<Movement>().TakeDamge(punchDamage); // punch deals 3 damage
                }
                src.clip = punch; //play sound when punch lands
                src.Play();
            }
            else
            {
                src.clip = missedPunch; //play sound when punch misses <<< doesn't work?
                src.Play();
            }
        }
    }

    private void OnDrawGizmosSelected() // hitboxes
    {
        if(punchPoint == null) // if there's no point for the punch return
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchPoint.position, punchRadius); //red cirlce around the fist
    }

    public void Kick()// kick physics similar to punch except 5 damage
    {
        Collider2D collInfo = Physics2D.OverlapCircle(kickPoint.position, kickRadius, kickLayer);
        if (collInfo)
        {
            if(collInfo.gameObject.GetComponent<Movement>() != null){
                collInfo.gameObject.GetComponent<Movement>().TakeDamge(5);
                src.clip = kick;
                src.Play();
            }
        }
        else
        {
            src.clip = missedKick; // this one works, punch doesnt?
            src.Play();
        }
    }
    
    public void EnglishPowerUp() // english powerup code
    {
        isPoweredUp = true; //poweredup
        powerUpTimer = 6f; //poweredup for 6 seconds
        punchDamage = 6; //punch damage doubled
        PlayRandomVoiceLine(); //call random voice line
    }
    public void FrenchPowerUp() //same as english
    {
        isPoweredUp = true;
        powerUpTimer = 6f;
        punchDamage = 6;

        PlayRandomVoiceLine();
    }
    public void AmericanPowerUp() // american gun physics <<<< works not as intended
    {
        Collider2D collInfo = Physics2D.OverlapCircle(gunPoint.position, gunRadius, gunLayer);
        
        if(collInfo)
        {
            if(collInfo.gameObject.GetComponent<Movement>() != null)
            {
                collInfo.gameObject.GetComponent<Movement>().TakeDamge(1); //has to do 12 damage because each bullet only counts once per frame (fixed)
            }
        }
        PlayRandomVoiceLine();
    }
   
    public void irishPowerUp() //irish powerup
    {
        TakeDamge(-25); //heal 25 health
        if (maxHealth > 100)
        {
            maxHealth = 100; //if over 100 health, set health to 100
        }
        PlayRandomVoiceLine();
    }

    private void FixedUpdate()
    {
        if (isMovable)
        {
            transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed; // player movement 
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse); //player jump 
    }
    private void OnCollisionEnter2D(Collision2D collision) // test if player is on ground
    {
        if (collision.gameObject.tag == "Ground") //if player collides with "Ground" tagged object
        {
            onGround = true; // states player is on ground
            animator.SetBool("Jump", false);
        }
    }

    public void TakeDamge(int damage) // health
    {
        if((damage > 0 && (maxHealth <= 0||isInvincible)))
        {
            return;
        }
        maxHealth -= damage; //take that much damage
        if (damage > 0)
        {
            //animator.SetTrigger("IsHit"); // doesn't work <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


            damageAccumulated += damage;

            if (!damageMonitoring) //start tracking how much damage it took
            {
                damageMonitoring = true;
                damageTakenWindow = 0f;
            }

            CheckDamageThreshold();
        }
    }

    void CheckDamageThreshold()
    {
        if (damageAccumulated >= damageThreshold) // if you took too much damage too quickly
        {
            animator.SetTrigger("isDead"); //fall down
            LockMovementFor(1f); //cant move
            damageMonitoring = false; //stop tracking damage taken
            damageTakenWindow = 0f;
            damageAccumulated = 0;
            StartCoroutine(TemporaryInvinciblity(1f)); //gain invincibility for 1 second while u get up
        }
    }

    void LockMovementFor(float duration) 
    {
        StartCoroutine(LockMovementCoroutine(duration));
    }

    IEnumerator LockMovementCoroutine(float duration) //lock movement code for using abilities falling etc.
    {
        isMovable = false;
        yield return new WaitForSeconds(duration);
        isMovable = true;
    }

    IEnumerator TemporaryInvinciblity(float duration) //temporary invinciblity 
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        isInvincible = false;
    }

    void PlayRandomVoiceLine() // play random voice lines
    {
        if (voiceLines.Length == 0 || src == null) return;

        int index = Random.Range(0, voiceLines.Length);
        src.clip = voiceLines[index];
        src.Play();
    }

    public void UpdateRoundIcons() // update round icons <<<<<<<< buggy
    {
        for (int i = 0; i < roundIcons.Length; i++)
        {
            if (i < roundsWon)
            {
                roundIcons[i].sprite = roundWonSprite;
            }
            else
            {
                roundIcons[i].sprite = roundEmptySprite;
            }
        }
    }

    public IEnumerator StartNextRound() // new round start
    {
        yield return new WaitForSeconds(2f);

        maxHealth = 100;
        health.fillAmount = 1;

        if(spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        isDead = false;
        isInvincible = false;
        isMovable = true;
    }
    void Die() //player dies code
    {
        if (isDead) return;
        
        isDead = true;

        Debug.Log(characterName + " Died."); //write in console a message when dead

        if (opponent != null)
        {
            Movement opponentScript = opponent.GetComponent<Movement>();

            if(opponentScript != null)
            {
                opponentScript.roundsWon++; //count rounds won

                opponentScript.UpdateRoundIcons(); //update round won icons

                if(opponentScript.roundsWon >= 2) //if 2 rounds win
                {
                    Debug.Log(opponentScript.characterName + " wins the matchs!"); //winner
                }
                else
                {
                    StartCoroutine(opponentScript.StartNextRound()); 
                    StartCoroutine(StartNextRound());
                }
            }
        }
    }
}
