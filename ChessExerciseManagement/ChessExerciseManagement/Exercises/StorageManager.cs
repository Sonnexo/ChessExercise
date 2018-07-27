using System;
using System.IO;

namespace ChessExerciseManagement.Exercises {
    public static class StorageManager {
        private static Random random = new Random();

        public static string AppDataPath {
            get;
            private set;
        }

        private static string basepath;
        public static string Basepath {
            get {
                return basepath;
            }
            set {
                if (Directory.Exists(value)) {
                    if (!value.EndsWith("\\", StringComparison.Ordinal)) {
                        value += "\\";
                    }

                    basepath = value;
                }
            }
        }

        private static string outputdir;
        public static string Outputdir {
            get {
                return outputdir;
            }
            set {
                if (Directory.Exists(value)) {
                    if (!value.EndsWith("\\", StringComparison.Ordinal)) {
                        value += "\\";
                    }

                    outputdir = value;
                }
            }
        }


        public static void Initialize() {
            InitializeAppDataPath();
            InitializeBasepath();
            InitializeOutputdir();
        }

        private static void InitializeAppDataPath() {
            AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        private static void InitializeBasepath() {
            Basepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private static void InitializeOutputdir() {
            Outputdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public static string GetNewTexPath() {
            return GetNewPath(".tex");
        }

        public static string GetNewPngPath() {
            return GetNewPath(".png");
        }

        public static string GetNewFenPath() {
            return GetNewPath(string.Empty, Index.FenFolderPath);
        }

        public static string GetNewOutputPath(string path) {
            return GetNewPath(".cee", path);
        }

        private static string GetNewPath(string ending, string bPath = "") {
            int rnd;
            return GetNewPath(ending, out rnd, bPath);
        }

        public static string GetNewTexPath(out int rnd) {
            return GetNewPath(".tex", out rnd);
        }

        public static string GetNewPngPath(out int rnd) {
            return GetNewPath(".png", out rnd);
        }

        public static string GetNewFenPath(out int rnd) {
            return GetNewPath(string.Empty, out rnd, Index.FenFolderPath);
        }

        private static string GetNewPath(string ending, out int rnd, string bPath = "") {
            if (!Directory.Exists(bPath)) {
                bPath = Basepath;
            }

            if (!bPath.EndsWith("\\", StringComparison.Ordinal)) {
                bPath += "\\";
            }

            var counter = 0;

            do {
                counter++;
                var num = random.Next();
                var path = bPath + num + ending;
                if (!File.Exists(path)) {
                    rnd = num;
                    return path;
                }

            } while (counter < 1000);

            throw new FileNotFoundException("Please clear the items in this location: " + bPath);
        }
    }
}
