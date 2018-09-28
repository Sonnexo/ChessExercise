using System;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class AnnotationWindow : Window {
        public string Header {
            get;
            private set;
        }

        public string Task {
            get;
            private set;
        }

        public string[] Captions {
            get;
            private set;
        }

        private string mes = string.Empty;
        private int numberOfItems = 0;

        public AnnotationWindow(int numberItems) {
            InitializeComponent();
            numberOfItems = numberItems;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {

            Header = HeaderTextBox.Text;
            Task = TaskTextBox.Text;
            Captions = CaptionsTextBox.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (string.IsNullOrWhiteSpace(Header)) {
                mes = "You have not entered a valid header.";
            } else if (string.IsNullOrWhiteSpace(Task)) {
                mes = "You have not entered a valid task.";
            } else if (Captions.Length != numberOfItems) {
                mes = "You have not entered a valid number of captions.";
            } else {
                mes = string.Empty;
            }

            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (string.IsNullOrWhiteSpace(mes)) {
                DialogResult = true;
                return;
            }

            MessageBox.Show(mes);
            e.Cancel = true;
            DialogResult = false;
        }
    }
}
