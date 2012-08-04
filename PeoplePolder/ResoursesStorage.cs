using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeoplePolder
{
    public class ResoursesStorage
    {
        private Dictionary<Resourses, int> _resourses;



        public int Max { get; set; }

        public ResoursesStorage(int max, int startAmount)
        {
            _resourses = new Dictionary<Resourses, int>();

            _resourses.Add(Resourses.Wood, startAmount);
            _resourses.Add(Resourses.Board, startAmount);
            Max = max;
        }


        public int TotalResourseCount()
        {
            int sum = 0;
            for (int i = 0; i < _resourses.Count; i++)
            {
                sum += _resourses[(Resourses) i];
            }
            return sum;
        }

        public int CheckResourse(Resourses resourse)
        {
            return _resourses[resourse];
        }

        public void AddResourses(ResoursesStorage resoursesStorage, bool takeOff = false)
        {
            for (int i = 0; i < _resourses.Count; i++)
            {
                AddResourse((Resourses) i, resoursesStorage._resourses[(Resourses) i]);
            }
            if(takeOff)
                resoursesStorage.DiscardAllResourses();
        }

        public void DiscardAllResourses()
        {
            for (int i = 0; i < _resourses.Count; i++)
            {
                _resourses[(Resourses)i] = 0;
            }
        }
        public bool Overload()
        {
            return Max <= TotalResourseCount();
        }
        public bool Overload(Resourses resourse)
        {
            return Max <= _resourses[resourse];
        }

        public bool IsAbscent(Resourses resourse)
        {
            return _resourses[resourse] == 0;
        }

        public void AddResourse(Resourses resourse, int amount)
        {
            _resourses[resourse] += amount;
         //   if (Overload)
            //    _resourses[resourse] -= amount;

        }
        public int DiscardResourse(Resourses resourse, int amount)
        {
            if (_resourses[resourse] == 0) return 0;
            if (_resourses[resourse] - amount < 0)
            {
                amount -= _resourses[resourse];
                _resourses[resourse] = 0;
                return amount;
            }
            _resourses[resourse] -= amount;
           return amount;
        }
    }
}
