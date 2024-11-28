using UnityEngine;

public class SprintEffectController : MonoBehaviour
{
    private ParticleSystem SpringParticleSystem;
    //パーティクルシステムの放出モジュール
    ParticleSystem.EmissionModule emission;
    bool moduleEnabled;

    void Start()
    {
        SpringParticleSystem = GetComponent<ParticleSystem>();
        emission = SpringParticleSystem.emission;
        //開始時放出したくないので、は放出用のブールをfalseにしておく
        moduleEnabled = false;
        //ブールのオンオフで放出のオンオフを切り替え
        emission.enabled = moduleEnabled;
    }

    void Update()
    {
        //放出する条件式
        if (Input.GetAxis("Vertical") > 0.8f) moduleEnabled = true;
        else moduleEnabled = false;

        //ブールのオンオフで放出のオンオフを切り替え
        emission.enabled = moduleEnabled;
    }
}