using System.Collections.Generic;
using System.Linq;

namespace PeoplePolder.GUI.Elements
{
    public class Container:UIObject
    {
        public List<UIObject> Elements { get; set; }

        public UIObject this[int index]
        {
            get
            {
                return Elements[index];
            }
        }

        public Container()
        {
            Elements = new List<UIObject>();
        }

        public override void Initialize(){}

        internal override void Update(float dt)
        {
            foreach (UIObject uiObject in Elements)
            {
                uiObject.Update(dt);
            }
        }

        internal override void Draw(float dt)
        {
            foreach (UIObject uiObject in Elements)
            {
                uiObject.Draw(dt);
            }
        }

        public void Add(UIObject uiObject)
        {
            uiObject.Parent = this;
            uiObject.GUIManager = GUIManager;
            uiObject.Initialize();
            Elements.Add(uiObject);
        }

        public void Remove(UIObject uiObject)
        {
            Elements.Remove(uiObject);
        }

        public bool Exist(UIObject uiObject)
        {
            return Elements.Any(u => u == uiObject);
        }

        public UIObject FoundFocus(float mouseX, float mouseY, bool setToFront = false)
        {
            if (Elements.Count != 0)
            {
                int min = 0;
                while (!Elements[min].MouseInteserect((int)mouseX, (int)mouseY) || !Elements[min].Visible)
                {
                    min++;
                    if (min == Elements.Count) break;
                }
                if (min != Elements.Count)
                {
                    int k = min;
                    if (setToFront)
                    {
                        SetToFront(Elements[min]);
                        k = 0;
                    }
                    if (Elements[k] is Container)
                    {
                        return (Elements[k] as Container).FoundFocus(mouseX, mouseY, setToFront) ?? Elements[k];
                    }
                    return Elements[k];
                }
            }
            return null;
        }

        public void SetToFront(UIObject uiobject)
        {
            Elements.Remove(uiobject);
            Elements.Insert(0, uiobject);
            if (Parent != null)
                Parent.SetToFront(this);
        }


    }
}
