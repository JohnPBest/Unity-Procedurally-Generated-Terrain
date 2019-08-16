using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class TextureGenerator {
    // create texture from 1D color map
    public static Texture2D TextureFromColorMap (Color[] colormap, int width, int height) {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colormap);
        texture.Apply();
        return texture;
    }

    // create texture from 2D height map
    public static Texture2D TextureFromHeightMap (float[,] heightMap) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        Texture2D texture = new Texture2D(width, height);

        // generate array of all colors for all pixels, set at once
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                // color map is 1D, noise map is 2D; y * width gives index of 'row', add x for 'column'
                // noiseMap is within range [0,1], convenient for Color.Lerp percentage parameter
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        
        return TextureFromColorMap(colorMap, width, height);
    }
}
