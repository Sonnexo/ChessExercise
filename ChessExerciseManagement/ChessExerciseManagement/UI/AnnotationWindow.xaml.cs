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

        public AnnotationWindow() {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = true;

            Header = HeaderTextBox.Text;
            Task = TaskTextBox.Text;
            Captions = CaptionsTextBox.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
