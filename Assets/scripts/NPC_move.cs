using UnityEngine;

public class NPC_move : MonoBehaviour
{
    Animator animator;

    public float moveSpeed = 0.4f;

    Vector3 stopPosition;

    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;

    int WalkDirection;

    public bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //So that all the prefabs don't move/stop at the same time
        walkTime = Random.Range(3, 6);


        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {

            animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                //case 1:
                //    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                //    break;
                //case 2:
                //    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                //    break;
                //case 3:
                //    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                //    break;
            }


        }
    }


    public void ChooseDirection()
    {
        WalkDirection = 0;

        isWalking = true;
        walkCounter = walkTime;
    }
}
