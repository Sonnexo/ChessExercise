using System;
using System.IO;
using System.Text;

namespace ChessExerciseManagement.Exercises {
    public static class TexGenerator {
        public static void GenerateTexFile(string path, string header, string task, string[] imagePaths, string[] exerciseComments) {
            if (File.Exists(path)) {
                throw new FileLoadException("File already found at: " + path);
            }

            if (header == null || task == null
                || imagePaths == null || exerciseComments == null
                || imagePaths.Length != exerciseComments.Length) {
                throw new ArgumentException("No valid meta data");
            }

            int rows, columns;
            double imageWidth;

            switch (imagePaths.Length) {
                case 1:
                    rows = 1;
                    columns = 1;
                    imageWidth = 0.94;
                    break;
                case 4:
                    rows = 2;
                    columns = 2;
                    imageWidth = 0.47;
                    break;
                case 6:
                    rows = 3;
                    columns = 2;
                    imageWidth = 0.47;
                    break;
                case 9:
                    rows = 3;
                    columns = 3;
                    imageWidth = 0.31;
                    break;
                default:
                    throw new ArgumentException("Cannot process " + imagePaths.Length + " images.");
            }

            var sb = new StringBuilder();
            sb.AppendLine(@"\documentclass[10pt, a4paper]{article}")
                .AppendLine(@"\usepackage[utf8]{inputenc}")
                .AppendLine(@"\usepackage{graphicx}")
                .AppendLine(@"\usepackage{subcaption}")
                .AppendLine(@"\usepackage{caption}")
                .AppendLine(@"\usepackage{amsmath}")
                .AppendLine(@"\usepackage{amsfonts}")
                .AppendLine(@"\usepackage{amssymb}")
                .AppendLine(@"\usepackage[left = 2cm, right = 2cm, top = 2cm, bottom = 2cm]{geometry}")
                .AppendLine(@"\begin{document}")
                .AppendLine(@"\pagestyle{empty}")
                .AppendLine(@"\textbf{\huge " + header + @"}\\")
                .AppendLine(@"\\")
                .AppendLine(@"\noindent \textit{\Large " + task + @"}\\");

            for (var i = 0; i < rows; i++) {
                sb.AppendLine(@"\begin{figure}[h]");

                for (var j = 0; j < columns; j++) {
                    var texPath = imagePaths[j + i * columns].Replace('\\', '/');
                    var caption = exerciseComments[j + i * columns];

                    sb.AppendLine(@"\begin{subfigure}[t]{" + imageWidth + @"\textwidth}");
                    sb.AppendLine(@"\includegraphics[width=\textwidth]{" + texPath + "}");
                    sb.AppendLine(@"\captionsetup{font=Large}");
                    sb.AppendLine(@"\caption{" + caption + "}");
                    sb.AppendLine(@"\end{subfigure}\hfill");
                }

                sb.AppendLine(@"\end{figure}\\").AppendLine(@"\\ \\ \\ \\ \\");
            }

            sb.AppendLine(@"\end{document}");

            using (var sw = new StreamWriter(File.Create(path))) {
                sw.Write(sb.ToString());
            }
        }
    }
}
