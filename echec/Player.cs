﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public abstract class Player
    {
        public abstract void Jouer();

        public Player(string couleur)
        {
            Color = couleur;
        }


       public Piece LastPiece { get; set; }
       public int[] LastPosition { get; set; }

        public string Color { get; private set; }

        public Player Clone()
        {
            Player clone;
            switch(this.ToString())
            {
                case "echec.Human":
                    clone = new Human(this.Color);
                    break;
                default:
                    clone = null;
                    break;
            }
            clone.LastPiece = this.LastPiece;
            clone.LastPosition = this.LastPosition;
            clone.Color = this.Color;


            return clone;
        }
    }
}
