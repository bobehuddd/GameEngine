using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool hasWon = false;
    public bool hasLost = false;

    public TextMeshProUGUI scoreItem;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    private float spawnRate = 1.5f; // Ubah spawn rate menjadi 1.5 detik
    public List<GameObject> targets; // Daftar objek yang akan di-spawn
    public Transform spawnPoint; // Titik spawn

    private int barrelBerbahayaCount = 0; // Hitung jumlah BarrelBerbahaya yang dikumpulkan

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        winText.gameObject.SetActive(false); // Sembunyikan teks kemenangan
        loseText.gameObject.SetActive(false); // Sembunyikan teks kekalahan
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        // Cek kondisi kemenangan
        if (score >= 10000 && !hasWon) // Update max score to 10000
        {
            hasWon = true;
            winText.gameObject.SetActive(true); // Tampilkan teks kemenangan
            Debug.Log("U Won");
            StopAllCoroutines(); // Hentikan semua coroutine jika menang
        }

        // Cek kondisi kekalahan
        if (barrelBerbahayaCount > 2 && !hasLost) // Cek jika lebih dari 2 BarrelBerbahaya
        {
            hasLost = true;
            loseText.gameObject.SetActive(true); // Tampilkan teks kekalahan
            Debug.Log("U Lost");
            StopAllCoroutines(); // Hentikan semua coroutine jika kalah
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        // Clamp the score to a maximum of 10000
        score = Mathf.Clamp(score, 0, 10000);
        scoreItem.text = "Poin   : " + score + " / 10000";
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            Spawn();
        }
    }

    void Spawn()
    {
        // Memilih target secara acak dari daftar
        int randomIndex = Random.Range(0, targets.Count);
        GameObject target = targets[randomIndex];

        // Menginstansiasi objek di posisi spawn
        Instantiate(target, spawnPoint.position, spawnPoint.rotation);
    }

    public void OnTargetDestroyed(GameObject target)
    {
        int scoreToAdd = 0;

        // Check the tag of the destroyed target and update score accordingly
        switch (target.tag)
        {
            case "Kaleng":
                scoreToAdd = 200;
                break;
            case "Botol":
                scoreToAdd = 500;
                break;
            case "MakananSisa":
                scoreToAdd = 100;
                break;
            case "Barrel":
                scoreToAdd = 1000;
                break;
            case "BarrelBerbahaya":
                scoreToAdd = -500;
                barrelBerbahayaCount++; // Tambah hitungan BarrelBerbahaya
                break;
            default:
                Debug.LogWarning("Unknown target tag: " + target.tag);
                break;
        }

        UpdateScore(scoreToAdd);

        // Hancurkan objek yang telah dihancurkan
        Destroy(target);

        // Spawn target baru segera setelah objek dihancurkan
        Spawn();
    }

    private IEnumerator SpawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Spawn();
    }
}