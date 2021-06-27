using Id3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace windows_music_metadata_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Tags
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public string Title { get; set; }
            public string Artists { get; set; }
            public string Artist { get; set; }
            public string Album { get; set; }
            public string Track { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lbxFiles.Items.Clear();

            string path = @"";

            string[] musicFiles = Directory.GetFiles(path, "*.mp3");

            foreach (string musicFile in musicFiles)
            {
                using (var mp3 = new Mp3(musicFile))
                {
                    Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);

                    var tagObject = new Tags
                    {
                        FileName = Path.GetFileName(musicFile),
                        FilePath = musicFile
                    };

                    if (tag != null)
                    {
                        tagObject.Album = string.IsNullOrEmpty(tag.Album) ? "" : tag.Album.ToString();
                        tagObject.Artists = string.IsNullOrEmpty(tag.Artists) ? "" : tag.Artists.ToString();
                        tagObject.Artist = string.IsNullOrEmpty(tag.Band) ? "" : tag.Band.ToString();
                        tagObject.Title = string.IsNullOrEmpty(tag.Title) ? "" : tag.Title.ToString();
                        tagObject.Track = string.IsNullOrEmpty(tag.Track) ? "" : tag.Track.ToString();
                    }

                    lbxFiles.Items.Add(tagObject);
                }
            }
        }

        private void lbxFiles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Action<List<TextBox>> clearTextBoxes = lst
                => lst.ForEach(b => b.Text = "");

            clearTextBoxes(new List<TextBox>
            {
                tbxAlbum,
                tbxArtist,
                tbxCover,
                tbxTitle,
                tbxTrackNo,
                tbxYear
            });

            var tags = lbxFiles.SelectedItem as Tags;

            tbxAlbum.Text = tags.Album;
            tbxArtist.Text = tags.Artist;
            tbxTitle.Text = tags.Title;
            tbxTrackNo.Text = tags.Track;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Save tags here.
        }
    }
}
