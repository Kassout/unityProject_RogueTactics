using UnityEditor;
using UnityEngine;

public static class AssetCreator
{
    [MenuItem("Assets/Create/Conversation Data")]
    public static void CreateConversationData ()
    {
        ScriptableObjectUtility.CreateAsset<ConversationData> ();
    }
}
