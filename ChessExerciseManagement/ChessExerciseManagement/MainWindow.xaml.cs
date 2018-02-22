﻿using System.Windows;

using ChessExerciseManagement.UI;

namespace ChessExerciseManagement {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void MoveButton_Click(object sender, RoutedEventArgs e) {
            var trainingWindow = new TrainingWindow();
            trainingWindow.ShowDialog();
        }

        private void FenButton_Click(object sender, RoutedEventArgs e) {
            var fenWindow = new FenWindow();
            fenWindow.ShowDialog();
        }

        private void ExploreButton_Click(object sender, RoutedEventArgs e) {
            var exploreWindow = new ExploreWindow();
            exploreWindow.ShowDialog();
        }
    }
}
