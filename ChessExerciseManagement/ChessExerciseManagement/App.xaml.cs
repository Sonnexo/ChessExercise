﻿using ChessExerciseManagement.Exercises;
using System.Windows;

namespace ChessExerciseManagement {
    public partial class App : Application {
        public App() {
            StorageManager.Initialize();
            Index.Load();
        }
    }
}
