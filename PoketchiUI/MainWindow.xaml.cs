using PoketchiCore.Controller;
using PoketchiCore.Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace PoketchiUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += async (_, _) =>
            {
                Egg.OnHatched += () =>
                {
                    Dispatcher.Invoke(UpdateEggToTamagotchi);
                };

                await InitializeCore();

                if(Egg.IsHatched) GlobalTimer.Status();

                Tamagotchi.OnStatusUpdated += () =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        UpdateVisuals();
                    });
                };
                
                UpdateVisuals();

                Tamagotchi.HealChargesChanged += UpdateHealImage;

                UpdateHealImage();

                _timer.Tick += (s, e) =>
                {
                    UpdateHealthBlocks();
                };
                _timer.Start();

                Tamagotchi.OnEvolve += () =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        UpdateTamagotchiEvolution();
                    });
                };

                Tamagotchi.OnDeath += () =>
                {
                    if (!Dispatcher.HasShutdownStarted && !Dispatcher.HasShutdownFinished)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            UpdateDeathVisuals();
                        });
                    }
                };
                UpdateVisibilityAfterEggHatch();
            };
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri(FilePath.Get("Sprites", "background.png"), UriKind.Relative));

            MainGrid.Background = imageBrush;
        }
        private async Task InitializeCore()
        {

            DotNetEnv.Env.Load(FilePath.Get("", "config.env"));

            var hasTamagotchiBkp = File.Exists(FilePath.Get("BackUp", "tbkp.dat"));
            var hasEggBkp = File.Exists(FilePath.Get("BackUp", "ebkp.dat"));

            if (hasTamagotchiBkp) Tamagotchi.Storage(StorageMode.Load);
            if (hasEggBkp) 
            {
                Egg.Storage(StorageMode.Load);
                Egg.CheckHatchAfterLoad();
            }
            else await Egg.Create();

            var tamagotchiGifPath = FilePath.Get("Sprites", "sprite.gif");
            var tamagotchiImage = new BitmapImage();
            tamagotchiImage.BeginInit();
            tamagotchiImage.UriSource = new Uri(tamagotchiGifPath, UriKind.Absolute);
            tamagotchiImage.EndInit();

            ImageBehavior.SetAnimatedSource(TamagotchiImage, tamagotchiImage);

            var eggGifPath = FilePath.Get("Sprites", "egg.gif");
            var eggImage = new BitmapImage();
            eggImage.BeginInit();
            eggImage.UriSource = new Uri(eggGifPath, UriKind.Absolute);
            eggImage.EndInit();

            ImageBehavior.SetAnimatedSource(EggImage, eggImage);

            TamagotchiLabel.Content = Egg.IsHatched ? Tamagotchi.Name : "Egg";
            TamagotchiHealth.Value = Tamagotchi.Health;
            HeigthValue.Content = Tamagotchi.Height;
            WeigthValue.Content = Tamagotchi.Weight;
            
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                Tamagotchi.Storage(StorageMode.Save);
                Egg.Storage(StorageMode.Save);
            };
        }
        private void UpdateVisibilityAfterEggHatch()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(UpdateVisibilityAfterEggHatch);
                return;
            }

            if (Egg.IsHatched)
            {
                TamagotchiHealth.Visibility = Visibility.Visible;
                HealthLabel.Visibility = Visibility.Visible;
                MoodLabel.Visibility = Visibility.Visible;
                MoodValue.Visibility = Visibility.Visible;
                AppetiteLabel.Visibility = Visibility.Visible;
                AppetiteValue.Visibility = Visibility.Visible;
                HygieneLabel.Visibility = Visibility.Visible;
                HygieneValue.Visibility = Visibility.Visible;
                HeigthValue.Visibility = Visibility.Visible;
                WeigthValue.Visibility = Visibility.Visible;
                TamagotchiHeight.Visibility = Visibility.Visible;
                TamagotchiWeight.Visibility = Visibility.Visible;
                TamagotchiHealth.Visibility = Visibility.Visible;
                btnBath.Visibility = Visibility.Visible;
                btnFeed.Visibility = Visibility.Visible;
                btnHeal.Visibility = Visibility.Visible;
                btnPlay.Visibility = Visibility.Visible;
                btnTrain.Visibility = Visibility.Visible;
                TamagotchiImage.Visibility = Visibility.Visible;
                HealImage.Visibility = Visibility.Visible;
            }
        }
        private void UpdateEggToTamagotchi()
        {
            TamagotchiLabel.Content = Tamagotchi.Name;
            TamagotchiImage.Visibility = Visibility.Visible;
            EggImage.Visibility = Visibility.Hidden;
            UpdateVisibilityAfterEggHatch();
        }
        private readonly DispatcherTimer _timer = new()
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        private void UpdateHealthBlocks()
        {
            const int totalBlocks = 28;
            int filled = (int)(Tamagotchi.Health / 100.0 * totalBlocks);

            var blocks = new List<int>();
            for (int i = 0; i < filled; i++)
                blocks.Add(1);

            var bar = TamagotchiHealth.Template.FindName("BlocksPanel", TamagotchiHealth) as ItemsControl;
            if (bar != null)
            {
                bar.ItemsSource = blocks;
            }
        }
        private void UpdateVisuals()
        {
            TamagotchiHealth.Value = Tamagotchi.Health;
            MoodValue.Content = Tamagotchi.Mood;
            AppetiteValue.Content = Tamagotchi.Appetite;
            HygieneValue.Content = Tamagotchi.Hygiene;
            UpdateHealImage();
        }
        private void UpdateHealImage()
        {
            int charges = Tamagotchi.HealCharges;

            string imagePath = charges switch
            {
                3 => "/Sprites/3charge.png",
                2 => "/Sprites/2charge.png",
                1 => "/Sprites/1charge.png",
                0 => "/Sprites/0charge.png",
                _ => "/Sprites/0charge.png",
            };

            HealImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
        }
        private void UpdateTamagotchiEvolution()
        {
            TamagotchiLabel.Content = Tamagotchi.Name;
            var tamagotchiGifPath = FilePath.Get("Sprites", "sprite.gif");
            var tamagotchiImage = new BitmapImage();
            tamagotchiImage.BeginInit();
            tamagotchiImage.UriSource = new Uri(tamagotchiGifPath, UriKind.Absolute);
            tamagotchiImage.EndInit();
            HeigthValue.Content = Tamagotchi.Height;
            WeigthValue.Content = Tamagotchi.Weight;
        }
        private void UpdateDeathVisuals()
        {
            TamagotchiHealth.Visibility = Visibility.Hidden;
            HealthLabel.Visibility = Visibility.Hidden;
            MoodLabel.Visibility = Visibility.Hidden;
            MoodValue.Visibility = Visibility.Hidden;
            AppetiteLabel.Visibility = Visibility.Hidden;
            AppetiteValue.Visibility = Visibility.Hidden;
            HygieneLabel.Visibility = Visibility.Hidden;
            HygieneValue.Visibility = Visibility.Hidden;
            HeigthValue.Visibility = Visibility.Hidden;
            WeigthValue.Visibility = Visibility.Hidden;
            TamagotchiHeight.Visibility = Visibility.Hidden;
            TamagotchiWeight.Visibility = Visibility.Hidden;
            TamagotchiHealth.Visibility = Visibility.Hidden;
            btnBath.Visibility = Visibility.Hidden;
            btnFeed.Visibility = Visibility.Hidden;
            btnHeal.Visibility = Visibility.Hidden;
            btnPlay.Visibility = Visibility.Hidden;
            btnTrain.Visibility = Visibility.Hidden;
            TamagotchiImage.Visibility = Visibility.Hidden;
            HealImage.Visibility = Visibility.Hidden;

            TimeLivedValue.Content = Tamagotchi.TimeLived.ToString(@"hh\:mm\:ss");

            Tumb.Visibility = Visibility.Visible;
            TimeLivedLabel.Visibility = Visibility.Visible;
            TimeLivedValue.Visibility = Visibility.Visible;
        }
        private void OnPlayClicked(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Play();
        }
        private void OnFeedClicked(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Feed();
        }
        private void OnBathClicked(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Bath();
        }
        private void OnTrainClicked(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Train();
        }
        private void OnHealClicked(object sender, RoutedEventArgs e)
        {
            Tamagotchi.Heal();
            UpdateHealImage();
        }
    }
}