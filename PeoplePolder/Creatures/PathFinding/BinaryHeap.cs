using System;

namespace PeoplePolder.Creatures.PathFinding
{
    class BinaryHeap
    {
        public WayPoint[] List;
        public int Count = 0;

        public BinaryHeap()
        {
            List = new WayPoint[Count];
        }
        public void Clear()
        {
            if (List.Length != 1)
            {
                Array.Resize<WayPoint>(ref List, 0);
                Count = 0;
            }
        }

        public WayPoint Get()
        {
            return List[1];
        }
        public WayPoint Get(int index)
        {
            return List[index];
        }

        public void Add(WayPoint point)
        {
            Count++;
            Array.Resize<WayPoint>(ref List, Count + 1);
            List[Count] = point;
            int CurrentIndex = Count;
            int parentIndex = CurrentIndex / 2;

            while (CurrentIndex != 1)
            {
                parentIndex = CurrentIndex / 2;
                if (List[parentIndex] == null)
                {
                    WayPoint tmpValue = this.List[parentIndex];
                    this.List[parentIndex] = this.List[CurrentIndex];
                    this.List[CurrentIndex] = tmpValue;
                    CurrentIndex = parentIndex;
                }
                else if (this.List[CurrentIndex].Cost <= List[parentIndex].Cost)
                {
                    WayPoint tmpValue = this.List[parentIndex];
                    this.List[parentIndex] = this.List[CurrentIndex];
                    this.List[CurrentIndex] = tmpValue;
                    CurrentIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }

        }

        public void Remove()
        {
            Count--;
            List[1] = List[Count + 1];
            Array.Resize<WayPoint>(ref List, Count + 1);
            int swapIndex = 1, parentIndex = 1;
            do
            {
                parentIndex = swapIndex;
                if ((2 * parentIndex + 1) <= List.Length - 1)
                {
                    // Both children exist
                    if (List[parentIndex].Cost >= List[2 * parentIndex].Cost)
                    {
                        swapIndex = 2 * parentIndex;
                    }
                    if (List[swapIndex].Cost >= List[2 * parentIndex + 1].Cost)
                    {
                        swapIndex = 2 * parentIndex + 1;
                    }
                }
                else if ((2 * parentIndex) <= List.Length - 1)
                {
                    if (List[parentIndex].Cost >= List[2 * parentIndex].Cost)
                    {
                        swapIndex = 2 * parentIndex;
                    }
                }
                if (parentIndex != swapIndex)
                {
                    WayPoint tmp = List[parentIndex];
                    List[parentIndex] = List[swapIndex];
                    List[swapIndex] = tmp;
                }
            } while (parentIndex != swapIndex);
        }
    }
}