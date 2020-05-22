using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof (ThirdPersonCharacter))]
public class PlayerController2 : MonoBehaviourPun {
    public string ownerId = ""; // プレイヤーID
    public int ownerNo = 0; // プレイヤーNO
    private float currentDirection = 0.0f; // 現在向いている角度
    private float originalDirection = 0.0f; // 原点となる角度
    private int runningTime = 0;
    public bool oniFlg = false; // 鬼か否か
    public int score = 0; // プレイヤースコア
    private Text scoreText;

    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam; // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward; // The current forward direction of the camera
    private Vector3 m_Move = new Vector3 (0, 0, 0);
    private bool m_Jump = false; // the world-relative desired move direction, calculated from the camForward and user input.
    private GameObject originalCamera;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] public GameObject oniImage;

    void Start () {
        Input.gyro.enabled = true; // ジャイロセンサー有効化
        Input.compass.enabled = true; // コンパス有効化
        Input.location.Start ();

        originalCamera = Camera.main.gameObject;
        m_Cam = mainCamera.GetComponent<Transform> ();
        m_Character = GetComponent<ThirdPersonCharacter> ();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        // ゲスト以外の場合は初期設定のカメラを無効化し、自身のカメラを有効化する
        if (ownerId == "guest") {
            originalCamera.SetActive (true);
            mainCamera.SetActive (false);
        } else {
            originalCamera.SetActive (false);
        }

        // 鬼の場合帽子を表示？
        if (oniFlg) {
            // oniImage.SetActive (true);
        }
    }

    void Update () {
        if (ownerId == "guest") {
            mainCamera.SetActive (false);
            return;
            // } else if (onwerId != "" && ownerId != PhotonNetwork.LocalPlayer.NickName) {
        } else if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            mainCamera.SetActive (false);
            return;
        }

        Rotate ();
        AddScore();

        if (!m_Jump) {
            m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
        }
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate () {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
            return;
        }

        float h = CrossPlatformInputManager.GetAxis ("Horizontal");
        float v = CrossPlatformInputManager.GetAxis ("Vertical");
        bool crouch = Input.GetKey (KeyCode.C);

        // 加速度センサーが使える場合、加速度センサーから移動速度を取得し移動
        if (Input.acceleration.y != 0.0f && Input.acceleration.x > -0.5f && Input.acceleration.z > -0.5f) {
            if (Mathf.Abs (0.98f + Input.acceleration.y) > 0.2f) {
                runningTime = 8;
            }

            if (runningTime > -1) {
                v = 1.0f;
            } else {
                v = 0.0f;
            }

            runningTime--;

            // transform.position += transform.forward * v * Time.deltaTime;
        }

        // calculate move direction to pass to character
        if (m_Cam != null) {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale (m_Cam.forward, new Vector3 (1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }

        m_Character.Move (m_Move, crouch, m_Jump);
        m_Jump = false;
    }

    // 方向転換処理
    void Rotate () {
        Vector3 rotationAngles = Vector3.zero;

        // Compassが利用できる場合、Compassに従ってY軸回転
        if (Input.compass.enabled) {
            rotationAngles = transform.localRotation.eulerAngles;

            // 初回のみ原点にあたる角度を取得
            if (originalDirection == 0.0f && Input.compass.magneticHeading > 0.0f) {
                originalDirection = Input.compass.magneticHeading;
            }

            currentDirection = Input.compass.magneticHeading - originalDirection;
            if (currentDirection > 360.0f) {
                currentDirection -= 360.0f;
            } else if (currentDirection < 0.0f) {
                currentDirection += 360.0f;
            }

            // 左に傾けたら左を向く
            if (currentDirection > 310.0f && currentDirection < 340.0f) {
                rotationAngles.y = rotationAngles.y - 2.0f;
                UnityEngine.Debug.Log ("左");
            } else if (currentDirection > 20.0f && currentDirection < 50.0f) {
                rotationAngles.y = rotationAngles.y + 2.0f;
                UnityEngine.Debug.Log ("右");
            }

            // transform.localRotation = Quaternion.Euler (0, Input.compass.magneticHeading - originalDirection, 0);
            transform.localRotation = Quaternion.Euler (0, rotationAngles.y, 0);
        }

        // Unity Editor上のY軸回転操作
        if (Input.GetKey (KeyCode.RightArrow)) {
            rotationAngles = transform.localRotation.eulerAngles + new Vector3 (0.0f, 10.0f, 0.0f);
            transform.localRotation = Quaternion.Euler (rotationAngles);
        }

        if (Input.GetKey (KeyCode.LeftArrow)) {
            rotationAngles = transform.localRotation.eulerAngles + new Vector3 (0.0f, -10.0f, 0.0f);
            transform.localRotation = Quaternion.Euler (rotationAngles);
        }
    }

    void AddScore () {
        if (!oniFlg) {
            score += (int) Time.deltaTime;
            scoreText.text = score.ToString() + "点";
        } else {
            scoreText.text = "あなたが鬼です！";
        }
    }

    // 接触時の処理
    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "Player") {
            bool colOniFlg = collision.gameObject.GetComponent<PlayerController2> ().oniFlg;

            if (colOniFlg) {
                // oniImage.SetActive (true);
                oniFlg = true;
                colOniFlg = false;
            }
        }
    }

    // デバッグ用表示
    // [Conditional ("UNITY_EDITOR")]
    // void OnGUI () {
    //     var sb = new System.Text.StringBuilder ();
    //     sb.Append ("Enabled        :").AppendLine (Input.compass.enabled.ToString ());
    //     sb.Append ("headingAccuracy:").AppendLine (Input.compass.headingAccuracy.ToString ());
    //     sb.Append ("magneticHeading:").AppendLine (Input.compass.magneticHeading.ToString ());
    //     sb.Append ("rawVector      :").AppendLine (Input.compass.rawVector.ToString ());
    //     sb.Append ("timestamp      :").AppendLine (Input.compass.timestamp.ToString ());
    //     sb.Append ("trueHeading    :").AppendLine (Input.compass.trueHeading.ToString ());
    //     sb.Append ("comps-X:       :").AppendLine (Input.compass.rawVector.x.ToString ());
    //     sb.Append ("comps-Y:       :").AppendLine (Input.compass.rawVector.y.ToString ());
    //     sb.Append ("comps-Z:       :").AppendLine (Input.compass.rawVector.z.ToString ());
    //     sb.Append ("accel-X:       :").AppendLine (Input.acceleration.x.ToString ("f2"));
    //     sb.Append ("accel-Y:       :").AppendLine (Input.acceleration.y.ToString ("f2"));
    //     sb.Append ("accel-Z:       :").AppendLine (Input.acceleration.z.ToString ("f2"));
    //     sb.Append ("gyro-X:        :").AppendLine (Input.gyro.attitude.x.ToString ("f2"));
    //     sb.Append ("gyro-Y:        :").AppendLine (Input.gyro.attitude.y.ToString ("f2"));
    //     sb.Append ("gyro-Z:        :").AppendLine (Input.gyro.attitude.z.ToString ("f2"));
    //     sb.Append ("gyro-W:        :").AppendLine (Input.gyro.attitude.w.ToString ("f2"));
    //     sb.Append ("OwnerId:       :").AppendLine (ownerId.ToString ());
    //     GUI.Label (new Rect (10, 10, 384, 384), sb.ToString ());
    // }
}
