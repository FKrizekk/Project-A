using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    public Rigidbody rb;
    public static float MSensitivity = 1f;
    private float MoveSpeed = 15f;
    private float CrouchSpeed = 4f;
    public float JumpForce = 200f;
    public bool canJump = false;
    public float maxVelocityChange = 10f;
    public float fov = 90;
    public bool sliding = false;
    public GameObject player;
    public Animator _animator;
    private bool airborne = false;
    public GameObject canvas;
    public static bool menuOpened = false;
    public GameObject FOVSlider;
    public GameObject MenuPanel;
    public GameObject SettingsPanel;
    public Slider fovslider;
    public GameObject FOVText;
    public Slider sensslider;
    public GameObject SENSText;
    public GameObject SENSSlider;
	public GameObject Camera;
    public GameObject MainCamera;
    public GameObject Reticle;
    public static int cWeapon = 420;
    public GameObject Katana;
    public GameObject HandCannon;
    public GameObject Shotgun;
    public GameObject SniperRifle;
    public GameObject Revolver;
    //string[] weapons = {"Katana","HandCannon","Shotgun","SniperRifle","Revolver","AssaultRifle"};
    public GameObject SniperAmmoCount;
    public GameObject weapon;
    public int UILayer;
    public GameObject tempWeapon;
    private int counter = 100;
    public GameObject textik;
    private float rechargeTime = 20f;
    public GameObject Textsp;
    int selectedWeapon;
    public GameObject WeaponParent;
    public static bool IsDamaging = false;//THIS IS FOR THE KATANA DAMAGE
    public static bool Loaded = false;
    // Start is called before the first frame update

    //BUTTONS
    public void QuitButton(){
        Application.Quit();
    }
    public void SettingsButton(){
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        FOVSlider = GameObject.Find("fovSlider");
    }
    public void ResumeButton(){
        CloseMenu();
    }

    void Shoot(string weapon){
        if(weapon == "Revolver" && Loaded){
            RaycastHit hit;
            Ray ray = MainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3((MainCamera.GetComponent<Camera>().pixelWidth-1)/2,(MainCamera.GetComponent<Camera>().pixelHeight-1)/2,0));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out hit, 400f))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<EnemyScript>().GotHit(50);
                }
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        FOVSlider = GameObject.Find("fovSlider");
        MenuPanel = GameObject.Find("MenuPanel");
        SettingsPanel = GameObject.Find("SettingsPanel");
        fovslider = FOVSlider.GetComponent<Slider>();
        FOVText = GameObject.Find("FOVText");
        SENSSlider = GameObject.Find("sensSlider");
        sensslider = SENSSlider.GetComponent<Slider>();
        SENSText = GameObject.Find("SENSText");
        Camera = GameObject.Find("VISUALCamera");
        MainCamera = GameObject.Find("Main Camera");
        Reticle = GameObject.Find("Reticle");
        Katana = GameObject.Find("Katana");
        SniperRifle = GameObject.Find("SniperRifle");
        Shotgun = GameObject.Find("Shotgun");
        Revolver = GameObject.Find("Revolver");
        SniperAmmoCount = GameObject.Find("SniperAmmoCount");
        UILayer = LayerMask.NameToLayer("UI");
        _animator = GameObject.Find("Katana").GetComponent<Animator>();
        textik = GameObject.Find("SniperAmmoCount");
        Textsp = GameObject.Find("Textsp");
        canvas = GameObject.Find("Canvas");
        WeaponParent = GameObject.Find("WeaponParent");
        canvas.SetActive(false);
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        SniperAmmoCount.SetActive(false);
        fov = PlayerPrefs.GetFloat("FOV",90f);
        fovslider.value = PlayerPrefs.GetFloat("FOV",90f);
        FOVText.GetComponent<Text>().text = "FOV: " + PlayerPrefs.GetFloat("FOV",90f);
        MSensitivity = PlayerPrefs.GetFloat("SENS",1f);
        sensslider.value = PlayerPrefs.GetFloat("SENS",1f) * 25;
        SENSText.GetComponent<Text>().text = "SENS: " + PlayerPrefs.GetFloat("SENS",1f);
        MainCamera.GetComponent<Camera>().fieldOfView = fov;
        Camera.GetComponent<Camera>().fieldOfView = fov;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Ground"){
            airborne = true;
        }
    }

    void OpenMenu(){
        menuOpened = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        canvas.SetActive(true);
        Reticle.SetActive(false);
        
    }
    void CloseMenu(){
        menuOpened = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);
        PlayerPrefs.SetFloat("SENS", MSensitivity);
        PlayerPrefs.SetFloat("FOV", fovslider.value);
        Reticle.SetActive(true);
    }

    public void ChangeFOV(){
        fovslider = FOVSlider.GetComponent<Slider>();
        MainCamera.GetComponent<Camera>().fieldOfView = fovslider.value;
        Camera.GetComponent<Camera>().fieldOfView = fovslider.value;
        FOVText.GetComponent<Text>().text = "FOV: " + fovslider.value.ToString();
    }

    public void ChangeSENS(){
        sensslider = SENSSlider.GetComponent<Slider>();
        MSensitivity = sensslider.value/25;
        SENSText.GetComponent<Text>().text = "SENS: " + (sensslider.value/25).ToString();
    }

    public void SwitchToKatana(){
        if(!sliding){
            SniperAmmoCount.SetActive(false);
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 0;
            
            weapon = Instantiate(Katana);
            _animator = GameObject.Find("Katana(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.569999993f,-100.870000002f,0.360000014f);
            weapon.transform.localRotation = new Quaternion(-0.441916347f,0.641260266f,-0.348535508f,0.521553695f);
            weapon.layer = UILayer;
        }else{
            SniperAmmoCount.SetActive(false);
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 0;
            
            weapon = Instantiate(Katana);
            _animator = GameObject.Find("Katana(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.569999993f,-100.870000002f,0.360000014f);
            weapon.transform.localRotation = new Quaternion(-0.441916347f,0.641260266f,-0.348535508f,0.521553695f);
            weapon.transform.localScale = weapon.transform.localScale/2;
            weapon.layer = UILayer;
        }
        
        
    }
    public void SwitchToRevolver(){
        if(!sliding){
            SniperAmmoCount.SetActive(false);
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 1;
            
            weapon = Instantiate(Revolver);
            _animator = GameObject.Find("Revolver(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.200000003f,-0.850000024f,0.43900001f);
            weapon.transform.localRotation = new Quaternion(1,0,0,0);
            weapon.layer = UILayer;
        }else{
            SniperAmmoCount.SetActive(false);
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 1;
            
            weapon = Instantiate(Revolver);
            _animator = GameObject.Find("Revolver(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.200000003f,-0.850000024f,0.43900001f);
            weapon.transform.localRotation = new Quaternion(1,0,0,0);
            weapon.transform.localScale = weapon.transform.localScale/2;
            weapon.layer = UILayer;
        }
    }
    public void SwitchToSniperRifle(){
        if(!sliding){
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 2;
            SniperAmmoCount.SetActive(true);
            
            weapon = Instantiate(SniperRifle);
            _animator = GameObject.Find("SniperRifle(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.26f,-1.41f,0.61f);
            weapon.transform.localRotation = Quaternion.identity;
            weapon.layer = UILayer;
        }else{
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 2;
            SniperAmmoCount.SetActive(true);
            
            weapon = Instantiate(SniperRifle);
            _animator = GameObject.Find("SniperRifle(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.26f,-1.41f,0.61f);
            weapon.transform.localRotation = Quaternion.identity;
            weapon.transform.localScale = weapon.transform.localScale/2;
            weapon.layer = UILayer;
        }
    }
    public void SwitchToShotgun(){
        if(!sliding){
            SniperAmmoCount.SetActive(false);
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 3;
            
            weapon = Instantiate(Shotgun);
            _animator = GameObject.Find("Shotgun(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.330000013f,-1.29999995f,0.699999988f);
            weapon.transform.localRotation = new Quaternion(0,1,0,0);
            weapon.layer = UILayer;
        }else{
            SniperAmmoCount.SetActive(false);
            if(weapon != null){
                Destroy(weapon);
            }
            cWeapon = 3;
            
            weapon = Instantiate(Shotgun);
            _animator = GameObject.Find("Shotgun(Clone)").GetComponent<Animator>();
            weapon.transform.parent = WeaponParent.transform;
            
            weapon.transform.localPosition = new Vector3(0.569999993f,-0.400000006f,0.360000014f);
            weapon.transform.localRotation = new Quaternion(0,1,0,0);
            weapon.transform.localScale = weapon.transform.localScale/2;
            weapon.layer = UILayer;
        }
    }


    IEnumerator Recharge(){
        while(counter < 100){
            yield return new WaitForSeconds(rechargeTime/100f);
            counter++;
        }
    }
    

    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!menuOpened){
            if(!sliding){
                //Walking
                if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.05 || Mathf.Abs(Input.GetAxis("Vertical")) > 0.05){
                    Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, Input.GetAxis("Vertical"));
                    targetVelocity = transform.TransformDirection(targetVelocity) * MoveSpeed;
                    Vector3 velocity = rb.velocity;
                    Vector3 velocityChange = (targetVelocity - velocity);
                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    velocityChange.y = 0;
                    rb.AddForce(velocityChange,ForceMode.VelocityChange);
                }else{
                    Vector3 targetVelocity = new Vector3(0,rb.velocity.y,0);
                    targetVelocity = transform.TransformDirection(targetVelocity);
                    Vector3 velocity = rb.velocity;
                    Vector3 velocityChange = (targetVelocity - velocity);
                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    velocityChange.y = 0;
                    rb.AddForce(velocityChange,ForceMode.VelocityChange);
                }
            }else{
                //CrouchWalking
                if(transform.InverseTransformDirection(rb.velocity).x + transform.InverseTransformDirection(rb.velocity).z < 5){
                    if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.05 || Mathf.Abs(Input.GetAxis("Vertical")) > 0.05){
                        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, Input.GetAxis("Vertical"));
                        targetVelocity = transform.TransformDirection(targetVelocity) * CrouchSpeed;
                        Vector3 velocity = rb.velocity;
                        Vector3 velocityChange = (targetVelocity - velocity);
                        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                        velocityChange.y = 0;
                        rb.AddForce(velocityChange,ForceMode.VelocityChange);
                    }
                }
            }
        }
    } 

    private void Update(){
        //Inventory
        if (Input.GetKeyDown("1")){
            selectedWeapon = 0;
        }else if(Input.GetKeyDown("2")){
            selectedWeapon = 1;
        }else if(Input.GetKeyDown("3")){
            selectedWeapon = 2;
        }else if(Input.GetKeyDown("4")){
            selectedWeapon = 3;
        }

        if(cWeapon == 2){
            if(counter > 100){
                counter = 100;
            }
            textik.GetComponent<Text>().text = counter.ToString();
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                if(counter == 100){
                    counter = 0;
                    StartCoroutine(Recharge());
                }else{

                }
            }
        }
        if(weapon != null){
            switch (selectedWeapon) {
                case 0:
                    if(weapon.name != "Katana(Clone)"){
                        SwitchToKatana();
                    }else if(weapon == null){
                        SwitchToKatana();
                    }
                    break;
                case 1:
                    if(weapon.name != "Revolver(Clone)"){
                        SwitchToRevolver();
                    }else if(weapon == null){
                        SwitchToRevolver();
                    }
                    break;
                case 2:
                    if(weapon.name != "SniperRifle(Clone)"){
                        SwitchToSniperRifle();
                    }else if(weapon == null){
                        SwitchToSniperRifle();
                    }
                    break;
                case 3:
                    if(weapon.name != "Shotgun(Clone)"){
                        SwitchToShotgun();
                    }else if(weapon == null){
                        SwitchToShotgun();
                    }
                    break;
            }
        }else if(weapon == null && Input.mouseScrollDelta.y > 0){
            selectedWeapon = 0;
        }else if(weapon == null && Input.mouseScrollDelta.y < 0){
            selectedWeapon = 3;
        }else{
            if (Input.GetKeyDown("1")){
                SwitchToKatana();
            }else if(Input.GetKeyDown("2")){
                SwitchToRevolver();
            }else if(Input.GetKeyDown("3")){
                SwitchToSniperRifle();
            }else if(Input.GetKeyDown("4")){
                SwitchToShotgun();
            }
        }
        if(Input.mouseScrollDelta.y > 0){
            selectedWeapon++;
        }else if(Input.mouseScrollDelta.y < 0){
            selectedWeapon--;
        }
        


        Textsp.GetComponent<Text>().text = Mathf.Abs(rb.velocity.x+rb.velocity.z).ToString("F2") + "m/s";



        //Buttons interaction
        if (Input.GetKeyDown("e"))
        {
            RaycastHit hit;
            Ray ray = MainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3((MainCamera.GetComponent<Camera>().pixelWidth-1)/2,(MainCamera.GetComponent<Camera>().pixelHeight-1)/2,0));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.CompareTag("Button"))
                {
                    ButtonScript rayObj = hit.collider.gameObject.GetComponent<ButtonScript>();
                    rayObj.ButtonIsUsed = true;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && menuOpened){
            CloseMenu();
        }else if(Input.GetKeyDown(KeyCode.Escape) && !menuOpened){
            OpenMenu();
        }

        //Attacking/Shooting
        if(weapon != null){
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                _animator.SetBool("MouseDown", true);
                if(cWeapon == 1){
                    Shoot("Revolver");
                }
            }
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                _animator.SetBool("MouseDown", false);
            }
        }
        if(transform.position.y < -200){
            transform.position = new Vector3(-3.75f,10f,-3.97300005f);
        }
        //Camera rotation
        if(!menuOpened){
            rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * MSensitivity, 0)));
        }
        //Jumping
        if(Input.GetKey(KeyCode.Space) && canJump){
            canJump = false;
            rb.AddForce(new Vector3(0,1,0) * JumpForce,ForceMode.Impulse);
        }
        //Sliding
        if(Input.GetKeyDown("c")){
            
            sliding = true;
            player.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            tempWeapon = weapon;
        }else if(Input.GetKeyUp("c")){
            sliding = false;
            player.transform.localScale = new Vector3(1f,1f,1f);
            if(weapon != null){
                if(weapon.name == "Katana(Clone)"){
                    _animator.SetBool("Crouched", false);
                }
            }
        }
        if(weapon != null){
            if(weapon.name == "Katana(Clone)" && sliding){
                _animator.SetBool("Crouched", true);
            }
        }
        if(weapon != null){
            if(weapon.name == "Katana(Clone)" && !sliding){
                _animator.SetBool("Crouched", false);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground"){
            canJump = true;
            airborne = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(airborne){
        }
    }
}
