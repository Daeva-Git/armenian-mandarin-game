using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;

public class Orange : MonoBehaviour
{
    [SerializeField] private float perlin_intensity = 0.5f;
    [SerializeField] private float perlin_speed = 0.1f;
    [SerializeField] private Material material;
    [SerializeField] private Color baseColor;
    private const float BaseIntensivity = 4.5f;
    
    private Vector3 pos_original;
    private Vector3 rot_original;
    private int perlinOffset = 0;
    private float x_pos;
    private float y_pos;
    private float rotate;


    private void Start()
    {
        pos_original = transform.position;
        rot_original = transform.rotation.eulerAngles;
        perlinOffset = Random.Range(0, 10000);
        
        material.SetColor("_EmissionColor", baseColor * BaseIntensivity);
    }

    private void Update()
    {
        x_pos = perlin_intensity * (Mathf.PerlinNoise(Time.time * perlin_speed + perlinOffset, perlinOffset) - 0.5f);
        y_pos = perlin_intensity * (Mathf.PerlinNoise(perlinOffset, Time.time * perlin_speed + perlinOffset) - 0.5f);
        rotate = 80 * perlin_intensity *
                 (Mathf.PerlinNoise(Time.time * perlin_speed + perlinOffset, Time.time * perlin_speed + perlinOffset) -
                  0.5f);

        transform.position = pos_original + new Vector3(x_pos, y_pos, 0);
        transform.rotation = Quaternion.Euler(rot_original.x, rot_original.y, rot_original.z + rotate);
    }

    public void Blink()
    {
        StartCoroutine(BlinkEnumerator());
    }

    private IEnumerator BlinkEnumerator()
    {
        for (var i = BaseIntensivity; i > 0; i -= 0.1f)
        {
            var finalColor = baseColor * i;
            material.SetColor("_EmissionColor", finalColor);
            yield return new WaitForSeconds(0.01f);
        }
        
        var playerFlashLight = GameManager.Instance.ComponentManager.PlayerFlashLight;
        
        for (var i = 1; i < 5; i++)
        {
            for (var ii = 1; ii < 20; ii += 1)
            {
                playerFlashLight.SetActive(!playerFlashLight.activeSelf);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.05f);
        }
        
        // Reset
        playerFlashLight.SetActive(true);
        material.SetColor("_EmissionColor", baseColor * BaseIntensivity);
    }
}