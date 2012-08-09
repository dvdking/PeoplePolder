using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PeoplePolder.GUI.Elements;
using PeoplePolder.Helpers;

namespace PeoplePolder.GUI.Elements
{

    public delegate int ElementPressedEventHandler(Type type);
    public class ConstructionMenu:Container
    {
        public int ShowElementsCount;

        private struct Element
        {
            public string Description;
            public SpriteType SpriteType;
            public Type Type;

        }

        public event ElementPressedEventHandler ElementPressed;

        public Size ButtonSize { get; set; }

        private readonly Dictionary<Button, int> _buttons;
        private readonly List<Element> _elements;

        private readonly Button _arrowRight;
        private readonly Button _arrowLeft;

        private int _currentElement;
        public ConstructionMenu(int elementsCount, Size buttonSize):base()
        {
            ButtonSize = buttonSize;
            ShowElementsCount = elementsCount;


            _buttons = new Dictionary<Button, int>();

            for (int i = 0; i < ShowElementsCount; i++)
            {
                Button button = new Button
                                    {
                                        Size = ButtonSize,
                                        Position = new Vector2(i*ButtonSize.Width + 32, 0),
                                    };
                button.OnLeftMouseButtonReleaseIn += OnButtonRealeaseIn;

                Add(button);
                _buttons.Add(button, i);
            }



            Size = new Size(ShowElementsCount * buttonSize.Width, buttonSize.Height);

            _elements = new List<Element>();

            _arrowRight = new Button
                              {
                                  Size = new Size(32, ButtonSize.Height),
                                  X = Size.Width - 32 + ShowElementsCount * ButtonSize.Width,
                                  TextureIdle = SpriteType.ArrowButtonRight,
                                  TexturePressed = SpriteType.ArrowButtonRight,
                              };
            _arrowRight.OnLeftMouseButtonReleaseIn += ArrowRightOnLeftMouseButtonReleaseIn;
            Add(_arrowRight);

            _arrowLeft = new Button
                             {
                                 Size = new Size(32, ButtonSize.Height),

                                 TextureIdle = SpriteType.ArrowButtonLeft,
                                 TexturePressed = SpriteType.ArrowButtonLeft,
                             };
            _arrowLeft.OnLeftMouseButtonReleaseIn += ArrowLeftOnLeftMouseButtonReleaseIn;
            Add(_arrowLeft);

        }


        public void AddElement(SpriteType spriteType, string description, Type type)
        {

            Element element = new Element
                                  {
                                      Description = description,
                                      Type = type,
                                      SpriteType = spriteType
                                  };
            _elements.Add(element);
            ChangeButtonsAssigment();
        }


        private void ArrowLeftOnLeftMouseButtonReleaseIn(object sender)
        {
            _currentElement--;
            if (_currentElement < 0)
                _currentElement = _elements.Count - 1;

            ChangeButtonsAssigment();
        }

        private void ArrowRightOnLeftMouseButtonReleaseIn(object sender)
        {
            _currentElement++;
            if (_currentElement > _elements.Count)
                _currentElement = 0;

            ChangeButtonsAssigment();
        }



        private void ChangeButtonsAssigment()
        {
            int k = _currentElement;

            foreach (var element in _buttons)
            {
                Button b = element.Key;
                if (k < _elements.Count)
                {
                    b.TextureIdle = _elements[k].SpriteType;
                    b.TexturePressed = _elements[k].SpriteType;
                    k++;
                }
                else
                {
                    b.TextureIdle = SpriteType.Nothing;
                    b.TexturePressed = SpriteType.Nothing;
                }
            }
        }

        private void OnButtonRealeaseIn(object sender)
        {
            if (ElementPressed != null)
            {
                Button button = (Button) sender;
                int i = _buttons[button];
                ElementPressed(_elements[i + _currentElement].Type);
            }
        }
    }
}
