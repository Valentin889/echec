using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace echec
{
    public class Fou : Piece
    {
        List<String> Renvoie;
        public Fou(string couleur)
            : base(couleur)
        {

        }
        public override List<string> DeplacementPossible(Jeu jeu)
        {
            Renvoie = new List<string>();

            return Renvoie;
        }
    }
}
