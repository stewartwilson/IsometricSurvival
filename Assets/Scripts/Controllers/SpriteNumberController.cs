using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteNumberController : MonoBehaviour
{

    public List<Sprite> numberSprites;
    public Vector2 spriteOffset;
    public float lifeTime;
    public float endTime;
    public float moveSpeed;
    public bool isMoving;
    public int damage;


    // Use this for initialization
    void Start()
    {
        transform.position = transform.position + new Vector3(spriteOffset.x, spriteOffset.y);
        endTime = Time.time + lifeTime;
        isMoving = true;
        switch(damage)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = numberSprites[1];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = numberSprites[2];
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = numberSprites[3];
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = numberSprites[4];
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = numberSprites[5];
                break;
            case 6:
                GetComponent<SpriteRenderer>().sprite = numberSprites[6];
                break;
            case 7:
                GetComponent<SpriteRenderer>().sprite = numberSprites[7];
                break;
            case 8:
                GetComponent<SpriteRenderer>().sprite = numberSprites[8];
                break;
            case 9:
                GetComponent<SpriteRenderer>().sprite = numberSprites[9];
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = numberSprites[0];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            transform.Translate(new Vector3(0, moveSpeed) * Time.deltaTime);
        }
        if(endTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

}
