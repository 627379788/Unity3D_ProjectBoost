using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip fallClip;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem fallParticle;
    
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();    
    }

    void Update() {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKey(KeyCode.L)) {
            // 加载下一级level
            LoadNextLevel();
        }else if(Input.GetKey(KeyCode.C)) {
            // 禁用碰撞
            collisionDisable = !collisionDisable;
        }
    }

    void OnCollisionEnter(Collision other) {
        string objectTag = other.gameObject.tag;
        if (isTransitioning || collisionDisable) return;
        switch (objectTag)
        {
            case "Finish":
                StartSuccessSequence();
                break;
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Start":
                Debug.Log("Start");
                break;    
            default:
                StartCrashSequence();
                break;
            }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // 碰撞冻结旋转，防止偏航
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(fallClip);
        fallParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
