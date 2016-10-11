using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Timers;
using System.Windows;

namespace ShipBattleTraining
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<List<BattlefieldCell>> battleField;
        private List<ShotTarget> shotsList;
        private Timer timer;
        private int shotsCounter;

        public MainWindow()
        {
            InitializeComponent();

            timer = new Timer(500);
            timer.Elapsed += Timer_Elapsed;

            if (!DesignerProperties.GetIsInDesignMode(this)) btnReset_Click(null, null);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() => btnShoot_Click(null, null));
        }

        private bool SelectPlacement(Ship[] ships, ShipPlacementInfo[][] inputGrid, out ShipPlacementInfo[][] outputGrid)
        {
            var suitablePlacesCount = 0;
            bool placeSelected = false;
            List<Tuple<int, int, bool>> suitablePlaces = new List<Tuple<int, int, bool>>();

            outputGrid = null;

            for (int x = 0; x < inputGrid.Length; x++)
            {
                for (int y = 0; y < inputGrid[x].Length; y++)
                {
                    if (DoesShipFits(x, y, ships[0].Size, true, inputGrid))
                    {
                        suitablePlaces.Add(new Tuple<int, int, bool>(x, y, true));
                    }
                    if ((ships[0].Size > 1) && DoesShipFits(x, y, ships[0].Size, false, inputGrid))
                    {
                        suitablePlaces.Add(new Tuple<int, int, bool>(x, y, false));
                    }
                }
            }

            suitablePlacesCount = suitablePlaces.Count;

            while (suitablePlacesCount > 0)
            {
                var place = new Random().Next(suitablePlaces.Count);
                var updatedGrid = inputGrid.Select(row => row.Select(cell => cell).ToArray()).ToArray();
                ShipPlacementInfo[][] filledGrid;

                for (int x = 0; x < updatedGrid.Length; x++)
                {
                    for (int y = 0; y < updatedGrid[x].Length; y++)
                    {
                        if ((suitablePlaces[place].Item1 == x) && (suitablePlaces[place].Item2 == y))
                        {
                            for (int shipSection = 0; shipSection < ships[0].Size; shipSection++)
                            {
                                var shiftedX = (x + (suitablePlaces[place].Item3 ? shipSection : 0));
                                var shiftedY = (y + (suitablePlaces[place].Item3 ? 0 : shipSection));

                                updatedGrid[shiftedX][shiftedY] = new ShipPlacementInfo
                                {
                                    Ship = ships[0],
                                    Horizontal = suitablePlaces[place].Item3,
                                    SectionIndex = shipSection
                                };
                            }
                        }
                    }
                }

                if (ships.Length <= 1)
                {
                    placeSelected = true;
                    outputGrid = updatedGrid;
                    suitablePlacesCount = 0;
                }
                else if (SelectPlacement(ships.Skip(1).ToArray(), updatedGrid, out filledGrid))
                {
                    placeSelected = true;
                    outputGrid = filledGrid;
                    suitablePlacesCount = 0;
                }
                else
                {
                    suitablePlaces.RemoveAt(place);

                    suitablePlacesCount = suitablePlaces.Count;
                }
            }

            return placeSelected;
        }

        private bool DoesShipFits(int shipX, int shipY, int shipSize, bool horizontal, ShipPlacementInfo[][] grid)
        {
            var returnValue = true;

            if ((shipX + (horizontal ? shipSize - 1 : 0)) >= grid.Length)
            {
                returnValue = false;
            }
            else if ((shipY + (horizontal ? 0 : shipSize - 1)) >= grid[0].Length)
            {
                returnValue = false;
            }
            else
            {
                var minX = (shipX - 1);
                var maxX = (shipX + (horizontal ? shipSize : 1));
                var minY = (shipY - 1);
                var maxY = (shipY + (horizontal ? 1 : shipSize));

                if (minX < 0) minX = 0;
                if (maxX >= grid.Length) maxX = (grid.Length - 1);
                if (minY < 0) minY = 0;
                if (maxY >= grid[0].Length) maxY = (grid[0].Length - 1);

                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        if (grid[x][y] != null)
                        {
                            returnValue = false;

                            break;
                        }
                    }

                    if (!returnValue) break;
                }
            }

            return returnValue;
        }

        private void btnShoot_Click(object sender, RoutedEventArgs e)
        {
            if ((battleField.Count > 0) && (battleField[0].Count > 0))
            {
                var hasShipsLeft = false;
                var hasCellsLeft = false;
                var grid = battleField
                    .Select(row => row.Select(cell =>
                    {
                        if (!cell.HasBeenFiredAt)
                        {
                            return FieldData.StatusUnknonw;
                        }
                        else if (cell.PlacedShip == null)
                        {
                            return FieldData.Empty;
                        }
                        else if (cell.PlacedShip?.Ship?.Sunken == true)
                        {
                            return FieldData.SunkenShip;
                        }
                        else
                        {
                            return FieldData.Ship;
                        }
                    }).ToArray())
                    .ToArray();
                var target = Functions.SelectShotCoordinates(grid);

                shotsCounter++;
                txtShotsCounter.Text = shotsCounter.ToString();

                if ((battleField.Count) > 0 && (target.X >= 0) && (target.X < battleField.Count) && (target.Y >= 0) && (target.Y < battleField[0].Count))
                {
                    battleField[target.X][target.Y].HasBeenFiredAt = true;

                    if ((battleField[target.X][target.Y].PlacedShip != null) && !battleField[target.X][target.Y].PlacedShip.Ship.Sunken)
                    {
                        var allSectionsHit = true;

                        for (int i = 0; i < battleField[target.X][target.Y].PlacedShip.Ship.Size; i++)
                        {
                            if (battleField[target.X][target.Y].PlacedShip.Horizontal)
                            {
                                if (!battleField[target.X - battleField[target.X][target.Y].PlacedShip.SectionIndex + i][target.Y].HasBeenFiredAt)
                                {
                                    allSectionsHit = false;

                                    break;
                                }
                            }
                            else
                            {
                                if (!battleField[target.X][target.Y - battleField[target.X][target.Y].PlacedShip.SectionIndex + i].HasBeenFiredAt)
                                {
                                    allSectionsHit = false;

                                    break;
                                }
                            }
                        }

                        if (allSectionsHit) battleField[target.X][target.Y].PlacedShip.Ship.Sunken = true;
                    }
                }

                for (int x = 0; x < battleField.Count; x++)
                {
                    for (int y = 0; y < battleField[x].Count; y++)
                    {
                        if (!hasShipsLeft && (battleField[x][y].PlacedShip?.Ship?.Sunken == false))
                        {
                            hasShipsLeft = true;
                        }

                        if (!hasCellsLeft && !battleField[x][y].HasBeenFiredAt)
                        {
                            hasCellsLeft = true;
                        }

                        if (hasShipsLeft && hasCellsLeft) break;
                    }

                    if (hasShipsLeft && hasCellsLeft) break;
                }

                if (!hasCellsLeft)
                {
                    chkAutoFire.IsChecked = false;

                    MessageBox.Show("No more places to shoot at!", "Game end", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else if (!hasShipsLeft)
                {
                    chkAutoFire.IsChecked = false;

                    MessageBox.Show("All shipps are sinked!", "Game end", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            int battleFieldWidth = 10;
            int battleFieldHeight = 10;
            int corvettesCount = 4;
            int frigatesCount = 3;
            int destroyersCount = 2;
            int cruisersCount = 1;
            List<Ship> ships = new List<Ship>();

            #region Battle data initialization

            if (ConfigurationManager.AppSettings.AllKeys.Contains("BattleFieldWidth"))
            {
                int parsedValue;

                if (int.TryParse(ConfigurationManager.AppSettings["BattleFieldWidth"], out parsedValue) && (parsedValue > 0) && (parsedValue <= 100))
                {
                    battleFieldWidth = parsedValue;
                }
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("BattleFieldHeight"))
            {
                int parsedValue;

                if (int.TryParse(ConfigurationManager.AppSettings["BattleFieldHeight"], out parsedValue) && (parsedValue > 0) && (parsedValue <= 100))
                {
                    battleFieldHeight = parsedValue;
                }
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("CorvettesCount"))
            {
                int parsedValue;

                if (int.TryParse(ConfigurationManager.AppSettings["CorvettesCount"], out parsedValue) && (parsedValue >= 0) && (parsedValue <= 10))
                {
                    corvettesCount = parsedValue;
                }
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("FrigatesCount"))
            {
                int parsedValue;

                if (int.TryParse(ConfigurationManager.AppSettings["FrigatesCount"], out parsedValue) && (parsedValue >= 0) && (parsedValue <= 10))
                {
                    frigatesCount = parsedValue;
                }
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("DestroyersCount"))
            {
                int parsedValue;

                if (int.TryParse(ConfigurationManager.AppSettings["DestroyersCount"], out parsedValue) && (parsedValue >= 0) && (parsedValue <= 10))
                {
                    destroyersCount = parsedValue;
                }
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("CruisersCount"))
            {
                int parsedValue;

                if (int.TryParse(ConfigurationManager.AppSettings["CruisersCount"], out parsedValue) && (parsedValue >= 0) && (parsedValue <= 10))
                {
                    cruisersCount = parsedValue;
                }
            }

            battleField = new List<List<BattlefieldCell>>();

            for (int i = 0; i < battleFieldWidth; i++)
            {
                battleField.Add(new List<BattlefieldCell>());

                for (int j = 0; j < battleFieldHeight; j++)
                {
                    battleField[i].Add(new BattlefieldCell());
                }
            }

            shotsCounter = 0;

            #endregion

            #region Battle field generation

            if (cruisersCount > 0) for (int i = 0; i < cruisersCount; i++) ships.Add(new Ship(4));
            if (destroyersCount > 0) for (int i = 0; i < destroyersCount; i++) ships.Add(new Ship(3));
            if (frigatesCount > 0) for (int i = 0; i < frigatesCount; i++) ships.Add(new Ship(2));
            if (corvettesCount > 0) for (int i = 0; i < corvettesCount; i++) ships.Add(new Ship(1));

            if (ships.Count > 0)
            {
                ShipPlacementInfo[][] filledGrid;

                if (SelectPlacement(ships.ToArray(), battleField.Select(row => new ShipPlacementInfo[row.Count]).ToArray(), out filledGrid))
                {
                    for (int x = 0; x < filledGrid.Length; x++)
                    {
                        for (int y = 0; y < filledGrid[x].Length; y++)
                        {
                            battleField[x][y].PlacedShip = filledGrid[x][y];
                        }
                    }
                }
                else
                {
                    chkAutoFire.IsChecked = false;

                    MessageBox.Show("Could not poppulate battle field!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                chkAutoFire.IsChecked = false;

                MessageBox.Show("No ships to place on battle field!", "Information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            #endregion

            itmcBattleField.ItemsSource = battleField;
            txtShotsCounter.Text = string.Empty;
        }

        private void chkAutoFire_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAutoFire.IsChecked == true)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }
    }
}