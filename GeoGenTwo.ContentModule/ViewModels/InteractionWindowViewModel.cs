using GeoGenTwo.Core.Mvvm;
using GeoGenTwo.Core.Interfaces;
using Prism.Commands;
using Prism.Events;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System;

namespace GeoGenTwo.ContentModule.ViewModels
{
    public class InteractionWindowViewModel : RegionViewModelBase
    {
        #region Commands

        public DelegateCommand GeneratePatternCommand { get; private set; }
        public DelegateCommand<OutputOrientationType?> SaveToImageCommand { get; private set; }

        #endregion

        #region Fields

        private IEventAggregator _eventAggregator;
        private ISettings _settings;

        #endregion

        #region Properties

        public ISettings Settings
        {
            get { return _settings; }
            set { SetProperty(ref _settings, value); }
        }

        #endregion           

        #region Constructor

        public InteractionWindowViewModel(IEventAggregator eventAggregator, ISettings settings) : base()
        {
            _eventAggregator = eventAggregator;
            Settings = settings;

            GeneratePatternCommand = new DelegateCommand(GeneratePattern_Command);
            SaveToImageCommand = new DelegateCommand<OutputOrientationType?>(SaveToImage_Command);

            _eventAggregator.GetEvent<SettingsChangedEvent>().Subscribe(OnSettingsChangedEventReceived);
            _eventAggregator.GetEvent<ReturnLinesEvent>().Subscribe(OnReturnLinesEventReceived);
        }

        #endregion

        #region Overrides

        public override void Destroy()
        {
            base.Destroy();
            _eventAggregator.GetEvent<SettingsChangedEvent>().Unsubscribe(OnSettingsChangedEventReceived);
        }

        #endregion

        #region Callbacks

        private void GeneratePattern_Command()
        {
            _eventAggregator.GetEvent<GeneratePatternEvent>().Publish();
        }

        private void SaveToImage_Command(OutputOrientationType? orientation)
        {
            if (!orientation.HasValue) { return; }

            switch (orientation)
            {
                case OutputOrientationType.Portrait:
                    _eventAggregator.GetEvent<RequestLinesEvent>().Publish(OutputOrientationType.Portrait);
                    break;
                case OutputOrientationType.Landscape:
                    _eventAggregator.GetEvent<RequestLinesEvent>().Publish(OutputOrientationType.Landscape);
                    break;
            }
        }

        private void OnSettingsChangedEventReceived(ISettings settings)
        {
            Settings = settings;
        }

        private void OnReturnLinesEventReceived(ReturnLinesPayload payload) 
        {
            int width = (payload.outputOrientation == OutputOrientationType.Portrait)
                        ? Settings.PortraitResolution.Width
                        : Settings.LandscapeResolution.Width;
            int height = (payload.outputOrientation == OutputOrientationType.Portrait)
                        ? Settings.PortraitResolution.Height
                        : Settings.LandscapeResolution.Height;
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Settings.BackgroundBrush, null, new Rect(new Point(0, 0), new Size(width, height)));
                // Draw lines onto the drawing context
                foreach (Line line in payload.lineListPayloard)
                {
                    drawingContext.DrawLine(new Pen(line.Stroke, line.StrokeThickness), new Point(line.X1, line.Y1), new Point(line.X2, line.Y2));
                }
            }

            renderBitmap.Render(drawingVisual);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            string currentTime = DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss");
            string filePath = Settings.SaveDirectoryFilePath + $"\\{currentTime}.jpg";
            using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            encoder.Save(fileStream);
        }

        #endregion
    }
}
