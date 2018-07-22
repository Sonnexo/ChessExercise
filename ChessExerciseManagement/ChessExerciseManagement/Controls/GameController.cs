using System.Text;
using System.Collections.Generic;

using ChessExerciseManagement.Models;
using ChessExerciseManagement.Models.Pieces;
using System;

namespace ChessExerciseManagement.Controls {
    public class GameController {
        public Game Game {
            private set;
            get;
        }

        public BoardController BoardController {
            private set;
            get;
        }

        public PlayerController White {
            get;
            set;
        }

        public PlayerController Black {
            get;
            set;
        }

        public GameController() {
            SetupBoard();
            AddStandardPieces();
        }

        public GameController(string fen) {
            SetupBoard();
            LoadPosition(fen);
        }

        private void SetupBoard() {
            BoardController = new BoardController(8, 8);

            White = new PlayerController(PlayerAffiliation.White);
            Black = new PlayerController(PlayerAffiliation.Black);

            BoardController.AddPlayer(White);
            BoardController.AddPlayer(Black);

            Game = new Game();
        }

        private void AddStandardPieces() {
            AddWhitePieces();
            AddBlackPieces();
        }

        private void ParseFen(string fen) {
            var fenComps = fen.Split(' ');
            LoadPosition(fenComps[0]);

            if (fenComps.Length > 1) {
                Game.WhosTurn = fenComps[1].Equals("w") ? PlayerAffiliation.White : PlayerAffiliation.Black;
            } else {
                Game.WhosTurn = PlayerAffiliation.White;
            }

            if (fenComps.Length > 2) {
                White.Player.MayCastleShort = fenComps[2].Contains("K");
                White.Player.MayCastleLong = fenComps[2].Contains("Q");
                Black.Player.MayCastleShort = fenComps[2].Contains("k");
                White.Player.MayCastleLong = fenComps[2].Contains("q");
            } else {
                White.Player.MayCastleShort = false;
                White.Player.MayCastleLong = false;
                Black.Player.MayCastleShort = false;
                White.Player.MayCastleLong = false;
            }

            if (fenComps.Length > 4) {
                Game.HalfmovesSinceLastCaptureOrPawn = int.Parse(fenComps[4]);
            } else {
                Game.HalfmovesSinceLastCaptureOrPawn = 0;
            }

            if (fenComps.Length > 5) {
                Game.Movecounter = int.Parse(fenComps[5]);
            } else {
                Game.Movecounter = 1;
            }
        }

