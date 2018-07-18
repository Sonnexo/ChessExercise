using System;
using System.IO;

namespace ChessExerciseManagement.Exercises {
    public static class StorageManager {
        private static Random random = new Random();

        private static string basepath = @"C:\Users\fczappa\Desktop\";
        public static string Basepath {
            get {
                return basepath;
            }
        }

        private static string outputdir = @"C:\Users\fczappa\Documents\";
        public static string Outputdir {
            get {
                return outputdir;
            }
        }


        public static void Initialize() {

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

            throw new Exception("Please clear the items in this location: " + bPath);
        }
    }
}
