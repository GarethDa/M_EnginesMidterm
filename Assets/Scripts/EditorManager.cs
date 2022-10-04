using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    PlayerAction inputAction;

    public Camera mainCam;
    public Camera editorCam;

    public GameObject prefab1;
    public GameObject prefab2;

    GameObject item;

    public bool editorMode = false;

    bool instantiated = false;

    Vector3 mousePos;

    Subject subject = new Subject();

    // Start is called before the first frame update
    void Start()
    {
        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Editor.EnableEditor.performed += cntxt => SwitchCamera();

        inputAction.Editor.AddItem1.performed += cntxt => AddItem(1);
        inputAction.Editor.AddItem2.performed += cntxt => AddItem(2);
        inputAction.Editor.DropItem.performed += cntxt => DropItem();

        mainCam.enabled = true;
        editorCam.enabled = false;

        editorMode = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SwitchCamera()
    {
        mainCam.enabled = !mainCam.enabled;
        editorCam.enabled = !editorCam.enabled;

        if (mainCam.enabled == false && editorCam.enabled == true)
        {
            editorMode = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            editorMode = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void AddItem(int itemId)
    {
        if(editorMode && !instantiated)
        {
            switch(itemId)
            {
                case 1:
                    item = Instantiate(prefab1);
                    SpikeBall spike1 = new SpikeBall(item, new GreenMat());
                    subject.AddObserver(spike1);
                    break;
                case 2:
                    item = Instantiate(prefab2);
                    SpikeBall spike2 = new SpikeBall(item, new YellowMat());
                    subject.AddObserver(spike2);
                    break;
                default:
                    break;
            }

            subject.Notify();

            instantiated = true;
        }
    }

    private void DropItem()
    {
        if (editorMode && instantiated)
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Collider>().enabled = true;
            
            instantiated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(instantiated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 10f);

            item.transform.position = editorCam.ScreenToWorldPoint(mousePos);
        }
    }
}
