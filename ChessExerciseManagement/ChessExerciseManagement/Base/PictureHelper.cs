using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ChessExerciseManagement.Base {
    public class PictureHelper : IDisposable {
        private static Dictionary<char, PictureHelper> m_dictionary = new Dictionary<char, PictureHelper>(12);
        public static PictureHelper GetPictureHelper(char key) => m_dictionary[key];

        private string m_path;
        public string Path => m_path;

        private Bitmap m_bitmap;
        public Bitmap Bitmap => m_bitmap;

        private BitmapImage m_bitmapimage;
        public BitmapImage BitmapImage => m_bitmapimage;

        private PictureHelper(string path) {
            m_path = path;
            m_bitmap = new Bitmap(path);
            m_bitmapimage = new BitmapImage(new Uri(path, UriKind.Absolute));
        }

        public static void AddPicture(string path, char key) {
            var ph = new PictureHelper(path);
            m_dictionary.Add(key, ph);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool flag) {
            m_bitmap.Dispose();
        }
    }
}
