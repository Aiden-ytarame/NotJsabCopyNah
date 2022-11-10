using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvents : MonoBehaviour
{
    [SerializeField] float testtime;
    [SerializeField] Vector2 testpos;
    [SerializeField] float delaytest;
    [SerializeField] float timer; 
    public static float time = 0;
    
    //objects references to spawn in methods(may remove later, I didnt know how to use prefabs before this)
    GameObject ballexpo;
    GameObject line;
    GameObject conelaser;
    GameObject ballscaled;
    GameObject camera_level;
    GameObject big_ass_lines;
    GameObject attack_1;
    GameObject attack_2;
    GameObject bullet;
    GameObject bullets1, bullets2, bullets3, bullets4;

    static public int checkpoint = 0;
    bool call_once = true;
    bool can_stop = false;
    bool can_start = false;
    int min = 1;
    int max = 5;
    Quaternion rot;
    public SmallerBallSpawn ballspawner;
    AudioSource[] audioSources;
    AudioSource music;
    AudioSource check_audio;
    bool has_started = false;
    
    private void Awake()
    {
        Time.timeScale = 0;
        time = 0;
        bullet = GameObject.Find("projectile");
        ballexpo = GameObject.Find("ball_expo");
        line = GameObject.Find("line_attack");
        conelaser = GameObject.Find("cone_laser");
        ballscaled = GameObject.Find("scaled_ball_pop");
        camera_level = GameObject.Find("Main Camera");
        big_ass_lines = GameObject.Find("big_lined_attack");
        audioSources = GetComponents<AudioSource>();
        music = audioSources[0];
        check_audio = audioSources[1];
        music.time = 118;
        music.volume = Main_Menu.volumemaster;
        StartCoroutine(list_of_moves());
    }

   

    private void Update()
    {
        if(Input.anyKey == true && call_once)
        {
            can_start = true;
            call_once = false;
            if (GameObject.Find("any_key_to_start_text") != null)
            {
                GameObject.Find("any_key_to_start_text").SetActive(false);
            }
        }
        if (has_started == true)
        { time = music.time - 118; }
    }

    IEnumerator lastattack()
    {
        GameObject obj1 = GameObject.Find("spawner");
        GameObject obj2 = GameObject.Find("spawner (1)");
        GameObject ball;
        float angleMath = 3;
        float angle_offset = 90 / angleMath;
        float angle = 225;
        Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

        for (int x = 0; x < 7; x++)
        {
            angle += angle_offset;
            ball = GameObject.Instantiate(GameObject.Find("dot"), obj1.transform.position,Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(dir * 100);
            dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            Destroy(ball, 10);
        }
        angle = 225;
        yield return new WaitForSeconds(0.32f);
        for (int x = 0; x < 7; x++)
        {
            angle += angle_offset;
            ball = GameObject.Instantiate(GameObject.Find("dot"), obj2.transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(dir * 100);
            dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            Destroy(ball, 10);
        }
    }
    GameObject random_pos()
    {
        int random = Random.Range(min, max);
        switch(random)
        {
            case 1:
                return bullets1;
            case 2:
                return bullets2;
            case 3:
                return bullets3;
            case 4:
                return bullets4;
            default:
                return null;
        }
    }

    GameObject spawn_bullet_spawner(float x = 0, float y = 0)
    {
        GameObject placeHolder = GameObject.Instantiate(GameObject.Find("shoot_projectiles"), new Vector2(x, y), Quaternion.identity);
        placeHolder.transform.parent = camera_level.transform;
        return placeHolder;
    }
    void shoot_bullet(GameObject spawn_pos)
    {
        GameObject placeHolder = GameObject.Instantiate(bullet,spawn_pos.transform.position,spawn_pos.transform.rotation);
        placeHolder.SetActive(true);
    }
    void cone_laser(float x = 0, float y = 0, float angle = 0,float delay = 0)
    {
        conelaser_script.public_delay_cone = delay;
        rot.eulerAngles = new Vector3(0,0,angle);
        GameObject.Instantiate(conelaser,new Vector2(x,y),rot);
    }

    void spawn_pop_ball(float x,float y,float delay = 0)
    {
        ballspawner.publicdelay = delay;
        GameObject.Instantiate(ballexpo, new Vector2(x,y), Quaternion.identity);
    }

   void line_spawn(float x = 0, float y = 0, float angle = 0, float delay = 0)
    {
        rot.eulerAngles = new Vector3(0, 0, angle);
        
        LineScript.delay_p_line = delay;
        GameObject lineph = GameObject.Instantiate(line, new Vector2(x,y), rot);
        lineph.transform.GetChild(0).gameObject.SetActive(true);
    }

    void ball_scaled(float x = 0,float y = 0)
    {
       GameObject ballscaledobj = GameObject.Instantiate(ballscaled, new Vector2(x,y), Quaternion.identity);
        ballscaledobj.transform.GetChild(0).gameObject.SetActive(true);
    }
    
    void camera_movement(float x = 0,float y = 0)
    {
        camera_level.GetComponent<Rigidbody2D>().velocity = new Vector2(x,y);
    }

    //start of big lines methods
    void big_attack_instantiate(float x = 0, float y = 0,float x1 = 0,float y1 = 0, float angle = 0)
    {
        rot.eulerAngles = new Vector3(0, 0, angle);
        attack_1 = GameObject.Instantiate(big_ass_lines, new Vector2(x, y), rot);
        attack_2 = GameObject.Instantiate(big_ass_lines, new Vector2(x1, y1), rot);
        attack_1.transform.GetChild(0).gameObject.SetActive(true);
        attack_1.transform.GetChild(1).gameObject.SetActive(true);
        attack_2.transform.GetChild(0).gameObject.SetActive(true);
        attack_2.transform.GetChild(1).gameObject.SetActive(true);
    }
    void big_attack_add_velocity()
    {
        lines_large_script.speed_of_lines = -18;
    }
    
    IEnumerator big_attack_destroy()
    {
        while(can_stop == false)
        {
            foreach (SpriteRenderer r in attack_1.GetComponentsInChildren<SpriteRenderer>())
            {
                r.material.color -= new Color32(0, 0, 0, 10);                  
            }
            foreach (SpriteRenderer r in attack_2.GetComponentsInChildren<SpriteRenderer>())
            {
                r.material.color -= new Color32(0, 0, 0, 10);
                if (r.material.color.a <= 0)
                    can_stop = true;
            }
            yield return new WaitForFixedUpdate();
        }
        Destroy(attack_1, 0);
        Destroy(attack_2, 0);
        can_stop = false;
        yield return null;
    }
    //end of methods
    //

    IEnumerator destroy_with_fade(GameObject go)
    {
        while (can_stop == false)
        {
            go.GetComponentInChildren<SpriteRenderer>().material.color -= new Color32(0, 0, 0, 10);
            if (go.GetComponentInChildren<SpriteRenderer>().material.color.a <= 0)
            {
                can_stop = true;
            }
            yield return new WaitForFixedUpdate();
           
        }
        Destroy(go, 0);
        can_stop = false;
        yield return null;
    }

    IEnumerator list_of_moves()
    {
        if (checkpoint <= 0)
        {
            yield return new WaitUntil(() => can_start == true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(2);

        
            has_started = true;
            music.Play();
            yield return new WaitUntil(() => time >= 2.7f);

            for (float x = 0; x < 7.2f; x += 0.9f)
            {
                line_spawn(-11 + x, -8);
                line_spawn(11 + x * -1, -8);
                yield return new WaitForSeconds(0.07f);
            }


            yield return new WaitUntil(() => time >= 2.85f);
            spawn_pop_ball(0, -6, 0.05f);

            yield return new WaitUntil(() => time >= 4.90f);
            ball_scaled(-3, 3.2f);

            yield return new WaitUntil(() => time >= 6.38f);
            spawn_pop_ball(-7, -6, 0.35f);
            spawn_pop_ball(7, -6, 0.35f);

            yield return new WaitUntil(() => time >= 6.51f);
            ball_scaled(5.4f, -2.7f);

            yield return new WaitUntil(() => time >= 8.1f);
            ball_scaled(-8.8f, -3.5f);

            yield return new WaitUntil(() => time >= 9.4f);
            line_spawn(-12f, 3.5f, 270);
            line_spawn(-12, -3.5f, 270, 0.5f);
            cone_laser(12, 0, 90, 0.58f);

            yield return new WaitUntil(() => time >= 9.74f);
            ball_scaled(-0.96f, -2.66f);

            yield return new WaitUntil(() => time >= 11.6f);
            ball_scaled(4.52f, 1.29f);
        }

        if (checkpoint <= 1)
        {
            if (music.isPlaying)
            {
                checkpoint = 1;
                check_audio.Play();
            }
            else
            {
                GameObject.Find("any_key_to_start_text").SetActive(false);
                music.time = 118 + 11.6f;
                music.Play();
                time = 11.6f;
                has_started = true;
                Time.timeScale = 1;
            }
            yield return new WaitUntil(() => time >= 13f);
            cone_laser(13f, 3, 90);
            cone_laser(-13f, -3, 270);
            big_attack_instantiate(-8, 0, 8);

            yield return new WaitUntil(() => time >= 14f);
            line_spawn(-3, -8, 0, 0.75f);//16,77
            line_spawn(-1, -8, 0, 1f);
            line_spawn(1, -8, 0, 1.25f);
            line_spawn(3, -8, 0, 1.5f);


            yield return new WaitUntil(() => time >= 15.7f);
            camera_movement(0, 1);
            big_attack_add_velocity();

            yield return new WaitUntil(() => time >= 17.6f);
            ball_scaled(1.6f, 2.15f);
            ball_scaled(-2.7f, 7);

            yield return new WaitUntil(() => time >= 18);
            side_to_side_movement.startangle = true;
            bullets1 = spawn_bullet_spawner(-3, -6);
            side_to_side_movement.startangle = false;
            bullets1.transform.eulerAngles += new Vector3(0, 0, 14);
            bullets2 = spawn_bullet_spawner(-1, -6);
            bullets2.transform.eulerAngles += new Vector3(0, 0, 8);
            side_to_side_movement.startangle = false;
            bullets3 = spawn_bullet_spawner(1, -6);
            bullets3.transform.eulerAngles += new Vector3(0, 0, -12);
            side_to_side_movement.startangle = true;
            bullets4 = spawn_bullet_spawner(3, -6);
            bullets4.transform.eulerAngles += new Vector3(0, 0, -3);


            yield return new WaitUntil(() => time >= 19.2f);
            camera_movement(0, 3);
            big_attack_add_velocity();
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 20.2f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 20.8f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 21f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 21.2f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 21.4f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 21.6f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 21.8f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 22.5f);
            camera_movement(0, 5.1f);
            big_attack_add_velocity();

            yield return new WaitUntil(() => time >= 22.65f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 22.85);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 23.05f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 23.25f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 23.45f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 23.65f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 23.85f);
            ball_scaled(-3.6f, 32.5f);
            ball_scaled(1.9f, 30.1f);




            yield return new WaitUntil(() => time >= 25.95f);
            camera_movement(0, 7);
            big_attack_add_velocity();

            yield return new WaitUntil(() => time >= 26.15f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 26.40f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 26.65f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 26.90f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 27.15f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 27.40f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 27.65f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 27.90f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 28.15f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 28.40f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 28.65f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 28.90f); //.38
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 29.15f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 29.40f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 29.65f);
            big_attack_add_velocity();
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 29.90f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 30.15f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 30.40f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 30.65f);
            big_attack_add_velocity();
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 30.90f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 31.1f);
            big_attack_add_velocity();

            yield return new WaitUntil(() => time >= 31.15f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 31.40f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 31.65f);
            big_attack_add_velocity();
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 31.90f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 32.15f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 32.40f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 32.65f);
            big_attack_add_velocity();
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 33.1f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.573f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.8f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 36.5f);
            max = 4;
            StartCoroutine(destroy_with_fade(bullets4));
            camera_movement(0, 3.75f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.573f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.8f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 39.9f);
            min = 2;
            StartCoroutine(destroy_with_fade(bullets1));
            camera_movement(0, 2.50f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.573f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.8f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time >= 43.25f);
            max = 3;
            camera_movement(0, 1.25f);
            StartCoroutine(destroy_with_fade(bullets3));
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.573f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.8f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());
            yield return new WaitForSeconds(0.3f);
            shoot_bullet(random_pos());

            yield return new WaitUntil(() => time > 46.8f);
            StartCoroutine(big_attack_destroy());
            StartCoroutine(destroy_with_fade(bullets2));
            camera_movement(0, 0);
        }
        
        if (music.isPlaying)
        {
            checkpoint = 2;
            check_audio.Play();
        }
        else
        {
            GameObject.Find("any_key_to_start_text").SetActive(false);
            music.time = 118 + 46.8f;
            music.Play();
            time = 46.8f;
            has_started = true;
            Time.timeScale = 1;
        }
        GameObject.Find("finalattack").GetComponent<Animation>().Play();
        //last attack
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 47.67f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 48.55f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 49.42f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 50.3f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 51.17f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 52.05f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 52.92f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 53.8f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 54.67f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 55.55f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 56.42f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 57.3f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 58.17f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 59.05f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 59.92f);
        StartCoroutine(lastattack());

        yield return new WaitUntil(() => time > 60.5f);
        GameObject.Find("text").GetComponent<Animation>().Play("criminal intent end anim");
        GameObject[] dots = GameObject.FindGameObjectsWithTag("dot");
        for(int x = 0; x < dots.Length;x++)
        {
            Destroy(dots[x], 0);
        }

        yield return new WaitUntil(() => time > 70);
        music.volume = Main_Menu.volumemaster / 3;
        GameObject.Find("UiCamera").transform.GetChild(2).gameObject.SetActive(true);
        Time.timeScale = 0;

        yield return null;
    }

}
