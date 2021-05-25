using FinalFantasy7ItemManager.DataSet1TableAdapters;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace FinalFantasy7ItemManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum EState
        {
            idle,
            clicked
        }
        EState state = EState.idle;
        private readonly ItemsTableAdapter itemTableAdapter = new();
        private readonly DataSet1 dataSet = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemTableAdapter.Fill(dataSet.Items);
            dataGrid.DataContext = dataSet.Items.DefaultView;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            state = EState.clicked;
            buttonHasBeenClicked btnEvent = new();
            btnEvent.ChocoboTime();
        }
    }

    public partial class buttonHasBeenClicked : INotifyPropertyChanged
    {
        private readonly Uri Path = new(@"pack://siteoforigin:,,,/Resources/hip_hop_de_chocobo.mp3");

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ItemsTableAdapter itemTableAdapter = new();
        private readonly DataSet1 dataSet = new();
        private readonly MediaPlayer soundPlayer = new();

        private enum EState
        {
            idle,
            clicked
        }

        private EState state = EState.idle;

        public void ChocoboTime()
        {
            if (state == EState.clicked)
            {
                itemTableAdapter.Update(dataSet);
                soundPlayer.Open(Path);
                soundPlayer.Play();
                state = EState.idle;
            }
        }
    }
}