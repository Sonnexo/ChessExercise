using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Controls {
    public class BoardController {
        private static Color whiteColor = Color.White;
        private static Color blackColor = Color.LightGray;

        public Board Board {
            get;
        }

        public FieldController[,] FieldControllers {
            get;
        } = new FieldController[8, 8];

        public BoardController(int width, int height) {
            if (width != 8 || height != 8) {
                throw new ArgumentException("A chess board is 8x8 fields.");
            }

            Board = new Board(width, height);

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    var fc = new FieldController(x, y);
                    FieldControllers[x, y] = fc;
                    Board.Fields[x, y] = fc.Field;
                }
            }
        }


        public void AddPlayer(PlayerController playerController) {
            Board.Players.Add(playerController.Player);
        }


        public Bitmap GetImage() {
            var images = GetImages();
            return MergePictures(images);
        }

        private Image[,] GetImages() {
            var images = new Image[8, 8];

            for (int x = 0; x < 8; x++) {
                for (int y = 0; y < 8; y++) {
                    var flag = x % 2 != y % 2;
                    var col = flag ? whiteColor : blackColor;
                    var field = FieldControllers[x, y];

                    var image = new Bitmap(100, 100);

                    using (var graphics = Graphics.FromImage(image)) {
                        graphics.Clear(col);

                        var val = field.PieceController?.GetBitmap();
                        if (val != null) {
                            graphics.DrawImage(val, new Rectangle(0, 0, 100, 100), new Rectangle(0, 0, 200, 200), GraphicsUnit.Pixel);
                        }
                    }

                    images[x, y] = image;
                }
            }


            return images;
        }

        private static Bitmap MergePictures(Image[,] images) {
            var outputBitmap = new Bitmap(800, 800, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(outputBitmap)) {
                for (var i = 0; i < 8; i++) {
                    for (var j = 0; j < 8; j++) {
                        var img = images[i, 7 - j];
                        if (img == null) {
                            continue;
                        }

                        graphics.DrawImage(img, new PointF(100 * i, 100 * j));
                    }
                }
            }

            return outputBitmap;
        }


        private string GetFenCodeClassical() {
            var sb = new StringBuilder();

            for (var y = 7; y >= 0; y--) {
                var emptyFieldCounter = 0;

                for (int x = 0; x < 8; x++) {
                    var field = FieldControllers[x, y];
                    var piece = field.Field.Piece;

                    if (piece == null) {
                        emptyFieldCounter++;
                        continue;
                    }

                    if (emptyFieldCounter != 0) {
                        sb.Append(emptyFieldCounter);
                        emptyFieldCounter = 0;
                    }

                    sb.Append(piece.FenChar);
                }

                if (emptyFieldCounter != 0) {
                    sb.Append(emptyFieldCounter);
                }
                if (y != 0) {
                    sb.Append('/');
                }
            }
            return sb.ToString();
        }

        private string GetFenCodeJonas() {
            var sb = new StringBuilder();

            var emptyFieldCounter = 0;
            for (var y = 7; y >= 0; y--) {
                for (int x = 0; x < 8; x++) {
                    var field = FieldControllers[x, y];
                    var piece = field.Field.Piece;

                    if (piece == null) {
                        emptyFieldCounter++;
                        continue;
                    }

                    if (emptyFieldCounter != 0) {
                        sb.Append(emptyFieldCounter);
                        emptyFieldCounter = 0;
                    }

                    sb.Append(piece.FenChar);
                }
            }

            if (emptyFieldCounter != 0) {
                sb.Append(emptyFieldCounter);
            }

            return sb.ToString();
        }

        public string GetFenCode(FenMode mode) {
            switch (mode) {
                case FenMode.Classical:
                    return GetFenCodeClassical();
                case FenMode.Jonas:
                    return GetFenCodeJonas();
            }

            return string.Empty;
        }

        public static List<Field> GetAttackedFields(Player player, bool v) {
            throw new NotImplementedException();
        }
    }
}
