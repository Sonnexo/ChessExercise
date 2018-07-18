using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

using ChessExerciseManagement.Models;
using ChessExerciseManagement.Controls;
using ChessExerciseManagement.Exercises;

namespace ChessExerciseManagement.UI {
    public partial class TrainingWindow : Window {
        public static GameController GameController {
            private set;
            get;
        }

        public TrainingWindow() {
            InitializeComponent();
            SetNewGame();
        }

        private void SetNewGame() {
            GameController = new GameController();
            BoardView.BoardController = GameController.BoardController;
        }

        private void SetNewGame(string fen) {
            GameController = new GameController(fen, FenMode.Jonas);
            BoardView.BoardController = GameController.BoardController;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e) {
            SetNewGame();
        }

        private void SaveFenButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "FEN files (*.fen)|*.fen";

            if (saveFileDialog.ShowDialog() == true) {
                var fen = GameController.GetFen();
                File.WriteAllText(saveFileDialog.FileName, fen);
            }
        }

        private void LoadFenButton_Click(object sender, RoutedEventArgs e) {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FEN files (*.fen)|*.fen";
            if (openFileDialog.ShowDialog() == true) {
                var fenPath = openFileDialog.FileName;
                var fen = File.ReadAllText(fenPath);

                SetNewGame(fen);
            }
        }

        private void SavePictureButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Portable Network Graphigs (*.png)|*.png";

            if (saveFileDialog.ShowDialog() == true) {
                var board = BoardView;

                var renderTargetBitmap = new RenderTargetBitmap(800, 800, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(board);
                var pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                var path = saveFileDialog.FileName;

                using (var stream = File.Create(path)) {
                    pngImage.Save(stream);
                }
            }
        }

        private void SaveExerciseButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "FEN files (*.fen)|*.fen";

            if (saveFileDialog.ShowDialog() == true) {
                var fen = GameController.GetFen();
                var filename = saveFileDialog.FileName;
                File.WriteAllText(filename, fen);

                var keywordWindow = new KeywordWindow();
                keywordWindow.ShowDialog();

                var keywords = keywordWindow.Keywords;

                Index.AddFile(filename, keywords);
                ExerciseManager.AddExercise(filename, keywords);
            }
        }
    }
}
