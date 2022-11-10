using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_2bossplayer : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float animtimer;
    [SerializeField] float diference;
    [SerializeField] public static int checkpoint;

    bool shoot = true;
    bool randomdots = true;
    AudioSource check_audio;
    AudioSource music;
    Animation anim;
    Transform spawnPos;
    Quaternion rot;

    GameObject line;
    public GameObject wallAt;
    public GameObject bullet;
    public GameObject spin_line;
    public GameObject dot;

    IEnumerator shoot_balls()
    {
        float angle = Random.value % 360;
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

       while(randomdots)
        {
            GameObject ball;
            angle = Random.value * 360;
            ball = GameObject.Instantiate(dot, spawnPos.position, spawnPos.rotation);
            ball.GetComponent<Rigidbody2D>().AddForce(dir * 250);
            dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            Destroy(ball, 4);
            yield return new WaitForSeconds(0.1f);
        }

    }

    void spawn_wall(float x = 0,float y = 0,float angle = 0)
    {
        rot.eulerAngles = new Vector3(0,0,angle);
        GameObject wall = GameObject.Instantiate(wallAt, new Vector2(x,y), rot);
        Destroy(wall, 3);
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = GameObject.Find("bossbody").transform;
        check_audio = GetComponents<AudioSource>()[1];
        music = gameObject.GetComponent<AudioSource>();
        anim = GameObject.Find("bossanimation").GetComponent<Animation>();
        StartCoroutine(events());
    }
    
    // Update is called once per frame
    void Update()
    {
        if(line != null)
        {
            line.transform.position = spawnPos.position;
        }
        timer = music.time;
        animtimer = anim["level2bossanim"].time;
        diference = timer - animtimer;
    }

    IEnumerator boss_shoot()
    {
        Transform point = GameObject.Find("shoot_warning_pivot").transform;
        Transform point1 = GameObject.Find("shoot_warning_pivot1").transform;
        while(shoot)
        {
            GameObject bullet1 = Instantiate(bullet, point.position, point.rotation);
            yield return new WaitForSeconds(0.1f);
            GameObject bullet2 = Instantiate(bullet, point1.position, point1.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator events()
    {
        if (checkpoint <= 0)
        {
            yield return new WaitForSeconds(3);
            music.Play();
            GameObject.Find("bossanimation").GetComponent<Animation>().Play();
            yield return new WaitUntil(() => animtimer >= 12);
        }

       
        if (checkpoint <= 1)
        {
            if(music.isPlaying)
            {
                checkpoint = 1;
                check_audio.Play();
            }
            else
            {
                music.time = 12;
                music.Play();
                anim["level2bossanim"].time = 12;
                GameObject.Find("bossanimation").GetComponent<Animation>().Play();
                Time.timeScale = 1;
            }

            yield return new WaitUntil(() => animtimer >= 13.07f);
            spawn_wall(0, 6, 180);

            yield return new WaitUntil(() => animtimer >= 14.76f);
            spawn_wall(9.5f, 0, 90);

            yield return new WaitUntil(() => animtimer >= 16.56f);
            spawn_wall(-9.5f, 0, 270);

            yield return new WaitUntil(() => animtimer >= 18.3f);
            spawn_wall(0, -6);

            yield return new WaitUntil(() => animtimer >= 20f);
            spawn_wall(-9.5f, 0, 270);

            yield return new WaitUntil(() => animtimer >= 21.7f);
            spawn_wall(0, 6, 180);

            yield return new WaitUntil(() => animtimer >= 23.37f);
            spawn_wall(9.5f, 0, 90);

            yield return new WaitUntil(() => animtimer >= 25.1f);
            spawn_wall(0, 6, 180);

            yield return new WaitUntil(() => animtimer >= 26);
        }
       
        if (checkpoint <= 2)
        {
            if (music.isPlaying)
            {
                checkpoint = 2;
                check_audio.Play();
            }
            else
            {
                music.time = 26f;
                music.Play();
                anim["level2bossanim"].time = 26f;
                GameObject.Find("bossanimation").GetComponent<Animation>().Play();
                Time.timeScale = 1;
            }
            yield return new WaitUntil(() => animtimer >= 27.1f);
            GameObject.Find("Main Camera").GetComponent<Animation>().Play();

            yield return new WaitUntil(() => animtimer >= 31.55f);
            StartCoroutine(boss_shoot());
             
            yield return new WaitUntil(() => animtimer >= 33.52f);
            shoot = false;

            yield return new WaitUntil(() => animtimer >= 34f);
            StartCoroutine(shoot_balls());

            yield return new WaitUntil(() => animtimer >= 37.9f);
            randomdots = false;

            yield return new WaitUntil(() => animtimer >= 38.55f);
            shoot = true;
            StartCoroutine(boss_shoot());

            yield return new WaitUntil(() => animtimer >= 40.125f);
            shoot = false;

            yield return new WaitUntil(() => animtimer >= 40.3f);
            line = Instantiate(spin_line,spawnPos.position,Quaternion.identity);
            Destroy(line,4.75f);

        }
    }
}
