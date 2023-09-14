using UnityEngine;
using UnityEngine.UI;

public static class ImageManager
{
    private const string IMAGE_PATH = "ImageResources";

    public static void LoadDialogNpcImage(GameObject imageObject, string fileName)
    {
        if (!imageObject)
            return;

        Image imageComponent = imageObject.GetComponent<Image>();
        if (!imageComponent)
            return;

        string imageFilePath = IMAGE_PATH + "/" + fileName;

        Sprite imageSprite = Resources.Load<Sprite>(imageFilePath);
        if (imageSprite)
            imageComponent.sprite = imageSprite;
        else
            Debug.LogError("LoadDialogNpcImage: File not found - " + imageFilePath);
    }
}
