using System.Windows;

using ChessExerciseManagement.Exercises;
using System.IO;

namespace ChessExerciseManagement.UI {
    public partial class SettingsWindow : Window {
        public SettingsWindow() {
            InitializeComponent();

            FenFolderTextBox.Text = Index.FenFolderPath;
            FileTextBox.Text = Index.FilePath;
            BaseFolderTextBox.Text = StorageManager.Basepath;
            DocumentFolderTextBox.Text = StorageManager.Outputdir;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e) {
            FenFolderTextBox.IsEnabled = true;
            FileTextBox.IsEnabled = true;
            BaseFolderTextBox.IsEnabled = true;
            DocumentFolderTextBox.IsEnabled = true;

            SaveButton.IsEnabled = true;
            ChangeButton.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (!validateInput()) {
                return;
            }

            FenFolderTextBox.IsEnabled = false;
            FileTextBox.IsEnabled = false;
            BaseFolderTextBox.IsEnabled = false;
            DocumentFolderTextBox.IsEnabled = false;

            SaveButton.IsEnabled = false;
            ChangeButton.IsEnabled = true;


            Index.FenFolderPath = FenFolderTextBox.Text;
            Index.FilePath = FileTextBox.Text;

            StorageManager.Basepath = BaseFolderTextBox.Text;
            StorageManager.Outputdir = DocumentFolderTextBox.Text;
        }

        private bool validateInput() {
            var str1 = FenFolderTextBox.Text;
            var str2 = FileTextBox.Text;
            var str3 = BaseFolderTextBox.Text;
            var str4 = DocumentFolderTextBox.Text;

            if (!Directory.Exists(str1)) {
                return false;
            }

            if (!File.Exists(str2)) {
                return false;
            }

            if (!Directory.Exists(str3)) {
                return false;
            }

            if (!Directory.Exists(str4)) {
                return false;
            }


            return true;
        }
    }
}