        private void LoadPosition(string fen) {
            var fieldcodes = GenFieldCodes(fen);

            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    var c = fieldcodes[x, y];
                    if (c == '-') {
                        continue;
                    }

                    var player = char.IsUpper(c) ? White : Black;
                    var field = BoardController.FieldControllers[x, y];

                    PieceController piece = null;
                    var tmpC = char.ToLower(c);
                    switch (tmpC) {
                        case 'r':
                            piece = new PieceController(new Rook(), player, field);
                            break;
                        case 'n':
                            piece = new PieceController(new Knight(), player, field);
                            break;
                        case 'b':
                            piece = new PieceController(new Bishop(), player, field);
                            break;
                        case 'q':
                            piece = new PieceController(new Queen(), player, field);
                            break;
                        case 'k':
                            piece = new PieceController(new King(), player, field);
                            break;
                        case 'p':
                            piece = new PieceController(new Pawn(), player, field);
                            break;
                    }

                    player.AddPiece(piece);
                }
            }


        }

        public void Flip() {
            Game.WhosTurn = Game.WhosTurn == PlayerAffiliation.White ? PlayerAffiliation.Black : PlayerAffiliation.White;
        }

        private void AddWhitePieces() {
            var fields = BoardController.FieldControllers;

            var rook1 = new PieceController(new Rook(), White, fields[0, 0]);
            var rook2 = new PieceController(new Rook(), White, fields[7, 0]);

            var knight1 = new PieceController(new Knight(), White, fields[1, 0]);
            var knight2 = new PieceController(new Knight(), White, fields[6, 0]);

            var bishop1 = new PieceController(new Bishop(), White, fields[2, 0]);
            var bishop2 = new PieceController(new Bishop(), White, fields[5, 0]);

            var queen = new PieceController(new Queen(), White, fields[3, 0]);
            var king = new PieceController(new King(), White, fields[4, 0]);

            var pawn1 = new PieceController(new Pawn(), White, fields[0, 1]);
            var pawn2 = new PieceController(new Pawn(), White, fields[1, 1]);
            var pawn3 = new PieceController(new Pawn(), White, fields[2, 1]);
            var pawn4 = new PieceController(new Pawn(), White, fields[3, 1]);
            var pawn5 = new PieceController(new Pawn(), White, fields[4, 1]);
            var pawn6 = new PieceController(new Pawn(), White, fields[5, 1]);
            var pawn7 = new PieceController(new Pawn(), White, fields[6, 1]);
            var pawn8 = new PieceController(new Pawn(), White, fields[7, 1]);

            White.AddPiece(rook1);
            White.AddPiece(rook2);
            White.AddPiece(knight1);
            White.AddPiece(knight2);
            White.AddPiece(bishop1);
            White.AddPiece(bishop2);
            White.AddPiece(queen);
            White.AddPiece(king);
            White.AddPiece(pawn1);
            White.AddPiece(pawn2);
            White.AddPiece(pawn3);
            White.AddPiece(pawn4);
            White.AddPiece(pawn5);
            White.AddPiece(pawn6);
            White.AddPiece(pawn7);
            White.AddPiece(pawn8);
        }

        private void AddBlackPieces() {
            var fields = BoardController.FieldControllers;

            var rook1 = new PieceController(new Rook(), Black, fields[0, 7]);
            var rook2 = new PieceController(new Rook(), Black, fields[7, 7]);

            var knight1 = new PieceController(new Knight(), Black, fields[1, 7]);
            var knight2 = new PieceController(new Knight(), Black, fields[6, 7]);

            var bishop1 = new PieceController(new Bishop(), Black, fields[2, 7]);
            var bishop2 = new PieceController(new Bishop(), Black, fields[5, 7]);

            var queen = new PieceController(new Queen(), Black, fields[3, 7]);
            var king = new PieceController(new King(), Black, fields[4, 7]);

            var pawn1 = new PieceController(new Pawn(), Black, fields[0, 6]);
            var pawn2 = new PieceController(new Pawn(), Black, fields[1, 6]);
            var pawn3 = new PieceController(new Pawn(), Black, fields[2, 6]);
            var pawn4 = new PieceController(new Pawn(), Black, fields[3, 6]);
            var pawn5 = new PieceController(new Pawn(), Black, fields[4, 6]);
            var pawn6 = new PieceController(new Pawn(), Black, fields[5, 6]);
            var pawn7 = new PieceController(new Pawn(), Black, fields[6, 6]);
            var pawn8 = new PieceController(new Pawn(), Black, fields[7, 6]);

            Black.AddPiece(rook1);
            Black.AddPiece(rook2);
            Black.AddPiece(knight1);
            Black.AddPiece(knight2);
            Black.AddPiece(bishop1);
            Black.AddPiece(bishop2);
            Black.AddPiece(queen);
            Black.AddPiece(king);
            Black.AddPiece(pawn1);
            Black.AddPiece(pawn2);
            Black.AddPiece(pawn3);
            Black.AddPiece(pawn4);
            Black.AddPiece(pawn5);
            Black.AddPiece(pawn6);
            Black.AddPiece(pawn7);
            Black.AddPiece(pawn8);
        }

        public string GetFen() {
            var sb = new StringBuilder();

            sb.Append(BoardController.GetFenCode(FenMode.Classical));
            sb.Append(" ");

            sb.Append(Game.WhosTurn == PlayerAffiliation.White ? 'w' : 'b');
            sb.Append(" ");

            var fenWhite = White.GetFenCastle();
            var fenBlack = Black.GetFenCastle();

            if (fenWhite == string.Empty && fenBlack == string.Empty) {
                sb.Append("-");
            } else {
                sb.Append(fenWhite);
                sb.Append(fenBlack);
            }
            sb.Append(" ");

            sb.Append("-");
            sb.Append(" ");

            sb.Append(Game.HalfmovesSinceLastCaptureOrPawn);
            sb.Append(" ");

            sb.Append(Game.Movecounter);
            sb.Append(" ");

            return sb.ToString();
        }

        private char[,] GenFieldCodes(string fen) {
            var ranks = fen.Split('/');

            if (ranks.Length == 8) {
                return GetFieldCodesNormalFen(ranks);
            }

            var breakSymbols = new[] { '\\', '_', '-', '/' };
            var lines = fen.Split(breakSymbols);

            var listOfLegalChars = new List<char>() {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'r', 'R', 'n', 'N', 'b', 'B', 'q', 'Q', 'k', 'K', 'p', 'P'
            };

            var tmpCounter = 0;
            var sb = new StringBuilder(64);

            foreach (var c in lines[0]) {
                if (!listOfLegalChars.Contains(c)) {
                    continue;
                }

                if (int.TryParse(c.ToString(), out int dummy)) {
                    tmpCounter *= 10;
                    tmpCounter += dummy;
                } else {
                    if (tmpCounter != 0) {
                        for (var i = 0; i < tmpCounter; i++) {
                            sb.Append('-');
                        }

                        tmpCounter = 0;
                    }

                    sb.Append(c);
                }
            }

            if (tmpCounter != 0) {
                for (var i = 0; i < tmpCounter; i++) {
                    sb.Append('-');
                }
            }

            var code = sb.ToString();
            var fieldcodes = new char[8, 8];

            for (var y = 0; y < 8; y++) {
                for (var x = 0; x < 8; x++) {
                    var idx = (7 - y) * 8 + x;
                    fieldcodes[x, y] = code[idx];
                }
            }


            return fieldcodes;
        }

        private char[,] GetFieldCodesNormalFen(string[] ranks) {
            var fieldcodes = new char[8, 8];

            for (var y = 0; y < 8; y++) {
                var pointer = 0;
                for (var x = 0; x < 8; x++) {

                    var c = ranks[7 - y][pointer];

                    if (byte.TryParse(c.ToString(), out byte b)) {
                        var oldX = x;
                        for (var i = oldX; i < b + oldX; i++) {
                            fieldcodes[i, y] = '-';
                            x++;
                        }
                        x--;
                        pointer++;
                    } else {
                        fieldcodes[x, y] = c;
                        pointer++;
                    }
                }
            }

            return fieldcodes;
        }


    }
}
