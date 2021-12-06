using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Components.UserSettings;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.UserSettings;
using VoltageMeasurementLogger.Views.Main;
using VoltageMeasurementLogger.Views.Menu;

namespace VoltageMeasurementLogger
{
    public partial class MainWindow
    {
        private bool _runExceptionAgain = false;

        public MainWindow()
        {
            this.InitializeComponent();

            this.Prepare();

            this.LoadSettings();
        }


        private void LoadSettings()
        {
            try
            {
                var setting = UserSettingsLoaderHelper.Load();

                this.LoadApplicationSize(setting);
                this.LoadApplicationWindowState(setting);
                this.LoadApplicationStartLocation(setting);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                if (this._runExceptionAgain)
                {
                    SimpleStatusOverlays.Show(
                        "ERROR",
                        "Error with the settings file. The settings could not be reloaded. Delete Ggg. manually. Programme is terminated!");

                    this.Close();
                    return;
                }

                this._runExceptionAgain = true;
                SimpleStatusOverlays.ShowAsk("Warning", "Setting file is currupted. Should the settings be reset?", b =>
                {
                    if (!b) { return; }

                    UserSettingsLoaderHelper.Save(new CustomSettingsFile());
                });

                LoadSettings();
            }
        }

        private void Prepare()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            var t = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(t));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) =>
            new ButtonCommandOpenMain().Execute(null);

        /// <summary>
        ///     Load the last size of the application
        /// </summary>
        private void LoadApplicationSize(CustomSettingsFile setting)
        {
            var size = new Size(setting.SizeX, setting.SizeY);

            if (size.Width < 100) size.Width = 800;

            if (size.Height < 600) size.Height = 600;

            this.Width = size.Width;
            this.Height = size.Height;
        }

        /// <summary>
        ///     Load the window state
        /// </summary>
        private void LoadApplicationWindowState(CustomSettingsFile setting)
        {
            if (string.IsNullOrEmpty(setting.ApplicationWindowState))
            {
                this.WindowState = WindowState.Normal;
                return;
            }

            this.WindowState = Enum
                .TryParse(setting.ApplicationWindowState,
                    out WindowState windowState)
                ? windowState
                : WindowState.Normal;
        }

        /// <summary>
        ///     Load the location and place the application to the position.
        /// </summary>
        private void LoadApplicationStartLocation(CustomSettingsFile setting)
        {
            var point = new Point(setting.ApplicationPositionX, setting.ApplicationPositionY);

            if (point.X < 0 && point.Y < 0)
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                return;
            }

            this.Left = point.X;
            this.Top = point.Y;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this._runExceptionAgain)
            {
                return;
            }

            var usl = UserSettingsLoader<CustomSettingsFile>.GetInstance(SerializeHelper.Serialize,
                SerializeHelper.Deserialize);
            var file = usl.Load();

            file.ApplicationPositionX = (int)this.Left;
            file.ApplicationPositionY = (int)this.Top;
            file.SizeX = (int)this.Width;
            file.SizeY = (int)this.Height;
            file.ApplicationWindowState = this.WindowState.ToString();

            usl.Save(file);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2) return;

            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
}