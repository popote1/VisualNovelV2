using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static  Story Story;
    public static List<Item> Inventory = new List<Item>();
    public UiManager StoryUi;
    public string StoryFileName;
    
    private static string _steamingAssetPath = Application.streamingAssetsPath;

    private void Start() {
        CheckDirectory();
//        SaveStory(path, Story);
        Story = LoadStory(StoryFileName);
        StoryUi.RefreshThumbnail(Story.Thumbnails.Find(thumbnail => thumbnail.Id == 0));
    }

    private void CheckDirectory() {
        if (!File.Exists(_steamingAssetPath))
            Directory.CreateDirectory(_steamingAssetPath);
    }
    
    private void SaveStory(string fileName, Story story) {
        string str = JsonUtility.ToJson(story);
        using (FileStream fs = new FileStream(_steamingAssetPath + "/" + fileName, FileMode.Create)) {
            using (StreamWriter writer = new StreamWriter(fs)){
                writer.Write(str);
            }
        }
    }

    private Story LoadStory(string fileName) {
        StreamReader reader = new StreamReader(_steamingAssetPath + "/" + fileName);
        Story story = JsonUtility.FromJson<Story>(reader.ReadToEnd());
        reader.Close();
        return story;
    }

    public static  Sprite  LoadSprite(string fileName) {
        byte[] bytes = File.ReadAllBytes(_steamingAssetPath + "/" + fileName);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }

    public void QuitGame() {
        Application.Quit();
    }
        
}