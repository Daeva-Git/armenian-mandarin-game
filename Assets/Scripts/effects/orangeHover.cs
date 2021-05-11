using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orangeHover : MonoBehaviour
{
    public bool perlinNoise = true;
    public float perlin_intensity = 0.5f;
    public float perlin_speed = 0.1f;
    // Start is called before the first frame update
    private int perlinOffset = 0;
    private float x_pos;
    private float y_pos;
    private float rotate;
    private Vector3 pos_original;
    private Vector3 rot_original;
    void Start()
    {
        pos_original = transform.position;
        rot_original = transform.rotation.eulerAngles;
        perlinOffset = UnityEngine.Random.Range(0,10000);
    }

    // Update is called once per frame
    void Update()
    {
        x_pos = perlin_intensity * (Mathf.PerlinNoise(Time.time * perlin_speed + perlinOffset, perlinOffset) - 0.5f);
        y_pos = perlin_intensity * (Mathf.PerlinNoise(perlinOffset, Time.time * perlin_speed + perlinOffset) - 0.5f);
        rotate = 80 * perlin_intensity * (Mathf.PerlinNoise(Time.time * perlin_speed + perlinOffset, Time.time * perlin_speed + perlinOffset) - 0.5f);

        transform.position = pos_original + new Vector3(x_pos, y_pos, 0);
        transform.rotation = Quaternion.Euler(rot_original.x, rot_original.y, rot_original.z + rotate);
    }
}