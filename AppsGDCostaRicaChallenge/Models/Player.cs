using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps_GD_Costa_Rica___Challenge.Models
{
    public class Player
    {
        #region Attributes

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _strategy;

        public string Strategy
        {
            get { return _strategy; }
            set { _strategy = value; }
        }

        private int _points;

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        private bool _winner;

        public bool Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }

        #endregion

        #region Methods

        public Player(string pName, int pPoints)
        {
            _name = pName;
            _points = pPoints;
        }
        public Player(string pName, string pStrategy)
        {
            _name = pName;
            _strategy = pStrategy;
            _winner = false;
        }
        #endregion
    }
}