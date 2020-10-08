using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //private Image innerBar;
    //private Transform playerTransform;
    //private Camera camera;

    //private float yRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (playerTransform != null)
        //{
        //    float xPosition = playerTransform.position.x;

        //    if (xPosition >= 0 || xPosition <= 0)
        //    {
        //        innerBar.fillAmount = Mathf.Clamp(innerBar.fillAmount, Mathf.Abs(1 / xPosition), -Mathf.Abs(1 / xPosition));
        //    }

        //    if (xPosition <= 5 || xPosition >= -5)
        //    {
        //        innerBar.fillAmount = Mathf.Clamp(innerBar.fillAmount, -Mathf.Abs(innerBar.fillAmount / xPosition), Mathf.Abs(innerBar.fillAmount / xPosition));
        //    }

        //    if (innerBar.fillAmount < 0.5f)
        //    {
        //        innerBar.color = Color.red;
        //    }

        //    if (innerBar.fillAmount > 0.5f)
        //    {
        //        innerBar.color = Color.green;
        //    }
        //}
    }

    private void LateUpdate()
    {
        //yRotation += 1.0f * Input.GetAxis("Camera");
        //if (camera != null)
        //{
        //    camera.transform.eulerAngles = new Vector3(45.0f, yRotation, 0.0f);
        //}
    }

    public void LoadFirstLevel()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadDesignLevel()
    {

    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Button exitButton = GameObject.FindWithTag("ExitButton").GetComponent<Button>();
            exitButton.onClick.AddListener(ExitGame);

            //innerBar = GameObject.FindWithTag("PlayerHealthBar").GetComponent<Image>();

            //playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

            //camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }

        if(scene.buildIndex == 2)
        {

        }
    }
}
