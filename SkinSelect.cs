using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinSelect : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public List<Skin> skins = new List<Skin>();
    private Vector2 startPosition = Vector3.zero; // Starting position for spawning
    [SerializeField] float xOffset = 20f; // Distance between each skin
    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;
    [SerializeField] Button setSkinBtn;
    [SerializeField] SavedSkin data;
    private int num = 0;


    private void Start()
    {
        SortSkins();
        SpawnSkins();
        leftBtn.onClick.AddListener(LeftBtn);
        rightBtn.onClick.AddListener(RightBtn);
        setSkinBtn.onClick.AddListener(SetSkinBtn);
    }

    // Update is called once per frame
    void Update()
    {
        BtnUpdater();
    }

    private void SortSkins()
    {
        skins.AddRange(inventory.skins);
        skins = skins
             .OrderBy(item => item.rarity) // First, sort by rarity (ascending)
             .ThenBy(item => item.num) // Then, sort by another number property (ascending)
             .ToList();
    }

    private void SpawnSkins()
    {
        Vector2 spawnPosition = startPosition;

        foreach (Skin skin in skins)
        {
            if (skin != null) // Ensure the skin is not null
            {
                Instantiate(skin, spawnPosition, Quaternion.Euler(0, 0, 0));
                spawnPosition.x += xOffset; // Move to the next x position
            }
        }
    }

    private void BtnUpdater()
    {
        if (num < 0 || num >= skins.Count - 1)
        {
            leftBtn.gameObject.SetActive(false);
        }
        else leftBtn.gameObject.SetActive(true);
        if (num <= 0 || num > skins.Count)
        {
            rightBtn.gameObject.SetActive(false);
        }
        else rightBtn.gameObject.SetActive(true);
    }

    public void LeftBtn()
    {
        Vector3 pos = Camera.main.transform.position;
        Vector3 nextPos = new Vector3(pos.x + 20, pos.y, -1);
        Camera.main.transform.position = nextPos;
        num++;
    }

    public void RightBtn()
    {
        Vector3 pos = Camera.main.transform.position;
        Vector3 nextPos = new Vector3(pos.x - 20, pos.y, -1);
        Camera.main.transform.position = nextPos;
        num--;
    }

    public void SetSkinBtn()
    {
        data.savedSkin = skins[num];
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(data);
#endif
        SceneManager.LoadScene(1);
    }
}
