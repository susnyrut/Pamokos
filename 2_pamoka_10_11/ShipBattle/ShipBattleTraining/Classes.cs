using System.ComponentModel;

namespace ShipBattleTraining
{
    /// <summary>
    /// Holds a data for battlefield cell
    /// </summary>
    public class BattlefieldCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _hasBeenFiredAt;
        public bool HasBeenFiredAt
        {
            get { return _hasBeenFiredAt; }
            set
            {
                _hasBeenFiredAt = value;

                NotifyPropertyChanged(nameof(HasBeenFiredAt));
            }
        }

        private ShipPlacementInfo _placedShip;
        public ShipPlacementInfo PlacedShip
        {
            get { return _placedShip; }
            set
            {
                _placedShip = value;

                NotifyPropertyChanged(nameof(PlacedShip));
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;

            if ((handler != null) && !string.IsNullOrWhiteSpace(propertyName))
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <summary>
    /// Holds data about ship placement onto battlefield cell
    /// </summary>
    public class ShipPlacementInfo : INotifyPropertyChanged
    {
        public enum SectionDrawingType
        {
            BackHorizontal,
            MiddleHorizontal,
            FrontHorizontal,
            BackVertical,
            MiddleVertical,
            FrontVertical,
            WholeShip
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Ship _ship;
        public Ship Ship
        {
            get { return _ship; }
            set
            {
                _ship = value;

                NotifyPropertyChanged(nameof(Ship));
                NotifyPropertyChanged(nameof(SectionDrawing));
            }
        }

        private bool _horizontal;
        public bool Horizontal
        {
            get { return _horizontal; }
            set
            {
                _horizontal = value;

                NotifyPropertyChanged(nameof(Horizontal));
                NotifyPropertyChanged(nameof(SectionDrawing));
            }
        }

        private int _sectionIndex;
        public int SectionIndex
        {
            get { return _sectionIndex; }
            set
            {
                _sectionIndex = value;

                NotifyPropertyChanged(nameof(SectionIndex));
                NotifyPropertyChanged(nameof(SectionDrawing));
            }
        }

        public SectionDrawingType? SectionDrawing
        {
            get
            {
                if (Ship != null)
                {
                    if (Ship.Size == 1)
                    {
                        return SectionDrawingType.WholeShip;
                    }
                    else if (SectionIndex == 0)
                    {
                        return (Horizontal ? SectionDrawingType.BackHorizontal : SectionDrawingType.BackVertical);
                    }
                    else if (SectionIndex == (Ship.Size - 1))
                    {
                        return (Horizontal ? SectionDrawingType.FrontHorizontal : SectionDrawingType.FrontVertical);
                    }
                    else
                    {
                        return (Horizontal ? SectionDrawingType.MiddleHorizontal : SectionDrawingType.MiddleVertical);
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;

            if ((handler != null) && !string.IsNullOrWhiteSpace(propertyName))
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <summary>
    /// Holds a data for ship
    /// </summary>
    public class Ship : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Size { get; private set; }

        private bool _sunken;
        public bool Sunken
        {
            get { return _sunken; }
            set
            {
                _sunken = value;

                NotifyPropertyChanged(nameof(Sunken));
            }
        }

        public Ship(int size)
        {
            Size = size;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;

            if ((handler != null) && !string.IsNullOrWhiteSpace(propertyName))
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FieldData
    {
        StatusUnknonw,
        Empty,
        Ship,
        SunkenShip
    }

    /// <summary>
    /// Holds target coordinates for shot
    /// </summary>
    public struct ShotTarget
    {
        public int X;
        public int Y;
    }
}