using System;
using DungeonPlatformer.Helpers;
using Microsoft.Xna.Framework;
using PeoplePolder.Buildings;
using PeoplePolder.Creatures.Behaviors;
using PeoplePolder.Creatures.Fabrics;
using PeoplePolder.Creatures.PathFinding;
using PeoplePolder.Helpers;

namespace PeoplePolder.Creatures
{

    public enum CreatureAnimationState
    {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        Stay
    }

    public enum CreatureState
    {
        GatheringResourses,
        Working,
        Moving,
        Idle,

    }

    public enum CreatureKind
    {
        Human
    }

    public enum CreatureRelation
    {
        Neutral,
        Agressive,
        Friendly,
    }

    public delegate void PathFinishedEventHandler();

    public class Creature
    {

        public event PathFinishedEventHandler PathFinished;

        private AStar _aStar;

        protected CreatureManager CreatureManager { get; set; }
        protected GameField GameField { get; set; }
        protected BuildingManager BuildingManager { get; set; }
        private float _x,_y;


        public readonly ResoursesStorage ResoursesStorage;

        public CreatureAnimationState AnimationState;
        public CreatureState CreatureState { get; set; }
        public CreatureKind Kind;
        public CreatureRelation Relation;

        public IBehavior Behavior { get;protected set; }

        public AnimSprite MoveRightSprite;

        private float _elapsed = 0.0f;

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        public Point Cell
        {
            get
            {
                return new Point((int) Math.Round(X/Settings.CellSize),
                        (int) Math.Round(Y/Settings.CellSize));
            }
            set
            {
                X = value.X*Settings.CellSize;
                Y = value.Y*Settings.CellSize;
            }
        }

        public Creature(CreatureManager creatureManager, GameField gameField, BuildingManager buildingManager)
        {
            BuildingManager = buildingManager;
            CreatureManager = creatureManager;
            GameField = gameField;

            ResoursesStorage = new ResoursesStorage(50, 0);

            CreatureState = CreatureState.Idle;
            _aStar = new AStar(gameField, this);



            AnimationState = CreatureAnimationState.MoveRight;
        }

        public void SetBehaviour(IBehavior behavior)
        {
            Behavior = behavior;

            behavior.Creature = this;
            behavior.GameField = this.GameField;
            behavior.CreatureManager = this.CreatureManager;
            behavior.BuildingManager =this.BuildingManager;
        }

        public void MoveTo(Point cell)
        {
            _aStar.FoundWayAsync(this.Cell, cell);
            CreatureState = CreatureState.Moving;
        }
        public void GatherResourses() 
        {

        }

        public void Update(float dt)
        {

            if(Behavior != null)
                Behavior.Update(dt);

            int res = 0;
            switch (CreatureState)
            {
                case CreatureState.Moving:
                    if(_aStar.HasEnded)
                    {
                        Position += _aStar.Path.GetMoveOffset(Position, dt / 10);

                        if (_aStar.Path.Finished)
                        {
                            CreatureState = CreatureState.Idle;
                            if(PathFinished != null)
                            {
                                PathFinished();
                            }
                        }
                    }
                    break;
                case CreatureState.GatheringResourses:
                    _elapsed += dt;
                    if (_elapsed >= 15)
                    {
                        _elapsed = 0;
                        res = GameField.GetResourses(Cell, 1);

                        if (res == 0) 
                            CreatureState = CreatureState.Idle;
                        else
                            ResoursesStorage.AddResourse(Resourses.Wood, res);//todo: all other reses

                    }
                    break;
                case CreatureState.Working:
                    Building building = BuildingManager.GetInCell(Cell);
                    if(!building.DoWork(this, dt))
                        CreatureState = CreatureState.Idle;
                    break;
            }

            switch (AnimationState)
            {
                case CreatureAnimationState.MoveRight:
                    MoveRightSprite.Update(dt);
                    break;
            }
        }

        public virtual void Draw(float dt)
        {
            switch (AnimationState)
            {
                    case CreatureAnimationState.MoveRight:
                        TextureManager.DrawAnimationSprite(MoveRightSprite, Position, Color.White);
                    break;
            }
        }

    }
}
