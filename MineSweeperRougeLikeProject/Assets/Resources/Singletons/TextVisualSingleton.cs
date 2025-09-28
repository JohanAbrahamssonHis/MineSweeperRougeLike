using UnityEngine;


[CreateAssetMenu(menuName = "Singletons/TextVisualSingleton", fileName = "TextVisualSingleton")]
public class TextVisualSingleton : ScriptableObject
{
    private static TextVisualSingleton _instance;

    public static TextVisualSingleton Instance {
        get
        {
            if (_instance == null) _instance = UnityEngine.Resources.Load<TextVisualSingleton>("Singletons/TextVisualSingleton");
            return _instance;
        }
    }

    public TextVisualObject textVisualObject;
    public Object textObject;
    public new string name;
    public string description;
    

}

