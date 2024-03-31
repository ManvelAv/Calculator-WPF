using Calculator_WPF.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator_WPF.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void  OnPropertyChanged([CallerMemberName] string name = null )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _KeyPressedString;

        public string KeyPressedString
        {
            get { return _KeyPressedString; }
            set { _KeyPressedString = value; OnPropertyChanged("KeyPressedString"); }
        }

        private string _Entered_Number;

        public string Entered_Number
        {
            get { return _Entered_Number; }
            set { _Entered_Number = value; OnPropertyChanged("Entered_Number"); }
        }

        private ButtonPressedCommand _buttonPressedCommand;
        public ButtonPressedCommand buttonPressedCommand
        {
            get { return _buttonPressedCommand; }
            set { _buttonPressedCommand = value;}
        }

        List<string> EnteredKeys;
        double Number = 0;
        bool FirstNumberEntered = true;
        bool EqualToFlag = true;
        bool FunctionPressed = false;
        string SelectedFunction = "";
        public string PreviousEnteredKey = "";

        public MainViewModel()
        {
            Entered_Number = "0";
            KeyPressedString = "";
            EnteredKeys = new List<string>();
            buttonPressedCommand = new ButtonPressedCommand(this);
        }

        private void PowerOff()
        {
            
            Application.Current.Shutdown();
        }

        void UpdateEnteredKeysOnGui()
        {
            string temp = "";
            for (int i = 0; i < EnteredKeys.Count; i++)
            {
                temp += EnteredKeys[i];
            }
            KeyPressedString = temp;

        }

        void PlusMinus()
        {
            Number = Number*(-1);
            Entered_Number = Number.ToString();
        }

        void Addition()
        {
            Number += Convert.ToDouble(Entered_Number);
            Entered_Number = Number.ToString();
        }

        void Substraction()
        {
            Number -= Convert.ToDouble(Entered_Number);
            Entered_Number = Number.ToString();
        }

        void Multiplication()
        {
            Number *= Convert.ToDouble(Entered_Number);
            Entered_Number = Number.ToString();
        }

        void Division()
        {
            Number /= Convert.ToDouble(Entered_Number);
            Entered_Number = Number.ToString();
        }

        void EqualTo()
        {
            EnteredKeys.Clear();
            EnteredKeys.Add(Entered_Number);
            EqualToFlag = false;
        }
        void Clear()
        {
            EnteredKeys.Clear();
            KeyPressedString = "";
            Entered_Number = "0";
            Number = 0;
            FirstNumberEntered = true;
            EqualToFlag = true;
        }

        bool PressedButtonOperator(string pressedButton)
        {
            if (pressedButton == "0" || pressedButton == "1" || pressedButton == "2" || pressedButton == "3" || pressedButton == "4" || pressedButton == "5" || pressedButton == "6" ||
       pressedButton == "7" || pressedButton == "8" || pressedButton == "9" || pressedButton == "," )
            {
                if (pressedButton == ",")
                {
                    pressedButton = "0,"; // Если вводится только запятая, добавляем "0," в Entered_Number
                }


                EnteredKeys.Add(pressedButton);
                UpdateEnteredKeysOnGui();

                PreviousEnteredKey = pressedButton;

                if (FunctionPressed)
                {
                    Entered_Number = "0";
                    FunctionPressed = false;
                }

               

                if (Entered_Number == "0" && pressedButton != ",")
                {
                    Entered_Number = pressedButton;
                }
                else if (pressedButton == "," && !Entered_Number.Contains(","))
                {
                    Entered_Number += pressedButton;
                }
                else if (pressedButton != ",")
                {
                    Entered_Number += pressedButton;
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        public void GetPressedButton(string pressedBtuuon)
        {
            if(PressedButtonOperator(pressedBtuuon))
            {
                if(PreviousEnteredKey != pressedBtuuon && PreviousEnteredKey != "+" && PreviousEnteredKey != "-" && PreviousEnteredKey != "/"
                    && PreviousEnteredKey != "*" && PreviousEnteredKey !="+-" ) 
                {
                    if (EnteredKeys.Count == 0)
                    {
                        EnteredKeys.Add(Entered_Number);
                        UpdateEnteredKeysOnGui();
                    }

                    if(FirstNumberEntered)
                    {
                        Number  = Convert.ToDouble(Entered_Number);
                        Entered_Number = Number.ToString();
                        FirstNumberEntered = false;
                    }
                    else
                    {
                        switch(SelectedFunction)
                        {
                            case "Addition": Addition(); break;
                            case "Substraction": Substraction(); break;
                            case "Multiplication": Multiplication(); break;
                            case "Division": Division(); break;
                            case "EqualTo": EqualTo(); break;
                            case "Pwr": PowerOff(); break;

                        }
                    }

                    switch(pressedBtuuon)
                    {
                        case "+":
                            SelectedFunction = "Addition";
                            EnteredKeys.Add(pressedBtuuon);
                            UpdateEnteredKeysOnGui() ;
                            PreviousEnteredKey = pressedBtuuon;
                            FunctionPressed = true;
                            break;

                        case "-":
                            SelectedFunction = "Substraction";
                            EnteredKeys.Add(pressedBtuuon);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedBtuuon;
                            FunctionPressed = true;
                            break;

                         case "*":
                            SelectedFunction = "Multiplication";
                            EnteredKeys.Add(pressedBtuuon);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedBtuuon;
                            FunctionPressed = true;
                            break;

                         case "/":
                            SelectedFunction = "Division";
                            EnteredKeys.Add(pressedBtuuon);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedBtuuon;
                            FunctionPressed = true;
                            break;

                        case "=":
                            SelectedFunction = "EqualTo";
                            EnteredKeys.Add(pressedBtuuon);
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedBtuuon;
                            FunctionPressed = true;
                            break;

                        case "+-":
                            PlusMinus();
                            UpdateEnteredKeysOnGui();
                            PreviousEnteredKey = pressedBtuuon;
                            FunctionPressed = true;
                            break;

                        case "Clr":
                            Clear();
                            FunctionPressed = true;
                            PreviousEnteredKey = pressedBtuuon;
                            break;

                        case "Pwr": PowerOff(); break;
                    }

                }
                else if(PreviousEnteredKey == "+" || PreviousEnteredKey == "-" || PreviousEnteredKey == "/" || PreviousEnteredKey == "*"
                    || PreviousEnteredKey == "Clr" || PreviousEnteredKey == "Pwr" || PreviousEnteredKey == "+-")
                {
                    if (EnteredKeys.Count > 0)
                    {
                        EnteredKeys.RemoveAt(EnteredKeys.Count - 1);
                        UpdateEnteredKeysOnGui();
                    }

                    EnteredKeys.Add(pressedBtuuon);
                    PreviousEnteredKey = pressedBtuuon;
                    FunctionPressed = true;

                    switch (pressedBtuuon)
                    {
                        case "+": SelectedFunction = "Addition"; break;
                        case "-": SelectedFunction = "Substraction"; break;
                        case "*": SelectedFunction = "Multiplication"; break;
                        case "/": SelectedFunction = "Division"; break;
                        case "=": SelectedFunction = "EqualTo"; break;
                        case "+-": SelectedFunction = "PlusMinus"; break;
                        case "Clr": Clear(); break;
                        case "Pwr": PowerOff(); break;
                    }
                }

            }
        }
    }
}
