using _game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace _game.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class BaseUI : MonoBehaviour
    {
        [SerializeField] protected UIDocument uiDocument;
        [SerializeField] protected StyleSheet[] styleSheets;
        
        protected VisualElement root;
        protected VisualElement container;

        protected virtual void Awake()
        {
            uiDocument = GetComponent<UIDocument>();
            root = uiDocument.rootVisualElement;
            foreach (var file in styleSheets)
            {
                root.styleSheets.Add(file);
            }
            container = root.CreateChild("container");
        }
    }
}
