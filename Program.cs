using System;
using Raylib_cs;

namespace CalculatorRayLib
{
    class Program
    {
        static void Main()
        {
            Raylib.InitWindow(400, 600, "CalculatorRayLib");
            int buttonWidth = 80;
            int buttonHeight = 60;
            Color buttonColor = Color.SkyBlue;
            Color buttonTextColor = Color.RayWhite;

            string currentInput = "0";
            string previousInput = "";
            string currentOperator = "";
            bool isResultDisplayed = false;

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                Raylib.DrawRectangle(20, 20, 360, 100, Color.LightGray);
                Raylib.DrawText(currentInput, 30, 50, 40, Color.Black);

                string[] mainButtons = { "AC", "+/-", "%", "7", "8", "9", "4", "5", "6", "1", "2", "3", "0", "." };
                string[] operatorButtons = { "÷", "×", "-", "+", "=" };
                int xOffset = 20;
                int yOffset = 140;

                for (int i = 0; i < mainButtons.Length; i++)
                {
                    int x = xOffset + (i % 3) * (buttonWidth + 10);
                    int y = yOffset + (i / 3) * (buttonHeight + 10);

                    if (mainButtons[i] == "0")
                    {
                        Raylib.DrawRectangle(x, y, buttonWidth * 2 + 10, buttonHeight, buttonColor);
                        Raylib.DrawText(mainButtons[i], x + 25, y + 15, 20, buttonTextColor);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            int mouseX = Raylib.GetMouseX();
                            int mouseY = Raylib.GetMouseY();
                            if (mouseX > x && mouseX < x + buttonWidth * 2 + 10 && mouseY > y && mouseY < y + buttonHeight)
                            {
                                HandleButtonPress(mainButtons[i], ref currentInput, ref previousInput, ref currentOperator, ref isResultDisplayed);
                            }
                        }
                        i++;
                    }
                    else
                    {
                        Raylib.DrawRectangle(x, y, buttonWidth, buttonHeight, buttonColor);
                        Raylib.DrawText(mainButtons[i], x + 25, y + 15, 20, buttonTextColor);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            int mouseX = Raylib.GetMouseX();
                            int mouseY = Raylib.GetMouseY();
                            if (mouseX > x && mouseX < x + buttonWidth && mouseY > y && mouseY < y + buttonHeight)
                            {
                                HandleButtonPress(mainButtons[i], ref currentInput, ref previousInput, ref currentOperator, ref isResultDisplayed);
                            }
                        }
                    }
                }

                int opX = xOffset + 3 * (buttonWidth + 10);
                int opY = yOffset;

                for (int i = 0; i < operatorButtons.Length; i++)
                {
                    int y = opY + i * (buttonHeight + 10);
                    Raylib.DrawRectangle(opX, y, buttonWidth, buttonHeight, Color.Orange);
                    Raylib.DrawText(operatorButtons[i], opX + 25, y + 15, 20, Color.White);
                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        int mouseX = Raylib.GetMouseX();
                        int mouseY = Raylib.GetMouseY();
                        if (mouseX > opX && mouseX < opX + buttonWidth && mouseY > y && mouseY < y + buttonHeight)
                        {
                            HandleButtonPress(operatorButtons[i], ref currentInput, ref previousInput, ref currentOperator, ref isResultDisplayed);
                        }
                    }
                }

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static void HandleButtonPress(string button, ref string currentInput, ref string previousInput, ref string currentOperator, ref bool isResultDisplayed)
        {
            if (char.IsDigit(button[0]) || button == ".")
            {
                if (isResultDisplayed)
                {
                    currentInput = button;
                    isResultDisplayed = false;
                }
                else
                {
                    currentInput = currentInput == "0" ? button : currentInput + button;
                }
            }
            else if (button == "AC")
            {
                currentInput = "0";
                previousInput = "";
                currentOperator = "";
            }
            else if (button == "+/-")
            {
                if (currentInput != "0")
                {
                    if (currentInput.StartsWith("-"))
                    {
                        currentInput = currentInput.Substring(1); 
                    }
                    else
                    {
                        currentInput = "-" + currentInput; 
                    }
                }
            }
            else if (button == "=")
            {
                if (!string.IsNullOrEmpty(previousInput) && !string.IsNullOrEmpty(currentOperator))
                {
                    float num1 = float.Parse(previousInput);
                    float num2 = float.Parse(currentInput);
                    currentInput = PerformCalculation(num1, num2, currentOperator).ToString();
                    previousInput = "";
                    currentOperator = "";
                    isResultDisplayed = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(currentInput))
                {
                    previousInput = currentInput;
                    currentInput = "0";
                    currentOperator = button;
                }
            }
        }

        static float PerformCalculation(float num1, float num2, string op)
        {
            switch (op)
            {
                case "+": return num1 + num2;
                case "-": return num1 - num2;
                case "×": return num1 * num2;
                case "÷": return num2 != 0 ? num1 / num2 : 0;
                default: return 0;
            }
        }
    }
}
