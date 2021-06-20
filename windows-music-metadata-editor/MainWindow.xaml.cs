using Id3;
using System.IO;
using System.Windows;

namespace windows_music_metadata_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Tags
        {
            public string Title { get; set; }
            public string Artists { get; set; }
            public string Album { get; set; }
            public string Track { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            test();
        }

        public void test()
        {
            string path = @"";

            string[] musicFiles = Directory.GetFiles(path, "*.mp3");

            foreach (string musicFile in musicFiles)
            {
                using (var mp3 = new Mp3(musicFile))
                {
                    Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);

                    var tags = new Tags
                    {
                        Album = tag.Album,
                        Artists = tag.Artists,
                        Title = tag.Artists,
                        Track = tag.Track,
                    };
                }
            }
        }
    }
}
