using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ChessExerciseManagement.Exercises {
    public static class ExerciseManager {

        private static Dictionary<string, string> m_translator;

        private static Dictionary<string, List<string>> m_exercises;
        public static Dictionary<string, List<string>> Exercises {
            set {
                if (value != null) {
                    m_exercises = value;

                    m_translator = new Dictionary<string, string>();
                    foreach (var val in value.Values) {
                        foreach (var path in val) {
                            var readFen = File.ReadAllText(path);
                            if (!m_translator.Keys.Contains(readFen)) {
                                m_translator.Add(readFen, path);
                            }
                        }
                    }

                }
            }
            get {
                return m_exercises;
            }
        }

        public static List<string> Keys {
            get {
                var keys = Exercises.Keys.ToList();
                keys.RemoveAll(x => x.Length == 0);
                return keys;
            }
        }

        public static IEnumerable<string> Filter(List<string> keywords) {
            if (keywords == null) {
                return new List<string>();
            }

            IEnumerable<string> list = Exercises[string.Empty];

            foreach (var keyword in keywords) {
                if (!Exercises.ContainsKey(keyword)) {
                    return new List<string>();
                }

                var annotatedList = Exercises[keyword];
                list = list.Where(l => annotatedList.Contains(l));
            }

            return list;
        }

        public static void AddExercise(string path, List<string> keywords) {
            var alreayExisting = Exercises[string.Empty];
            if (path == null || !File.Exists(path) || alreayExisting.Contains(path)) {
                return;
            }

            alreayExisting.Add(path);
            m_translator.Add(File.ReadAllText(path), path);

            foreach (var keyword in keywords) {
                if (!Exercises.ContainsKey(keyword)) {
                    Exercises.Add(keyword, new List<string>());
                }

                var list = Exercises[keyword];
                list.Add(path);
            }
        }

        public static void Export(string path) {
            if (!Directory.Exists(path)) {
                return;
            }

            var filePath = StorageManager.GetNewOutputPath(path);
            var keys = Exercises.Keys.ToArray();

            var dict = new Dictionary<string, List<int>>();

            for (var i = 0; i < keys.Length; i++) {
                var key = keys[i];

                var exs = Exercises[key];
                foreach (var ex in exs) {
                    if (!dict.ContainsKey(ex)) {
                        dict.Add(ex, new List<int>());
                    }

                    dict[ex].Add(i);
                }
            }

            using (var file = File.Create(filePath)) {
                using (var writer = new StreamWriter(file)) {
                    writer.WriteLine("keys:");

                    for (var i = 0; i < keys.Length; i++) {
                        var key = keys[i];
                        writer.WriteLine(i + ":" + key);
                    }

                    writer.WriteLine();
                    writer.WriteLine("fens:");

                    foreach (var key in dict.Keys) {

                        var l = File.ReadAllText(key);

                        writer.Write(l + ": ");
                        foreach (var ind in dict[key]) {
                            writer.Write(ind + ",");
                        }
                        writer.WriteLine();
                    }

                    writer.Flush();
                }
            }
        }

        public static void Import(string fileName) {
            var allLines = File.ReadAllLines(fileName);

            var phase = 0;
            var keys = new List<string>();
            var dict = new Dictionary<string, List<string>>();

            for (var i = 0; i < allLines.Length; i++) {
                switch (phase) {
                    case 0:
                        if (allLines[i].Equals("keys:")) {
                            phase = 1;
                        }
                        break;
                    case 1:
                        var lineParts1 = allLines[i].Split(new char[] { ':' });
                        if (lineParts1.Length == 2) {
                            keys.Add(lineParts1[1]);
                        } else {
                            phase = 2;
                        }
                        break;
                    case 2:
                        if (allLines[i].Equals("fens:")) {
                            phase = 3;
                        }
                        break;
                    case 3:
                        var lineParts3 = allLines[i].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                        var exercise = lineParts3[0];
                        var exercisePath = FindExercisePath(exercise);
                        var exerciseKeys = lineParts3[1].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var k in exerciseKeys) {
                            var key = keys[int.Parse(k)];
                            if (!dict.Keys.Contains(key)) {
                                dict.Add(key, new List<string>());
                            }

                            dict[key].Add(exercisePath);
                        }

                        break;
                }
            }

            MergeIndices(dict);
        }

        private static string FindExercisePath(string fen) {
            if (m_translator.Keys.Contains(fen)) {
                return m_translator[fen];
            }

            var newPath = StorageManager.GetNewFenPath();
            File.WriteAllText(newPath, fen);

            return newPath;
        }

        private static void MergeIndices(Dictionary<string, List<string>> dict) {
            foreach (var key in dict.Keys) {
                if (!Exercises.ContainsKey(key)) {
                    Exercises.Add(key, dict[key]);
                    continue;
                }

                var existingList = Exercises[key];
                var newList = dict[key];

                foreach (var newItem in newList) {
                    if (!existingList.Contains(newItem)) {
                        existingList.Add(newItem);

                        var keywords = new List<string>();
                        foreach(var k in dict.Keys) {
                            if(dict[k].Contains(newItem)) {
                                keywords.Add(k);
                            }
                        }

                        Index.AddFile(newItem, keywords);
                    }
                }
            }
        }
    }
}
