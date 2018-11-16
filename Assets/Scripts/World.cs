﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    //to enable the option in the world object to drop tiles
    [SerializeField]
    private GameObject[] tilePrefabs;

    private float groundPositionX;

    public AudioSource audioSource;
    public AudioClip sound_die;
    public AudioClip sound_wall;

    public float TileSize
    {
        //get width of sprite
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // call function to create level
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateLevel()
    {   //get info from text function
        string[] mapData = ReadLevelText();

        int XSize = mapData[0].ToCharArray().Length;
        int YSize = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        //tilemap 
        for (int y = 0; y < YSize; y++)
        {
            char[] mapTiles = mapData[y].ToCharArray();

            for (int x = 0; x < XSize; x++)
            {
                PlaceTile(mapTiles[x].ToString(), x, y, worldStart);
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);
        //access the tile
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]) as GameObject;
        //place tiles
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
        //float groundPositionX = (worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level_00") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split('-');
    }

    public void EnemyDieSound()
    {
        audioSource.clip = sound_die;
        audioSource.Play();
    }
}
