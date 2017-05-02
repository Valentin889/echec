using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using echec;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace TestEchec
{
    [TestClass]
    public class UnitTest1
    {
        private Piece[][] tabPiece;
        private Dictionary<string, Piece> dicWhitePiece;
        private Dictionary<string, Piece> dicBlackPiece;
        private const string Color1 = "White";
        private const string Color2 = "Black";
        [TestMethod]
        public void TestCheckmate()
        {
            Game game = new Game(new frmGame());
            game.CreatePlayer(new Human(Color1),new Human(Color2));
            dicWhitePiece = new Dictionary<string, Piece>();
            dicBlackPiece = new Dictionary<string, Piece>();
            dicWhitePiece.Add("KingWhite", new King(Color1));
            dicBlackPiece.Add("QueenBlack", new Queen(Color2));
            dicBlackPiece.Add("RookBlack", new Rook(Color2));
            dicBlackPiece.Add("KingBlack", new King(Color2));

            dicWhitePiece["KingWhite"].SetPosition(new int[] { 7, 4 });
            dicBlackPiece["QueenBlack"].SetPosition(new int[] { 7, 7 });
            dicBlackPiece["RookBlack"].SetPosition(new int[] { 6, 7 });
            dicBlackPiece["KingBlack"].SetPosition(new int[] { 0, 4 });
            game.DicBlackPiece = dicBlackPiece;
            game.DicWhitePiece = dicWhitePiece;
            game.FillTablePiece();
            game.Players[0].LastPiece = dicBlackPiece["QueenBlack"];
            Assert.AreEqual( true, game.IsCheckmat(Color1));
        }
        [TestMethod]
        public void TestDraw1()
        {
            Game game = new Game(new frmGame());
            game.CreatePlayer(new Human(Color1), new Human(Color2));
            dicWhitePiece = new Dictionary<string, Piece>();
            dicBlackPiece = new Dictionary<string, Piece>();
            dicWhitePiece.Add("KingWhite", new King(Color1));
            dicBlackPiece.Add("QueenBlack", new Queen(Color2));
            dicBlackPiece.Add("Rook1Black", new Rook(Color2));
            dicBlackPiece.Add("Rook2Black", new Rook(Color2));
            dicBlackPiece.Add("KingBlack", new King(Color2));

            dicWhitePiece["KingWhite"].SetPosition(new int[] { 7, 4 });
            dicBlackPiece["QueenBlack"].SetPosition(new int[] { 6, 7 });
            dicBlackPiece["Rook1Black"].SetPosition(new int[] { 1, 3 });
            dicBlackPiece["Rook2Black"].SetPosition(new int[] { 1, 5 });
            dicBlackPiece["KingBlack"].SetPosition(new int[] { 0, 4 });
            game.DicBlackPiece = dicBlackPiece;
            game.DicWhitePiece = dicWhitePiece;
            game.FillTablePiece();

            Assert.AreEqual(true, game.IsDraw(Color1));
        }
    }
}
