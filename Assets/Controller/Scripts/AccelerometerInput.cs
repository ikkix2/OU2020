using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour {
    // Firebaseに渡すことを考えている値
    // RoomNo: （例1）
    // PlayerNo:（例1）
    // Speed:（例0〜2）
    // Direction:（例0〜360）
    // Time:（UNIX時間） 

    private int roomNo = 0;
    private int playerNo = 0;
    private float speed = 5.0f;
    private float currentDirection = 0.0f;
    private float originalDirection = 0.0f;
    private string currentTime = "0";

    void Start () {
        Input.gyro.enabled = true; // ジャイロセンサー有効化
        Input.compass.enabled = true; // コンパス有効化
        Input.location.Start ();
    }

    void Update () {
        Rotate (); // 方向転換
        Walk (); // 移動
        Upload (); // 現在の情報をアップロード
    }

    // 方向転換処理
    void Rotate () {
        Vector3 rotationAngles = Vector3.zero;

        // Compassが利用できる場合、Compassに従ってY軸回転
        if (Input.compass.enabled) {
            // 初回のみ原点にあたる角度を取得
            if (originalDirection == 0.0f && Input.compass.magneticHeading > 0.0f) {
                originalDirection = Input.compass.magneticHeading;
            }

            transform.localRotation = Quaternion.Euler (0, Input.compass.magneticHeading - originalDirection, 0);
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

    // 移動処理
    void Walk () {
        float runningSpeed = 0.98f;

        // 加速度センサーが使える場合、加速度センサーから移動速度を取得し移動
        if (Input.acceleration.z > -0.5f) {
            runningSpeed = Mathf.Abs (runningSpeed + Input.acceleration.y);

            UnityEngine.Debug.Log (runningSpeed);

            if (runningSpeed < 0.1f) {
                runningSpeed = 0.0f;
            } else {
                runningSpeed *= 3.0f;
            }

            transform.position += transform.forward * runningSpeed * Time.deltaTime;
        }

        // Unity Editor上の移動操作
        if (Input.GetKey (KeyCode.UpArrow)) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey (KeyCode.DownArrow)) {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }

    void Upload () {
        // 開発中
    }

    // デバッグ用表示
    // [Conditional ("UNITY_EDITOR")]
    void OnGUI () {
        var sb = new System.Text.StringBuilder ();
        sb.Append ("Enabled        :").AppendLine (Input.compass.enabled.ToString ());
        sb.Append ("headingAccuracy:").AppendLine (Input.compass.headingAccuracy.ToString ());
        sb.Append ("magneticHeading:").AppendLine (Input.compass.magneticHeading.ToString ());
        sb.Append ("rawVector      :").AppendLine (Input.compass.rawVector.ToString ());
        sb.Append ("timestamp      :").AppendLine (Input.compass.timestamp.ToString ());
        sb.Append ("trueHeading    :").AppendLine (Input.compass.trueHeading.ToString ());
        sb.Append ("comps-X:       :").AppendLine (Input.compass.rawVector.x.ToString ());
        sb.Append ("comps-Y:       :").AppendLine (Input.compass.rawVector.y.ToString ());
        sb.Append ("comps-Z:       :").AppendLine (Input.compass.rawVector.z.ToString ());
        sb.Append ("accel-X:       :").AppendLine (Input.acceleration.x.ToString ("f2"));
        sb.Append ("accel-Y:       :").AppendLine (Input.acceleration.y.ToString ("f2"));
        sb.Append ("accel-Z:       :").AppendLine (Input.acceleration.z.ToString ("f2"));
        sb.Append ("gyro-X:        :").AppendLine (Input.gyro.attitude.x.ToString ("f2"));
        sb.Append ("gyro-Y:        :").AppendLine (Input.gyro.attitude.y.ToString ("f2"));
        sb.Append ("gyro-Z:        :").AppendLine (Input.gyro.attitude.z.ToString ("f2"));
        sb.Append ("gyro-W:        :").AppendLine (Input.gyro.attitude.w.ToString ("f2"));

        GUI.Label (new Rect (10, 10, 256, 256), sb.ToString ());
    }
}
