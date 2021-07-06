using ExpressionTreeTestObjects;
using ExpressionTreeVisualizer;
using ExpressionTreeVisualizer.Serialization;
using System.Linq;
using System.Windows;
using ExpressionTreeTestObjects.VB;

namespace ExpressionTreeVisualizer.UIDemo {
    public partial class MainWindow : Window {
        private readonly ConfigViewModel configViewModel;

        public MainWindow() {
            InitializeComponent();

            configViewModel = new ConfigViewModel(new Config());

            Loaded += (sender, e) => {
                Loader.Load();
                availableObjects.ItemsSource = Objects.Get()
                    .Select(x => new TestObject(x))
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Category)
                    .ThenBy(x => x.Source)
                    .ToArray();
                settingsControl.DataContext = configViewModel;
            };

            availableObjects.SelectionChanged += (sender, e) => refresh();
            configViewModel.PropertyChanged += (sender, e) => refresh();
        }

        private void refresh() {
            if (availableObjects.SelectedValue is null) { return; }
            var (selectedValue, config) = (
                availableObjects.SelectedValue,
                configViewModel.Model
            );
            var m = new VisualizerData(selectedValue, config);
            visualizerControl.DataContext = new VisualizerDataViewModel(m);
        }
    }
}
