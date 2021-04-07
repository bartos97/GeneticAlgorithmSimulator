using GeneticAlgorithmSimulator.TestFunctions;
using GeneticAlgorithmSimulator.Validation;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneticAlgorithmSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsPlotLivePreview { get; set; } = true;

        private double _lastComputationTime;
        public double LastComputationTime
        {
            get => _lastComputationTime;
            private set
            {
                _lastComputationTime = value;
                OnPropertyChanged();
            }
        }

        private bool isWindowLoaded = false;
        private GeneticAlgorithmSettings settings = GeneticAlgorithmSettings.GetDefault();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            isWindowLoaded = true;
            StackPanelSettings.DataContext = settings;
            DisableConditionalSelecionInputs(null);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            var manager = new GeneticAlgorithmManager(settings);
            var resultsLinePoints = new List<DataPoint>(settings.EpochsAmount);

            foreach (EpochResult result in manager.GetResults())
            {
                resultsLinePoints.Add(new DataPoint(result.epochNumber, result.functionValue));
            }

            PlotResultsLine.ItemsSource = resultsLinePoints;
            LastComputationTime = manager.GetLastComputationTime();
        }

        private void ComboBoxSelectionMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isWindowLoaded)
                return;
            DisableConditionalSelecionInputs((SelectionMethodEnum)e.AddedItems[0]);
        }

        private void DisableConditionalSelecionInputs(SelectionMethodEnum? currentValue)
        {
            currentValue = currentValue != null ? currentValue : (SelectionMethodEnum)ComboBoxSelectionMethod.SelectedItem;
            if (currentValue == null)
                return;

            TextBoxPercentageToCross.IsEnabled = false;
            TextBoxTournamentsAmount.IsEnabled = false;

            switch (currentValue)
            {
                case SelectionMethodEnum.BEST:
                    TextBoxPercentageToCross.IsEnabled = true;
                    break;
                case SelectionMethodEnum.TOURNAMENT:
                    TextBoxTournamentsAmount.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void OnlyNumberInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regexes.number.IsMatch(e.Text);
        }

        private void OnlyIntegerInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regexes.integer.IsMatch(e.Text);
        }

        private void OnlyPositiveIntegerInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regexes.positiveInteger.IsMatch(e.Text);
        }
    }
}
