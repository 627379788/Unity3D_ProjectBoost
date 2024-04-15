using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] AudioClip crashAudioClip;

    [SerializeField] ParticleSystem mainParticleSystem;
    [SerializeField] ParticleSystem leftParticleSystem;
    [SerializeField] ParticleSystem rightParticleSystem;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            StratLeftRotation();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            StartRightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        leftParticleSystem.Stop();
        rightParticleSystem.Stop();
    }

    private void StartRightRotation()
    {
        ApplyRotation(rotationThrust);
        if (!rightParticleSystem.isPlaying) rightParticleSystem.Play();
    }

    private void StratLeftRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!leftParticleSystem.isPlaying) leftParticleSystem.Play();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void StopThrust()
    {
        audioSource.Stop();
        mainParticleSystem.Stop();
    }

    private void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(crashAudioClip);
        if (!mainParticleSystem.isPlaying) mainParticleSystem.Play();
    }

    void ApplyRotation (float rotationThisFrame)
    {
        transform.Rotate(Vector3.right * rotationThisFrame * Time.deltaTime);
    }
}
