using ChessExerciseManagement.Controls;
using ChessExerciseManagement.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessExerciseManagement.UI {
    public partial class ViewWindow : Window {
        public ViewWindow(string path) {
            InitializeComponent();

            var fen = File.ReadAllText(path);

            var gc = new GameController(fen, FenMode.Jonas);
            var bc = gc.BoardController;

            BoardView.SetReadonly(true);
            BoardView.BoardController = bc;
        }

        private void SavePictureButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Portable Network Graphigs (*.png)|*.png";

            if (saveFileDialog.ShowDialog() == true) {
                var boardView = BoardView;

                var renderTargetBitmap = new RenderTargetBitmap(800, 800, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(boardView);
                var pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                var path = saveFileDialog.FileName;

                using (var stream = File.Create(path)) {
                    pngImage.Save(stream);
                }
            }
        }

    }
}
